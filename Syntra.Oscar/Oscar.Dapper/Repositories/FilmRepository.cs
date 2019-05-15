using Dapper;
using Oscar.BL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Oscar.Dapper.Repositories
{
    public class FilmRepository
    {
        /////////////////////////////////////////
        // Functions.

        // This function gets all the properties that belong to all the Films objects form the database.
        // It places these properties in a Films object.
        // It places these objects into an IEnumerable "list".
        public IEnumerable<Films> GetFilms()
        {
            using (var connection = new SqlConnection(Connection.Instance.ConnectionString))
            {
                return connection.Query<Films>
                    (@"
                        SELECT FilmId, FilmTitle, ReleaseYear, FilmLengthInMinutes, FilmRating, AmountOfRatings, FilmPlot 
                        FROM Films
                    ");
            }
        }

        // This function inserts the properties of the parameter (Films object) 
        // Into the database as a new entry in the Films table.
        public void InsertFilm(Films film)
        {
            using (var connection = new SqlConnection(Connection.Instance.ConnectionString))
            {
                connection.Execute(@"
                    INSERT INTO Films (FilmId, FilmTitle, ReleaseYear, FilmLengthInMinutes, FilmRating, AmountOfRatings, FilmPlot)
                    VALUES (@FilmId, @FilmTitle, @ReleaseYear, @FilmLengthInMinutes, @FilmRating, @AmountOfRatings, @FilmPlot)
                ", new
                {
                    FilmId = film.FilmId,
                    FilmTitle = film.FilmTitle,
                    ReleaseYear = film.ReleaseYear,
                    FilmLengthInMinutes = film.FilmLengthInMinutes,
                    FilmRating = film.FilmRating,
                    AmountOfRatings = film.AmountOfRatings,
                    FilmPlot = film.FilmPlot
                });
            }
        }

        // This function deletes a film from the database.
        public void DeleteFilm(Films film)
        {
            using (SqlConnection connection = new SqlConnection(Connection.Instance.ConnectionString))
            {
                connection.Execute(@"
                    DELETE FROM Films 
                    WHERE FilmId = @FilmId
                ", new
                {
                    FilmId = film.FilmId
                });
            }
        }

        // This function updates the Film properties inside the database.
        public void UpdateFilm(Films film)
        {
            using (SqlConnection connection = new SqlConnection(Connection.Instance.ConnectionString))
            {
                connection.Execute(@"
                    UPDATE Films
                    SET FilmTitle = @FilmTitle, 
                        FilmPlot = @FilmPlot, 
                        ReleaseYear = @ReleaseYear, 
                        FilmLengthInMinutes = @FilmLengthInMinutes
                    WHERE FilmId = @FilmId
                    ", new
                {
                    FilmTitle = film.FilmTitle,
                    FilmPlot = film.FilmPlot,
                    FilmId = film.FilmId,
                    ReleaseYear = film.ReleaseYear,
                    FilmLengthInMinutes = film.FilmLengthInMinutes
                });
            }
        }

        // This function will return an IEnumerable filled with the genres associated with a film.
        public IEnumerable<Genres> GetGenreNameForFilm(Films film)
        {
            using (var connection = new SqlConnection(Connection.Instance.ConnectionString))
            {
                return connection.Query<Genres>(@"
                        SELECT GenreName
                        FROM Genres
                        INNER JOIN GenresInFilms
                            ON Genres.GenreId = GenresInFilms.GenreId
                        INNER JOIN Films
                            ON GenresInFilms.FilmId = Films.FilmId
                        WHERE FilmId = @FilmId
                        ", new
                {
                    FilmId = film.FilmId
                });
            }

        }        

        // Get the films of a genre. 
        // The below query returns al the films of the input genre.
        //SELECT Films.FilmTitle, Genres.GenreName
        //FROM Films
        //INNER JOIN GenresInFilms
        //ON GenresInFilms.FilmId = Films.FilmId
        //INNER JOIN Genres
        //ON Genres.GenreId = GenresInFilms.GenreId
        //WHERE Genres.GenreId = INPUT
    }
}
