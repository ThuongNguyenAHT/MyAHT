# MyAHT - Database Connection Guide
The application only staff

## 📚 Tài liệu hướng dẫn kết nối Database

Dự án này cung cấp tài liệu chi tiết và ví dụ code về cách kết nối với các loại database phổ biến trong .NET/C#.

### 📚 Complete Documentation
**Xem toàn bộ tài liệu và learning paths:** [INDEX.md](INDEX.md) - Mục lục đầy đủ

## 🚀 Quick Start
**Bắt đầu nhanh trong 5 phút:** [QUICK_START.md](QUICK_START.md)

### 📖 Tài liệu chi tiết

1. **[DATABASE_CONNECTION_GUIDE.md](DATABASE_CONNECTION_GUIDE.md)**
   - Tổng quan về các loại database (SQL Server, MySQL, PostgreSQL, SQLite)
   - Connection strings cho từng loại database
   - So sánh các phương pháp kết nối (ADO.NET, Entity Framework Core, Dapper)
   - Best practices và performance tips

2. **[SECURITY_BEST_PRACTICES.md](SECURITY_BEST_PRACTICES.md)**
   - Bảo mật connection string
   - Phòng chống SQL Injection
   - Mã hóa mật khẩu
   - Principle of least privilege
   - SSL/TLS encryption
   - Security checklist

3. **[TROUBLESHOOTING.md](TROUBLESHOOTING.md)**
   - Khắc phục các lỗi thường gặp
   - Connection errors
   - Entity Framework errors
   - Migration issues
   - Performance problems
   - Debug tips & tools

### 💻 Ví dụ Code

Thư mục [Examples/](Examples/) chứa các ví dụ đầy đủ về:

1. **[1_ADO_NET_Example.cs](Examples/1_ADO_NET_Example.cs)**
   - Kết nối cơ bản với ADO.NET
   - CRUD operations
   - Stored procedures
   - Transactions
   - Async operations

2. **[2_EntityFramework_Example.cs](Examples/2_EntityFramework_Example.cs)**
   - DbContext setup
   - CRUD operations với EF Core
   - LINQ queries
   - Relationships & navigation properties
   - Migrations
   - Async operations

3. **[3_Dapper_Example.cs](Examples/3_Dapper_Example.cs)**
   - Query với Dapper
   - CRUD operations
   - Bulk operations
   - Multi-mapping
   - Stored procedures
   - Async operations

### 🔧 Configuration Files

- **[appsettings.example.json](appsettings.example.json)** - Ví dụ về configuration file với connection strings
- **[.gitignore](.gitignore)** - Danh sách files không commit (bao gồm connection strings thật)

### 📋 So sánh các phương pháp

| Tính năng | ADO.NET | Entity Framework Core | Dapper |
|-----------|---------|----------------------|--------|
| **Performance** | ⭐⭐⭐⭐⭐ Rất nhanh | ⭐⭐⭐ Trung bình | ⭐⭐⭐⭐⭐ Rất nhanh |
| **Dễ sử dụng** | ⭐⭐ Khó | ⭐⭐⭐⭐⭐ Rất dễ | ⭐⭐⭐⭐ Dễ |
| **LINQ Support** | ❌ Không | ✅ Có | ❌ Không |
| **Change Tracking** | ❌ Không | ✅ Có | ❌ Không |
| **Migrations** | ❌ Không | ✅ Có | ❌ Không |
| **Learning Curve** | Dốc | Thoải | Thoải |
| **Best For** | High performance | Rapid development | Performance + simplicity |

### 🎯 Khi nào dùng gì?

- **ADO.NET**: Khi cần performance tối đa, có kinh nghiệm, dự án nhỏ
- **Entity Framework Core**: Khi cần phát triển nhanh, dự án lớn, team mới
- **Dapper**: Khi cần balance giữa performance và simplicity

### 🔐 Security Checklist

- [ ] Connection string không hardcode trong code
- [ ] Sử dụng parameterized queries (chống SQL Injection)
- [ ] Hash passwords trước khi lưu
- [ ] Sử dụng SSL/TLS cho production
- [ ] User có quyền tối thiểu (least privilege)
- [ ] Backup database định kỳ
- [ ] Monitor và log (không log sensitive data)

### 🚦 Getting Started

```bash
# Clone repository
git clone https://github.com/ThuongNguyenAHT/MyAHT.git
cd MyAHT

# Đọc Quick Start guide
cat QUICK_START.md

# Xem ví dụ code
cd Examples
```

### 📞 Support

Nếu gặp vấn đề:
1. Xem [TROUBLESHOOTING.md](TROUBLESHOOTING.md)
2. Check [Issues](https://github.com/ThuongNguyenAHT/MyAHT/issues)
3. Tạo Issue mới

### 📝 License

MIT License - Free to use

---

**Lưu ý:** Tài liệu này được viết song ngữ Việt-Anh để dễ hiểu. Code examples có comments bằng cả hai ngôn ngữ.
