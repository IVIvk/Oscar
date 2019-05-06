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
        List<Review> listOfReviews = new List<Review>();
        public AllReviewsOfOneFilm(Films film)
        {
            InitializeComponent();
            listOfReviews = DatabaseManager.Instance.ReviewRepository.GetReviewsPerFilm(film).ToList();
            ShowReviews();
        }

        public void ShowReviews()
        {
            foreach (Review review in listOfReviews)
            {
                ListViewItem item = new ListViewItem();

                item.Tag = review;
                item.Content = review.ReviewContent;

                lstReviews.Items.Add(item);
            }
        }
    }
}
