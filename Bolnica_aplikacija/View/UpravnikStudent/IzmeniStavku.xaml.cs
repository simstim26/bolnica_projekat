using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.PomocneKlase;
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
    /// Interaction logic for IzmeniStavku.xaml
    /// </summary>
    public partial class IzmeniStavku : UserControl
    {
        public IzmeniStavku()
        {
            InitializeComponent();
            var stavka = (Stavka)InventarPogled.dobaviDataGridInventar().SelectedItem;
            txtBoxKolicinaStavke.Text = stavka.kolicina.ToString();
            txtBoxNazivStavke.Text = stavka.naziv;
            txtBoxProizvodjacStavke.Text = stavka.proizvodjac;
            labelNepravilanUnosIzmena.Visibility = Visibility.Hidden;

            if (stavka.jePotrosnaRoba == true)
            {
                checkBoxPotrosnaIzmeni.IsChecked = true;
            }
            else if (stavka.jePotrosnaRoba == false)
            {
                checkBoxPotrosnaIzmeni.IsChecked = false;
            }

            if (stavka.jeStaticka == true)
            {
                cbTipStavke.SelectedIndex = 0;
            }
            else if (stavka.jeStaticka == false)
            {
                cbTipStavke.SelectedIndex = 1;
            }
        }

        private void btnPotvrdiIzmenuStavke_Click(object sender, RoutedEventArgs e)
        {
            labelNepravilanUnosIzmena.Visibility = Visibility.Hidden;
            kolicinaIzmena.Visibility = Visibility.Hidden;
            var stavka = (Stavka)InventarPogled.dobaviDataGridInventar().SelectedItem;

            String pat = @"^[0-9]+$";
            Regex r = new Regex(pat);
            Match m = r.Match(txtBoxKolicinaStavke.Text.Replace(" ", ""));

            if (!String.IsNullOrEmpty(txtBoxNazivStavke.Text) && !String.IsNullOrEmpty(txtBoxProizvodjacStavke.Text) && m.Success && cbTipStavke.SelectedIndex != -1)
            {
                StavkaDTO stavkaZaIzmenu = new StavkaDTO();
                stavkaZaIzmenu.naziv = txtBoxNazivStavke.Text;
                stavkaZaIzmenu.proizvodjac = txtBoxProizvodjacStavke.Text;
                stavkaZaIzmenu.kolicina = Int32.Parse(txtBoxKolicinaStavke.Text);
                if (cbTipStavke.SelectedIndex == 0)
                {
                    stavkaZaIzmenu.jeStaticka = true;
                }
                else if (cbTipStavke.SelectedIndex == 1)
                {
                    stavkaZaIzmenu.jeStaticka = false;
                }
                if (checkBoxPotrosnaIzmeni.IsChecked == true)
                {
                    stavkaZaIzmenu.jePotrosnaRoba = true;
                }
                else if (checkBoxPotrosnaIzmeni.IsChecked == false)
                {
                    stavkaZaIzmenu.jePotrosnaRoba = false;
                }
                stavkaZaIzmenu.id = ((Stavka)InventarPogled.dobaviDataGridInventar().SelectedItem).id;
                StavkaKontroler.IzmeniStavku(stavkaZaIzmenu);
                InventarPogled.dobaviDataGridInventar().ItemsSource = StavkaKontroler.UcitajNeobrisaneStavke();
                GlavniProzor.DobaviProzorZaIzmenu().Children.Clear();
                GlavniProzor.DobaviProzorZaIzmenu().Children.Add(new InventarPogled());
            }
            else if (!m.Success)
            {
                kolicinaIzmena.Visibility = Visibility.Visible;
            }
            else
            {
                labelNepravilanUnosIzmena.Visibility = Visibility.Visible;
            }
        }

        private void btnOtkaziIzmenuStavke_Click(object sender, RoutedEventArgs e)
        {
            GlavniProzor.DobaviProzorZaIzmenu().Children.Clear();
            GlavniProzor.DobaviProzorZaIzmenu().Children.Add(new InventarPogled());
        }
    }
}
