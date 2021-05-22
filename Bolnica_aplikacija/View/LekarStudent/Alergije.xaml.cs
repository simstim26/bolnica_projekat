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

namespace Bolnica_aplikacija.LekarStudent
{
    /// <summary>
    /// Interaction logic for Alergije.xaml
    /// </summary>
    public partial class Alergije : UserControl
    {
        public static bool aktivan;
        private static FrameworkElement fm = new FrameworkElement();
        public Alergije(String idPacijenta)
        {
            InitializeComponent();
            LekarProzor.getGlavnaLabela().Content = "Alergije";
            PacijentInfo.aktivanPacijentInfo = false;
            aktivan = true;
            fm.DataContext = idPacijenta;
            ucitaj();
        }

        public static FrameworkElement getFM()
        {
            return fm;
        }
        private void ucitaj()
        {
            dataGridAlergije.ItemsSource = PacijentKontroler.procitajAlergije((String)fm.DataContext);

        }
        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            PacijentKontroler.napraviAlergiju((String)fm.DataContext, txtDodavanjeAlergije.Text);
            ucitaj();
            txtDodavanjeAlergije.Text = "";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridAlergije.SelectedIndex != -1)
            {
                AlergijaKontroler.obrisiAlergiju((Alergija)dataGridAlergije.SelectedItem);
                ucitaj();
            }
            else
            {
                MessageBox.Show("Potrebno je izabrati alergiju!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtDodavanjeAlergije.Text))
            {
                btnDodaj.IsEnabled = true;
            }
            else
            {
                btnDodaj.IsEnabled = false;
            }
        }
    }
}
