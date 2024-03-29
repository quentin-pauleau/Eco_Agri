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
    /// Logique d'interaction pour DefaultPage.xaml
    /// </summary>
    public partial class DefaultPage : Page
    {
        double? Kc;

        public DefaultPage()
        {
            InitializeComponent();
            ListBox_Terrain_Initialise();
        }


        private async void ListBox_Terrain_Initialise()
        {
            try
            {
                var terrains = await Api_request.GetListTerrains();
                if (terrains == null)
                    MessageBox.Show("An Error occured in ListBox_Stade_Initialise");
                else
                {
                    ListBox_Terrains.ItemsSource = terrains;
                }
            }
            catch { MessageBox.Show("An Error occured in ListBox_Stade_Initialise"); }
        }

        private void ListBox_Stades_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                MessageBox.Show("In Stade Selection");
                if (ListBox_Terrains.SelectedItems != null && ListBox_Stades.SelectedItems != null)
                {
                    var selectedStade = ListBox_Stades.SelectedItems[0] as Stade;
                    var selectedPlante = ListBox_Terrains.SelectedItems[0] as Plante;

                    Label_Stade.Content = selectedStade;

                    Kc = selectedStade.StadeKc;
                }
            }
            catch { MessageBox.Show("An Error occured in ListBox_Terrains_SelectionChanged"); }
        }

        private async void ListBox_Terrains_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                MessageBox.Show("In Plante Selection");
                if (ListBox_Terrains.SelectedItems != null)
                {
                    var selectedPlante = ListBox_Terrains.SelectedItems[0] as Plante;
                    Label_terrain.Content = selectedPlante;

                    var Stades = await Api_request.GetListStadesFromPlante(selectedPlante);
                    ListBox_Stades.ItemsSource = Stades;
                }
                else
                {
                    Label_terrain.Content = "";
                }
            }
            catch (Exception excep) { MessageBox.Show("An Error occured in ListBox_Terrains_SelectionChanged :\n" + excep); }
        }

        private async void Actualise_Stade_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show("In Plante Selection");
                if (ListBox_Terrains.SelectedItems != null)
                {
                    var selectedPlante = ListBox_Terrains.SelectedItems[0] as Plante;
                    Label_terrain.Content = selectedPlante;
                    
                    ListBox_Stades.ItemsSource = await Api_request.GetListStadesFromPlante(selectedPlante);
                }
            }
            catch { MessageBox.Show("An Error occured in ListBox_Terrains_SelectionChanged"); }
        }

        private void newTerrain_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new newTerrain());
        }

        private void ListBox_Reserves_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
