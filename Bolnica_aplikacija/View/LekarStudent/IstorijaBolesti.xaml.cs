using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.PomocneKlase;
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

namespace Bolnica_aplikacija.LekarStudent
{
    /// <summary>
    /// Interaction logic for IstorijaBolesti.xaml
    /// </summary>
    public partial class IstorijaBolesti : UserControl
    {
        public static bool aktivan;
        public IstorijaBolesti()
        {
            InitializeComponent();
            LekarProzor.getGlavnaLabela().Content = "Istorija bolesti";
            aktivan = true;
            PacijentInfo.aktivanPacijentInfo = false;
            this.dataGridIstorijaBolesti.ItemsSource = PacijentKontroler.nadjiIstorijuBolestiZaPacijenta();
        }

        private void btnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridIstorijaBolesti.SelectedIndex != -1)
            {
                PacijentKontroler.sacuvajBolestTerapiju((BolestTerapija)dataGridIstorijaBolesti.SelectedItem);
                Content = new IzmenaBolesti();
                aktivan = false;
            }
            else
            {
                MessageBox.Show("Potrebno je izabrati bolest!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
