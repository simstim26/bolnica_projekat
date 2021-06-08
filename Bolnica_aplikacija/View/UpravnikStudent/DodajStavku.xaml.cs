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
    /// Interaction logic for DodajStavku.xaml
    /// </summary>
    public partial class DodajStavku : UserControl
    {
        public DodajStavku()
        {
            InitializeComponent();
        }

        private void btnPotvrdiStavku_Click(object sender, RoutedEventArgs e)
        {
            labelNepravilnoUneseno.Visibility = Visibility.Hidden;
            kolicina.Visibility = Visibility.Visible;

            String pat = @"^[0-9]+$";
            Regex r = new Regex(pat);
            Match m = r.Match(textBoxKolicina.Text.Replace(" ", ""));

            if (!String.IsNullOrEmpty(textBoxNaziv.Text) && !String.IsNullOrEmpty(textBoxProizvodjac.Text) && m.Success && comboBoxTipOpreme.SelectedIndex != -1)
            {
                StavkaDTO stavka = new StavkaDTO();
                stavka.kolicina = Int32.Parse(textBoxKolicina.Text);
                stavka.naziv = textBoxNaziv.Text;
                stavka.proizvodjac = textBoxProizvodjac.Text;
                if (comboBoxTipOpreme.SelectedIndex == 0)
                {
                    stavka.jeStaticka = true;
                }
                else if (comboBoxTipOpreme.SelectedIndex == 1)
                {
                    stavka.jeStaticka = false;
                }

                if (checkBoxPotrosna.IsChecked == true)
                {
                    stavka.jePotrosnaRoba = true;
                }
                else if (checkBoxPotrosna.IsChecked == false)
                {
                    stavka.jePotrosnaRoba = false;
                }


                StavkaKontroler.DodajStavku(stavka);
                InventarPogled.dobaviDataGridInventar().ItemsSource = StavkaKontroler.UcitajNeobrisaneStavke();
                GlavniProzor.DobaviProzorZaIzmenu().Children.Clear();
                GlavniProzor.DobaviProzorZaIzmenu().Children.Add(new InventarPogled());
            }
            else if (!m.Success)
            {
                kolicina.Visibility = Visibility.Visible;
            }
            else
            {
                labelNepravilnoUneseno.Visibility = Visibility.Visible;
            }
        }

        private void btnOtkaziStavku_Click(object sender, RoutedEventArgs e)
        {
            GlavniProzor.DobaviProzorZaIzmenu().Children.Clear();
            GlavniProzor.DobaviProzorZaIzmenu().Children.Add(new InventarPogled());
        }
    }
}
