# Quick Start Guide - Kết nối Database trong 5 phút

## Bước 1: Chọn Database và cài đặt package

### SQL Server
```bash
dotnet add package Microsoft.Data.SqlClient
# Hoặc với Entity Framework Core:
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
# Hoặc với Dapper:
dotnet add package Dapper
```

### MySQL
```bash
dotnet add package MySql.Data
# Hoặc với Entity Framework Core:
dotnet add package Pomelo.EntityFrameworkCore.MySql
# Hoặc với Dapper:
dotnet add package Dapper
dotnet add package MySql.Data
```

### PostgreSQL
```bash
dotnet add package Npgsql
# Hoặc với Entity Framework Core:
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
# Hoặc với Dapper:
dotnet add package Dapper
dotnet add package Npgsql
```

### SQLite
```bash
dotnet add package System.Data.SQLite
# Hoặc với Entity Framework Core:
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
```

## Bước 2: Tạo Connection String

### Tạo file appsettings.json:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=MyAHTDB;User Id=sa;Password=YourPassword123;TrustServerCertificate=True;"
  }
}
```

## Bước 3: Test kết nối đơn giản

### Cách 1: ADO.NET (Không cần thêm package)
```csharp
using System;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        string connectionString = "Server=localhost;Database=MyAHTDB;User Id=sa;Password=YourPassword123;TrustServerCertificate=True;";
        
        try
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("✅ Kết nối thành công!");
                Console.WriteLine($"Database: {connection.Database}");
                Console.WriteLine($"Server: {connection.DataSource}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Lỗi: {ex.Message}");
        }
    }
}
```

### Cách 2: Entity Framework Core
```csharp
using Microsoft.EntityFrameworkCore;

// 1. Định nghĩa model
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}

// 2. Tạo DbContext
public class MyDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer("Server=localhost;Database=MyAHTDB;User Id=sa;Password=YourPassword123;TrustServerCertificate=True;");
    }
}

// 3. Sử dụng
class Program
{
    static void Main()
    {
        using (var context = new MyDbContext())
        {
            // Test connection
            bool canConnect = context.Database.CanConnect();
            Console.WriteLine(canConnect ? "✅ Kết nối thành công!" : "❌ Kết nối thất bại!");
            
            // Tạo database nếu chưa có
            context.Database.EnsureCreated();
            
            // Thêm user
            var user = new User { Name = "Nguyen Van A", Email = "a@example.com" };
            context.Users.Add(user);
            context.SaveChanges();
            
            Console.WriteLine($"✅ Đã thêm user: {user.Name}");
        }
    }
}
```

### Cách 3: Dapper
```csharp
using System;
using System.Data.SqlClient;
using Dapper;
using System.Linq;

class Program
{
    static void Main()
    {
        string connectionString = "Server=localhost;Database=MyAHTDB;User Id=sa;Password=YourPassword123;TrustServerCertificate=True;";
        
        using (var connection = new SqlConnection(connectionString))
        {
            // Test connection
            var result = connection.ExecuteScalar<int>("SELECT 1");
            Console.WriteLine($"✅ Kết nối thành công! Result: {result}");
            
            // Query dữ liệu
            var users = connection.Query<User>("SELECT * FROM Users").ToList();
            Console.WriteLine($"✅ Tìm thấy {users.Count} users");
        }
    }
}

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}
```

## Bước 4: Tạo bảng đầu tiên

### SQL Script:
```sql
CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    CreatedDate DATETIME DEFAULT GETDATE(),
    Active BIT DEFAULT 1
);

-- Insert sample data
INSERT INTO Users (Name, Email) VALUES 
    ('Nguyen Van A', 'a@example.com'),
    ('Tran Thi B', 'b@example.com'),
    ('Le Van C', 'c@example.com');
```

### Hoặc với Entity Framework Core Migration:
```bash
# Tạo migration
dotnet ef migrations add InitialCreate

