# Troubleshooting - Khắc phục sự cố Database Connection

## 1. CONNECTION ERRORS (Lỗi kết nối)

### Error: "A network-related or instance-specific error occurred"

**Mô tả:** Không thể kết nối đến SQL Server

**Nguyên nhân có thể:**
1. SQL Server service không chạy
2. Firewall chặn
3. TCP/IP không được bật
4. Server name sai

**Cách khắc phục:**

#### Bước 1: Kiểm tra SQL Server service
```bash
# Windows: Mở Services.msc và tìm "SQL Server"
# Hoặc dùng PowerShell
Get-Service -Name MSSQL*

# Khởi động service nếu stopped
net start MSSQLSERVER
```

#### Bước 2: Kiểm tra SQL Server đang lắng nghe port nào
```bash
# Mở SQL Server Configuration Manager
# SQL Server Network Configuration → Protocols for MSSQLSERVER → TCP/IP
# Enable TCP/IP và kiểm tra port (thường là 1433)
```

#### Bước 3: Test connection với sqlcmd
```bash
sqlcmd -S localhost -U sa -P YourPassword
```

#### Bước 4: Kiểm tra firewall
```bash
# Windows Firewall - Allow port 1433
netsh advfirewall firewall add rule name="SQL Server" dir=in action=allow protocol=TCP localport=1433
```

### Error: "Login failed for user 'sa'"

**Nguyên nhân có thể:**
1. Sai password
2. SQL Server Authentication chưa được bật
3. User bị khóa

**Cách khắc phục:**

#### Bật SQL Server Authentication
```sql
-- Trong SSMS, right-click server → Properties → Security
-- Chọn "SQL Server and Windows Authentication mode"
-- Restart SQL Server service
```

#### Reset password cho SA
```sql
-- Đăng nhập bằng Windows Authentication
ALTER LOGIN sa WITH PASSWORD = 'NewPassword123!';
ALTER LOGIN sa ENABLE;
```

#### Tạo user mới
```sql
CREATE LOGIN myuser WITH PASSWORD = 'Password123!';
CREATE USER myuser FOR LOGIN myuser;
ALTER ROLE db_datareader ADD MEMBER myuser;
ALTER ROLE db_datawriter ADD MEMBER myuser;
```

### Error: "Cannot open database 'MyDB' requested by the login"

**Nguyên nhân:** Database không tồn tại hoặc user không có quyền

**Cách khắc phục:**

#### Tạo database
```sql
CREATE DATABASE MyAHTDB;
```

#### Cấp quyền cho user
```sql
USE MyAHTDB;
CREATE USER myuser FOR LOGIN myuser;
ALTER ROLE db_owner ADD MEMBER myuser;
```

### Error: "Timeout expired"

**Nguyên nhân:**
1. Query chạy quá lâu
2. Database bị lock
3. Network chậm
4. Connection timeout quá ngắn

**Cách khắc phục:**

#### Tăng timeout
```csharp
// Connection timeout
"Server=localhost;Database=MyDB;User Id=sa;Password=pwd;Connection Timeout=60;"

// Command timeout
command.CommandTimeout = 60; // seconds

// EF Core
options.UseSqlServer(connectionString, sqlOptions =>
{
    sqlOptions.CommandTimeout(60);
});
```

#### Kiểm tra query chậm
```sql
-- Xem query đang chạy
SELECT session_id, status, command, cpu_time, total_elapsed_time
FROM sys.dm_exec_requests
WHERE status = 'running';

-- Kill query chậm
KILL 52; -- Thay 52 bằng session_id
```

#### Optimize query
```sql
-- Check execution plan
SET STATISTICS IO ON;
SET STATISTICS TIME ON;

-- Thêm index nếu cần
CREATE INDEX idx_email ON Users(Email);
```

## 2. ENTITY FRAMEWORK CORE ERRORS

### Error: "The entity type 'User' requires a primary key"

