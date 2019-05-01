using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using Oscar.BL;

namespace Oscar.Dapper.Repositories
{
    public class ReviewRepository
    {
        // SeLect Reviews per User
        public IEnumerable<Review> GetReviewsPerUser(User user)
        {
            using (var connection = new SqlConnection(Connection.Instance.ConnectionString))
            {
                return connection.Query<Review>
                    (@" 
                        SELECT ReviewId, ReviewContent, ReviewScore, Userid
                        FROM Reviews
                        WHERE UserId = @UserId
                    ", new
                    {
                         Userid = user.userId
                    });
            }
        }
    }
}
