# So sánh chi tiết các phương pháp kết nối Database

## 1. SO SÁNH TỔNG QUÁT

| Tiêu chí | ADO.NET | Entity Framework Core | Dapper |
|----------|---------|----------------------|--------|
| **Loại** | Low-level API | Full ORM | Micro-ORM |
| **Performance** | ⭐⭐⭐⭐⭐ (100%) | ⭐⭐⭐ (70-80%) | ⭐⭐⭐⭐⭐ (95-98%) |
| **Code Length** | ❌ Dài | ✅ Ngắn | ⭐ Trung bình |
| **Learning Curve** | Dốc | Thoải | Thoải |
| **LINQ Support** | ❌ Không | ✅ Có | ❌ Không |
| **Change Tracking** | ❌ Không | ✅ Có | ❌ Không |
| **Lazy Loading** | ❌ Không | ✅ Có | ❌ Không |
| **Migrations** | ❌ Không | ✅ Có | ❌ Không |
| **Raw SQL Support** | ✅ Tốt | ⭐ Có nhưng limited | ✅ Tốt |
| **Stored Procedures** | ✅ Excellent | ⭐ Good | ✅ Excellent |
| **Bulk Operations** | ⭐ Manual | ❌ Slow | ✅ Fast |
| **Memory Usage** | ✅ Thấp | ❌ Cao | ✅ Thấp |
| **Package Size** | ✅ Built-in | ❌ Lớn (~10MB) | ✅ Nhỏ (~200KB) |
| **Community Support** | ⭐⭐⭐ | ⭐⭐⭐⭐⭐ | ⭐⭐⭐⭐ |
| **Best For** | High-perf apps | Rapid development | Performance + simplicity |

## 2. CHI TIẾT CODE COMPARISON

### Ví dụ: Lấy danh sách Users

#### ADO.NET (~ 20 dòng):
```csharp
var users = new List<User>();
using (var connection = new SqlConnection(connectionString))
{
    connection.Open();
    using (var command = new SqlCommand("SELECT Id, Name, Email FROM Users WHERE Active = 1", connection))
    {
        using (var reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                users.Add(new User
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Email = reader.GetString(2)
                });
            }
        }
    }
}
```

#### Entity Framework Core (~ 3 dòng):
```csharp
var users = context.Users
    .Where(u => u.Active)
    .ToList();
```

#### Dapper (~ 5 dòng):
```csharp
using (var connection = new SqlConnection(connectionString))
{
    var users = connection.Query<User>(
        "SELECT Id, Name, Email FROM Users WHERE Active = @Active",
        new { Active = true }).ToList();
}
```

**Kết luận:** EF Core ngắn nhất, ADO.NET dài nhất

---

### Ví dụ: Insert User

#### ADO.NET (~ 15 dòng):
```csharp
using (var connection = new SqlConnection(connectionString))
{
    connection.Open();
    var query = "INSERT INTO Users (Name, Email, CreatedDate) VALUES (@Name, @Email, @CreatedDate); SELECT SCOPE_IDENTITY();";
    using (var command = new SqlCommand(query, connection))
    {
        command.Parameters.AddWithValue("@Name", "John");
        command.Parameters.AddWithValue("@Email", "john@example.com");
        command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
        int newId = Convert.ToInt32(command.ExecuteScalar());
    }
}
```

#### Entity Framework Core (~ 4 dòng):
```csharp
var user = new User { Name = "John", Email = "john@example.com", CreatedDate = DateTime.Now };
context.Users.Add(user);
context.SaveChanges();
// user.Id now contains the new ID
```

#### Dapper (~ 7 dòng):
```csharp
using (var connection = new SqlConnection(connectionString))
{
    var query = "INSERT INTO Users (Name, Email, CreatedDate) VALUES (@Name, @Email, @CreatedDate); SELECT SCOPE_IDENTITY();";
    int newId = connection.ExecuteScalar<int>(query,
        new { Name = "John", Email = "john@example.com", CreatedDate = DateTime.Now });
}
```

**Kết luận:** EF Core dễ nhất, tự động lấy ID

---

### Ví dụ: Join với Navigation Properties

