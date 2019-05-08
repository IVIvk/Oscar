using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oscar.BL;

namespace Oscar.UI.WPF.SingletonClasses
{
    public class SingletonFilms
    {
        // Singleton class.
        //
        /////////////////////////////////
        // Data members.
        private static readonly SingletonFilms _film = new SingletonFilms();

        private Nullable<Guid> filmId;
        private string filmTitle = string.Empty;
        private int releaseYear = 0;
        private int filmLengthInMinutes = 0;
        private Genres filmGenre;
        private double filmRating = -1;
        private int amountOfRatings = 0;
        private string filmPlot = string.Empty;

        /////////////////////////////////
        // Properties.        
        public Nullable<Guid> FilmId
        {
            get { return filmId; }
            set { filmId = value; }
        }

        public string FilmTitle
        {
            get { return filmTitle; }
            set { filmTitle = value; }
        }

        public int ReleaseYear
        {
            get { return releaseYear; }
            set { releaseYear = value; }
        }
        
        public int FilmLengthInMinutes
        {
            get { return filmLengthInMinutes; }
            set { filmLengthInMinutes = value; }
        }
        
        public Genres FilmGenre
        {
            get { return filmGenre; }
            set { filmGenre = value; }
        }
        
        public double FilmRating
        {
            get { return filmRating; }
            set { filmRating = value; }
        }
        public int AmountOfRatings
        {
            get { return amountOfRatings; }
            set { amountOfRatings = value; }
        }
        
        public string FilmPlot
        {
            get { return filmPlot; }
            set { filmPlot = value; }
        }

        /////////////////////////////////
        // Singleton stuff. 
        public static SingletonFilms OnlyInstanceOfFilms
        {
            get { return _film; }
        }

        private SingletonFilms()
        {

        }
        
    }
}
