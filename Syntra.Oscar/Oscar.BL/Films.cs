using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oscar.BL
{
    public class Films
    {
        // Data members.
        private Nullable<Guid> filmId;
        private string filmTitle = string.Empty;
        private int releaseYear = 0;
        private int filmLengthInMinutes = 0;
        private Genres filmGenre;
        private double filmRating = -1;
        private int amountOfRatings = 0;
        private string filmPlot = string.Empty;

        /////////////////////////////////////////
        // Access to the data members.
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
        /////////////////////////////////////////
        // Functions.

        // This function uses user input to fill in the properties of a new Films object.
        public void AddNewFilm(string inputOfTitle, int inputOfLength, Genres inputOfGenre)
        {
            filmTitle = inputOfTitle;
            filmLengthInMinutes = inputOfLength;
            filmGenre = inputOfGenre;
        }

        // When a user rates a film, this function will be used to alter the overall rating of a Films object.
        // Also the total amount of ratings will be incremented by 1.
        // The film-, user rating and the total amount of ratings will be used to calculate the new overall rating.
        public void AlterRating(Films film, int usersVote)
        {
            // initialization of variables.
            double newRating = 0;
            double previousRating = film.FilmRating;
            int previousAmountOfVotes = film.AmountOfRatings;
            double previousTotal = 0;

            // Check if there have been ratings before. 
            // If this is the case, the users rating will become the new overall film rating. (if)
            // If it is not the case, then the new overall rating will be calculated. (else)
            if (previousRating == -1)
            {
                newRating = usersVote;
            }
            else
            {
                // Calculations
                previousTotal = previousRating * previousAmountOfVotes;
                newRating = (previousTotal + usersVote) / (previousAmountOfVotes + 1);               
            }

            // Adjust the total amount of ratings and the overall rating of the film.
            film.amountOfRatings++;
            film.filmRating = newRating;
        }
        /////////////////////////////////////////

        // A function to update the general filmrating
        public void UpdateRating(List<Review> allReviewsOfThisFilm)
        {
            int numberOfReviews = allReviewsOfThisFilm.Count;
            int allRatingsCombined = 0;

            foreach (var review in allReviewsOfThisFilm)
            {
                allRatingsCombined = +review.ReviewScore;
            }

            filmRating = allRatingsCombined / numberOfReviews;
        }
    }
}
