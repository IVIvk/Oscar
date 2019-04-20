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
            LstUsers.Items.Clear();

            foreach (User user in userlist)
            {
                ListViewItem item = new ListViewItem();

                item.Tag = user;
                item.Content = user.userId;

                LstUsers.Items.Add(item);
            }
        }

        private void btnSaveUsers_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Update(object sender, EventArgs e)
        {
            // Sounds good, doesn't work
            /*
            User user = new User();
            user = (User)dgUsers.SelectedItem;
            DatabaseManager.Instance.UserRepository.UpdateUser(user);
            */
        }

        private void btnShowSelectedUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ListViewItem item = ((ListViewItem)LstUsers.SelectedItem);
                User user = (User)item.Tag;

                txtUserId.Text = user.userId;
                txtPasswordUser.Text = user.UserPassword;

                if (user.UserAdmin)
                {
                    cheUserAdmin.IsChecked = true;
                }
                else
                {
                    cheUserAdmin.IsChecked = false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Geen gebruiker geselecteerd");
            }
          
        }
    }
}
