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

namespace Oscar.UI.WPF
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Page
    {
        User mainUser = new User();
        public UserWindow(User user)
        {
            InitializeComponent();
            mainUser = user;

            if (!mainUser.UserAdmin)
            {
                btnAdmin.Visibility = Visibility.Hidden;
            }
        }

        private void BtnAdmin_Click(object sender, RoutedEventArgs e)
        {
            frmUserFrame.NavigationService.Navigate(new AdminOverview(mainUser));
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Login());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            frmUserFrame.NavigationService.Navigate(new UserPages.FilmOverview(mainUser));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            frmUserFrame.NavigationService.Navigate(new Pages.UlaInterface());
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            frmUserFrame.NavigationService.Navigate(new UserPages.UserReview(mainUser));
        }
    }
}
