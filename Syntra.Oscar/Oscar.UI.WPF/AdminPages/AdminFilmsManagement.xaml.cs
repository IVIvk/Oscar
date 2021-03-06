﻿using Oscar.BL;
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
        List<Genres> genresList = new List<Genres>();
        List<Actors> actorsInFilmList = new List<Actors>();
        string messageSelecteerFilm = "Selecteer eerst een film.";
        //int selectionIndex = -1;
        User mainUser = new User();

        public AdminFilmsManagement(User user)
        {
            InitializeComponent();
            mainUser = user;
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
            txtFilmDuration.Text = string.Empty;
            txtFilmPlot.Text = string.Empty;
            lstGenres.Items.Clear();
            lstActors.Items.Clear();
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
                txtFilmDuration.Text = Convert.ToString(film.FilmLengthInMinutes);
                txtFilmPlot.Text = Convert.ToString(film.FilmPlot);

                lstGenres.Items.Clear();
                // The genres that are linked to the selected film are put in a list.
                genresList = DatabaseManager.Instance.FilmRepository.GetGenresForFilm(film.FilmId.Value).ToList();

                // All genres from this list get added to the "Genre" ListView.
                foreach (Genres genre in genresList)
                {
                    ListViewItem itemGenre = new ListViewItem();
                    itemGenre.Tag = genre;
                    itemGenre.Content = genre.GenreName;
                    lstGenres.Items.Add(itemGenre);
                }

                ShowActorsOfSelectedFilm(film);
            }
            catch (Exception)
            {

            }
        }

        // This function loads the selected films properties into the singleton class.
        private void LoadFilmIntoSingleton()
        {
            try
            {
                ListViewItem item = ((ListViewItem)LstFilms.SelectedItem);
                Films film = (Films)item.Tag;

                SingletonClasses.SingletonFilms.OnlyInstanceOfFilms.FilmId = Guid.Parse(txtFilmId.Text);
                SingletonClasses.SingletonFilms.OnlyInstanceOfFilms.FilmTitle = txtFilmTitle.Text;
                SingletonClasses.SingletonFilms.OnlyInstanceOfFilms.ReleaseYear = Convert.ToInt32(txtFilmReleaseYear.Text);
                SingletonClasses.SingletonFilms.OnlyInstanceOfFilms.FilmLengthInMinutes = Convert.ToInt32(txtFilmDuration.Text);
                SingletonClasses.SingletonFilms.OnlyInstanceOfFilms.FilmPlot = txtFilmPlot.Text;

                // Load the genre of the selected film into the SingletonGenre.
                SingletonClasses.SingletonGenre.OnlyInstanceOfGenre.GenreId = (genresList[0].GenreId);

            }
            catch (Exception)
            {

            }
        }

        //This function shows the actors of the selected film.
        private void ShowActorsOfSelectedFilm(Films film)
        {
            actorsInFilmList = DatabaseManager.Instance.FilmRepository.GetActorsForFilm(film.FilmId.Value).ToList();

            lstActors.Items.Clear();

            foreach (Actors actor in actorsInFilmList)
            {
                ListViewItem itemActor = new ListViewItem();
                itemActor.Tag = actor;
                itemActor.Content = actor.FirstName + " " + actor.LastName;
                lstActors.Items.Add(itemActor);
            }
        }
        #endregion

        #region Buttons
        /////////////////////////////////////////
        // Buttons (Click functions)

        // "Ververs lijst" button.
        private void BtnLoadFilms_Click(object sender, RoutedEventArgs e)
        {
            ShowFilms();
        }

        // "Nieuwe film" button.
        private void BtnNewFilm_Click(object sender, RoutedEventArgs e)
        {
            SingletonClasses.SingletonFilms.OnlyInstanceOfFilms.FilmTitle = string.Empty;
            NavigationService.Navigate(new EditFilm(mainUser));
        }

        // "Verander film" button.
        private void BtnEditFilm_Click(object sender, RoutedEventArgs e)
        {
            if (txtFilmTitle.Text != string.Empty)
            {
                LoadFilmIntoSingleton();
                NavigationService.Navigate(new EditFilm(mainUser));
            }
            else
            {
                MessageBox.Show(messageSelecteerFilm);
            }
        }

        // "Verwijder film" button.
        private void BtnDeleteFilm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Create new Films object.
                Films film = new Films();

                // Get the selected item in the list.
                ListViewItem item = ((ListViewItem)LstFilms.SelectedItem);

                // Place this item into the object.
                film = (Films)item.Tag;

                // Get the genres that are linked to the film so the links can be deleted.
                IEnumerable<Genres> linkedGenres = new List<Genres>();
                linkedGenres = DatabaseManager.Instance.FilmRepository.GetGenresForFilm(film.FilmId.Value);

                // Initiate delete in database of the linked genres.
                foreach (Genres genreThatIsLinked in linkedGenres)
                {
                    DatabaseManager.Instance.FilmRepository.DeleteLinkFilmGenre(film, genreThatIsLinked);
                }

                // Get the actors that are linked to the film so the links can be deleted.
                IEnumerable<Actors> linkedActors = new List<Actors>();
                linkedActors = DatabaseManager.Instance.FilmRepository.GetActorsForFilm(film.FilmId.Value);

                // Initiate delete in database of the linked actors.
                foreach (Actors actorThatIsLinked in linkedActors)
                {
                    DatabaseManager.Instance.FilmRepository.DeleteLinkFilmAllActor(film.FilmId.Value);
                }

                // Initiate delete in database of the selected item.
                DatabaseManager.Instance.FilmRepository.DeleteFilm(film);

                // Clear out the text boxes.
                ClearTextBoxes();

                // Refresh the list of films.
                ShowFilms();
            }

            catch (Exception)
            {
                MessageBox.Show(messageSelecteerFilm);
            }
        }
        #endregion

        /////////////////////////////////////////
        // Loaded event.
        #region LoadedEvent
        private void AdminFilmsManagementLoaded(object sender, RoutedEventArgs e)
        {
            ShowFilms();
        }
        #endregion


    }
}
