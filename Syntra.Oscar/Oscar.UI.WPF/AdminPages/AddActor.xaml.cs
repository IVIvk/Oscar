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
    /// Interaction logic for AddActor.xaml
    /// </summary>
    public partial class AddActor : Page
    {
        public AddActor()
        {
            InitializeComponent();
        }

        private void BtnAddActor_Click(object sender, RoutedEventArgs e)
        {
            Actors actor = new Actors();

            actor.ActorId = Guid.NewGuid();
            actor.FirstName = txtFirstNameActor.Text;
            actor.LastName = txtLastNameActor.Text;

            DatabaseManager.Instance.ActorRepository.InsertActor(actor);

            NavigationService.Navigate(new AdminActorsManagement());
        }

        private void BtnCancelAddActor_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminActorsManagement());
        }
    }
}
