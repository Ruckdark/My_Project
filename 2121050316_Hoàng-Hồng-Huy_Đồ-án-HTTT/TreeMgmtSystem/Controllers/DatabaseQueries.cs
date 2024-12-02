using bookMgmtADO;
using System;
using System.Data;
using System.Data.SqlClient;
using TreeMgmtSystem.Models;

namespace TreeMgmtSystem.Controllers
{
    public class DatabaseQueries
    {
        private SqlConnection connection;

        public DatabaseQueries()
        {
            var connect = new Connect();
            connection = connect.GetConnection();
        }
        #region Tree
        public DataTable GetTrees()
        {
            DataTable table = new DataTable();
            string query = "SELECT Tree.TreeId, Species.SpeciesName AS Species, Tree.Location, Tree.HealthStatus, Tree.ReminderDate FROM Tree INNER JOIN Species ON Tree.Species = Species.SpeciesId";
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching trees: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return table;
        }



        public DataTable GetDetailedTrees()
        {
            DataTable table = new DataTable();
            string query = @"
        SELECT 
            Tree.TreeId,
            Species.SpeciesName AS Species, 
            Tree.Age, 
            Tree.Height, 
            Tree.Diameter, 
            Tree.HealthStatus, 
            Tree.Note, 
            Tree.Location
        FROM 
            Tree 
        INNER JOIN 
            Species ON Tree.Species = Species.SpeciesId";
            try
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching detailed trees: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return table;
        }


        public int GetTotalTrees()
        {
            return GetCount("SELECT COUNT(*) FROM Tree");
        }

        public int GetTotalServices()
        {
            return GetCount("SELECT COUNT(*) FROM Services");
        }

        public int GetTotalReports()
        {
            return GetCount("SELECT COUNT(*) FROM Report");
        }

        public int GetTotalUsers()
        {
            return GetCount("SELECT COUNT(*) FROM Users");
        }

        private int GetCount(string query)
        {
            int count = 0;
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                count = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching count: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return count;
        }

