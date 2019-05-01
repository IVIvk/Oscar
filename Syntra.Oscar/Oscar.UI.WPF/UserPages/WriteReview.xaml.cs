using Oscar.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Oscar.UI.WPF.UserPages
{
    /// <summary>
    /// Interaction logic for WriteReview.xaml
    /// </summary>
    public partial class WriteReview : Page
    {
        List<Review> reviewList = new List<Review>();
        Films selectedFilm = new Films();
        string userReviewText;
        bool newReview = true;
        Review userReview = new Review();
        User currentUser = new User();

        public WriteReview(Films film, User user)
        {
            InitializeComponent();
            selectedFilm = film;
            currentUser = user;
            reviewList = DatabaseManager.Instance.ReviewRepository.GetReviewsPerUser(user).ToList();

            foreach (var review in reviewList)
            {
                if (review.UserId == user.userId)
                {
                    userReview = review;
                    newReview = false;
                }
                else
                {
                    userReviewText = "Je hebt nog geen review hiervoor geschreven";
                }
            }

            txtFilmTitle.Text = film.FilmTitle;
            txtFilmReleaseYear.Text = Convert.ToString(film.ReleaseYear);
            txtFilmDuration.Text = Convert.ToString(film.FilmLengthInMinutes);
            txtReview.Text = userReview.ReviewContent;
            
        }

        private void BtnSaveReview_Click(object sender, RoutedEventArgs e)
        {
            userReview.ReviewContent = txtReview.Text;
            userReview.ReviewScore = (int)cbbScore.SelectedItem;
            userReview.UserId = currentUser.userId;

            if (!newReview)
            {
                userReview.ReviewId = new Guid();
                DatabaseManager.Instance.ReviewRepository.SaveReview(selectedFilm, currentUser, userReview);
            }
        }
    }
}
