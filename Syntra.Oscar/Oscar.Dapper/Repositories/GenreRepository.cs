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

        // This function returns the GenreId of a Genres object.
        public IEnumerable<Genres> GetGenreId(Genres genre)
        {
            using (var connection = new SqlConnection(Connection.Instance.ConnectionString))
            {
                return connection.Query<Genres>
                    (@"
                    SELECT GenreId
                    FROM Genres
                    ");
            }
        }

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

        public void DeleteGenre(Genres genre)
        {
            using (SqlConnection connection = new SqlConnection(Connection.Instance.ConnectionString))
            {
                connection.Execute
                    (@"
                        DELETE FROM Genres
                        WHERE GenreId = @GenreId
                        ", new
                    {
                        GenreId = genre.GenreId
                    });
            }
        }

        // This function returns a IEnumerable list with all links between films and genres.
        public IEnumerable<GenresInFilms> GetGenresInFilms()
        {
            using (var connection = new SqlConnection(Connection.Instance.ConnectionString))
            {
                return connection.Query<GenresInFilms>
                    (@"
                        SELECT GenreId, FilmId
                        FROM GenresInFilms
                    ");
            }
        }

        // This function inserts a link between a film and a genre inside the GenresInFIlms table.
        public void InsertLinkGenreAndFilm(Genres genre, Films film)
        {
            using (SqlConnection connection = new SqlConnection(Connection.Instance.ConnectionString))
            {
                connection.Execute
                    (@"
                        INSERT INTO GenresInFilms(GenreId, FilmId)
                        VALUES (@GenreId, @FilmId)                      
                        ", new
                    {
                        GenreId = genre.GenreId,
                        FilmId = film.FilmId
                    });
            }
        }

        // This function deletes the links between a film and its genres inside the GenresInFilms table.
        public void DeleteLinkGenreAndFilm(Films film)
        {
            using (SqlConnection connection = new SqlConnection(Connection.Instance.ConnectionString))
            {
                connection.Execute
                    (@"
                        DELETE FROM GenresInFilms
                        WHERE FilmId = @FilmId
                        ", new
                    {
                        FilmId = film.FilmId
                    });
            }
        }

        // This function updates the Genre name.
        public void UpdateGenre(Genres genre)
        {
            using (SqlConnection connection = new SqlConnection(Connection.Instance.ConnectionString))
            {
                connection.Execute(@"
                    UPDATE Genres 
                    SET GenreName = @GenreName 
                    WHERE GenreId = @GenreId
                    ", new
                {
                    GenreId = genre.GenreId,
                    GenreName = genre.GenreName
                });
            }
        }
    }
}
