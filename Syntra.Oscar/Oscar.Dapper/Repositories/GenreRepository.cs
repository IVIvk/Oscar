using Dapper;
using Oscar.BL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Oscar.Dapper.Repositories
{
    public class GenreRepository
    {
        public IEnumerable<Genres> GetGenres()
        {
            using (var connection = new SqlConnection(Connection.Instance.ConnectionString))
            {
                return connection.Query<Genres>
                    (@"
                    SELECT GenreId, GenreName
                    FROM Genres
                    ");
            }
        }

        public void InsertGenre(Genres genre)
        {
            using (SqlConnection connection = new SqlConnection(Connection.Instance.ConnectionString))
            {
                connection.Execute
                    (@"
                        INSERT INTO Genres(GenreId, GenreName)
                        VALUES (@GenreId, @GenreName)                        
                        ", new
                    {
                        GenreId = genre.GenreId,
                        GenreName = genre.GenreName
                    });
            }
        }
    }
}
