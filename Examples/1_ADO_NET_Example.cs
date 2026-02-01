using System;
using System.Data;
using System.Data.SqlClient;

namespace MyAHT.Examples
{
    /// <summary>
    /// Ví dụ kết nối database sử dụng ADO.NET
    /// Example of database connection using ADO.NET
    /// </summary>
    public class AdoNetExample
    {
        private readonly string _connectionString;

        public AdoNetExample(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Test kết nối cơ bản
        /// Basic connection test
        /// </summary>
        public bool TestConnection()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Kết nối thành công! / Connection successful!");
                    Console.WriteLine($"Database: {connection.Database}");
                    Console.WriteLine($"Server Version: {connection.ServerVersion}");
                    return true;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Lỗi SQL / SQL Error: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi / Error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Thực thi câu lệnh SELECT
        /// Execute SELECT query
        /// </summary>
        public void ExecuteSelectQuery()
        {
            string query = "SELECT Id, Name, Email FROM Users WHERE Active = 1";

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new SqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string name = reader.GetString(1);
                                string email = reader.GetString(2);

                                Console.WriteLine($"ID: {id}, Name: {name}, Email: {email}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi thực thi query / Error executing query: {ex.Message}");
            }
        }

        /// <summary>
        /// Thực thi câu lệnh INSERT với parameters (tránh SQL Injection)
        /// Execute INSERT with parameters (prevent SQL Injection)
        /// </summary>
        public int InsertUser(string name, string email)
        {
            string query = @"
                INSERT INTO Users (Name, Email, CreatedDate, Active) 
                VALUES (@Name, @Email, @CreatedDate, @Active);
                SELECT CAST(SCOPE_IDENTITY() as int);";

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new SqlCommand(query, connection))
                    {
                        // Sử dụng parameters để tránh SQL Injection
                        // Use parameters to prevent SQL Injection
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                        command.Parameters.AddWithValue("@Active", true);

                        int newId = (int)command.ExecuteScalar();
                        Console.WriteLine($"Thêm user thành công / User added successfully. ID: {newId}");
                        return newId;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi thêm user / Error adding user: {ex.Message}");
                return -1;
            }
        }

        /// <summary>
        /// Thực thi câu lệnh UPDATE
        /// Execute UPDATE query
        /// </summary>
        public bool UpdateUser(int userId, string newEmail)
        {
            string query = "UPDATE Users SET Email = @Email, UpdatedDate = @UpdatedDate WHERE Id = @UserId";

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", newEmail);
                        command.Parameters.AddWithValue("@UpdatedDate", DateTime.Now);
                        command.Parameters.AddWithValue("@UserId", userId);

                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine($"Số dòng đã cập nhật / Rows updated: {rowsAffected}");
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi cập nhật / Error updating: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Thực thi Stored Procedure
        /// Execute Stored Procedure
        /// </summary>
        public void ExecuteStoredProcedure(int userId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new SqlCommand("sp_GetUserDetails", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserId", userId);

                        // Output parameter
                        var outputParam = new SqlParameter("@TotalRecords", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputParam);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Process results
                                Console.WriteLine($"User details: {reader["Name"]}");
                            }
                        }

                        int totalRecords = (int)outputParam.Value;
                        Console.WriteLine($"Total records: {totalRecords}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi thực thi SP / Error executing SP: {ex.Message}");
            }
        }

        /// <summary>
        /// Sử dụng Transaction để đảm bảo tính toàn vẹn dữ liệu
        /// Use Transaction to ensure data integrity
        /// </summary>
        public bool TransferMoney(int fromUserId, int toUserId, decimal amount)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Bắt đầu transaction
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Trừ tiền từ user nguồn
                        using (var command = new SqlCommand(
                            "UPDATE Accounts SET Balance = Balance - @Amount WHERE UserId = @UserId", 
                            connection, 
                            transaction))
                        {
                            command.Parameters.AddWithValue("@Amount", amount);
                            command.Parameters.AddWithValue("@UserId", fromUserId);
                            command.ExecuteNonQuery();
                        }

                        // Cộng tiền cho user đích
                        using (var command = new SqlCommand(
                            "UPDATE Accounts SET Balance = Balance + @Amount WHERE UserId = @UserId", 
                            connection, 
                            transaction))
                        {
                            command.Parameters.AddWithValue("@Amount", amount);
                            command.Parameters.AddWithValue("@UserId", toUserId);
                            command.ExecuteNonQuery();
                        }

                        // Commit nếu tất cả thành công
                        transaction.Commit();
                        Console.WriteLine("Chuyển tiền thành công / Transfer successful");
                        return true;
                    }
                    catch (Exception ex)
                    {
                        // Rollback nếu có lỗi
                        transaction.Rollback();
                        Console.WriteLine($"Lỗi chuyển tiền / Transfer error: {ex.Message}");
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// Kết nối bất đồng bộ (Async)
        /// Asynchronous connection
        /// </summary>
        public async System.Threading.Tasks.Task<bool> TestConnectionAsync()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    Console.WriteLine("Kết nối async thành công! / Async connection successful!");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi / Error: {ex.Message}");
                return false;
            }
        }
    }
}