        public void AddTree(Tree tree)
        {
            try
            {
                connection.Open();
                string query = "INSERT INTO Tree (UserId, Species, Age, Height, Diameter, HealthStatus, Note, Location, ReminderDate) VALUES (@userId, @species, @age, @height, @diameter, @healthStatus, @note, @location, @reminderDate)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@userId", tree.UserId);
                cmd.Parameters.AddWithValue("@species", tree.Species);
                cmd.Parameters.AddWithValue("@age", tree.Age);
                cmd.Parameters.AddWithValue("@height", tree.Height);
                cmd.Parameters.AddWithValue("@diameter", tree.Diameter);
                cmd.Parameters.AddWithValue("@healthStatus", tree.HealthStatus);
                cmd.Parameters.AddWithValue("@note", tree.Note);
                cmd.Parameters.AddWithValue("@location", tree.Location);
                cmd.Parameters.AddWithValue("@reminderDate", tree.ReminderDate);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding tree: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public void UpdateTree(Tree tree)
        {
            try
            {
                connection.Open();
                string query = "UPDATE Tree SET UserId = @userId, Species = @species, Age = @age, Height = @height, Diameter = @diameter, HealthStatus = @healthStatus, Note = @note, Location = @location, ReminderDate = @reminderDate WHERE TreeId = @treeId";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@treeId", tree.TreeId);
                cmd.Parameters.AddWithValue("@userId", tree.UserId);
                cmd.Parameters.AddWithValue("@species", tree.Species);
                cmd.Parameters.AddWithValue("@age", tree.Age);
                cmd.Parameters.AddWithValue("@height", tree.Height);
                cmd.Parameters.AddWithValue("@diameter", tree.Diameter);
                cmd.Parameters.AddWithValue("@healthStatus", tree.HealthStatus);
                cmd.Parameters.AddWithValue("@note", tree.Note);
                cmd.Parameters.AddWithValue("@location", tree.Location);
                cmd.Parameters.AddWithValue("@reminderDate", tree.ReminderDate);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating tree: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public Tree GetTreeById(int treeId)
        {
            Tree tree = null;
            string query = "SELECT TreeId, UserId, Species, Age, Height, Diameter, HealthStatus, Note, Location, ReminderDate FROM Tree WHERE TreeId = @treeId";
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@treeId", treeId);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    tree = new Tree
                    {
                        TreeId = reader.GetInt32(0),
                        UserId = reader.GetInt32(1),
                        Species = reader.GetInt32(2),
                        Age = reader.GetInt32(3),
                        Height = reader.GetInt32(4),
                        Diameter = reader.GetInt32(5),
                        HealthStatus = reader.GetString(6),
                        Note = reader.GetString(7),
                        Location = reader.GetString(8),
                        ReminderDate = reader.GetDateTime(9)
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching tree by ID: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return tree;
        }

        public void DeleteTree(int treeId)
        {
            try
            {
                connection.Open();
                string query = "DELETE FROM Tree WHERE TreeId = @treeId";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@treeId", treeId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting tree: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        #endregion

        #region Service
        public DataTable GetServices()
        {
            DataTable table = new DataTable();
            string query = "SELECT ServiceId, ServiceType, RequestDate, Status, UserId FROM Services";
            try
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching services: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return table;
        }

        public bool AddService(Service service)
        {
            bool isSuccessful = false;
            string query = "INSERT INTO Services (ServiceType, RequestDate, UserId) VALUES (@serviceType, @requestDate, @userId)";
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@serviceType", service.ServiceType);
                cmd.Parameters.AddWithValue("@requestDate", service.RequestDate);
                cmd.Parameters.AddWithValue("@userId", service.UserId);
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    isSuccessful = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding service: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return isSuccessful;
        }


        public void UpdateService(Service service)
        {
            try
            {
                connection.Open();
                string query = "UPDATE Services SET ServiceType = @serviceType, RequestDate = @requestDate, Status = @status, UserId = @userId WHERE ServiceId = @serviceId";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@serviceId", service.ServiceId);
                cmd.Parameters.AddWithValue("@serviceType", service.ServiceType);
                cmd.Parameters.AddWithValue("@requestDate", service.RequestDate);
                cmd.Parameters.AddWithValue("@status", service.Status);
                cmd.Parameters.AddWithValue("@userId", service.UserId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating service: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public Service GetServiceById(int serviceId)
        {
            Service service = null;
            string query = "SELECT ServiceId, ServiceType, RequestDate, Status, UserId FROM Services WHERE ServiceId = @serviceId";
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@serviceId", serviceId);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    service = new Service
                    {
                        ServiceId = reader.GetInt32(0),
                        ServiceType = reader.GetString(1),
                        RequestDate = reader.GetDateTime(2),
                        Status = reader.GetString(3),
                        UserId = reader.GetInt32(4)
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching service by ID: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return service;
        }

        public void DeleteService(int serviceId)
        {
            try
            {
                connection.Open();
                string query = "DELETE FROM Services WHERE ServiceId = @serviceId";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@serviceId", serviceId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting service: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        #endregion

        #region Users
        public DataTable GetUsers()
        {
            DataTable table = new DataTable();
            string query = "SELECT UserId, UserName, FullName, Email, PhoneNumber, Address, Role FROM Users";
            try
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching users: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return table;
        }

        public void AddUser(User user)
        {
            try
            {
                connection.Open();
                string query = "INSERT INTO Users (UserName, FullName, Email, PhoneNumber, Address, Role) VALUES (@userName, @fullName, @email, @phoneNumber, @address, @role)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@userName", user.UserName);
                cmd.Parameters.AddWithValue("@fullName", user.FullName);
                cmd.Parameters.AddWithValue("@email", user.Email);
                cmd.Parameters.AddWithValue("@phoneNumber", user.PhoneNumber);
                cmd.Parameters.AddWithValue("@address", user.Address);
                cmd.Parameters.AddWithValue("@role", user.Role);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding user: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public void UpdateUser(User user)
        {
            try
            {
                connection.Open();
                string query = "UPDATE Users SET UserName = @userName, FullName = @fullName, Email = @email, PhoneNumber = @phoneNumber, Address = @address, Role = @role WHERE UserId = @userId";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@userId", user.UserId);
                cmd.Parameters.AddWithValue("@userName", user.UserName);
                cmd.Parameters.AddWithValue("@fullName", user.FullName);
                cmd.Parameters.AddWithValue("@email", user.Email);
                cmd.Parameters.AddWithValue("@phoneNumber", user.PhoneNumber);
                cmd.Parameters.AddWithValue("@address", user.Address);
                cmd.Parameters.AddWithValue("@role", user.Role);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating user: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public User GetUserById(int userId)
        {
            User user = null;
            string query = "SELECT UserId, UserName, FullName, Email, PhoneNumber, Address, Role FROM Users WHERE UserId = @userId";
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@userId", userId);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    user = new User
                    {
                        UserId = reader.GetInt32(0),
                        UserName = reader.GetString(1),
                        FullName = reader.GetString(2),
                        Email = reader.GetString(3),
                        PhoneNumber = reader.GetString(4),
                        Address = reader.GetString(5),
                        Role = reader.GetString(6)
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching user by ID: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return user;
        }

        public void DeleteUser(int userId)
        {
            try
            {
                connection.Open();
                string query = "DELETE FROM Users WHERE UserId = @userId";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting user: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        #endregion

        #region Login/Register
        public bool AuthenticateUser(string userName, string password)
        {
            bool isAuthenticated = false;
            string query = "SELECT COUNT(1) FROM Users WHERE UserName = @userName AND Password = @password";
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@userName", userName);
                cmd.Parameters.AddWithValue("@password", password);
                int result = (int)cmd.ExecuteScalar();
                if (result == 1)
                {
                    isAuthenticated = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error authenticating user: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return isAuthenticated;
        }
        public bool AuthenticateUser(string userName, string password, out string role)
        {
            bool isAuthenticated = false;
            role = string.Empty;
            string query = "SELECT COUNT(1), Role FROM Users WHERE UserName = @userName AND Password = @password GROUP BY Role";
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@userName", userName);
                cmd.Parameters.AddWithValue("@password", password);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    if (reader.GetInt32(0) == 1)
                    {
                        isAuthenticated = true;
                        role = reader.GetString(1);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error authenticating user: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return isAuthenticated;
        }

        public bool RegisterUser(string userName, string fullName, string email, string phoneNumber, string address, string role)
        {
            bool isRegistered = false;
            string query = "INSERT INTO Users (UserName, FullName, Email, PhoneNumber, Address, Role) VALUES (@userName, @fullName, @email, @phoneNumber, @address, @role)";
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@userName", userName);
                cmd.Parameters.AddWithValue("@fullName", fullName);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@role", role);
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    isRegistered = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error registering user: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return isRegistered;
        }
        public int GetUserId(string userName)
        {
            int userId = 0;
            string query = "SELECT UserId FROM Users WHERE UserName = @userName";
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@userName", userName);
                userId = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching user ID: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return userId;
        }
        public DataTable GetTreesForUser(int userId)
        {
            DataTable table = new DataTable();
            string query = "SELECT TreeId, Species, Location, HealthStatus, ReminderDate FROM Tree WHERE UserId = @userId";
            try
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(table);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching trees for user: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return table;
        }




        #endregion

        #region Warning
        // Truy vấn để lấy dữ liệu cảnh báo
        public DataTable GetWarnings()
        {
            string query = @"
        SELECT 
            TreeId, 
            Species,
            Location, 
            HealthStatus, 
            ReminderDate,
            CASE 
                WHEN HealthStatus = N'Nguy hiểm' THEN N'Cần xử lý ngay lập tức'
                WHEN HealthStatus = N'Bệnh' THEN N'Kiểm tra và điều trị'
                WHEN HealthStatus = N'Cần tỉa' THEN N'Cần cắt tỉa sớm'
                WHEN HealthStatus = N'Yếu' THEN N'Cần chú ý và chăm sóc thêm'
                WHEN ReminderDate <= GETDATE() THEN N'Nhắc nhở kiểm tra định kỳ'
                ELSE N'Tình trạng ổn định'
            END AS WarningMessage
        FROM Tree
        WHERE HealthStatus IN (N'Nguy hiểm', N'Bệnh', N'Yếu', N'Cần tỉa');";

            DataTable warnings = new DataTable();
            try
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(warnings);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching warnings: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return warnings;
        }
        public DataTable GetUnfinishedServices()
        {
            string query = "SELECT ServiceId, ServiceType, Status, UserId FROM Services WHERE Status != N'Hoàn thành'";
            DataTable services = new DataTable();
            try
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(services);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching unfinished services: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return services;
        }


        #endregion

        #region UserForm
        public bool RequestService(int userId, string serviceRequest)
        {
            bool isSuccessful = false;
            string query = "INSERT INTO ServiceRequests (UserId, Request, RequestDate) VALUES (@userId, @request, @requestDate)";
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@request", serviceRequest);
                cmd.Parameters.AddWithValue("@requestDate", DateTime.Now);
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    isSuccessful = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error requesting service: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return isSuccessful;
        }

        public bool SendReport(int userId, string report)
        {
            bool isSuccessful = false;
            string query = "INSERT INTO Reports (UserId, Report, ReportDate) VALUES (@userId, @report, @reportDate)";
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@report", report);
                cmd.Parameters.AddWithValue("@reportDate", DateTime.Now);
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    isSuccessful = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error sending report: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return isSuccessful;
        }
        #endregion

        #region Report
        public DataTable GetAllReports()
        {
            DataTable table = new DataTable();
            string query = "SELECT ReportId, Annunciator, Description, ReportDate FROM Report";
            try
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching reports: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return table;
        }


        public bool DeleteReport(int reportId)
        {
            bool isSuccessful = false;
            string query = "DELETE FROM Reports WHERE ReportId = @reportId";
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@reportId", reportId);
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    isSuccessful = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting report: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return isSuccessful;
        }
        #endregion

        #region History

        // Phương thức để lấy lịch sử chăm sóc của cây
        public DataTable GetTreeHistory(int treeId)
        {
            DataTable treeHistory = new DataTable();
            try
            {
                connection.Open();
                string query = @"
                SELECT EventType, EventDescription, Date, UserId 
                FROM History 
                WHERE TreeId = @treeId
                ORDER BY Date DESC";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@treeId", treeId);

                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(treeHistory);

                    if (treeHistory == null || treeHistory.Columns.Count == 0)
                    {
                        throw new Exception("Không có dữ liệu lịch sử cây hoặc dữ liệu không hợp lệ.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy lịch sử chăm sóc cây: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return treeHistory;
        }
        public DataTable GetServiceHistory(int serviceId)
        {
            DataTable serviceHistory = new DataTable();
            string query = @"
                SELECT 
                    ActionType, 
                    ActionDescription, 
                    ActionDate, 
                    UserId 
                FROM ServiceHistory 
                WHERE ServiceId = @serviceId
                ORDER BY ActionDate DESC";
            try
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@serviceId", serviceId);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(serviceHistory);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy lịch sử dịch vụ: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return serviceHistory;
        }

        #endregion  
    }
}
