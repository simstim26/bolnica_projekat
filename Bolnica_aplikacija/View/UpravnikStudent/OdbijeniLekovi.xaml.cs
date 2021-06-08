using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.Model;
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

namespace Bolnica_aplikacija.View.UpravnikStudent
{
    /// <summary>
    /// Interaction logic for OdbijeniLekovi.xaml
    /// </summary>
    /// 
    
    public partial class OdbijeniLekovi : UserControl
    {
        public static DataGrid dataGridLekoviOdbijeni;
        public static Grid gridOdbacenihLekova;
        public OdbijeniLekovi()
        {
            InitializeComponent();
            List<LekZaOdobravanje> lekovi = LekKontroler.ucitajOdbaceneLekove();
            dataGridOdbijeniLekovi.ItemsSource = lekovi;
            dataGridLekoviOdbijeni = dataGridOdbijeniLekovi;
            gridOdbacenihLekova = gridOdbijeniLekovi;
        }

        public static DataGrid dobaviDataGridOdbijenihLekova()
        {
            return dataGridLekoviOdbijeni;
        }

        private void btnIzmeniLek_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridOdbijeniLekovi.SelectedIndex != -1)
            {
                GlavniProzor.DobaviProzorZaIzmenu().Children.Clear();
                GlavniProzor.DobaviProzorZaIzmenu().Children.Add(new IzmenaOdbijenog());
            }
        }

        private void btnObrisiOdbacenLek_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridOdbijeniLekovi.SelectedIndex != -1)
            {
                Potvrda potvrda = new Potvrda("odbačen lek");
                potvrda.Show();
            }
        }

        public static Grid dobaviGridOdbijeniLekovi()
        {
            return gridOdbacenihLekova;
        }

        private void btnOtkaziOdbacenLek_Click(object sender, RoutedEventArgs e)
        {
            GlavniProzor.DobaviProzorZaIzmenu().Children.Clear();
            GlavniProzor.DobaviProzorZaIzmenu().Children.Add(new LekoviProzor());
        }
    }
}
