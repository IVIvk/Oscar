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
            string newPassword = txtPassword.Text;
            string newPasswordConfirm = txtPasswordConfirm.Text;
            bool validUser = true;

            foreach (User user in _userList)
            {
                if (newUsername == user.userName)
                {
                    MessageBox.Show("Deze gebruikersnaam bestaat reeds. Gebruik een andere.");
                    validUser = false;
                }

                if (!(newPassword == newPasswordConfirm))
                {
                    MessageBox.Show("Het wachtwoord komt niet overeen.");
                    validUser = false;
                }
            }

            if (validUser)
            {
                _userList.Add(newUser);
                new DataAccess().SaveUsers(_userList);
            }
        }
    }
}
