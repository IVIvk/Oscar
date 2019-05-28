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
        List<Genres> genresList = new List<Genres>();
        List<Actors> actorsInFilmList = new List<Actors>();
        User user = new User();

        public FilmOverview(User currentUser)
        {
            InitializeComponent();

            filmsList = DatabaseManager.Instance.FilmRepository.GetFilms().ToList();

            user = currentUser;
        }

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

        private void OverviewFilmsLoaded(object sender, RoutedEventArgs e)
        {
            ShowFilms(filmsList);
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

        private void txtSearchFilmFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearchFilm.Text == "Search Film")
            {
                txtSearchFilm.Text = String.Empty;
            }
        }

        private void TxtSearchFilm_TextChanged(object sender, TextChangedEventArgs e)
        {
            ShowFilms(filmsList);
        }

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
    }
}
