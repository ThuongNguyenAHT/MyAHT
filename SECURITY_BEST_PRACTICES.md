# Security Best Practices cho Database Connection

## 1. BẢO MẬT CONNECTION STRING

### ❌ KHÔNG BAO GIỜ làm thế này:
```csharp
// KHÔNG hardcode connection string trong code
string connectionString = "Server=localhost;Database=MyDB;User Id=sa;Password=123456;";
```

### ✅ Làm thế này thay vào:

#### a. Sử dụng appsettings.json
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;..."
  }
}
```

```csharp
// Đọc từ configuration
var connectionString = Configuration.GetConnectionString("DefaultConnection");
```

#### b. Sử dụng Environment Variables
```bash
# Linux/Mac
export ConnectionStrings__DefaultConnection="Server=..."

# Windows
set ConnectionStrings__DefaultConnection=Server=...
```

```csharp
var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");
```

#### c. Sử dụng User Secrets (Development)
```bash
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=..."
```

#### d. Sử dụng Azure Key Vault (Production)
```csharp
builder.Configuration.AddAzureKeyVault(
    new Uri($"https://{keyVaultName}.vault.azure.net/"),
    new DefaultAzureCredential());
```

## 2. PHÒNG CHỐNG SQL INJECTION

### ❌ NGUY HIỂM - SQL Injection vulnerability:
```csharp
string query = $"SELECT * FROM Users WHERE Username = '{username}' AND Password = '{password}'";
// Hacker có thể nhập: username = "admin' OR '1'='1"
```

### ✅ AN TOÀN - Sử dụng Parameters:

#### ADO.NET:
```csharp
string query = "SELECT * FROM Users WHERE Username = @Username AND Password = @Password";
command.Parameters.AddWithValue("@Username", username);
command.Parameters.AddWithValue("@Password", password);
```

#### Entity Framework Core:
```csharp
// EF Core tự động parameterize
var user = context.Users
    .Where(u => u.Username == username && u.Password == password)
    .FirstOrDefault();

// Hoặc với raw SQL
var users = context.Users
    .FromSqlRaw("SELECT * FROM Users WHERE Username = {0}", username)
    .ToList();
```

#### Dapper:
```csharp
var users = connection.Query<User>(
    "SELECT * FROM Users WHERE Username = @Username",
    new { Username = username }
);
```

## 3. MÃ HÓA MẬT KHẨU

### ❌ KHÔNG lưu plain text password:
```csharp
string password = "123456"; // NGUY HIỂM!
INSERT INTO Users (Password) VALUES ('123456')
```

### ✅ Hash password trước khi lưu:
```csharp
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

public string HashPassword(string password)
{
    // Generate salt
    byte[] salt = new byte[128 / 8];
    using (var rng = RandomNumberGenerator.Create())
    {
        rng.GetBytes(salt);
    }

    // Hash password with salt
    string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
        password: password,
        salt: salt,
        prf: KeyDerivationPrf.HMACSHA256,
        iterationCount: 10000,
        numBytesRequested: 256 / 8));

    return $"{Convert.ToBase64String(salt)}.{hashed}";
}

public bool VerifyPassword(string enteredPassword, string storedHash)
{
    var parts = storedHash.Split('.');
    var salt = Convert.FromBase64String(parts[0]);
    var storedPasswordHash = parts[1];

    string enteredPasswordHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
        password: enteredPassword,
        salt: salt,
        prf: KeyDerivationPrf.HMACSHA256,
        iterationCount: 10000,
        numBytesRequested: 256 / 8));

    return storedPasswordHash == enteredPasswordHash;
}
```

## 4. PRINCIPLE OF LEAST PRIVILEGE

### ❌ KHÔNG dùng admin account:
```
User Id=sa;Password=...  // Admin account, quá nhiều quyền!
```

### ✅ Tạo user riêng với quyền hạn chế:
```sql
-- Tạo login và user
CREATE LOGIN MyAppUser WITH PASSWORD = 'StrongPassword123!';
CREATE USER MyAppUser FOR LOGIN MyAppUser;

-- Chỉ cấp quyền cần thiết
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.Users TO MyAppUser;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.Orders TO MyAppUser;
GRANT EXECUTE ON dbo.sp_GetUserDetails TO MyAppUser;

