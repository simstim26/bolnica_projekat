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
using Model;
using System.Text.RegularExpressions;
using Bolnica_aplikacija.PomocneKlase;

namespace Bolnica_aplikacija.View.UpravnikStudent
{
    /// <summary>
    /// Interaction logic for DodajProstoriju.xaml
    /// </summary>
    public partial class DodajProstoriju : UserControl
    {
        public DodajProstoriju()
        {
            InitializeComponent();
            unosBrojaProstorije.Clear();
            unosSprata.Clear();
            cbTipProstorije.SelectedIndex = -1;
            lblBrojPostojiDodaj.Visibility = Visibility.Hidden;
        }

        private void btnPotvrdi_Click(object sender, RoutedEventArgs e)
        {
            lblNijePopunjenoDodaj.Visibility = Visibility.Hidden;
            lblMoraBitiBroj.Visibility = Visibility.Hidden;
            lblNijePopunjenoIspravnoDodaj.Visibility = Visibility.Hidden;
            lblBrojPostojiDodaj.Visibility = Visibility.Hidden;
            String pat = @"^[0-9]+$";
            Regex r = new Regex(pat);
            Match m = r.Match(unosBrojaProstorije.Text.Replace(" ", ""));
            Match m1 = r.Match(unosSprata.Text.Replace(" ", ""));
            if (String.IsNullOrEmpty(unosBrojaProstorije.Text) || String.IsNullOrEmpty(unosSprata.Text) || cbTipProstorije.SelectedIndex == -1)
            {
                lblNijePopunjenoDodaj.Visibility = Visibility.Visible;
            }
            else if (!m.Success || !m1.Success)
            {
                lblMoraBitiBroj.Visibility = Visibility.Visible;
                unosBrojaProstorije.Clear();
                unosSprata.Clear();
            }
            else
            {
                ProstorijaDTO prostorija = new ProstorijaDTO();
                prostorija.broj = unosBrojaProstorije.Text;
                prostorija.sprat = Int32.Parse(unosSprata.Text);
                if (cbTipProstorije.SelectedIndex == 0)
                {
                    prostorija.tipProstorije = TipProstorije.BOLNICKA_SOBA;
                }
                else if (cbTipProstorije.SelectedIndex == 1)
                {
                    prostorija.tipProstorije = TipProstorije.OPERACIONA_SALA;
                }
                else if (cbTipProstorije.SelectedIndex == 2)
                {
                    prostorija.tipProstorije = TipProstorije.SOBA_ZA_PREGLED;
                }

                bool provera = ProstorijaKontroler.NapraviProstoriju(prostorija);
                if (!provera)
                {
                    lblBrojPostojiDodaj.Visibility = Visibility.Visible;
                    unosBrojaProstorije.Clear();
                    unosSprata.Clear();
                }
                else
                {
                    GlavniProzor.DobaviProzorZaIzmenu().Children.Clear();
                    GlavniProzor.DobaviProzorZaIzmenu().Children.Add(new ProstorijePogled());
                }

            }
        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            GlavniProzor.DobaviProzorZaIzmenu().Children.Clear();
            GlavniProzor.DobaviProzorZaIzmenu().Children.Add(new ProstorijePogled());
        }

        
    }
}
