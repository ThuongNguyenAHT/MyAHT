using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace MyAHT.Examples
{
    /// <summary>
    /// Ví dụ kết nối database sử dụng Dapper (Micro-ORM)
    /// Example of database connection using Dapper (Micro-ORM)
    /// </summary>
    
    #region Models

    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
    }

    public class OrderDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
    }

    public class UserWithOrdersDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<OrderDto> Orders { get; set; } = new List<OrderDto>();
    }

    #endregion

    public class DapperExample
    {
        private readonly string _connectionString;

        public DapperExample(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        /// <summary>
        /// Test kết nối đơn giản
        /// Simple connection test
        /// </summary>
        public bool TestConnection()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    var result = connection.ExecuteScalar<int>("SELECT 1");
                    Console.WriteLine($"Kết nối thành công! Result: {result}");
                    Console.WriteLine("Connection successful!");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi / Error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Lấy danh sách users (Query)
        /// Get list of users (Query)
        /// </summary>
        public List<UserDto> GetAllUsers()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    var sql = "SELECT * FROM Users WHERE Active = 1 ORDER BY Name";
                    var users = connection.Query<UserDto>(sql).ToList();
                    
                    Console.WriteLine($"Tìm thấy {users.Count} users / Found {users.Count} users");
                    return users;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi / Error: {ex.Message}");
                return new List<UserDto>();
            }
        }

        /// <summary>
        /// Lấy user theo ID với parameters
        /// Get user by ID with parameters
        /// </summary>
        public UserDto GetUserById(int id)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    var sql = "SELECT * FROM Users WHERE Id = @Id";
                    var user = connection.QueryFirstOrDefault<UserDto>(sql, new { Id = id });
                    
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
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi / Error: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Thêm user mới (Insert)
        /// Add new user (Insert)
        /// </summary>
        public int InsertUser(string name, string email)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    var sql = @"
                        INSERT INTO Users (Name, Email, CreatedDate, Active) 
                        VALUES (@Name, @Email, @CreatedDate, @Active);
                        SELECT CAST(SCOPE_IDENTITY() as int);";
                    
                    var newId = connection.ExecuteScalar<int>(sql, new
                    {
                        Name = name,
                        Email = email,
                        CreatedDate = DateTime.Now,
                        Active = true
                    });
                    
                    Console.WriteLine($"Thêm user thành công / User added. ID: {newId}");
                    return newId;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi / Error: {ex.Message}");
                return -1;
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
                using (var connection = GetConnection())
                {
                    var sql = @"
                        UPDATE Users 
                        SET Email = @Email, UpdatedDate = @UpdatedDate 
                        WHERE Id = @Id";
                    
                    var rowsAffected = connection.Execute(sql, new
                    {
                        Id = id,
                        Email = newEmail,
                        UpdatedDate = DateTime.Now
                    });
                    
                    Console.WriteLine($"Số dòng cập nhật / Rows updated: {rowsAffected}");
                    return rowsAffected > 0;
                }
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
                using (var connection = GetConnection())
                {
                    var sql = "DELETE FROM Users WHERE Id = @Id";
                    var rowsAffected = connection.Execute(sql, new { Id = id });
                    
                    Console.WriteLine($"Số dòng xóa / Rows deleted: {rowsAffected}");
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi / Error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Thêm nhiều users cùng lúc (Bulk Insert)
        /// Add multiple users at once (Bulk Insert)
        /// </summary>
        public int BulkInsertUsers(List<UserDto> users)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    var sql = @"
                        INSERT INTO Users (Name, Email, CreatedDate, Active) 
                        VALUES (@Name, @Email, @CreatedDate, @Active)";
                    
                    var rowsAffected = connection.Execute(sql, users);
                    
                    Console.WriteLine($"Thêm {rowsAffected} users / Added {rowsAffected} users");
                    return rowsAffected;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi / Error: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// Thực thi Stored Procedure
        /// Execute Stored Procedure
        /// </summary>
        public List<UserDto> ExecuteStoredProcedure(int minId)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    var users = connection.Query<UserDto>(
                        "sp_GetUsers",
                        new { MinId = minId },
                        commandType: CommandType.StoredProcedure
                    ).ToList();
                    
                    Console.WriteLine($"Tìm thấy {users.Count} users / Found {users.Count} users");
                    return users;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi / Error: {ex.Message}");
                return new List<UserDto>();
            }
        }

        /// <summary>
        /// Multi-mapping: Join giữa Users và Orders
        /// Multi-mapping: Join between Users and Orders
        /// </summary>
        public List<UserWithOrdersDto> GetUsersWithOrders()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    var sql = @"
                        SELECT u.*, o.*
                        FROM Users u
                        LEFT JOIN Orders o ON u.Id = o.UserId
                        WHERE u.Active = 1
                        ORDER BY u.Name";

                    var userDictionary = new Dictionary<int, UserWithOrdersDto>();

                    connection.Query<UserWithOrdersDto, OrderDto, UserWithOrdersDto>(
                        sql,
                        (user, order) =>
                        {
                            if (!userDictionary.TryGetValue(user.Id, out var userEntry))
                            {
                                userEntry = user;
                                userEntry.Orders = new List<OrderDto>();
                                userDictionary.Add(user.Id, userEntry);
                            }

                            if (order != null)
                            {
                                userEntry.Orders.Add(order);
                            }

                            return userEntry;
                        },
                        splitOn: "Id"
                    );

                    var users = userDictionary.Values.ToList();
                    Console.WriteLine($"Tìm thấy {users.Count} users / Found {users.Count} users");
                    return users;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi / Error: {ex.Message}");
                return new List<UserWithOrdersDto>();
            }
        }

        /// <summary>
        /// Sử dụng Transaction
        /// Use Transaction
        /// </summary>
        public bool TransferMoney(int fromUserId, int toUserId, decimal amount)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Trừ tiền từ user nguồn
                        var debitSql = @"
                            UPDATE Accounts 
                            SET Balance = Balance - @Amount 
                            WHERE UserId = @UserId";
                        
                        connection.Execute(debitSql, 
                            new { Amount = amount, UserId = fromUserId }, 
                            transaction);

                        // Cộng tiền cho user đích
                        var creditSql = @"
                            UPDATE Accounts 
                            SET Balance = Balance + @Amount 
                            WHERE UserId = @UserId";
                        
                        connection.Execute(creditSql, 
                            new { Amount = amount, UserId = toUserId }, 
                            transaction);

                        // Commit transaction
                        transaction.Commit();
                        Console.WriteLine("Chuyển tiền thành công / Transfer successful");
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
        }

        /// <summary>
        /// Query với dynamic result
        /// Query with dynamic result
        /// </summary>
        public void QueryDynamic()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    var sql = "SELECT Id, Name, Email FROM Users WHERE Active = 1";
                    var users = connection.Query(sql);
                    
                    foreach (var user in users)
                    {
                        Console.WriteLine($"ID: {user.Id}, Name: {user.Name}, Email: {user.Email}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi / Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Async operations
        /// </summary>
        public async System.Threading.Tasks.Task<List<UserDto>> GetAllUsersAsync()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    var sql = "SELECT * FROM Users WHERE Active = 1 ORDER BY Name";
                    var users = await connection.QueryAsync<UserDto>(sql);
                    
                    var userList = users.ToList();
                    Console.WriteLine($"Tìm thấy {userList.Count} users / Found {userList.Count} users");
                    return userList;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi / Error: {ex.Message}");
                return new List<UserDto>();
            }
        }

        /// <summary>
        /// Query với IN clause
        /// Query with IN clause
        /// </summary>
        public List<UserDto> GetUsersByIds(int[] userIds)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    var sql = "SELECT * FROM Users WHERE Id IN @UserIds";
                    var users = connection.Query<UserDto>(sql, new { UserIds = userIds }).ToList();
                    
                    Console.WriteLine($"Tìm thấy {users.Count} users / Found {users.Count} users");
                    return users;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi / Error: {ex.Message}");
                return new List<UserDto>();
            }
        }
    }
}
