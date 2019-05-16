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
    /// Interaction logic for EditFilm.xaml
    /// </summary>
    public partial class EditFilm : Page
    {
        public EditFilm()
        {
            InitializeComponent();
        }
        // Variable.
        bool allTextBoxesFilled = false;
        List<Genres> GenresList = new List<Genres>();

        #region Buttons
        //////////////////////////////////
        // Butons.

        // Button "Toevoegen".
        private void BtnAddFilm_Click(object sender, RoutedEventArgs e)
        {
            // Make sure that the text boxes are filled in.
            allTextBoxesFilled = CheckNoEmptyTextBoxes();

            if (allTextBoxesFilled != false)
            {
                // Create new instance of Films.
                Films film = new Films();

                // Load all the text into this instance.
                film.FilmId = Guid.NewGuid();
                film.FilmTitle = txtFilmTitle.Text;
                film.ReleaseYear = Convert.ToInt32(txtReleaseYear.Text);
                film.FilmLengthInMinutes = Convert.ToInt32(txtDuration.Text);
                film.FilmPlot = txtPlot.Text;
                // Genre still needs some attention.
                //film.FilmGenre = txtGenre.Text;

                // Insert the new film into the database.
                DatabaseManager.Instance.FilmRepository.InsertFilm(film);

                // Navigate back to the AdminFilmsManagement page.
                NavigationService.Navigate(new Pages.AdminFilmsManagement());
            }
        }

        // Button "Veranderen".
        private void BtnEditFilm_Click(object sender, RoutedEventArgs e)
        {
            // Make sure that the text boxes are filled in.
            allTextBoxesFilled = CheckNoEmptyTextBoxes();

            if (allTextBoxesFilled != false)
            {
                // Create new instance of Films.
                Films film = new Films();

                // Load all the text into this instance.
                film.FilmId = SingletonClasses.SingletonFilms.OnlyInstanceOfFilms.FilmId;
                film.FilmTitle = txtFilmTitle.Text;
                film.ReleaseYear = Convert.ToInt32(txtReleaseYear.Text);
                film.FilmLengthInMinutes = Convert.ToInt32(txtDuration.Text);
                film.FilmPlot = txtPlot.Text;
                // Genre still needs some attention.
                //film.FilmGenre = txtGenre.Text;

                // Edit the film data in the database.
                DatabaseManager.Instance.FilmRepository.UpdateFilm(film);

                // Navigate back to the AdminFilmsManagement page.
                NavigationService.Navigate(new Pages.AdminFilmsManagement());
            }
        }

        // Button "Annuleren".
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.AdminFilmsManagement());
        }
        #endregion

        #region Functions
        //////////////////////////////////
        // Functions.

        // This function clears out all the text boxes.
        private void ClearTextBoxes()
        {
            txtFilmTitle.Text = string.Empty;
            txtReleaseYear.Text = string.Empty;
            txtDuration.Text = string.Empty;
            txtPlot.Text = string.Empty;

        }

        // This function fills in the text boxes with the properties inside the singleton object.
        private void FillTextBoxes()
        {
            txtFilmTitle.Text = SingletonClasses.SingletonFilms.OnlyInstanceOfFilms.FilmTitle;
            txtReleaseYear.Text = Convert.ToString(SingletonClasses.SingletonFilms.OnlyInstanceOfFilms.ReleaseYear);
            txtDuration.Text = Convert.ToString(SingletonClasses.SingletonFilms.OnlyInstanceOfFilms.FilmLengthInMinutes);
            txtPlot.Text = SingletonClasses.SingletonFilms.OnlyInstanceOfFilms.FilmPlot;

        }

        // This function checks that all text boxes are filled in.
        private bool CheckNoEmptyTextBoxes()
        {
            // Initialize variables.
            bool succes = false;
            bool isNotFilled = false;
            string content = string.Empty;

            // Check that film title is filled in.
            content = txtFilmTitle.Text;
            isNotFilled = string.IsNullOrWhiteSpace(content);
            if (isNotFilled != true)
            {
                // Check that the plot is filled in.
                content = txtPlot.Text;
                isNotFilled = string.IsNullOrWhiteSpace(content);
                if (isNotFilled != true)
                {
                    // Check that the release year is filled in.
                    content = txtReleaseYear.Text;
                    isNotFilled = string.IsNullOrWhiteSpace(content);
                    if (isNotFilled != true)
                    {
                        // Check that the duration is filled in.
                        content = txtDuration.Text;
                        isNotFilled = string.IsNullOrWhiteSpace(content);
                        if (isNotFilled != true)
                        {
                            // Check that the genre is filled in.
                            //content = txtGenre.Text;
                            //isNotFilled = string.IsNullOrWhiteSpace(content);
                            //if (isNotFilled != true)
                            //{
                            succes = true;
                            //}
                        }
                    }
                }
            }
            return succes;
        }

        // This function Gets the genres from the database and puts them inside the genre ComboBox.
        // The user can choose the genre from this ComboBox.
        private void FillGenreComboBox()
        {
            GenresList = DatabaseManager.Instance.GenreRepository.GetGenres().ToList();

            foreach (Genres genre in GenresList)
            {
                ListViewItem item = new ListViewItem();

                item.Tag = genre;
                item.Content = genre.GenreName.ToString();

                cmbGenre.Items.Add(item);
            }
        }
        #endregion

        #region Loaded event
        //////////////////////////////////
        // Loaded event.
        //
        // Depending on how the user navigates to this page, some different stuff happens.
        // EDIT) If the user chose edit film, the films properties will be loaded inside the text boxes.
        //       The Add button will be disabled.
        // ADD) If the user chose Add film, the text boxes will be emptied.
        //      The Edit button will be greyed out.
        private void LoadedEditFilm(object sender, RoutedEventArgs e)
        {
            if (SingletonClasses.SingletonFilms.OnlyInstanceOfFilms.FilmTitle != string.Empty)
            {
                // Disable the add button and enable the Edit button.
                btnAddFilm.IsEnabled = false;
                btnEditFilm.IsEnabled = true;

                // Fill the text boxes with the properties inside the singleton object.
                FillTextBoxes();
            }
            else
            {
                // Enable the add button and disable the Edit button.
                btnAddFilm.IsEnabled = true;
                btnEditFilm.IsEnabled = false;

                // Clear the text boxes.
                ClearTextBoxes();
            }
            FillGenreComboBox();
        }
        #endregion
    }
}