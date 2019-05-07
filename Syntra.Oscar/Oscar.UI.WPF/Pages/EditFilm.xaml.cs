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

        private void BtnAddFilm_Click(object sender, RoutedEventArgs e)
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

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.AdminFilmsManagement());
        }
    }
}