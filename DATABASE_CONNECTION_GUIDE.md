# Hướng dẫn Kết nối Database (Database Connection Guide)

## Tổng quan (Overview)
Tài liệu này cung cấp hướng dẫn chi tiết về cách kết nối với các loại database phổ biến trong ứng dụng .NET/C#.

## Các loại Database phổ biến (Common Database Types)

### 1. SQL Server
SQL Server là database phổ biến nhất trong hệ sinh thái .NET.

#### Connection String cơ bản:
```
Server=localhost;Database=MyAHTDB;User Id=sa;Password=YourPassword123;TrustServerCertificate=True;
```

#### Connection String với Windows Authentication:
```
Server=localhost;Database=MyAHTDB;Integrated Security=True;TrustServerCertificate=True;
```

### 2. MySQL/MariaDB
MySQL là database mã nguồn mở phổ biến.

#### Connection String:
```
Server=localhost;Database=MyAHTDB;Uid=root;Pwd=YourPassword123;
```

### 3. PostgreSQL
PostgreSQL là database mã nguồn mở mạnh mẽ.

#### Connection String:
```
Host=localhost;Database=MyAHTDB;Username=postgres;Password=YourPassword123;
```

### 4. SQLite
SQLite là database nhẹ, không cần server.

#### Connection String:
```
Data Source=myaht.db;Version=3;
```

## Các phương pháp kết nối (Connection Methods)

### 1. ADO.NET (Cơ bản)
Phương pháp kết nối truyền thống và cơ bản nhất.

**Ưu điểm:**
- Kiểm soát hoàn toàn
- Hiệu năng cao
- Không phụ thuộc thư viện bên ngoài

**Nhược điểm:**
- Code dài dòng
- Khó maintain
- Phải quản lý connection thủ công

### 2. Entity Framework Core (ORM)
Framework ORM (Object-Relational Mapping) mạnh mẽ của Microsoft.

**Ưu điểm:**
- Code ngắn gọn, dễ đọc
- Tự động quản lý connection
- LINQ support
- Migration support
- Change tracking

**Nhược điểm:**
- Overhead nhỏ về performance
- Khó debug các query phức tạp

### 3. Dapper (Micro-ORM)
Micro-ORM nhẹ và nhanh.

**Ưu điểm:**
- Performance gần như ADO.NET
- Code đơn giản hơn ADO.NET thuần
- Mapping tự động

**Nhược điểm:**
- Ít tính năng hơn EF Core
- Không có change tracking
- Không có migration

## Best Practices (Thực hành tốt nhất)

### 1. Connection Pooling
Sử dụng connection pooling để tái sử dụng kết nối:
```
Server=localhost;Database=MyAHTDB;User Id=sa;Password=YourPassword123;
Max Pool Size=100;Min Pool Size=5;Pooling=true;
```

### 2. Bảo mật Connection String
- **KHÔNG** lưu connection string trong code
- Sử dụng `appsettings.json` hoặc Environment Variables
- Mã hóa connection string trong production
- Sử dụng Azure Key Vault hoặc AWS Secrets Manager

### 3. Xử lý Exception
```csharp
try
{
    // Database operations
}
catch (SqlException ex)
{
    // Log error
    // Handle specific SQL errors
}
catch (Exception ex)
{
    // Log error
    // Handle general errors
}
finally
{
    // Ensure connection is closed
}
```

### 4. Sử dụng Using Statement
```csharp
using (var connection = new SqlConnection(connectionString))
{
    // Connection tự động đóng khi ra khỏi scope
}
```

### 5. Async/Await
Sử dụng async methods để tránh block main thread:
```csharp
await connection.OpenAsync();
var result = await command.ExecuteReaderAsync();
```

## Khắc phục sự cố (Troubleshooting)

### Lỗi thường gặp:

1. **"Cannot open database"**
   - Kiểm tra database có tồn tại
   - Kiểm tra quyền truy cập

2. **"Login failed"**
   - Kiểm tra username/password
   - Kiểm tra SQL Server authentication mode

3. **"Server not found"**
   - Kiểm tra server name
   - Kiểm tra firewall
   - Kiểm tra SQL Server service đang chạy

4. **"Timeout expired"**
   - Tăng connection timeout
   - Kiểm tra network
   - Tối ưu query

## Testing Connection
Luôn test connection trước khi deploy:
```csharp
try
{
    using (var connection = new SqlConnection(connectionString))
    {
        connection.Open();
        Console.WriteLine("Connection successful!");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Connection failed: {ex.Message}");
}
```

## Tài nguyên tham khảo (References)
- [Microsoft Docs - Connection Strings](https://learn.microsoft.com/en-us/dotnet/framework/data/adonet/connection-strings)
- [Entity Framework Core Docs](https://learn.microsoft.com/en-us/ef/core/)
- [Dapper Documentation](https://github.com/DapperLib/Dapper)
