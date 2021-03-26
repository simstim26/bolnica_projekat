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
using System.Windows.Shapes;

namespace Bolnica_aplikacija
{
    /// <summary>
    /// Interaction logic for SekretarProzor.xaml
    /// </summary>
    public partial class SekretarProzor : Window
    {
        public SekretarProzor()
        {
            InitializeComponent();

        }

        private void pacijenti_Click(object sender, RoutedEventArgs e)
        {
            
            this.PocetniEkranGrid.Visibility = Visibility.Hidden;
            this.PacijentGrid.Visibility = Visibility.Visible;

        }

        private void povratakGlavniProzorLabela_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.PacijentGrid.Visibility = Visibility.Hidden;
            this.PocetniEkranGrid.Visibility = Visibility.Visible;
            
        }
    }
}
