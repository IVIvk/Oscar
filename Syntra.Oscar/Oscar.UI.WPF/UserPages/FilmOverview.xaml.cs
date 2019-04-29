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

        public FilmOverview()
        {
            InitializeComponent();
        }

        private void ShowFilms()
        {
            filmsList = DatabaseManager.Instance.FilmRepository.GetFilms().ToList();
            lstFilmOverview.Items.Clear();

            foreach (Films film in filmsList)
            {
                ListViewItem item = new ListViewItem();

                item.Tag = film;
                item.Content = film.FilmTitle + " (" + film.FilmRating + ")";

                lstFilmOverview.Items.Add(item);
            }
        }

        private void OverviewFilmsLoaded(object sender, RoutedEventArgs e)
        {
            ShowFilms();
        }

        private void DoubleClickOnItem(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ListViewItem item = ((ListViewItem)lstFilmOverview.SelectedItem);
                Films film = (Films)item.Tag;

                txtFilmTitle.Text = Convert.ToString(film.FilmTitle);
                txtFilmReleaseYear.Text = Convert.ToString(film.ReleaseYear);
                txtFilmDuration.Text = Convert.ToString(film.FilmLengthInMinutes);
            }
            catch (Exception)
            {

            }
        }
    }
}
