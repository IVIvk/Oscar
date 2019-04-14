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
        }

        private void BtnAdmin_Click(object sender, RoutedEventArgs e)
        {
            if (!mainUser.UserAdmin)
            {
                MessageBox.Show("U heeft hierop geen rechten");
            }
        }
    }
}