# Apply migration
dotnet ef database update
```

## Bước 5: CRUD Operations

### Create (Thêm)
```csharp
var user = new User { Name = "New User", Email = "new@example.com" };
context.Users.Add(user);
context.SaveChanges();
```

### Read (Đọc)
```csharp
var users = context.Users.Where(u => u.Active).ToList();
var user = context.Users.Find(1); // Tìm theo ID
```

### Update (Cập nhật)
```csharp
var user = context.Users.Find(1);
user.Email = "newemail@example.com";
context.SaveChanges();
```

### Delete (Xóa)
```csharp
var user = context.Users.Find(1);
context.Users.Remove(user);
context.SaveChanges();
```

## Các lỗi thường gặp và cách khắc phục

### Lỗi 1: "Cannot open database"
**Nguyên nhân:** Database chưa tồn tại

**Giải pháp:**
```csharp
context.Database.EnsureCreated(); // Tạo database nếu chưa có
```

Hoặc tạo bằng SQL:
```sql
CREATE DATABASE MyAHTDB;
```

### Lỗi 2: "Login failed for user"
**Nguyên nhân:** Sai username/password hoặc user không có quyền

**Giải pháp:**
- Kiểm tra lại username/password
- Đảm bảo SQL Server Authentication được bật
- Cấp quyền cho user:
```sql
CREATE LOGIN myuser WITH PASSWORD = 'Password123';
CREATE USER myuser FOR LOGIN myuser;
ALTER ROLE db_owner ADD MEMBER myuser;
```

### Lỗi 3: "A network-related or instance-specific error"
**Nguyên nhân:** Không kết nối được tới server

**Giải pháp:**
- Kiểm tra SQL Server service đang chạy
- Kiểm tra firewall
- Kiểm tra server name đúng
- Enable TCP/IP trong SQL Server Configuration Manager

### Lỗi 4: "Timeout expired"
**Nguyên nhân:** Query chạy quá lâu

**Giải pháp:**
```csharp
// Tăng timeout
options.UseSqlServer(connectionString, sqlOptions =>
{
    sqlOptions.CommandTimeout(60); // 60 seconds
});
```

## Tips & Tricks

### 1. Connection String nhanh
```csharp
// Local SQL Server với Windows Auth
"Server=localhost;Database=MyDB;Integrated Security=True;"

// Local SQL Server với SQL Auth
"Server=localhost;Database=MyDB;User Id=sa;Password=YourPassword;"

// SQLite (không cần server)
"Data Source=mydb.db"
```

### 2. Logging SQL queries (Development)
```csharp
options.UseSqlServer(connectionString)
    .LogTo(Console.WriteLine, LogLevel.Information)
    .EnableSensitiveDataLogging(); // Chỉ dùng trong development!
```

### 3. Check connection trước khi dùng
```csharp
if (context.Database.CanConnect())
{
    // Làm việc với database
}
else
{
    Console.WriteLine("Không thể kết nối database!");
}
```

## Next Steps

1. ✅ Đã kết nối thành công? → Đọc [DATABASE_CONNECTION_GUIDE.md](DATABASE_CONNECTION_GUIDE.md)
2. 🔒 Quan tâm về security? → Đọc [SECURITY_BEST_PRACTICES.md](SECURITY_BEST_PRACTICES.md)
3. 💡 Xem thêm ví dụ? → Xem thư mục [Examples/](Examples/)
4. ❓ Gặp lỗi? → Đọc [TROUBLESHOOTING.md](TROUBLESHOOTING.md)

## Resources

- [Microsoft SQL Server Docs](https://learn.microsoft.com/en-us/sql/)
- [Entity Framework Core Docs](https://learn.microsoft.com/en-us/ef/core/)
- [Dapper GitHub](https://github.com/DapperLib/Dapper)
- [Connection Strings Reference](https://www.connectionstrings.com/)
