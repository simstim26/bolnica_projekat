using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.Model;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for DodajLek.xaml
    /// </summary>
    public partial class DodajLek : UserControl
    {
        int lekariBroj;
        public DodajLek()
        {
            InitializeComponent();
            LekZaOdobravanje noviLek = LekZaOdobravanje.getInstance();
            List<String> sastojci = new List<string>();
            noviLek.sastojci = sastojci;
            lekariBroj = 0;

            gridLekoviDodaj.Visibility = Visibility.Visible;
            cbTipLekaDodavanje.ItemsSource = LekKontroler.tipLeka();
            comboBoxNacinUpotrebe.ItemsSource = LekKontroler.nacinUpotrebeLeka();
        }

        private void btnDodajSastojke_Click(object sender, RoutedEventArgs e)
        {
            gridLekoviDodajSastojke.Visibility = Visibility.Visible;
            gridLekoviDodaj.Visibility = Visibility.Hidden;
            dataGridDodajSastojke.ItemsSource = LekZaOdobravanje.getInstance().sastojci;
        }

        private void btnDodajZamenskeLekove_Click(object sender, RoutedEventArgs e)
        {
            dataGridSviLekoviZaZamenski.ItemsSource = LekKontroler.ucitajSve();
            List<Lek> zamenskiLekovi = new List<Lek>();
            dataGridZamenskiUbaceniLekovi.ItemsSource = zamenskiLekovi;
            gridLekoviDodajZamenskeLekove.Visibility = Visibility.Visible;
            gridLekoviDodaj.Visibility = Visibility.Hidden;
        }

        private void btnIzaberiLekare_Click(object sender, RoutedEventArgs e)
        {
            dataGridLekariZaSlanje.ItemsSource = LekarKontroler.ucitajSve();
            gridIzaberiLekareZaSlanje.Visibility = Visibility.Visible;
            gridLekoviDodaj.Visibility = Visibility.Hidden;
        }

        private void btnOtkaziDodavanjeLeka_Click(object sender, RoutedEventArgs e)
        {
            GlavniProzor.DobaviProzorZaIzmenu().Children.Clear();
            GlavniProzor.DobaviProzorZaIzmenu().Children.Add(new LekoviProzor());
        }

        private void btnPotvrdiDodavanjeLekova_Click(object sender, RoutedEventArgs e)
        {
            svaPolja.Visibility = Visibility.Hidden;
            jacinBroj.Visibility = Visibility.Hidden;
            lekari.Visibility = Visibility.Hidden;

            String pat = @"^[0-9]+$";
            Regex r = new Regex(pat);
            Match m = r.Match(textBoxKolicinaLekaUnos.Text.Replace(" ", ""));

            if (String.IsNullOrEmpty(textBoxNazivLekaUnos.Text) || cbTipLekaDodavanje.SelectedIndex == -1 ||
                String.IsNullOrEmpty(textBoxProizvodjacLekaUnos.Text) || String.IsNullOrEmpty(textBoxKolicinaLekaUnos.Text) ||
                comboBoxNacinUpotrebe.SelectedIndex == -1)
            {
                svaPolja.Visibility = Visibility.Visible;
            }
            else if (!m.Success)
            {
                jacinBroj.Visibility = Visibility.Visible;
            }
            else if (lekariBroj < 2)
            {
                lekari.Visibility = Visibility.Visible;
            }
            else
            {
                TipLeka tipLeka = (TipLeka)cbTipLekaDodavanje.SelectedItem;
                NacinUpotrebe nacinUpotrebe = (NacinUpotrebe)comboBoxNacinUpotrebe.SelectedItem;

                LekZaOdobravanje noviLek = LekZaOdobravanje.getInstance();

                noviLek.naziv = textBoxNazivLekaUnos.Text;
                noviLek.tip = tipLeka;
                noviLek.kolicina = Int32.Parse(textBoxKolicinaLekaUnos.Text);
                noviLek.proizvodjac = textBoxProizvodjacLekaUnos.Text;
                noviLek.nacinUpotrebe = nacinUpotrebe;
                noviLek.id = (LekKontroler.ucitajLekoveZaOdobravanje().Count() + 1).ToString();

                LekKontroler.napraviLek(noviLek);

                GlavniProzor.DobaviProzorZaIzmenu().Children.Clear();
                GlavniProzor.DobaviProzorZaIzmenu().Children.Add(new LekoviProzor());
            }

        }

        private void btnSastojakUnesi_Click(object sender, RoutedEventArgs e)
        {
            prazanSastojak.Visibility = Visibility.Hidden;
            if (String.IsNullOrEmpty(textBoxUpisiSastojak.Text))
            {
                prazanSastojak.Visibility = Visibility.Visible;
            }
            else
            {
                LekZaOdobravanje noviLek = LekZaOdobravanje.getInstance();
                String sastojak = textBoxUpisiSastojak.Text;
                noviLek.sastojci.Add(sastojak);
                dataGridDodajSastojke.Items.Refresh();
                textBoxUpisiSastojak.Clear();
            }  
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            gridLekoviDodajSastojke.Visibility = Visibility.Hidden;
            gridLekoviDodaj.Visibility = Visibility.Visible;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridSviLekoviZaZamenski.SelectedIndex != -1)
            {
                List<Lek> sviLekovi = (List<Lek>)dataGridSviLekoviZaZamenski.ItemsSource;
                List<Lek> zamenSkiLekovi = (List<Lek>)dataGridZamenskiUbaceniLekovi.ItemsSource;
                zamenSkiLekovi.Add((Lek)dataGridSviLekoviZaZamenski.SelectedItem);
                sviLekovi.Remove((Lek)dataGridSviLekoviZaZamenski.SelectedItem);
                dataGridZamenskiUbaceniLekovi.Items.Refresh();
                dataGridSviLekoviZaZamenski.Items.Refresh();
            }
        }

        private void btnPotvrdiZamenske_Click(object sender, RoutedEventArgs e)
        {
            LekZaOdobravanje noviLek = LekZaOdobravanje.getInstance();
            var provera = (List<Lek>)dataGridZamenskiUbaceniLekovi.ItemsSource;
            noviLek.zamenskiLekovi = (List<Lek>)dataGridZamenskiUbaceniLekovi.ItemsSource;
            gridLekoviDodajZamenskeLekove.Visibility = Visibility.Hidden;
            gridLekoviDodaj.Visibility = Visibility.Visible;
        }

        private void btnPotvrdiLekare_Click(object sender, RoutedEventArgs e)
        {
            lekariSlanje.Visibility = Visibility.Hidden;
            if (dataGridLekariZaSlanje.SelectedItems.Count < 2)
            {
                lekariSlanje.Visibility = Visibility.Visible;
            }
            else
            {
                List<String> lekariId = new List<String>();
                foreach (var selektovan in dataGridLekariZaSlanje.SelectedItems)
                {
                    Lekar lekar = (Lekar)selektovan;
                    lekariId.Add(lekar.id);
                    lekariBroj++;
                }

                LekZaOdobravanje noviLek = LekZaOdobravanje.getInstance();
                noviLek.lekariKojimaJePoslatLek = lekariId;
                gridIzaberiLekareZaSlanje.Visibility = Visibility.Hidden;
                gridLekoviDodaj.Visibility = Visibility.Visible;
            }
            
        }

        private void btnOtkaziLekare_Click(object sender, RoutedEventArgs e)
        {
            gridIzaberiLekareZaSlanje.Visibility = Visibility.Hidden;
            gridLekoviDodaj.Visibility = Visibility.Visible;
        }
    }
}
