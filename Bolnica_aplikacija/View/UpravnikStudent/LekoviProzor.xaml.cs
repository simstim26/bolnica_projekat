using Bolnica_aplikacija.Kontroler;
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
    /// Interaction logic for LekoviProzor.xaml
    /// </summary>
    public partial class LekoviProzor : UserControl
    {
        public static DataGrid dataGridLekovidata;
        public static Grid gridLekova;
        public LekoviProzor()
        {
            InitializeComponent();
            dataGridLekovi.ItemsSource = LekKontroler.ucitajSve();
            dataGridLekovidata = dataGridLekovi;
            gridLekova = gridLekovi;
        }

        private void btnDodajLek_Click(object sender, RoutedEventArgs e)
        {
            GlavniProzor.DobaviProzorZaIzmenu().Children.Clear();
            GlavniProzor.DobaviProzorZaIzmenu().Children.Add(new DodajLek());
        }

        public static DataGrid dobaviDataGridLekova()
        {
            return dataGridLekovidata;
        }

        public static Grid dobaviGridLekova()
        {
            return gridLekova;
        }

        private void btnIzmenaLeka_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridLekovi.SelectedIndex != -1)
            {
                GlavniProzor.DobaviProzorZaIzmenu().Children.Clear();
                GlavniProzor.DobaviProzorZaIzmenu().Children.Add(new IzmeniLek());
            }    
        }

        private void btnObrisiLek_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridLekovi.SelectedIndex != -1)
            {
                Potvrda potvrda = new Potvrda("lek");
                potvrda.Show();
            }  
        }

        private void btnOdbijeniZahtevi_Click(object sender, RoutedEventArgs e)
        {
            GlavniProzor.DobaviProzorZaIzmenu().Children.Clear();
            GlavniProzor.DobaviProzorZaIzmenu().Children.Add(new OdbijeniLekovi());
        }
    }
}
