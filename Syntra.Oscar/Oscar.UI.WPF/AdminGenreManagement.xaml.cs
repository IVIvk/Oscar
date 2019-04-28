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
    }
}