**Cách khắc phục:**
```csharp
public class User
{
    [Key]
    public int Id { get; set; }
    // Hoặc dùng convention: Id hoặc UserId
}
```

### Error: "No database provider has been configured"

**Cách khắc phục:**
```csharp
// Thêm UseXXX method
options.UseSqlServer(connectionString);
// hoặc
options.UseNpgsql(connectionString);
// hoặc
options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
```

### Error: "Unable to track an entity because another instance with the same key is already being tracked"

**Nguyên nhân:** EF Core đang track 2 entities với cùng key

**Cách khắc phục:**

#### Cách 1: AsNoTracking
```csharp
var user = context.Users.AsNoTracking().FirstOrDefault(u => u.Id == 1);
```

#### Cách 2: Detach entity
```csharp
context.Entry(user).State = EntityState.Detached;
```

#### Cách 3: Update đúng cách
```csharp
var user = context.Users.Find(id);
if (user != null)
{
    user.Email = newEmail;
    context.SaveChanges();
}
```

### Error: "Cannot insert explicit value for identity column"

**Cách khắc phục:**
```csharp
// KHÔNG set Id khi insert
var user = new User 
{ 
    // Id = 1,  // KHÔNG làm thế này!
    Name = "Test"
};

// Hoặc dùng DatabaseGenerated
[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
public int Id { get; set; }
```

## 3. MIGRATION ERRORS

### Error: "Build failed"

**Cách khắc phục:**
```bash
# Build project trước
dotnet build

# Sau đó chạy migration
dotnet ef migrations add InitialCreate
```

### Error: "Your startup project doesn't reference Microsoft.EntityFrameworkCore.Design"

**Cách khắc phục:**
```bash
dotnet add package Microsoft.EntityFrameworkCore.Design
```

### Error: "Unable to create an object of type 'MyDbContext'"

**Cách khắc phục:**

#### Cách 1: Tạo IDesignTimeDbContextFactory
```csharp
public class MyDbContextFactory : IDesignTimeDbContextFactory<MyDbContext>
{
    public MyDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
        optionsBuilder.UseSqlServer("Server=localhost;Database=MyDB;Integrated Security=True;");
        return new MyDbContext(optionsBuilder.Options);
    }
}
```

#### Cách 2: Thêm parameterless constructor
```csharp
public class MyDbContext : DbContext
{
    public MyDbContext() { }
    
    public MyDbContext(DbContextOptions<MyDbContext> options) 
        : base(options) { }
}
```

## 4. PERFORMANCE ISSUES (Vấn đề hiệu năng)

### Query chậm

**Cách khắc phục:**

#### 1. Sử dụng AsNoTracking cho read-only queries
```csharp
var users = context.Users
    .AsNoTracking()
    .Where(u => u.Active)
    .ToList();
```

#### 2. Select chỉ các columns cần thiết
```csharp
var users = context.Users
    .Select(u => new { u.Id, u.Name })
    .ToList();
```

#### 3. Sử dụng Include thay vì lazy loading
```csharp
// TỐT
var users = context.Users
    .Include(u => u.Orders)
    .ToList();

// TỆ - N+1 queries
foreach (var user in context.Users.ToList())
{
    var orders = user.Orders; // Mỗi lần 1 query
}
```

#### 4. Pagination
```csharp
var users = context.Users
    .Skip(pageNumber * pageSize)
    .Take(pageSize)
    .ToList();
```

#### 5. Compiled queries
```csharp
private static readonly Func<MyDbContext, int, User> _getUserById =
    EF.CompileQuery((MyDbContext context, int id) =>
        context.Users.FirstOrDefault(u => u.Id == id));

var user = _getUserById(context, 1);
```

### Connection pool exhausted

**Error:** "Timeout expired. The timeout period elapsed prior to obtaining a connection from the pool"

**Cách khắc phục:**

#### 1. Đảm bảo dispose connection
```csharp
// TỐT
using (var context = new MyDbContext())
{
    // Use context
}

// TỆ - Không dispose
var context = new MyDbContext();
// Use context
// Không dispose!
```

