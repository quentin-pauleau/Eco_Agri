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

namespace WPF_Agri
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double? Kc;

        public MainWindow()
        {
            InitializeComponent();
            if (Api_request.Instance == null)
            {
                MessageBox.Show("Erreur : Impossible d'accéder à l'API");
                Close();
            }
            ListBox_Stade_Initialise();
        }

        private async void ListBox_Stade_Initialise()
        {
            try
            {
                var Plantes = await Api_request.GetListPlantes();
                ListBox_Plantes.ItemsSource = Plantes;
                if (Plantes == null)
                    MessageBox.Show("An Error occured in ListBox_Stade_Initialise");
            }
            catch { MessageBox.Show("An Error occured in ListBox_Stade_Initialise"); }
        }
        private async void ListBox_Stades_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                MessageBox.Show("In Stade Selection");
                if (ListBox_Plantes.SelectedItems != null && ListBox_Stades.SelectedItems != null)
                {
                    var selectedStade = ListBox_Stades.SelectedItems[0] as Stade;
                    var selectedPlante = ListBox_Plantes.SelectedItems[0] as Plante;

                    Label_Stade.Content = selectedStade;

                    Kc = selectedStade.StadeKc;
                }
            }
            catch { MessageBox.Show("An Error occured in ListBox_Plantes_SelectionChanged"); }
        }

        private async void ListBox_Plantes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                MessageBox.Show("In Plante Selection");
                if (ListBox_Plantes.SelectedItems != null)
                {
                    var selectedPlante = ListBox_Plantes.SelectedItems[0] as Plante;
                    Label_plante.Content = selectedPlante;

                    var Stades = await Api_request.GetListStadesFromPlante(selectedPlante);
                    ListBox_Stades.ItemsSource = Stades;
                }
                else
                {
                    Label_plante.Content = "";
                }
            }
            catch (Exception excep) { MessageBox.Show("An Error occured in ListBox_Plantes_SelectionChanged :\n" + excep); }
        }

        private async void Actualise_Stade_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show("In Plante Selection");
                if (ListBox_Plantes.SelectedItems != null)
                {
                    var selectedPlante = ListBox_Plantes.SelectedItems[0] as Plante;
                    Label_plante.Content = selectedPlante;

                    ListBox_Stades.ItemsSource = await Api_request.GetListStadesFromPlante(selectedPlante);
                }
            }
            catch { MessageBox.Show("An Error occured in ListBox_Plantes_SelectionChanged"); }
        }
    }
}
