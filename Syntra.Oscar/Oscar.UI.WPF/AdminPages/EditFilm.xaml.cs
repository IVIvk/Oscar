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
using Oscar.Dapper;

namespace Oscar.UI.WPF
{
    /// <summary>
    /// Interaction logic for EditFilm.xaml
    /// </summary>
    public partial class EditFilm : Page
    {
        User mainUser = new User();
        public EditFilm(User user)
        {
            InitializeComponent();
            mainUser = user;
        }
        // Variable.
        bool allTextBoxesFilled = false;
        List<Genres> GenresList = new List<Genres>();
        List<Genres> GenresListForLinking = new List<Genres>();
        List<Actors> ActorsList = new List<Actors>();
        List<Actors> actorsInFilmList = new List<Actors>();
        int index = -1;
        int selectedIndex = -1;
        Guid genreId;
        Review userReview = new Review();
        User currentUser = new User();
        string messageSelecteerScore = "Selecteer een score voor je een review tekst kan toevoegen.";
        string messageGeenActeur = "Geen acteur geselecteerd";
        string messageVulTekstvakken = "Vul alle nodige tekstvakken.";

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
                // Create new instance of Films and Genres.
                Films film = new Films();
                Genres genre = new Genres();
                ComboBoxItem selection = new ComboBoxItem();

                // Load all the data into these instances.
                film.FilmId = Guid.NewGuid();
                film.FilmTitle = txtFilmTitle.Text;
                film.ReleaseYear = Convert.ToInt32(txtReleaseYear.Text);
                film.FilmLengthInMinutes = Convert.ToInt32(txtDuration.Text);
                film.FilmPlot = txtPlot.Text;

                // Insert the new film into the database.
                DatabaseManager.Instance.FilmRepository.InsertFilm(film);

                // Get the index of the selection inside the Gneres combo box.
                index = cmbGenre.SelectedIndex;

                // Use this index to get the genres object from the GenresList.
                genre = GenresList[index];

                // Insert the link between the film and the genre.
                DatabaseManager.Instance.GenreRepository.InsertLinkGenreAndFilm(genre, film);

                // Insert link actors and film in database.
                foreach (Actors actor in actorsInFilmList)
                {
                    DatabaseManager.Instance.ActorRepository.InsertLinkActorAndFilm(actor, film.FilmId.Value);
                }

                // Insert an initial rating if a score is selected.
                // Index -1 = nothing selected.
                // Index 0 = "Geen score".
                // Index 1 to 5 are valid scores.
                if (cmbScore.SelectedIndex > 0)
                {
                    // Fill up the userReview with the relevant data, so it can be inserted into the database.
                    CreateInitialScore();

                    // Insert the review into the database.
                    DatabaseManager.Instance.ReviewRepository.SaveReview(film, mainUser, userReview);
                }
                // Navigate back to the AdminFilmsManagement page.
                NavigationService.Navigate(new Pages.AdminFilmsManagement(mainUser));
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
                Genres genre = new Genres();

                // Load all the text into this instance.
                film.FilmId = SingletonClasses.SingletonFilms.OnlyInstanceOfFilms.FilmId;
                film.FilmTitle = txtFilmTitle.Text;
                film.ReleaseYear = Convert.ToInt32(txtReleaseYear.Text);
                film.FilmLengthInMinutes = Convert.ToInt32(txtDuration.Text);
                film.FilmPlot = txtPlot.Text;

                // Edit the film data in the database.
                DatabaseManager.Instance.FilmRepository.UpdateFilm(film);

                // Get the index of the selection inside the Gneres combo box.
                index = cmbGenre.SelectedIndex;

                // Use this index to get the genres object from the GenresList.
                genre = GenresList[index];

                // Delete existing links between films and genres.
                DatabaseManager.Instance.GenreRepository.DeleteLinkGenreAndFilm(film);

                // Insert the link between the film and the genre.
                DatabaseManager.Instance.GenreRepository.InsertLinkGenreAndFilm(genre, film);

                // Delete existing links between films and actors
                DatabaseManager.Instance.FilmRepository.DeleteLinkFilmAllActor(film.FilmId.Value);

                // Insert link actors and film in database.
                foreach (Actors actor in actorsInFilmList)
                {
                    DatabaseManager.Instance.ActorRepository.InsertLinkActorAndFilm(actor, SingletonClasses.SingletonFilms.OnlyInstanceOfFilms.FilmId);
                }