-- KHÔNG cấp quyền DROP, ALTER, CREATE
```

## 5. ENCRYPTION IN TRANSIT

### ✅ Sử dụng SSL/TLS:

#### SQL Server:
```
Server=localhost;Database=MyDB;User Id=user;Password=pwd;Encrypt=True;TrustServerCertificate=False;
```

#### MySQL:
```
Server=localhost;Database=MyDB;Uid=user;Pwd=pwd;SslMode=Required;
```

#### PostgreSQL:
```
Host=localhost;Database=MyDB;Username=user;Password=pwd;SSL Mode=Require;
```

## 6. CONNECTION POOLING SECURITY

### ✅ Cấu hình đúng pool:
```
Server=localhost;Database=MyDB;User Id=user;Password=pwd;
Min Pool Size=5;
Max Pool Size=100;
Connection Lifetime=300;  // Tự động refresh connection sau 5 phút
```

## 7. TIMEOUT & RETRY LOGIC

### ✅ Cấu hình timeout hợp lý:
```csharp
services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions =>
    {
        sqlOptions.CommandTimeout(30);
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null);
    }));
```

## 8. LOGGING & MONITORING

### ✅ Log SQL queries (Development only):
```json
{
  "Logging": {
    "LogLevel": {
      "Microsoft.EntityFrameworkCore.Database.Command": "Information"
    }
  }
}
```

### ❌ KHÔNG log sensitive data:
```csharp
// KHÔNG log passwords, credit cards, etc.
logger.LogInformation($"User {username} logged in"); // OK
logger.LogInformation($"Password: {password}");       // NGUY HIỂM!
```

## 9. INPUT VALIDATION

### ✅ Validate tất cả input:
```csharp
public bool IsValidEmail(string email)
{
    if (string.IsNullOrWhiteSpace(email))
        return false;
    
    try
    {
        var addr = new System.Net.Mail.MailAddress(email);
        return addr.Address == email;
    }
    catch
    {
        return false;
    }
}

public bool IsValidUsername(string username)
{
    // Chỉ cho phép letters, numbers, underscore
    return Regex.IsMatch(username, @"^[a-zA-Z0-9_]{3,20}$");
}
```

## 10. REGULAR SECURITY AUDITS

### Checklist định kỳ:
- [ ] Review connection strings
- [ ] Check user permissions
- [ ] Update passwords
- [ ] Review logs for suspicious activity
- [ ] Update database software and patches
- [ ] Review and update firewall rules
- [ ] Test backup and recovery procedures

## 11. BACKUP & DISASTER RECOVERY

### ✅ Backup strategy:
```sql
-- Full backup
BACKUP DATABASE MyAHTDB 
TO DISK = 'C:\Backup\MyAHTDB_Full.bak'
WITH INIT, COMPRESSION;

-- Differential backup
BACKUP DATABASE MyAHTDB 
TO DISK = 'C:\Backup\MyAHTDB_Diff.bak'
WITH DIFFERENTIAL, COMPRESSION;

-- Transaction log backup
BACKUP LOG MyAHTDB 
TO DISK = 'C:\Backup\MyAHTDB_Log.trn'
WITH COMPRESSION;
```

## 12. COMPLIANCE & REGULATIONS

Tùy vào ứng dụng, bạn có thể cần tuân thủ:
- GDPR (EU)
- PDPA (Vietnam)
- HIPAA (Healthcare - US)
- PCI DSS (Payment cards)

## TÓM TẮT CHECKLIST BẢO MẬT

✅ **PHẢI LÀM:**
1. Sử dụng parameterized queries
2. Hash passwords
3. Encrypt connection strings
4. Use SSL/TLS
5. Implement proper error handling
6. Use least privilege principle
7. Regular backups
8. Monitor and log (không log sensitive data)
9. Input validation
10. Keep software updated

❌ **KHÔNG BAO GIỜ:**
1. Hardcode connection strings
2. Store plain text passwords
3. Use SA/root account in production
4. Trust user input
5. Expose error details to users
6. Commit secrets to source control
7. Disable SSL/TLS in production
8. Give excessive permissions
9. Ignore security warnings
10. Skip backups
