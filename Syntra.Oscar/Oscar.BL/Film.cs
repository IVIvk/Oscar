using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oscar.BL
{
    internal class Film
    {
        // Data members.
        private string filmTitle = string.Empty;
        private int filmLengthInMinutes = 0;
        private Genre filmGenre;
        private int filmRating = 0;

        // Access to the data members.
        public string FilmTitle
        {
            get { return filmTitle; }
            set { filmTitle = value; }
        }

        public int FilmLengthInMinutes
        {
            get { return filmLengthInMinutes; }
            set { filmLengthInMinutes = value; }
        }

        public Genre FilmGenre
        {
            get { return filmGenre; }
            set { filmGenre = value; }
        }

        public int FilmRating
        {
            get { return filmRating; }
            set { filmRating = value; }
        }

        // Functions.

        // This function uses user input to fill in the properties of a new film.
        public void AddNewFilm(string inputOfTitle, int inputOfLength, Genre inputOfGenre)
        {
            filmTitle = inputOfTitle;
            filmLengthInMinutes = inputOfLength;
            filmGenre = inputOfGenre;
        }

        // 
    }
}