                // Navigate back to the AdminFilmsManagement page.
                NavigationService.Navigate(new Pages.AdminFilmsManagement(mainUser));
            }
        }

        // Button "Annuleren".
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.AdminFilmsManagement(mainUser));
        }

        // Button "Acteur toegoegen".
        private void BtnAddActor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Actors actor = new Actors();
                bool checkActorInList = false;

                actor = ActorsList[cmbActors.SelectedIndex];


                foreach (Actors actorInList in actorsInFilmList)
                {
                    if (actor.ActorId == actorInList.ActorId)
                    {
                        checkActorInList = true;
                        break;
                    }
                }

                if (!checkActorInList)
                {
                    actorsInFilmList.Add(actor);

                    ShowActorsOfSelectedFilm(); ;
                }
                cmbActors.SelectedIndex = -1;
            }
            catch (Exception)
            {
                MessageBox.Show(messageGeenActeur);
            }
        }

        // Button "Verwijder acteur".
        private void BtnDeleteActor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ListViewItem item = ((ListViewItem)lstActors.SelectedItem);
                Actors actor = (Actors)item.Tag;

                foreach (Actors allActors in actorsInFilmList)
                {
                    if (allActors == actor)
                    {
                        actorsInFilmList.Remove(actor);
                        break;
                    }
                }
                ShowActorsOfSelectedFilm();
            }
            catch (Exception)
            {

                MessageBox.Show(messageGeenActeur);
            }
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
            ShowActorsOfSelectedFilm();
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
                            if (cmbGenre.SelectedIndex > -1)
                            {
                                succes = true;
                            }
                        }
                    }
                }
            }
            if (succes == false)
            {                
                MessageBox.Show(messageVulTekstvakken);
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

                // Genre object.
                item.Tag = genre;
                // Display Genre name.
                item.Content = genre.GenreName.ToString();
                // Add the item to the list.
                cmbGenre.Items.Add(item);
            }
        }

        // This function selects the correct genre when editing a film.
        private void FillGenreComboBoxWhenEditing()
        {
            if (SingletonClasses.SingletonGenre.OnlyInstanceOfGenre.GenreId.ToString() != string.Empty)
            {
                genreId = Guid.Parse(SingletonClasses.SingletonGenre.OnlyInstanceOfGenre.GenreId.ToString());

                for (int i = 0; i < GenresList.Count; i++)
                {
                    if (genreId == GenresList[i].GenreId)
                    {
                        selectedIndex = i;
                    }
                }
                cmbGenre.SelectedIndex = selectedIndex;
            }
        }

        // This function Inserts an initial review score.
        private void CreateInitialScore()
        {
            // Get and set the selected score, User, set review content and generate a new guid Id.
            userReview.ReviewScore = Convert.ToInt32(((ComboBoxItem)cmbScore.SelectedItem).Content);
            userReview.ReviewContent = txtReview.Text;
            userReview.ReviewId = Guid.NewGuid();
            userReview.UserId = mainUser.userId;
        }

        // This function Gets the genres from the database and puts them inside the genre ComboBox.
        // The user can choose the genre from this ComboBox.
        private void FillActorsComboBox()
        {
            ActorsList = DatabaseManager.Instance.ActorRepository.GetActors().ToList();

            foreach (Actors actor in ActorsList)
            {
                ListViewItem item = new ListViewItem();

                // Genre object.
                item.Tag = actor;
                // Display Genre name.
                item.Content = actor.FirstName.ToString() + " " + actor.LastName.ToString();
                // Add the item to the list.
                cmbActors.Items.Add(item);
            }
        }

        //This function shows the actors of the selected film.
        private void ShowActorsOfSelectedFilm()
        {
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
        #region Events
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
            txtReview.Text = messageSelecteerScore;
            FillGenreComboBox();
            // EDIT path. Some different controls will be usable.
            if (SingletonClasses.SingletonFilms.OnlyInstanceOfFilms.FilmTitle != string.Empty)
            {
                // Load actors of selected film
                actorsInFilmList = DatabaseManager.Instance.FilmRepository.GetActorsForFilm(SingletonClasses.SingletonFilms.OnlyInstanceOfFilms.FilmId.Value).ToList();

                // Disable the add button and enable the Edit button.
                btnAddFilm.IsEnabled = false;
                btnAddFilm.Visibility = Visibility.Hidden;
                btnEditFilm.IsEnabled = true;

                // Disable the initial review UI when editing a film.
                cmbScore.IsEnabled = false;
                lblScore.Visibility = Visibility.Hidden;
                cmbScore.Visibility = Visibility.Hidden;
                txtReview.IsEnabled = false;
                txtReview.Visibility = Visibility.Hidden;
                lblReview.Visibility = Visibility.Hidden;

                // Fill the text boxes with the properties inside the singleton object.
                FillTextBoxes();
                FillGenreComboBoxWhenEditing();
            }
            // ADD path. Some different controls will be usable.
            else
            {
                // Enable the add button and disable the Edit button.
                btnAddFilm.IsEnabled = true;
                btnEditFilm.IsEnabled = false;
                btnEditFilm.Visibility = Visibility.Hidden;

                // Enable the initial review score UI when adding a film.
                cmbScore.IsEnabled = true;
                lblScore.Visibility = Visibility.Visible;
                cmbScore.Visibility = Visibility.Visible;
                txtReview.IsEnabled = false;
                txtReview.Visibility = Visibility.Visible;
                lblReview.Visibility = Visibility.Visible;

                // Clear the text boxes.
                ClearTextBoxes();
            }

            FillActorsComboBox();
        }

        // SelectionChanged Event.
        //
        // This event will check if the score is between 0-5 before enabling the UI to write and inital review.
        private void CmbScore_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Check for valid scores.
            // Index -1 = nothing selected.
            // Index 0 = "Geen score".
            // Index 1 to 5 are valid scores.
            if (cmbScore.SelectedIndex > 0)
            {
                txtReview.IsEnabled = true;
                if (txtReview.Text == messageSelecteerScore)
                {
                    txtReview.Text = string.Empty;
                }
            }
            else
            {
                txtReview.Text = messageSelecteerScore;
                txtReview.IsEnabled = false;
            }
        }

        #endregion


    }
}