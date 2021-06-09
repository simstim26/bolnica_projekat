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
    /// Interaction logic for IzmenaOdbijenog.xaml
    /// </summary>
    public partial class IzmenaOdbijenog : UserControl
    {
        public IzmenaOdbijenog()
        {
            InitializeComponent();
            cbTipLekaDodavanjeOdbijenog.ItemsSource = LekKontroler.tipLeka();
            comboBoxNacinUpotrebeOdbijenog.ItemsSource = LekKontroler.nacinUpotrebeLeka();

            LekZaOdobravanje odbijeniLek = (LekZaOdobravanje)OdbijeniLekovi.dobaviDataGridOdbijenihLekova().SelectedItem;

            textBoxNazivLekaUnosOdbijenog.Text = odbijeniLek.naziv;
            cbTipLekaDodavanjeOdbijenog.SelectedItem = odbijeniLek.tip;
            textBoxKolicinaLekaUnosOdbijenog.Text = odbijeniLek.kolicina.ToString();
            textBoxProizvodjacLekaUnosOdbijenog.Text = odbijeniLek.proizvodjac;
            comboBoxNacinUpotrebeOdbijenog.SelectedItem = odbijeniLek.nacinUpotrebe;
        }

        private void btnDodajSastojkeOdbijenog_Click(object sender, RoutedEventArgs e)
        {
            gridLekoviIzmeniSastojkeOdbijenog.Visibility = Visibility.Visible;
            gridLekoviIzmenaOdbijenih.Visibility = Visibility.Hidden;
            LekZaOdobravanje odbijeniLek = (LekZaOdobravanje)OdbijeniLekovi.dobaviDataGridOdbijenihLekova().SelectedItem;
            List<String> sastojci = odbijeniLek.sastojci;
            dataGridDodajSastojkeOdbijenog.ItemsSource = sastojci;
        }

        private void btnDodajZamenskeLekoveOdbijenog_Click(object sender, RoutedEventArgs e)
        {
            gridLekoviIzmenaOdbijenih.Visibility = Visibility.Hidden;
            gridLekoviIzmeniZamenskeLekoveOdbijenog.Visibility = Visibility.Visible;

            LekZaOdobravanje odbijeniLek = (LekZaOdobravanje)OdbijeniLekovi.dobaviDataGridOdbijenihLekova().SelectedItem;
            dataGridZamenskiUbaceniLekoviOdbijenog.ItemsSource = odbijeniLek.zamenskiLekovi;
            dataGridSviLekoviZaZamenskiOdbijenog.ItemsSource = LekKontroler.ucitajSve();
        }

        private void btnPotvrdiDodavanjeLekovaOdbijenog_Click(object sender, RoutedEventArgs e)
        {
            svaPolja.Visibility = Visibility.Hidden;
            jacinBroj.Visibility = Visibility.Hidden;

            String pat = @"^[0-9]+$";
            Regex r = new Regex(pat);
            Match m = r.Match(textBoxKolicinaLekaUnosOdbijenog.Text.Replace(" ", ""));

            if (String.IsNullOrEmpty(textBoxNazivLekaUnosOdbijenog.Text) || cbTipLekaDodavanjeOdbijenog.SelectedIndex == -1 ||
                String.IsNullOrEmpty(textBoxProizvodjacLekaUnosOdbijenog.Text) || String.IsNullOrEmpty(textBoxKolicinaLekaUnosOdbijenog.Text) ||
                comboBoxNacinUpotrebeOdbijenog.SelectedIndex == -1)
            {
                svaPolja.Visibility = Visibility.Visible;
            }
            else if (!m.Success)
            {
                jacinBroj.Visibility = Visibility.Visible;
            }
            else
            {
                TipLeka tipLeka = (TipLeka)cbTipLekaDodavanjeOdbijenog.SelectedItem;
                NacinUpotrebe nacinUpotrebe = (NacinUpotrebe)comboBoxNacinUpotrebeOdbijenog.SelectedItem;

                LekZaOdobravanje odbijeniLek = (LekZaOdobravanje)OdbijeniLekovi.dobaviDataGridOdbijenihLekova().SelectedItem;
                LekKontroler.fizickiObrisiLekZaOdbacivanje(odbijeniLek);

                odbijeniLek.naziv = textBoxNazivLekaUnosOdbijenog.Text;
                odbijeniLek.tip = tipLeka;
                odbijeniLek.kolicina = Int32.Parse(textBoxKolicinaLekaUnosOdbijenog.Text);
                odbijeniLek.proizvodjac = textBoxProizvodjacLekaUnosOdbijenog.Text;
                odbijeniLek.nacinUpotrebe = nacinUpotrebe;
                odbijeniLek.id = (LekKontroler.ucitajLekoveZaOdobravanje().Count() + 1).ToString();

                LekKontroler.napraviCeoLek(odbijeniLek);
            }
            
        }

        private void btnOtkaziDodavanjeLekaOdbijenog_Click(object sender, RoutedEventArgs e)
        {
            GlavniProzor.DobaviProzorZaIzmenu().Children.Clear();
            GlavniProzor.DobaviProzorZaIzmenu().Children.Add(new OdbijeniLekovi());
        }

        private void btnSastojakUnesiOdbijenog_Click(object sender, RoutedEventArgs e)
        {
            prazanSastojak.Visibility = Visibility.Hidden;
            if (String.IsNullOrEmpty(textBoxUpisiSastojakOdbijenog.Text))
            {
                prazanSastojak.Visibility = Visibility.Visible;
            }
            else
            {
                String sastojak = textBoxUpisiSastojakOdbijenog.Text;
                List<String> sastojci = (List<String>)dataGridDodajSastojkeOdbijenog.ItemsSource;
                sastojci.Add(sastojak);

                dataGridDodajSastojkeOdbijenog.ItemsSource = sastojci;
                dataGridDodajSastojkeOdbijenog.Items.Refresh();
                textBoxUpisiSastojakOdbijenog.Clear();
            }   
        }

        private void btnIzbrisiSastojakOdbijenog_Click(object sender, RoutedEventArgs e)
        {
            izaberiteZamenski.Visibility = Visibility.Hidden;
            svaPolja.Visibility = Visibility.Hidden;
            jacinBroj.Visibility = Visibility.Hidden;
            obrisiSastojak.Visibility = Visibility.Hidden;
            if (dataGridDodajSastojkeOdbijenog.SelectedIndex != -1)
            {
                List<String> sastojci = (List<String>)dataGridDodajSastojkeOdbijenog.ItemsSource;
                sastojci.Remove((String)dataGridDodajSastojkeOdbijenog.SelectedItem);
                dataGridDodajSastojkeOdbijenog.ItemsSource = sastojci;
                dataGridDodajSastojkeOdbijenog.Items.Refresh();
            }
            else
            {
                obrisiSastojak.Visibility = Visibility.Visible;

            }
        }

        private void btnPotvrdiSastojkeOdbijenog_Click(object sender, RoutedEventArgs e)
        {
            LekZaOdobravanje odbijeniLek = (LekZaOdobravanje)OdbijeniLekovi.dobaviDataGridOdbijenihLekova().SelectedItem;
            odbijeniLek.sastojci = (List<String>)dataGridDodajSastojkeOdbijenog.ItemsSource;

            gridLekoviIzmeniSastojkeOdbijenog.Visibility = Visibility.Hidden;
            gridLekoviIzmenaOdbijenih.Visibility = Visibility.Visible;
        }

        private void dodajZamenskiOdbijenog_Click(object sender, RoutedEventArgs e)
        {
            izaberiteZamenski.Visibility = Visibility.Hidden;
            svaPolja.Visibility = Visibility.Hidden;
            jacinBroj.Visibility = Visibility.Hidden;
            obrisiSastojak.Visibility = Visibility.Hidden;

            if (dataGridSviLekoviZaZamenskiOdbijenog.SelectedIndex != -1)
            {
                LekZaOdobravanje odbijeniLek = (LekZaOdobravanje)OdbijeniLekovi.dobaviDataGridOdbijenihLekova().SelectedItem;
                List<Lek> sviLekovi = (List<Lek>)dataGridSviLekoviZaZamenskiOdbijenog.ItemsSource;
                odbijeniLek.zamenskiLekovi.Add((Lek)dataGridSviLekoviZaZamenskiOdbijenog.SelectedItem);
                sviLekovi.Remove((Lek)dataGridSviLekoviZaZamenskiOdbijenog.SelectedItem);
                dataGridZamenskiUbaceniLekoviOdbijenog.ItemsSource = odbijeniLek.zamenskiLekovi;
                dataGridSviLekoviZaZamenskiOdbijenog.ItemsSource = sviLekovi;
                dataGridZamenskiUbaceniLekoviOdbijenog.Items.Refresh();
                dataGridSviLekoviZaZamenskiOdbijenog.Items.Refresh();
            }
            else
            {
                izaberiteZamenski.Visibility = Visibility.Visible;

            }
        }

        private void btnPotvrdiZamenskeOdbijenog_Click(object sender, RoutedEventArgs e)
        {
            LekZaOdobravanje odbijeniLek = (LekZaOdobravanje)OdbijeniLekovi.dobaviDataGridOdbijenihLekova().SelectedItem;
            odbijeniLek.zamenskiLekovi = (List<Lek>)dataGridZamenskiUbaceniLekoviOdbijenog.ItemsSource;
            gridLekoviIzmeniZamenskeLekoveOdbijenog.Visibility = Visibility.Hidden;
            gridLekoviIzmenaOdbijenih.Visibility = Visibility.Visible;
        }
    }
}
