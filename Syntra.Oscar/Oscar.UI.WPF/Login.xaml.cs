using Oscar.BL;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        List<User> userList;

        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            bool check = true;

            foreach (User user in userList)
            {
                if (username == user.userName)
                {
                    if (password == user.passWord)
                    {
                        check = false;
                    }
                }
            }

            if (check)
            {
                MessageBox.Show("Gebruikersnaam of wachtwoord is niet correct");
            }
            else
            {
                //NavigationService.Navigate();
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            string userFile = @"c:\temp\usersOscar.txt";
            userList = new List<User>();

            bool fileExist = File.Exists(userFile);

            if (fileExist) // Checking if userfile exists. If not, creating one with admin-user in it.
            {
                userList = new DataAccess().LoadUsers(userFile);
            }
            else
            {
                string defaultText = @"Admin/AdminPassword/true";
                StreamWriter file = new StreamWriter(File.Create(userFile));
                file.Close();

                using (StreamWriter sw = new StreamWriter(userFile))
                {
                    sw.WriteLine(defaultText);
                }
            }
        }

        private void btnNewUser_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageNewUser(userList));
        }
    }
}
