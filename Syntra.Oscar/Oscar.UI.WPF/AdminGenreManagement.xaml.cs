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
    /// Interaction logic for AdminGenreManagement.xaml
    /// </summary>
    public partial class AdminGenreManagement : Page
    {
        List<Genres> GenresList = new List<Genres>();

        public AdminGenreManagement()
        {
            InitializeComponent();
        }

        private void Genre_OnLoaded(object sender, RoutedEventArgs e)
        {
            ShowGenres();
        }

        #region Functions
        /////////////////////////////////
        // Functions.

        // This function loads the genres from the database into the listview.
        private void ShowGenres()
        {
            GenresList = DatabaseManager.Instance.GenreRepository.GetGenres().ToList();

            lstGenres.Items.Clear();

            foreach (Genres genre in GenresList)
            {
                ListViewItem item = new ListViewItem();

                item.Tag = genre;
                item.Content = genre.GenreName.ToString();

                lstGenres.Items.Add(item);
            }
        }


        // This function loads the selected genre properties into the singleton class.
        private void LoadGenreIntoSingleton()
        {
            try
            {
                ListViewItem item = ((ListViewItem)lstGenres.SelectedItem);
                Genres genre = (Genres)item.Tag;

                SingletonClasses.SingletonGenre.OnlyInstanceOfGenre.GenreId = genre.GenreId;
                SingletonClasses.SingletonGenre.OnlyInstanceOfGenre.GenreName = genre.GenreName;
            }
            catch (Exception)
            {

            }
        }
        #endregion

        #region Buttons
        /////////////////////////////////
        // Buttons.

        // "Genre toevoegen" button.
        private void BtnAddGenre_Click(object sender, RoutedEventArgs e)
        {
            Genres newGenre = new Genres();

            newGenre.GenreId = Guid.NewGuid();
            newGenre.GenreName = txtNewGenreInput.Text;

            txtNewGenreInput.Text = "Nieuwe genre";

            DatabaseManager.Instance.GenreRepository.InsertGenre(newGenre);
            MessageBox.Show("Nieuwe genre toegevoegd");

            ShowGenres();
        }

        // "Verwijder genre" button.
        private void BtnDeleteGenre_Click(object sender, RoutedEventArgs e)
        {
            List<GenresInFilms> genresInFilmsList = new List<GenresInFilms>();
            Genres genre = new Genres();
            bool checkIfGenreIsInFilms = false;

            genresInFilmsList = DatabaseManager.Instance.GenreRepository.GetGenresInFilms().ToList();

            try
            {
                ListViewItem item = ((ListViewItem)lstGenres.SelectedItem);

                genre = (Genres)item.Tag;

                foreach (var genreInFilm in genresInFilmsList)
                {
                    if (genreInFilm.FilmId == genre.GenreId)
                    {
                        checkIfGenreIsInFilms = true;
                    }
                }

                if (checkIfGenreIsInFilms)
                {
                    MessageBox.Show("Dit genre is nog gekoppeld aan films");
                }
                else
                {
                    DatabaseManager.Instance.GenreRepository.DeleteGenre(genre);
                    MessageBox.Show("Het genre \"" + genre.GenreName + "\" werd gewist");
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Geen genre geselecteerd");
            }
            ShowGenres();
        }

        // When clicking the edit button, the user navigates to the EditGenre page.
        private void BtnEditGenre_Click(object sender, RoutedEventArgs e)
        {
            LoadGenreIntoSingleton();
            NavigationService.Navigate(new Pages.EditGenre());
        }
        #endregion

        #region Events
        /////////////////////////////////
        // Events.

        // (GetsFocus) This event empties the "nieuw genre" text box when it gets focus.
        private void TxtNewGenreGetsFocus(object sender, RoutedEventArgs e)
        {
            if (txtNewGenreInput.Text != "Nieuw genre")
            {

            }
            else
            {
                txtNewGenreInput.Text = string.Empty;
            }
        }
        #endregion

    }
}
