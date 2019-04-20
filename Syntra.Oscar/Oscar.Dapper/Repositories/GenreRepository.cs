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
        /////////////////////////////////////////
        // Functions.

        // This function gets all the properties that belong to all the Genres objects form the database.
        // It places these properties in an Genres object.
        // It places these objects into an IEnumerable "list".
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

        // This function inserts the properties of the parameter (Genres object) 
        // Into the database as a new entry in the Genres table.
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
