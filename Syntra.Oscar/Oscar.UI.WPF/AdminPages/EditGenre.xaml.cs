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
using Oscar.BL;

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
        // Variable.
        bool allTextBoxesFilled = false;

        #region Functions
        //////////////////////////////////
        // Functions.
        //
        // This function fills in the text boxes with the properties inside the singleton object.
        private void FillTextBoxes()
        {
            txtGenre.Text = SingletonClasses.SingletonGenre.OnlyInstanceOfGenre.GenreName;
        }

        // This function checks that all text boxes are filled in.
        private bool CheckNoEmptyTextBoxes()
        {
            // Initialize variables.
            bool succes = false;
            bool isNotFilled = false;
            string content = string.Empty;

            // Check that film title is filled in.
            content = txtGenre.Text;
            isNotFilled = string.IsNullOrWhiteSpace(content);
            if (isNotFilled != true)
            {
                succes = true;                
            }
            return succes;
        }
        #endregion

        #region Buttons
        //////////////////////////////////
        // Buttons.
        //
        // "Aanpassen" button.
        private void BtnEditGenre_Click(object sender, RoutedEventArgs e)
        {
            // Make sure that the text boxes are filled in.
            allTextBoxesFilled = CheckNoEmptyTextBoxes();

            if (allTextBoxesFilled != false)
            {
                // Create new instance of Genres.
                Genres genre = new Genres();

                // Load all the text into this instance.
                genre.GenreId = SingletonClasses.SingletonGenre.OnlyInstanceOfGenre.GenreId;
                genre.GenreName = txtGenre.Text;

                // Edit the genre data in the database.
                DatabaseManager.Instance.GenreRepository.UpdateGenre(genre);

                // Navigate back to the AdminGenreManagement page.
                NavigationService.Navigate(new AdminGenreManagement());
            }
        }

        // "Annuleren" button.
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminGenreManagement());

        }
        #endregion

        #region Events
        //////////////////////////////////
        // Events.
        //
        // (Loaded)
        private void LoadedEditGenre(object sender, RoutedEventArgs e)
        {
            // Fill the text boxes with the properties inside the singleton object.
            FillTextBoxes();
        }
        #endregion
    }
}