#### 2. Tăng pool size
```
Server=localhost;Database=MyDB;User Id=sa;Password=pwd;Max Pool Size=200;
```

#### 3. Giảm connection lifetime
```
Server=localhost;Database=MyDB;User Id=sa;Password=pwd;Connection Lifetime=30;
```

## 5. SSL/TLS ERRORS

### Error: "A connection was successfully established with the server, but then an error occurred during the login process"

**Cách khắc phục:**
```
Server=localhost;Database=MyDB;User Id=sa;Password=pwd;TrustServerCertificate=True;
```

**Hoặc với Encrypt:**
```
Server=localhost;Database=MyDB;User Id=sa;Password=pwd;Encrypt=False;
```

## 6. TOOLS & UTILITIES

### Debug Connection String
```csharp
var builder = new SqlConnectionStringBuilder(connectionString);
Console.WriteLine($"Server: {builder.DataSource}");
Console.WriteLine($"Database: {builder.InitialCatalog}");
Console.WriteLine($"User: {builder.UserID}");
Console.WriteLine($"Pooling: {builder.Pooling}");
Console.WriteLine($"Max Pool Size: {builder.MaxPoolSize}");
```

### Test Query Performance
```csharp
var stopwatch = System.Diagnostics.Stopwatch.StartNew();
var users = context.Users.ToList();
stopwatch.Stop();
Console.WriteLine($"Query took {stopwatch.ElapsedMilliseconds}ms");
```

### Monitor SQL Queries (EF Core)
```csharp
options.UseSqlServer(connectionString)
    .LogTo(Console.WriteLine, LogLevel.Information)
    .EnableSensitiveDataLogging();
```

## 7. CHECKLIST TỔNG HỢP

Khi gặp lỗi, kiểm tra theo thứ tự:

1. ✅ SQL Server service đang chạy?
2. ✅ Connection string đúng?
3. ✅ Database có tồn tại?
4. ✅ User có quyền truy cập database?
5. ✅ Firewall không chặn?
6. ✅ TCP/IP được bật?
7. ✅ Port đúng (thường là 1433)?
8. ✅ Timeout đủ lớn?
9. ✅ Connection được dispose đúng cách?
10. ✅ Packages đã được cài đặt?

## 8. USEFUL COMMANDS

### SQL Server
```sql
-- Kiểm tra database tồn tại
SELECT name FROM sys.databases WHERE name = 'MyAHTDB';

-- Kiểm tra user có quyền gì
SELECT dp.name, dp.type_desc, o.name as ObjectName, p.permission_name
FROM sys.database_permissions p
INNER JOIN sys.database_principals dp ON p.grantee_principal_id = dp.principal_id
LEFT JOIN sys.objects o ON p.major_id = o.object_id
WHERE dp.name = 'myuser';

-- Xem connections đang active
SELECT * FROM sys.dm_exec_sessions WHERE is_user_process = 1;

-- Kill all connections to a database
ALTER DATABASE MyAHTDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
ALTER DATABASE MyAHTDB SET MULTI_USER;
```

### Entity Framework Core
```bash
# List migrations
dotnet ef migrations list

# Remove last migration
dotnet ef migrations remove

# Update to specific migration
dotnet ef database update MigrationName

# Generate SQL script
dotnet ef migrations script

# Drop database
dotnet ef database drop
```

## 9. GETTING HELP

Nếu vẫn gặp vấn đề:

1. 📖 Xem logs chi tiết
2. 🔍 Google error message chính xác
3. 💬 Hỏi trên Stack Overflow
4. 📚 Đọc documentation
5. 🐛 Check GitHub issues của package đang dùng

## Resources

- [SQL Server Error Messages](https://docs.microsoft.com/sql/relational-databases/errors-events/database-engine-events-and-errors)
- [EF Core Troubleshooting](https://docs.microsoft.com/ef/core/miscellaneous/logging-events-diagnostics)
- [Connection String Reference](https://www.connectionstrings.com/)