#### ADO.NET (~ 30+ dòng):
```csharp
var userDict = new Dictionary<int, User>();
using (var connection = new SqlConnection(connectionString))
{
    connection.Open();
    var query = "SELECT u.*, o.* FROM Users u LEFT JOIN Orders o ON u.Id = o.UserId";
    using (var command = new SqlCommand(query, connection))
    {
        using (var reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                int userId = reader.GetInt32(0);
                if (!userDict.ContainsKey(userId))
                {
                    userDict[userId] = new User
                    {
                        Id = userId,
                        Name = reader.GetString(1),
                        Orders = new List<Order>()
                    };
                }
                if (!reader.IsDBNull(5)) // Has order
                {
                    userDict[userId].Orders.Add(new Order
                    {
                        Id = reader.GetInt32(5),
                        TotalAmount = reader.GetDecimal(7)
                    });
                }
            }
        }
    }
}
```

#### Entity Framework Core (~ 3 dòng):
```csharp
var users = context.Users
    .Include(u => u.Orders)
    .ToList();
```

#### Dapper (~ 15 dòng):
```csharp
var userDict = new Dictionary<int, User>();
using (var connection = new SqlConnection(connectionString))
{
    var query = "SELECT u.*, o.* FROM Users u LEFT JOIN Orders o ON u.Id = o.UserId";
    connection.Query<User, Order, User>(query,
        (user, order) =>
        {
            if (!userDict.TryGetValue(user.Id, out var userEntry))
            {
                userEntry = user;
                userEntry.Orders = new List<Order>();
                userDict.Add(user.Id, userEntry);
            }
            if (order != null) userEntry.Orders.Add(order);
            return userEntry;
        },
        splitOn: "Id");
}
```

**Kết luận:** EF Core wins dễ dàng cho complex queries

---

## 3. PERFORMANCE BENCHMARKS

### Scenario: Select 10,000 records

| Method | Time (ms) | Memory (MB) | Relative |
|--------|-----------|-------------|----------|
| ADO.NET | 100 | 15 | 1.0x (baseline) |
| Dapper | 105 | 16 | 1.05x |
| EF Core (AsNoTracking) | 145 | 25 | 1.45x |
| EF Core (with tracking) | 280 | 45 | 2.8x |

### Scenario: Insert 1,000 records

| Method | Time (ms) | Relative |
|--------|-----------|----------|
| ADO.NET (Bulk) | 150 | 1.0x |
| Dapper (Bulk) | 165 | 1.1x |
| EF Core (Individual) | 8,500 | 56.6x 😱 |
| EF Core (BulkExtensions) | 200 | 1.33x |

### Scenario: Update 1,000 records

| Method | Time (ms) | Relative |
|--------|-----------|----------|
| ADO.NET | 120 | 1.0x |
| Dapper | 135 | 1.12x |
| EF Core (AsNoTracking) | 180 | 1.5x |
| EF Core (with tracking) | 450 | 3.75x |

**Kết luận:**
- ADO.NET nhanh nhất nhưng code nhiều
- Dapper gần như ngang ADO.NET với code ít hơn
- EF Core chậm hơn nhưng dễ dùng, dễ maintain

---

## 4. KHI NÀO DÙNG GÌ?

### 🎯 Dùng ADO.NET khi:
- ✅ Performance là ưu tiên số 1
- ✅ Cần kiểm soát hoàn toàn SQL
- ✅ Làm việc với stored procedures phức tạp
- ✅ Bulk operations với data lớn
- ✅ Legacy system phải maintain
- ❌ Team nhỏ, thời gian limited
- ❌ Dự án mới, cần develop nhanh

**Use Cases:**
- High-traffic APIs
- Data processing/ETL
- Reporting systems
- Performance-critical microservices

### 🎯 Dùng Entity Framework Core khi:
- ✅ Rapid development
- ✅ Team lớn, nhiều developers
- ✅ Domain-driven design
- ✅ Cần migrations
- ✅ Complex business logic
- ✅ Cần LINQ và strongly-typed queries
- ❌ High-performance requirements
- ❌ Legacy database schema

**Use Cases:**
- Web applications (ASP.NET Core)
- CRUD applications
- Admin panels
- Business applications
- Prototypes/MVPs

### 🎯 Dùng Dapper khi:
- ✅ Cần balance performance và productivity
- ✅ Đã có SQL skills tốt
- ✅ Cần flexibility với SQL
- ✅ Microservices
- ✅ Legacy database không thể thay đổi
- ✅ Stored procedures nhiều
- ❌ Team ít kinh nghiệm SQL
- ❌ Cần change tracking

**Use Cases:**
- APIs (read-heavy)
- Microservices
- Data access layer
- Query-heavy applications
- Legacy database integration

---

## 5. HYBRID APPROACH (Kết hợp)

Nhiều dự án sử dụng kết hợp:

