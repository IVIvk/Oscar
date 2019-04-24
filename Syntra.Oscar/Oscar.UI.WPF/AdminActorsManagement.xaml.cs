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
    /// Interaction logic for AdminActorsManagement.xaml
    /// </summary>
    public partial class AdminActorsManagement : Page
    {
        List<Actors> actorlist = new List<Actors>();
        public AdminActorsManagement()
        {
            InitializeComponent();
        }

        private void BtnLoadActors_Click(object sender, RoutedEventArgs e)
        {
            actorlist = DatabaseManager.Instance.ActorRepository.GetActors().ToList();
            LstActors.Items.Clear();

            foreach (Actors actor in actorlist)
            {
                ListViewItem item = new ListViewItem();

                item.Tag = actor;
                item.Content = actor.FirstName + " " + actor.LastName;

                LstActors.Items.Add(item);
            }
        }

        private void LoadActors(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ListViewItem item = ((ListViewItem)LstActors.SelectedItem);
                Actors actor = (Actors)item.Tag;

                txtActorId.Text = Convert.ToString(actor.ActorId);
                txtActorFirstName.Text = actor.FirstName;
                txtActorLastName.Text = actor.LastName;
            }
            catch (Exception)
            {
                MessageBox.Show("Geen acteur geselecteerd");
            }
        }
    }
}
