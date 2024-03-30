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
    /// Logique d'interaction pour newTerrain.xaml
    /// </summary>
    public partial class newTerrain : Page
    {
        public newTerrain()
        {
            InitializeComponent();
        }

        private async void newTerrain_Confirm_Click(object sender, RoutedEventArgs e)
        {
            Terrain terrain= new Terrain
            {
                TerrainInsee = newTerrain_Insee.Text,
                TerrainNom = newTerrain_Name.Text,
                TerrainSurface = int.Parse(newTerrain_Surface.Text)
            };

            if (!await Api_request.CreateTerrain(terrain))
            {
                MessageBox.Show("Impossible to create the terrain with the values :\n" +
                    $"Insee : {terrain.TerrainInsee}\n" +
                    $"Nom : {terrain.TerrainNom}\n" +
                    $"Surface : {terrain.TerrainSurface}\n" +
                    $"Plante : {terrain.Plante.PlanteNom}");
            }
        }
    }
}
