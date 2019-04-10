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
        //Example of a query. User database need to be set up.
        /*
        public IEnumerable<User> GetUsers()
        {
            using (var connection = new SqlConnection(Connection.Instance.ConnectionString))
            {
                return connection.Query<User>(@"
                    SELECT Id, Name, Age
                    FROM Users
                ");
            }
        }
        */
    }
}