```csharp
public class UserRepository
{
    private readonly MyDbContext _context;
    private readonly IDbConnection _connection;

    // Sử dụng EF Core cho CRUD đơn giản
    public async Task<User> GetByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task AddAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    // Sử dụng Dapper cho queries phức tạp
    public async Task<List<UserStatistics>> GetUserStatisticsAsync()
    {
        var sql = @"
            SELECT u.Id, u.Name, 
                   COUNT(o.Id) as TotalOrders,
                   SUM(o.TotalAmount) as TotalSpent
            FROM Users u
            LEFT JOIN Orders o ON u.Id = o.UserId
            GROUP BY u.Id, u.Name";
        
        return (await _connection.QueryAsync<UserStatistics>(sql)).ToList();
    }

    // Sử dụng ADO.NET cho bulk operations
    public async Task BulkInsertUsersAsync(List<User> users)
    {
        using var bulkCopy = new SqlBulkCopy(_connection as SqlConnection);
        bulkCopy.DestinationTableName = "Users";
        // Configure and execute bulk copy
    }
}
```

**Lợi ích:**
- Dùng công cụ tốt nhất cho từng tình huống
- Optimize performance where needed
- Maintain developer productivity

---

## 6. FEATURE COMPARISON MATRIX

| Feature | ADO.NET | EF Core | Dapper |
|---------|---------|---------|--------|
| **Basic CRUD** | ⚠️ Manual | ✅ Easy | ⚠️ Manual |
| **Complex Queries** | ✅ Full control | ⚠️ Limited | ✅ Full control |
| **Stored Procedures** | ✅ Excellent | ⚠️ Good | ✅ Excellent |
| **Transactions** | ✅ Manual | ✅ Automatic | ✅ Manual |
| **Connection Management** | ⚠️ Manual | ✅ Automatic | ⚠️ Manual |
| **Object Mapping** | ❌ Manual | ✅ Automatic | ✅ Automatic |
| **Relationship Management** | ❌ Manual | ✅ Automatic | ❌ Manual |
| **Query Optimization** | ✅ Full control | ⚠️ Auto (sometimes bad) | ✅ Full control |
| **Unit Testing** | ⚠️ Difficult | ✅ Easy (with InMemory) | ⚠️ Difficult |
| **Code-First** | ❌ No | ✅ Yes | ❌ No |
| **Database-First** | ✅ Yes | ✅ Yes | ✅ Yes |
| **Async/Await** | ✅ Yes | ✅ Yes | ✅ Yes |
| **Dynamic Queries** | ✅ Easy | ⚠️ Difficult | ✅ Easy |
| **Multi-tenancy** | ✅ Manual | ✅ Supported | ✅ Manual |

---

## 7. COST COMPARISON

### Development Time:
- **ADO.NET:** 100% (baseline) - Nhiều code nhất
- **Dapper:** 70% - Code ít hơn với mapping
- **EF Core:** 50% - Nhanh nhất

### Maintenance Time:
- **ADO.NET:** 100% (baseline) - Khó maintain
- **Dapper:** 80% - Trung bình
- **EF Core:** 60% - Dễ maintain

### Performance (Higher is better):
- **ADO.NET:** 100% (baseline)
- **Dapper:** 95-98%
- **EF Core:** 70-80%

### Learning Curve (Lower is better):
- **ADO.NET:** 100% (baseline) - Dốc nhất
- **Dapper:** 60% - Dễ học
- **EF Core:** 70% - Nhiều concepts

---

## 8. DECISION TREE

```
Bạn cần gì?
│
├─ Performance tuyệt đối? ──► ADO.NET
│
├─ Rapid development? ──► Entity Framework Core
│
├─ Balance cả hai? ──► Dapper
│
├─ Complex business logic? ──► Entity Framework Core
│
├─ Simple CRUD? ──► Entity Framework Core
│
├─ Legacy database? ──► Dapper hoặc ADO.NET
│
├─ Microservices? ──► Dapper
│
├─ Bulk operations? ──► ADO.NET
│
└─ Team mới, ít exp? ──► Entity Framework Core
```

---

## KẾT LUẬN

**Không có "best" solution - chỉ có "best for your use case"!**

- **ADO.NET:** Low-level, high-performance, nhiều code
- **Entity Framework Core:** High-level, feature-rich, dễ dùng
- **Dapper:** Sweet spot - balance performance và productivity

**Recommendation:** 
- Dự án mới, team lớn: **Entity Framework Core**
- API performance-critical: **Dapper**
- Data processing, ETL: **ADO.NET**
- Hybrid: Mix & match theo nhu cầu!
