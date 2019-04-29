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
using Oscar.UI.WPF.Pages;

namespace Oscar.UI.WPF
{
    /// <summary>
    /// Interaction logic for AdminOverview.xaml
    /// </summary>
    public partial class AdminOverview : Page
    {
        public AdminOverview()
        {
            InitializeComponent();
        }

        private void BtnUsersOverview_Click(object sender, RoutedEventArgs e)
        {
            frmOverviewFrame.NavigationService.Navigate(new AdminUserManagement());
        }

        private void BtnFilmsOverview_Click(object sender, RoutedEventArgs e)
        {
            frmOverviewFrame.NavigationService.Navigate(new AdminFilmsManagement());
        }

        private void BtnActorsOverview_Click(object sender, RoutedEventArgs e)
        {
            frmOverviewFrame.NavigationService.Navigate(new AdminActorsManagement());
        }

        private void BtnGenresOverview_Click(object sender, RoutedEventArgs e)
        {
            frmOverviewFrame.NavigationService.Navigate(new AdminGenreManagement());
        }
    }
}
