using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oscar.BL
{
    public class Genres
    {
        // Data members.
        private Guid genreId;
        private string genreName = string.Empty;

        // Access to the data members.
        public Guid GenreId
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
        public string GenreName
        {
            get { return genreName; }
            //set { genreName = value; }
        }

        public void AddNewGenre(string InputGenreName)
        {
            genreName = InputGenreName;
        }
    }
}
