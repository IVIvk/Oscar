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

namespace Oscar.UI.WPF.Pages
{
    /// <summary>
    /// Interaction logic for EditGenre.xaml
    /// </summary>
    public partial class EditGenre : Page
    {
        public EditGenre()
        {
            InitializeComponent();
        }

        // Functions.
        // This function fills in the text boxes with the properties inside the singleton object.
        private void FillTextBoxes()
        {
            txtGenre.Text = SingletonClasses.SingletonGenre.OnlyInstanceOfGenre.GenreName;
        }

        private void BtnEditGenre_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminGenreManagement());

        }

        private void LoadedEditGenre(object sender, RoutedEventArgs e)
        {
            // Fill the text boxes with the properties inside the singleton object.
            FillTextBoxes();            
        }
    }
}
