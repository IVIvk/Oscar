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
    /// Interaction logic for UlaInterface.xaml
    /// </summary>
    public partial class UlaInterface : Page
    {
        public UlaInterface()
        {
            InitializeComponent();
        }

        // This function loads the properties of the selected film into the correct text boxes.
        private void LoadFilms(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ListViewItem item = ((ListViewItem)FilmList.SelectedItem);
                Films film = (Films)item.Tag;
                
                FilmTitle.Text = Convert.ToString(film.FilmTitle);
                PremiereDatum.Text = Convert.ToString(film.ReleaseYear);
                // Genres requires some more SQL queries before it can be shown.
                //txtFilmGenre.Text = Convert.ToString(film.FilmGenre);
                Duurtijd.Text = Convert.ToString(film.FilmLengthInMinutes);
                Omschrijving.Text = Convert.ToString(film.FilmPlot);

            }
            catch (Exception)
            {

            }
        }

        private void TitleFocus(object sender, RoutedEventArgs e)
        {
            
        }

        private void Toevoegen_Click(object sender, RoutedEventArgs e)
        {
            //Films film = new Films();
            //film.FilmTitle = FilmTitle.Text;
            //film.ReleaseYear = Convert.ToInt32(PremiereDatum.Text);

            // List to hold all the Films objects.
            List<Films> filmsList = new List<Films>();

            filmsList = DatabaseManager.Instance.FilmRepository.GetFilms().ToList();
            FilmList.Items.Clear();

            foreach (Films film in filmsList)
            {
                ListViewItem item = new ListViewItem();

                item.Tag = film;
                item.Content = film.FilmTitle;

                FilmList.Items.Add(item);
            }

        }

        private void Zoek_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Review_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Review.Text == "My review")
            {
                Review.Text = string.Empty;
            }           
            
        }

        private void ReviewToevoegen_Click(object sender, RoutedEventArgs e)
        {
            ListViewItem item = ((ListViewItem)FilmList.SelectedItem);
            Films film = (Films)item.Tag;

            User gebruiker = new User();

            Review review = new Review();
            review.ReviewContent = Review.Text;
            review.ReviewId = new Guid();
            review.ReviewScore = 5;
            DatabaseManager.Instance.ReviewRepository.SaveReview(film, gebruiker, review);
            // Films film, User user, Review review
        }


    }
}
