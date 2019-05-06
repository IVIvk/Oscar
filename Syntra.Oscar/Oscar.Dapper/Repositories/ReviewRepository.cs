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

        public IEnumerable<Review> GetReviewsPerFilm(Films film)
        {
            using (var connection = new SqlConnection(Connection.Instance.ConnectionString))
            {
                return connection.Query<Review>
                    (@"
                        SELECT Reviews.ReviewId, ReviewContent, ReviewScore, Userid 
                        FROM Reviews
                        INNER JOIN ReviewsForFilms
                        ON Reviews.ReviewId = ReviewsForFilms.ReviewId
                        WHERE ReviewsForFilms.FilmId = @FilmId
                    ", new
                    {
                         FilmId = film.FilmId
                    });
            }
        }

        public void SaveReview(Films film, User user, Review review)
        {
            using (SqlConnection connection = new SqlConnection(Connection.Instance.ConnectionString))
            {
                connection.Execute
                    (@"
                        INSERT INTO Reviews(ReviewId, ReviewContent, ReviewScore, UserId)
                        VALUES (@ReviewId, @ReviewContent, @ReviewScore, @UserId)
                    ", new
                    {
                        ReviewId = review.ReviewId,
                        ReviewContent = review.ReviewContent,
                        ReviewScore = review.ReviewScore,
                        UserId = user.userId
                    });

                connection.Execute
                    (@"
                        INSERT INTO ReviewsForFilms(FilmId, ReviewId)
                        VALUES (@FilmId, @ReviewId)
                    ", new
                    {
                         FilmId = film.FilmId,
                         ReviewId = review.ReviewId
                    });
            }
        }

        public void UpdateReview(Review review)
        {
            using (SqlConnection connection = new SqlConnection(Connection.Instance.ConnectionString))
            {
                connection.Execute
                    (@"
                        UPDATE Reviews
                        SET ReviewContent = @ReviewContent, ReviewScore = @ReviewScore
                        WHERE ReviewId = @ReviewId
                    ", new
                    {
                         ReviewId = review.ReviewId,
                         ReviewContent = review.ReviewContent,
                         ReviewScore = review.ReviewScore
                    });
            }
        }
    }
}
