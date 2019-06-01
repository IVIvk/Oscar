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
    /// Interaction logic for AllReviewsOfOneFilm.xaml
    /// </summary>
    public partial class AllReviewsOfOneFilm : Page
    {
        private List<Review> listOfReviews = new List<Review>();
        private List<User> listOfUsers = new List<User>();
        public AllReviewsOfOneFilm(Films film)
        {
            InitializeComponent();

            listOfReviews = DatabaseManager.Instance.ReviewRepository.GetReviewsPerFilm(film).ToList();
            listOfUsers = DatabaseManager.Instance.UserRepository.GetUsers().ToList();

            ShowReviews();
        }

        public void ShowReviews()
        {
            foreach (Review review in listOfReviews)
            {
                string username = "";
                ListViewItem item = new ListViewItem();

                foreach (var user in listOfUsers)
                {
                    if(user.userId == review.UserId)
                    {
                        username = user.userId;
                    }
                }

                item.Tag = review;
                item.Content = username + " (" + review.ReviewScore + "): " + review.ReviewContent;

                lstReviews.Items.Add(item);
            }
        }

        private void LstReviews_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListViewItem item = (ListViewItem)lstReviews.SelectedItem;
            Review review = (Review)item.Tag;

            txtSelectedReview.Text = "";
            txtSelectedReview.Text = review.ReviewContent;
        }
    }
}
