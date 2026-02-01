using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MyAHT.Examples
{
    /// <summary>
    /// Ví dụ kết nối database sử dụng Entity Framework Core
    /// Example of database connection using Entity Framework Core
    /// </summary>

    #region Models
    
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        
        // Navigation property
        public ICollection<Order> Orders { get; set; }
    }

    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        
        // Navigation property
        public User User { get; set; }
    }

    #endregion

    #region DbContext

    public class MyAHTDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }

        public MyAHTDbContext(DbContextOptions<MyAHTDbContext> options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cấu hình quan hệ giữa User và Order
            // Configure relationship between User and Order
            modelBuilder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId);

            // Cấu hình index
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Cấu hình giá trị mặc định
            modelBuilder.Entity<User>()
                .Property(u => u.CreatedDate)
                .HasDefaultValueSql("GETDATE()");

            base.OnModelCreating(modelBuilder);
        }
    }

    #endregion

    #region Example Usage

    public class EntityFrameworkExample
    {
        private readonly MyAHTDbContext _context;

        public EntityFrameworkExample(MyAHTDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Test kết nối và kiểm tra database có sẵn
        /// Test connection and check if database is available
        /// </summary>
        public bool TestConnection()
        {
            try
            {
                bool canConnect = _context.Database.CanConnect();
                Console.WriteLine($"Kết nối database: {(canConnect ? "Thành công" : "Thất bại")}");
                Console.WriteLine($"Connection: {(canConnect ? "Successful" : "Failed")}");
                return canConnect;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi / Error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Tạo database và apply migrations
        /// Create database and apply migrations
        /// </summary>
        public void EnsureDatabaseCreated()
        {
            _context.Database.EnsureCreated();
            Console.WriteLine("Database đã được tạo / Database created");
        }

        /// <summary>
        /// Thêm user mới (Create)
        /// Add new user (Create)
        /// </summary>
        public User CreateUser(string name, string email)
        {
            try
            {
                var user = new User
                {
                    Name = name,
                    Email = email,
                    CreatedDate = DateTime.Now,
                    Active = true
                };

                _context.Users.Add(user);
                _context.SaveChanges();

                Console.WriteLine($"Thêm user thành công / User added. ID: {user.Id}");
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi / Error: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Lấy danh sách users (Read)
        /// Get list of users (Read)
        /// </summary>
        public List<User> GetAllUsers()
        {
            try
            {
                var users = _context.Users
                    .Where(u => u.Active)
                    .OrderBy(u => u.Name)
                    .ToList();

                Console.WriteLine($"Tìm thấy {users.Count} users / Found {users.Count} users");
                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi / Error: {ex.Message}");
                return new List<User>();
            }
        }

        /// <summary>
        /// Tìm user theo ID (Read)
        /// Find user by ID (Read)
        /// </summary>
        public User GetUserById(int id)
        {
            try
            {
                var user = _context.Users.Find(id);
                if (user != null)
                {
                    Console.WriteLine($"Tìm thấy user: {user.Name} / Found user: {user.Name}");
                }
                else
                {
                    Console.WriteLine("Không tìm thấy user / User not found");
                }
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi / Error: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Cập nhật user (Update)
        /// Update user (Update)
        /// </summary>
        public bool UpdateUser(int id, string newEmail)
        {
            try
            {
                var user = _context.Users.Find(id);
                if (user == null)
                {
                    Console.WriteLine("Không tìm thấy user / User not found");
                    return false;
                }

                user.Email = newEmail;
                user.UpdatedDate = DateTime.Now;

                _context.SaveChanges();
                Console.WriteLine("Cập nhật thành công / Update successful");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi / Error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Xóa user (Delete)
        /// Delete user (Delete)
        /// </summary>
        public bool DeleteUser(int id)
        {
            try
            {
                var user = _context.Users.Find(id);
                if (user == null)
                {
                    Console.WriteLine("Không tìm thấy user / User not found");
                    return false;
                }

                _context.Users.Remove(user);
                _context.SaveChanges();
                Console.WriteLine("Xóa thành công / Delete successful");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi / Error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Sử dụng LINQ phức tạp với Include
        /// Use complex LINQ with Include
        /// </summary>
        public List<User> GetUsersWithOrders()
        {
            try
            {
                var users = _context.Users
                    .Include(u => u.Orders)
                    .Where(u => u.Active)
                    .ToList();

                foreach (var user in users)
                {
                    Console.WriteLine($"User: {user.Name}, Orders: {user.Orders.Count}");
                }

                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi / Error: {ex.Message}");
                return new List<User>();
            }
        }

        /// <summary>
        /// Sử dụng Transaction
        /// Use Transaction
        /// </summary>
        public bool CreateUserWithOrder(string userName, string email, decimal orderAmount)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Tạo user
                    var user = new User
                    {
                        Name = userName,
                        Email = email,
                        CreatedDate = DateTime.Now,
                        Active = true
                    };
                    _context.Users.Add(user);
                    _context.SaveChanges();

                    // Tạo order
                    var order = new Order
                    {
                        UserId = user.Id,
                        TotalAmount = orderAmount,
                        OrderDate = DateTime.Now
                    };
                    _context.Orders.Add(order);
                    _context.SaveChanges();

                    // Commit transaction
                    transaction.Commit();
                    Console.WriteLine("Tạo user và order thành công / User and order created successfully");
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Lỗi / Error: {ex.Message}");
                    return false;
                }
            }
        }

        /// <summary>
        /// Thực thi Raw SQL
        /// Execute Raw SQL
        /// </summary>
        public List<User> ExecuteRawSql()
        {
            try
            {
                var users = _context.Users
                    .FromSqlRaw("SELECT * FROM Users WHERE Active = 1")
                    .ToList();

                Console.WriteLine($"Tìm thấy {users.Count} users / Found {users.Count} users");
                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi / Error: {ex.Message}");
                return new List<User>();
            }
        }

        /// <summary>
        /// Sử dụng Async methods
        /// Use Async methods
        /// </summary>
        public async System.Threading.Tasks.Task<List<User>> GetAllUsersAsync()
        {
            try
            {
                var users = await _context.Users
                    .Where(u => u.Active)
                    .ToListAsync();

                Console.WriteLine($"Tìm thấy {users.Count} users / Found {users.Count} users");
                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi / Error: {ex.Message}");
                return new List<User>();
            }
        }
    }

    #endregion

    #region Configuration Example

    /// <summary>
    /// Cách cấu hình DbContext trong Program.cs hoặc Startup.cs
    /// How to configure DbContext in Program.cs or Startup.cs
    /// </summary>
    public class ConfigurationExample
    {
        public static void ConfigureServices()
        {
            /*
            // SQL Server
            services.AddDbContext<MyAHTDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // MySQL
            services.AddDbContext<MyAHTDbContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection"), 
                    ServerVersion.AutoDetect(Configuration.GetConnectionString("DefaultConnection"))));

            // PostgreSQL
            services.AddDbContext<MyAHTDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            // SQLite
            services.AddDbContext<MyAHTDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            // In-Memory (cho testing)
            services.AddDbContext<MyAHTDbContext>(options =>
                options.UseInMemoryDatabase("TestDb"));
            */
        }
    }

    #endregion
}
