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
        public WriteReview(Films film, User user)
        {
            InitializeComponent();
            selectedFilm = film;
            reviewList = DatabaseManager.Instance.ReviewRepository.GetReviewsPerUser(user).ToList();
        }
    }
}
