using Dapper;
using Oscar.BL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Oscar.Dapper.Repositories
{
    public class UserRepository
    {
        public IEnumerable<User> GetUsers()
        {
            using (var connection = new SqlConnection(Connection.Instance.ConnectionString))
            {
                return connection.Query<User>(@"
                    SELECT UserId, UserPassword, UserAdmin
                    FROM Users
                ");
            }
        }

        public void InsertUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(Connection.Instance.ConnectionString))
            {
                connection.Execute(@"
                    INSERT INTO Users(UserId, UserPassword, UserAdmin)
                    VALUES (@UserId, @UserPassword, @UserAdmin)
                ", new
                {
                    UserId = user.userId,
                    UserPassword = user.UserPassword,
                    UserAdmin = user.UserAdmin
                });
            }
        }
    }
}
