using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oscar.BL
{
    public class GenresInFilms
    {
        private Nullable<Guid> genreId;
        private Nullable<Guid> filmId;

        public Nullable<Guid> GenreId
        {
            get { return genreId; }
            set
            {
                if (genreId == null)
                {
                    genreId = value;
                }
            }
        }

        public Nullable<Guid> FilmId
        {
            get { return filmId; }
            set
            {
                if (filmId == null)
                {
                    filmId = value;
                }
            }
        }
    }
}
