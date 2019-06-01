using Oscar.BL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Interaction logic for PageNewUser.xaml
    /// </summary>
    public partial class PageNewUser : Page
    {
        private List<User> _userList = new List<User>();

        public PageNewUser(List<User> userList)
        {
            InitializeComponent();
            _userList = userList;
        }

        private void btnRegisterNewUser_Click(object sender, RoutedEventArgs e)
        {
            User newUser = new User();
            string newUsername = txtUsername.Text;
            string newPassword = txtPassword.Password;
            string newPasswordConfirm = txtPasswordConfirm.Password;
            bool validUser = true;
            
            foreach (User user in _userList)
            {
                if (newUsername == user.userId)
                {
                    MessageBox.Show("Deze gebruikersnaam bestaat reeds. Gebruik een andere.");
                    validUser = false;
                }
            }

            if (newUsername.Length < 5)
            {
                MessageBox.Show("De gebruikersnaam moet minstens 5 karakters lang zijn.");
                validUser = false;
            }
            else if (!(newPassword == newPasswordConfirm))
            {
                MessageBox.Show("Het wachtwoord komt niet overeen.");
                validUser = false;
            }
            else if (newPassword.Length < 5)
            {
                MessageBox.Show("Het wachtwoord moet minstens 5 karakters lang zijn.");
                validUser = false;
            }


            if (validUser)
            {
                newUser.userId = newUsername;
                newUser.UserPassword = newPassword;
                newUser.UserAdminInput = "false";

                _userList.Add(newUser);
                DatabaseManager.Instance.UserRepository.InsertUser(newUser);
                NavigationService.Navigate(new Login());
            }
        }
    }
}
