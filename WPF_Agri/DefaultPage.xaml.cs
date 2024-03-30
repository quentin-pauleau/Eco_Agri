using System;
using System.Collections.Generic;
using System.Globalization;
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
        List<Terrain> allTerrains;
        List<Meteo> Meteos;

        public DefaultPage()
        {
            InitializeComponent();
            allTerrains = new List<Terrain>();

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
                    allTerrains = terrains;
                    ListBox_Terrains.ItemsSource = allTerrains;
                }
            }
            catch { MessageBox.Show("An Error occured in ListBox_Stade_Initialise"); }
        }

        private void ListBox_Stades_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (ListBox_Terrains.SelectedItems != null && ListBox_Stades.SelectedItems != null)
                {
                    var selectedStade = ListBox_Stades.SelectedItems[0] as Stade;
                    var selectedTerrain = ListBox_Terrains.SelectedItems[0] as Terrain;

                    Label_Stade.Content = selectedStade;

                    Kc = selectedStade.StadeKc;

                    Label_kc_j0.Content = Api_request.GetKcByIds(selectedTerrain.TerrainId, selectedStade.StadeId);
                }
            }
            catch { MessageBox.Show("An Error occured in ListBox_Terrains_SelectionChanged"); }
        }

        private async void ListBox_Terrains_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (ListBox_Terrains.SelectedItems != null)
                {
                    var selectedTerrain = ListBox_Terrains.SelectedItems[0] as Terrain;
                    Label_terrain.Content = selectedTerrain;
                    Label_plante.Content = selectedTerrain.Plante.ToString() ?? "Aucune plante";
                    Label_ListBox_Stade.Content = selectedTerrain.Plante.ToString() ?? "Aucune plante";

                    var Stades = await Api_request.GetListStadesFromPlante(selectedTerrain.Plante);
                    ListBox_Stades.ItemsSource = Stades;
                    var Reserves = await Api_request.GetListReservesByTerrainId(selectedTerrain.TerrainId);
                    ListBox_Reserves.ItemsSource = Reserves;

                    Meteos = await Api_request.GetMeteo(selectedTerrain.TerrainInsee);
                    Label_j0.Content = DateTime.Now.AddDays(Meteos[0].MeteoId).Date.ToString("d", new CultureInfo("fr-FR"));
                    Label_meteo_j0.Content = Meteos[0].MeteoWeather;

                    Label_j1.Content = DateTime.Now.AddDays(Meteos[1].MeteoId).Date.ToString("d", new CultureInfo("fr-FR"));
                    Label_meteo_j1.Content = Meteos[1].MeteoWeather;

                    Label_j2.Content = DateTime.Now.AddDays(Meteos[2].MeteoId).Date.ToString("d", new CultureInfo("fr-FR"));
                    Label_meteo_j2.Content = Meteos[2].MeteoWeather;
                }
                else
                {
                    Label_terrain.Content = "";
                    Label_plante.Content = "";
                    Label_ListBox_Stade.Content = "";

                    Label_j0.Content = "";
                    Label_meteo_j0.Content = "";

                    Label_j1.Content = "";
                    Label_meteo_j1.Content = "";

                    Label_j2.Content = "";
                    Label_meteo_j2.Content = "";
                }
            }
            catch (Exception excep) { MessageBox.Show("An Error occured in ListBox_Terrains_SelectionChanged :\n" + excep); }
        }

        private async void Actualise_Stade_Click(object sender, RoutedEventArgs e)
        {
            try
            {
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

        private void ListBox_Terrain_insee_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ListBox_Terrain_insee.Text != null && ListBox_Terrain_insee.Text != "")
            {
                List<Terrain> rechercheTerrain = new List<Terrain>();
                foreach (var terrain in allTerrains)
                {
                    if (terrain.TerrainInsee.Contains(ListBox_Terrain_insee.Text))
                        rechercheTerrain.Add(terrain);
                }
                ListBox_Terrains.ItemsSource = rechercheTerrain;
            }
        }

        private async void Actualise_ListBox_Plante_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var terrains = await Api_request.GetListTerrains();
                if (terrains == null)
                    MessageBox.Show("An Error occured in Actualise_ListBox_Plante_Click");
                else
                {
                    allTerrains = terrains;
                    if (ListBox_Terrain_insee.Text != null && ListBox_Terrain_insee.Text != "")
                    {
                        List<Terrain> rechercheTerrain = new List<Terrain>();
                        foreach (var terrain in allTerrains)
                        {
                            if (terrain.TerrainInsee.Contains(ListBox_Terrain_insee.Text))
                                rechercheTerrain.Add(terrain);
                        }
                        ListBox_Terrains.ItemsSource = rechercheTerrain;
                    }
                }
            }
            catch { MessageBox.Show("An Error occured in Actualise_ListBox_Plante_Click"); }
        }
    }
}
