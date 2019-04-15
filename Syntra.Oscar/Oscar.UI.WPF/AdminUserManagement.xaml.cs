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
    /// Interaction logic for AdminUserManagement.xaml
    /// </summary>
    public partial class AdminUserManagement : Page
    {
        List<User> userlist = new List<User>();
        public AdminUserManagement()
        {
            InitializeComponent();
        }

        private void btnLoadUsers_Click(object sender, RoutedEventArgs e)
        {
            userlist = DatabaseManager.Instance.UserRepository.GetUsers().ToList();
            dgUsers.ItemsSource = userlist;
        }

        private void btnSaveUsers_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Update(object sender, EventArgs e)
        {
            User user = new User();
            user = (User)dgUsers.SelectedItem;
            //Call an instance of DatabaseManager
            //Make in the UserRepository a method to update users
            //Update users
        }
    }
}
