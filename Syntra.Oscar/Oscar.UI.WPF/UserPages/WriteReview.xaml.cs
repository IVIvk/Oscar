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
        List<Review> reviewListUser = new List<Review>();
        List<Review> reviewListFilm = new List<Review>();
        List<Genres> genresOfFilm = new List<Genres>();

        Films selectedFilm = new Films();
        bool newReview = true;
        Review userReview = new Review();
        User currentUser = new User();

        public WriteReview(Films film, User user)
        {
            InitializeComponent();
            selectedFilm = film;
            currentUser = user;

            reviewListUser = DatabaseManager.Instance.ReviewRepository.GetReviewsPerUser(user).ToList();
            reviewListFilm = DatabaseManager.Instance.ReviewRepository.GetReviewsPerFilm(film).ToList();
            genresOfFilm = DatabaseManager.Instance.FilmRepository.GetGenresForFilm(film.FilmId.Value).ToList();

            foreach (var reviewUser in reviewListUser)
            {
                if (reviewUser.UserId == user.userId)
                {
                    foreach (var reviewFilm in reviewListFilm)
                    {
                        if (reviewFilm.UserId == reviewUser.UserId)
                        {
                            userReview = reviewUser;
                            newReview = false;
                        }
                    }
                }
                else
                {
                    userReview.ReviewContent = "Je hebt nog geen review hiervoor geschreven";
                }
            }

            txtFilmTitle.Text = film.FilmTitle;
            txtFilmReleaseYear.Text = Convert.ToString(film.ReleaseYear);
            txtFilmDuration.Text = Convert.ToString(film.FilmLengthInMinutes);
            txtReview.Text = userReview.ReviewContent;
            txtFilmPlot.Text = Convert.ToString(film.FilmPlot);
            cbbScore.Text = Convert.ToString(userReview.ReviewScore);
            txtFilmGenre.Text = "";

            foreach (var genre in genresOfFilm)
            {
                txtFilmGenre.Text = txtFilmGenre.Text + " " + genre.GenreName;
            }
        }

        private void BtnSaveReview_Click(object sender, RoutedEventArgs e)
        {
            userReview.ReviewContent = txtReview.Text;
            userReview.ReviewScore = Convert.ToInt32(((ComboBoxItem)cbbScore.SelectedItem).Content);
            userReview.UserId = currentUser.userId;

            if (newReview)
            {
                userReview.ReviewId = Guid.NewGuid();
                DatabaseManager.Instance.ReviewRepository.SaveReview(selectedFilm, currentUser, userReview);
                newReview = false;
            }
            else
            {
                DatabaseManager.Instance.ReviewRepository.UpdateReview(userReview);
            }

            NavigationService.Navigate(new FilmOverview(currentUser));
        }
    }
}
