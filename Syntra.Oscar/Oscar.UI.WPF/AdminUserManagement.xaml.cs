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

        // Button "Ververs gebruikers"
        private void btnLoadUsers_Click(object sender, RoutedEventArgs e)
        {
            LoadAllUsers();
            LstUsers.SelectedIndex = -1;
            txtPasswordUser.Text = string.Empty;
            txtUserId.Text = string.Empty;
            cheUserAdmin.IsChecked = false;
        }

        // Button "Sla gebruikers op"
        private void btnSaveUsers_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();
            try
            {
                // Set the selected UserId and Password.
                user.userId = userlist[LstUsers.SelectedIndex].userId;
                user.UserPassword = txtPasswordUser.Text;

                // Set the Admin rights.
                if (cheUserAdmin.IsChecked == true)
                {
                    user.UserAdminInput = "true";
                }
                else
                {
                    user.UserAdminInput = "false";
                }

                DatabaseManager.Instance.UserRepository.UpdateUser(user);
                LoadAllUsers();

            }
            catch (Exception)
            {
                MessageBox.Show("Geen gebruiker geselecteerd");
            }
        }

        // This function loads all the users from the database into the ListView.
        private void LoadAllUsers()
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

        // SelectionChanged event.
        private void LoadUsers(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ListViewItem item = ((ListViewItem)LstUsers.SelectedItem);
                User user = (User)item.Tag;

                txtUserId.Text = Convert.ToString(user.userId);
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
                //MessageBox.Show("Geen gebruiker geselecteerd");
            }
        }

        // Loaded event.
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadAllUsers();
        }
    }
}
