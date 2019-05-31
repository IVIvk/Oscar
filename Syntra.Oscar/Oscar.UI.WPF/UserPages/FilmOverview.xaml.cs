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

namespace Oscar.UI.WPF.UserPages
{
    /// <summary>
    /// Interaction logic for FilmOverview.xaml
    /// </summary>
    public partial class FilmOverview : Page
    {
        List<Films> filmsList = new List<Films>();
        List<Films> filmsListFilteredOnGenre = new List<Films>();
        List<Genres> genresList = new List<Genres>();
        List<Genres> genresFilterList = new List<Genres>();
        List<Actors> actorsInFilmList = new List<Actors>();
        List<Actors> actorsFilterList = new List<Actors>();
        User user = new User();

        public FilmOverview(User currentUser)
        {
            InitializeComponent();

            filmsList = DatabaseManager.Instance.FilmRepository.GetFilms().ToList();

            user = currentUser;
        }

        // This function loads the films from the database into the ListView.
        // Also used for the search fill functionality.
        private void ShowFilms(List<Films> filmsListInFunction)
        {
            lstFilmOverview.Items.Clear();

            foreach (Films film in filmsListInFunction)
            {
                List<Review> listOfReviews = DatabaseManager.Instance.ReviewRepository.GetReviewsPerFilm(film).ToList();
                ListViewItem item = new ListViewItem();

                if (txtSearchFilm.Text == "Search Film" || film.FilmTitle.ToLower().Contains(txtSearchFilm.Text.ToLower()))
                {
                    film.UpdateRating(listOfReviews);

                    item.Tag = film;
                    item.Content = film.FilmTitle + " (" + film.FilmRating + ")";

                    lstFilmOverview.Items.Add(item);
                }
            }
        }

        // This function Gets the genres from the database and puts them inside the genre ComboBox.
        // The user can choose the genre from this ComboBox.
        private void FillGenreComboBox()
        {
            genresFilterList = DatabaseManager.Instance.GenreRepository.GetGenres().ToList();

            foreach (Genres genre in genresFilterList)
            {
                ComboBoxItem item = new ComboBoxItem();

                // Genre object.
                item.Tag = genre;
                // Display Genre name.
                item.Content = genre.GenreName.ToString();
                // Add the item to the list.
                cmbGenre.Items.Add(item);
            }
        }

        // This function Gets the actors from the database and puts them inside the actor ComboBox.
        // The user can choose the actor from this ComboBox.

        private void FillActorComboBox()
        {
            actorsFilterList = DatabaseManager.Instance.ActorRepository.GetActors().ToList();

            foreach (Actors actor in actorsFilterList)
            {
                ComboBoxItem item = new ComboBoxItem();

                //Actor object.
                item.Tag = actor;
                // Display Actor name.
                item.Content = actor.FirstName.ToString() + " " + actor.LastName.ToString();
                // Add the item to the list.
                cmbActor.Items.Add(item);
            }
        }

        // Loaded Event.
        private void OverviewFilmsLoaded(object sender, RoutedEventArgs e)
        {
            ShowFilms(filmsList);
            FillGenreComboBox();
            FillActorComboBox();
        }

        // This function loads the properties of the selected film into the correct text boxes.
        private void DoubleClickOnItem(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ListViewItem item = ((ListViewItem)lstFilmOverview.SelectedItem);
                Films film = (Films)item.Tag;

                txtFilmTitle.Text = Convert.ToString(film.FilmTitle);
                txtFilmReleaseYear.Text = Convert.ToString(film.ReleaseYear);
                txtFilmDuration.Text = Convert.ToString(film.FilmLengthInMinutes);
                txtFilmPlot.Text = Convert.ToString(film.FilmPlot);

                // The genres that are linked to the selected film are put in a list.
                genresList = DatabaseManager.Instance.FilmRepository.GetGenresForFilm(film.FilmId.Value).ToList();

                // All genres from this list get added to the "Genre" ListView.
                lstGenres.Items.Clear();
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

        // Button "Schrijf review".
        // The user wil navigate to the WriteReview page.
        private void BtnWriteReview_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ListViewItem item = ((ListViewItem)lstFilmOverview.SelectedItem);
                Films film = (Films)item.Tag;

                NavigationService.Navigate(new WriteReview(film, user));
            }
            catch (Exception)
            {
                MessageBox.Show("Selecteer eerst een film");
            }
        }

        // Button "Toon alle reviews".
        // The user wil navigate to the AllReviewsOfOneFilm page.
        private void BtnShowAllReviews_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ListViewItem item = ((ListViewItem)lstFilmOverview.SelectedItem);
                Films film = (Films)item.Tag;

                NavigationService.Navigate(new AllReviewsOfOneFilm(film));
            }
            catch (Exception)
            {
                MessageBox.Show("Selecteer eerst een film");
            }
        }

        // Get Focus event --txtSearchFilm--
        // The text box will be emptied.
        private void txtSearchFilmFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearchFilm.Text == "Search Film")
            {
                txtSearchFilm.Text = String.Empty;
            }
        }

        // TextChanged event --txtSearchFilm--
        // The list of films will be updated to films only containing the entered text.
        private void TxtSearchFilm_TextChanged(object sender, TextChangedEventArgs e)
        {
            ShowFilms(filmsList);
        }

        // Button "Top vijf".
        // The top 5 rated movies will be shown in the ListView.
        private void BtnTopFive_Click(object sender, RoutedEventArgs e)
        {
            List<Films> filmTopFive = new List<Films>();
            Films usedFilm = new Films();
            Films cachedFilm = new Films();

            filmTopFive.Add(filmsList[0]);

            int lenghtOfList = 1;

            foreach (Films film in filmsList)
            {
                usedFilm = film;

                if (usedFilm.FilmId != filmTopFive[0].FilmId)
                {
                    int i = 0;
                    do
                    {
                        if (usedFilm.FilmRating > filmTopFive[i].FilmRating)
                        {
                            cachedFilm = filmTopFive[i];
                            filmTopFive[i] = usedFilm;
                            usedFilm = cachedFilm;
                        }

                        i++;
                    } while (i < lenghtOfList);

                    if (lenghtOfList < 5)
                    {
                        filmTopFive.Add(usedFilm);
                        lenghtOfList++;
                    }
                }
            }

            ShowFilms(filmTopFive);
        }

        // This function shows the actors that star in a selected film.
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

        // Button "Verwijder filters.
        // This will reset the Genre/Actor filters to NO filter.
        // The SelectedIndex of both Combo Boxes will be set to -1. (=nothing selected)
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            cmbActor.SelectedIndex = -1;
            cmbGenre.SelectedIndex = -1;
        }

        // When the Genre selection changes, the list of films is updated to only show the films of the selected Genre.
        private void CmbGenre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //genresFilterList

            // If something is selected the actions take place.
            // Index -1 = nothing selected.
            if (cmbGenre.SelectedIndex > -1 )
            {
                cmbActor.SelectedIndex = -1;
                int selectedIndex = cmbGenre.SelectedIndex;
                Guid genreId = genresFilterList[selectedIndex].GenreId.Value;

                filmsListFilteredOnGenre = DatabaseManager.Instance.FilmRepository.GetFilmsOfGenre(genreId).ToList();

                lstFilmOverview.Items.Clear();
                foreach (Films film in filmsListFilteredOnGenre)
                {
                    ListViewItem itemFilm = new ListViewItem();
                    itemFilm.Tag = film;
                    itemFilm.Content = film.FilmTitle;
                    lstFilmOverview.Items.Add(itemFilm);
                }
            }
        }
    }
}
