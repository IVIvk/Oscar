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

namespace Oscar.UI.WPF.Pages
{
    /// <summary>
    /// Interaction logic for AdminFilmsManagement.xaml
    /// </summary>
    public partial class AdminFilmsManagement : Page
    {
        // List to hold all the Films objects.
        List<Films> filmsList = new List<Films>();

        public AdminFilmsManagement()
        {
            InitializeComponent();
        }

        #region Functions
        /////////////////////////////////////////
        // Functions

        // This function clears all the text boxes on this page.
        private void ClearTextBoxes()
        {
            txtFilmId.Text = string.Empty;
            txtFilmTitle.Text = string.Empty;
            txtFilmReleaseYear.Text = string.Empty;
            txtFilmGenre.Text = string.Empty;
            txtFilmDuration.Text = string.Empty;
            txtFilmPlot.Text = string.Empty;
        }

        // This function Adds all the films inside the database into the ListView.
        private void ShowFilms()
        {
            filmsList = DatabaseManager.Instance.FilmRepository.GetFilms().ToList();
            LstFilms.Items.Clear();

            foreach (Films film in filmsList)
            {
                ListViewItem item = new ListViewItem();

                item.Tag = film;
                item.Content = film.FilmTitle;

                LstFilms.Items.Add(item);
            }
            ClearTextBoxes();
        }

        // This function loads the properties of the selected film into the correct text boxes.
        private void LoadFilms(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ListViewItem item = ((ListViewItem)LstFilms.SelectedItem);
                Films film = (Films)item.Tag;

                txtFilmId.Text = Convert.ToString(film.FilmId);
                txtFilmTitle.Text = Convert.ToString(film.FilmTitle);
                txtFilmReleaseYear.Text = Convert.ToString(film.ReleaseYear);
                // Genres requires some more SQL queries before it can be shown.
                //txtFilmGenre.Text = Convert.ToString(film.FilmGenre);
                txtFilmDuration.Text = Convert.ToString(film.FilmLengthInMinutes);
                txtFilmPlot.Text = Convert.ToString(film.FilmPlot);

            }
            catch (Exception)
            {

            }
        }
        #endregion
        
        #region Buttons
        /////////////////////////////////////////
        // Buttons (Click functions)

        // "Laad films" button.
        private void BtnLoadFilms_Click(object sender, RoutedEventArgs e)
        {
            ShowFilms();
        }

        // "Nieuwe film" button.
        private void BtnNewFilm_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditFilm());
        }

        // "Verander film" button.
        private void BtnEditFilm_Click(object sender, RoutedEventArgs e)
        {

        }

        // "Verwijder film" button.
        private void BtnDeleteFilm_Click(object sender, RoutedEventArgs e)
        {
            // Create new Films object.
            Films film = new Films();

            // Get the selected item in the list.
            ListViewItem item = ((ListViewItem)LstFilms.SelectedItem);

            // Place this item into the object.
            film = (Films)item.Tag;

            // Initiate delete in database of the selected item.
            DatabaseManager.Instance.FilmRepository.DeleteFilm(film);

            // Clear out the text boxes.
            ClearTextBoxes();
            
            // Refresh the list of films.
            ShowFilms();
        }        
        #endregion

        /////////////////////////////////////////
        // Loaded event
        #region LoadedEvent
        private void AdminFilmsManagementLoaded(object sender, RoutedEventArgs e)
        {
            ShowFilms();            
        }
        #endregion


    }
}
