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
    /// Interaction logic for UvidUTerapije.xaml
    /// </summary>
    public partial class UvidUTerapije : UserControl
    {
        public static bool aktivan;
        public UvidUTerapije(String idPacijenta)
        {
            InitializeComponent();
            LekarProzor.getGlavnaLabela().Content = "Uvid u terapije";
            PacijentInfo.aktivanPacijentInfo = false;
            aktivan = true;
            this.dataGridTerapije.ItemsSource = PacijentKontroler.ucitajSveTerapijeZaPacijenta(idPacijenta);
        }

        private void btnIzdavanjeRecepta_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridTerapije.SelectedIndex != -1)
            {
                aktivan = false;
                Content = new TerapijeIzdavanjeRecpeta((BolestTerapija)dataGridTerapije.SelectedItem);
            }
            else
            {
                MessageBox.Show("Potrebno je izabrati lek!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
