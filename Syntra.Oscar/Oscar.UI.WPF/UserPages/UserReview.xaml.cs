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
    /// Interaction logic for UserReview.xaml
    /// </summary>
    public partial class UserReview : Page
    {
        User user = new User();
        List<Review> reviewList = new List<Review>();
        List<Films> filmsList = new List<Films>();

        public UserReview(User userInput)
        {
            InitializeComponent();

            user = userInput;

            filmsList = DatabaseManager.Instance.FilmRepository.GetFilms().ToList();

            ShowReviews();
        }

        public void ShowReviews()
        {
            foreach (var film in filmsList)
            {
                reviewList = DatabaseManager.Instance.ReviewRepository.GetReviewsPerFilm(film).ToList();

                foreach (var review in reviewList)
                {
                    if (review.UserId == user.userId)
                    {
                        ListViewItem item = new ListViewItem();

                        item.Tag = review;
                        item.Content = film.FilmTitle + " (" + review.ReviewScore + "): " + review.ReviewContent;

                        lstUserReviews.Items.Add(item);
                    }
                }
            }
            /*
            foreach (Review review in reviewList)
            {
                string filmTitle = "";
                ListViewItem item = new ListViewItem();

                if (review.UserId == user.userId)
                {
                    item.Tag = review;
                    item.Content = review. + " (" + review.ReviewScore + "): " + review.ReviewContent;

                    lstReviews.Items.Add(item);
                }
            }
            */
        }

        private void LstUserReviews_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListViewItem item = (ListViewItem)lstUserReviews.SelectedItem;
            Review review = (Review)item.Tag;

            txtReview.Text = "";
            txtReview.Text = review.ReviewContent;
        }
    }
}
