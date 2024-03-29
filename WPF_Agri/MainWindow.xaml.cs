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
        public MainWindow()
        {
            InitializeComponent();
            if (Api_request.Instance == null)
            {
                MessageBox.Show("Erreur : Impossible d'accéder à l'API");
                Close();
            }

            ChangeFrame(new DefaultPage());
        }

        public bool ChangeFrame(Page page)
        {
            if (FrameContent.Navigate(page))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Erreur : Impossible d'accéder à la page");
                Close();
            }
            return false;
        }

    }
}
