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
        public IEnumerable<Films> GetFilms()
        {
            using (var connection = new SqlConnection(Connection.Instance.ConnectionString))
            {
                return connection.Query<Films>
                    (@"
                        SELECT FilmId, FilmTitle, ReleaseYear, FilmLenghtInMinutes, FilmRating, AmountOfRatings 
                        FROM Films
                    ");
            }
        }

        public void InsertFilm(Films film)
        {
            using (var connection = new SqlConnection(Connection.Instance.ConnectionString))
            {
                connection.Execute(@"
                    INSERT INTO Films (FilmId, FilmTitle, ReleaseYear, FilmLenghtInMinutes, FilmRating, AmountOfRatings)
                    VALUES (@FilmId, @FilmTitle, @ReleaseYear, @FilmLenghtInMinutes, @FilmRating, @AmountOfRatings)
                ", new
                {
                    FilmId = film.FilmId,
                    FilmTitle = film.FilmTitle,
                    ReleaseYear = film.ReleaseYear,
                    FilmLenghtInMinutes = film.FilmLengthInMinutes,
                    FilmRating = film.FilmRating,
                    AmountOfRatings = film.AmountOfRatings
                });
            }
        }
    }
}
