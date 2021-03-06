using Bolnica_aplikacija.Kontroler;
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
    /// Interaction logic for IzmeniLek.xaml
    /// </summary>
    public partial class IzmeniLek : UserControl
    {
        public IzmeniLek()
        {
            InitializeComponent();

            Lek lek = (Lek)LekoviProzor.dobaviDataGridLekova().SelectedItem;
            gridLekoviIzmena.Visibility = Visibility.Visible;
            cbTipLekaDodavanjeIzmena.ItemsSource = LekKontroler.tipLeka();
            comboBoxNacinUpotrebeIzmena.ItemsSource = LekKontroler.nacinUpotrebeLeka();

            textBoxNazivLekaUnosIzmena.Text = lek.naziv;
            textBoxKolicinaLekaUnosIzmena.Text = lek.kolicina.ToString();
            textBoxProizvodjacLekaUnosIzmena.Text = lek.proizvodjac;
            comboBoxNacinUpotrebeIzmena.SelectedItem = lek.nacinUpotrebe;
            cbTipLekaDodavanjeIzmena.SelectedItem = lek.tip;
        }

        private void btnDodajSastojkeIzmena_Click(object sender, RoutedEventArgs e)
        {
            gridLekoviIzmeniSastojke.Visibility = Visibility.Visible;
            gridLekoviIzmena.Visibility = Visibility.Hidden;
            dataGridDodajSastojkeIzmena.ItemsSource = ((Lek)LekoviProzor.dobaviDataGridLekova().SelectedItem).sastojci;
        }

        private void btnDodajZamenskeLekoveIzmena_Click(object sender, RoutedEventArgs e)
        {
            gridLekoviIzmena.Visibility = Visibility.Hidden;
            gridLekoviIzmeniZamenskeLekove.Visibility = Visibility.Visible;
            dataGridSviLekoviZaZamenskiIzmena.ItemsSource = LekKontroler.ucitajSve();
            dataGridZamenskiUbaceniLekoviIzmena.ItemsSource = ((Lek)LekoviProzor.dobaviDataGridLekova().SelectedItem).zamenskiLekovi;
        }

        private void btnPotvrdiDodavanjeLekovaIzmena_Click(object sender, RoutedEventArgs e)
        {

            svaPolja.Visibility = Visibility.Hidden;
            jacinBroj.Visibility = Visibility.Hidden;

            String pat = @"^[0-9]+$";
            Regex r = new Regex(pat);
            Match m = r.Match(textBoxKolicinaLekaUnosIzmena.Text.Replace(" ", ""));

            if (String.IsNullOrEmpty(textBoxNazivLekaUnosIzmena.Text) || cbTipLekaDodavanjeIzmena.SelectedIndex == -1 ||
                String.IsNullOrEmpty(textBoxProizvodjacLekaUnosIzmena.Text) || String.IsNullOrEmpty(textBoxKolicinaLekaUnosIzmena.Text) ||
                comboBoxNacinUpotrebeIzmena.SelectedIndex == -1)
            {
                svaPolja.Visibility = Visibility.Visible;
            }
            else if (!m.Success)
            {
                jacinBroj.Visibility = Visibility.Visible;
            }
            else
            {
                TipLeka tipLeka = (TipLeka)cbTipLekaDodavanjeIzmena.SelectedItem;
                NacinUpotrebe nacinUpotrebe = (NacinUpotrebe)comboBoxNacinUpotrebeIzmena.SelectedItem;

                Lek lek = (Lek)LekoviProzor.dobaviDataGridLekova().SelectedItem;

                lek.naziv = textBoxNazivLekaUnosIzmena.Text;
                lek.tip = tipLeka;
                lek.kolicina = Int32.Parse(textBoxKolicinaLekaUnosIzmena.Text);
                lek.proizvodjac = textBoxProizvodjacLekaUnosIzmena.Text;
                lek.nacinUpotrebe = nacinUpotrebe;
                lek.zamenskiLekovi = (List<Lek>)dataGridZamenskiUbaceniLekoviIzmena.ItemsSource;
                lek.sastojci = (List<String>)dataGridDodajSastojkeIzmena.ItemsSource;

                LekKontroler.azurirajLek(lek);
                LekoviProzor.dobaviDataGridLekova().Items.Refresh();
                GlavniProzor.DobaviProzorZaIzmenu().Children.Clear();
                GlavniProzor.DobaviProzorZaIzmenu().Children.Add(new LekoviProzor());
            }
            
        }

        private void btnOtkaziDodavanjeLekaOdbijenog_Click(object sender, RoutedEventArgs e)
        {
            GlavniProzor.DobaviProzorZaIzmenu().Children.Clear();
            GlavniProzor.DobaviProzorZaIzmenu().Children.Add(new LekoviProzor());
        }

        private void btnSastojakUnesiIzmena_Click(object sender, RoutedEventArgs e)
        {
            prazanSastojak.Visibility = Visibility.Hidden;
            if (String.IsNullOrEmpty(textBoxUpisiSastojakIzmena.Text))
            {
                prazanSastojak.Visibility = Visibility.Visible;
            }
            else
            {
                String sastojak = textBoxUpisiSastojakIzmena.Text;
                List<String> sastojci;
                if (((List<String>)dataGridDodajSastojkeIzmena.ItemsSource) == null)
                {
                    sastojci = new List<String>();
                }
                else
                {
                    sastojci = (List<String>)dataGridDodajSastojkeIzmena.ItemsSource;
                }

                sastojci.Add(sastojak);

                dataGridDodajSastojkeIzmena.ItemsSource = sastojci;
                dataGridDodajSastojkeIzmena.Items.Refresh();
                textBoxUpisiSastojakIzmena.Clear();
            }
        }

        private void btnIzbrisiSastojakIzmena_Click(object sender, RoutedEventArgs e)
        {
            izaberiteZamenski.Visibility = Visibility.Hidden;
            svaPolja.Visibility = Visibility.Hidden;
            jacinBroj.Visibility = Visibility.Hidden;
            obrisiSastojak.Visibility = Visibility.Hidden;
            if (dataGridDodajSastojkeIzmena.SelectedIndex != -1)
            {
                List<String> sastojci = (List<String>)dataGridDodajSastojkeIzmena.ItemsSource;
                sastojci.Remove((String)dataGridDodajSastojkeIzmena.SelectedItem);
                dataGridDodajSastojkeIzmena.ItemsSource = sastojci;
                dataGridDodajSastojkeIzmena.Items.Refresh();
            }
            else
            {
                obrisiSastojak.Visibility = Visibility.Visible;
            }
        }

        private void btnPotvrdiSastojkeIzmena_Click(object sender, RoutedEventArgs e)
        {
            Lek lek = (Lek)LekoviProzor.dobaviDataGridLekova().SelectedItem;
            lek.sastojci = (List<String>)dataGridDodajSastojkeIzmena.ItemsSource;

            gridLekoviIzmeniSastojke.Visibility = Visibility.Hidden;
            gridLekoviIzmena.Visibility = Visibility.Visible;
        }

        private void dodajZamenskiIzmena_Click(object sender, RoutedEventArgs e)
        {
            izaberiteZamenski.Visibility = Visibility.Hidden;
            svaPolja.Visibility = Visibility.Hidden;
            jacinBroj.Visibility = Visibility.Hidden;
            obrisiSastojak.Visibility = Visibility.Hidden;

            if (dataGridSviLekoviZaZamenskiIzmena.SelectedIndex  != -1)
            {
                Lek lek = (Lek)LekoviProzor.dobaviDataGridLekova().SelectedItem;
                List<Lek> sviLekovi = (List<Lek>)dataGridSviLekoviZaZamenskiIzmena.ItemsSource;
                if(lek.zamenskiLekovi == null)
                {
                    List<Lek> zamenski = new List<Lek>();
                    lek.zamenskiLekovi = zamenski;
                }
              
                lek.zamenskiLekovi.Add((Lek)dataGridSviLekoviZaZamenskiIzmena.SelectedItem);
                sviLekovi.Remove((Lek)dataGridSviLekoviZaZamenskiIzmena.SelectedItem);
                dataGridZamenskiUbaceniLekoviIzmena.ItemsSource = lek.zamenskiLekovi;
                dataGridSviLekoviZaZamenskiIzmena.ItemsSource = sviLekovi;
                dataGridZamenskiUbaceniLekoviIzmena.Items.Refresh();
                dataGridSviLekoviZaZamenskiIzmena.Items.Refresh();
            }
            else
            {
                izaberiteZamenski.Visibility = Visibility.Visible;
            }
            
        }

        private void btnPotvrdiZamenskeIzmena_Click(object sender, RoutedEventArgs e)
        {
            Lek lek = (Lek)LekoviProzor.dobaviDataGridLekova().SelectedItem;
            lek.zamenskiLekovi = (List<Lek>)dataGridZamenskiUbaceniLekoviIzmena.ItemsSource;
            gridLekoviIzmeniZamenskeLekove.Visibility = Visibility.Hidden;
            gridLekoviIzmena.Visibility = Visibility.Visible;
        }
    }
}
