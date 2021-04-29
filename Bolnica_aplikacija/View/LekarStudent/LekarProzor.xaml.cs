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
using System.Windows.Shapes;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using Model;
using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.LekarStudent;

namespace Bolnica_aplikacija
{
    /// <summary>
    /// Interaction logic for LekarProzor.xaml
    /// </summary>
    public partial class LekarProzor : Window
    {
        private static ContentControl x;
        private static Button nazad;
        private static Button pretraga;
        private static Label glavnaLabela;
        private String prethodniSadrzajGlavneLabele;
        private Visibility btnNazadVisibility;
        private Visibility btnPretragaVisibility;
        public LekarProzor()
        {
            InitializeComponent();
            this.contentControl.Content = new LekarTabovi();
            x = this.contentControl;
            lblGlavna.Content = "Zdravo korporacija";
            glavnaLabela = this.lblGlavna;
            nazad = this.btnNazad;
            pretraga = this.btnPretraga;
            btnNazad.Visibility = Visibility.Hidden;
        }

        public static Label getGlavnaLabela()
        {
            return glavnaLabela;
        }
        public static Button getPretraga()
        {
            return pretraga;
        }
        public static Button getNazad()
        {
            return nazad;
        }
        public static ContentControl getX()
        {
            return x;
        }

        private void meniOdjava_Click(object sender, RoutedEventArgs e)
        {
            Prijava prijava = new Prijava();
            this.Close();
            prijava.ShowDialog();
        }

        private void btnNazad_Click(object sender, RoutedEventArgs e)
        {
            if(gridOdobravanjeLekova.Visibility == Visibility.Visible)
            {
                podesiKretanjeUnazadZaOdobravanjeLekova();
            }
            else if(gridRULekovi.Visibility == Visibility.Visible)
            {
                podesiKretanjeUnazadZaRULekova();
            }
            else if (PacijentInfo.aktivanPacijentInfo)
            {
                lblGlavna.Content = "Zdravo korporacija";
                LekarProzor.getX().Content = new LekarTabovi();
                LekarTabovi.getTab().SelectedIndex = 1;
                btnPretraga.Visibility = Visibility.Visible;
                btnNazad.Visibility = Visibility.Hidden;
                PacijentInfo.aktivanPacijentInfo = false;
            }
            else if (ZakaziTermin.aktivan)
            {
                LekarProzor.getX().Content = new PacijentInfo();
                PacijentInfo.getTab().SelectedIndex = 2;
                PacijentInfo.aktivanPacijentInfo = true;
                ZakaziTermin.aktivan = false;
            }
            else if (PrikazProstorija.aktivan)
            {
                LekarProzor.getX().Content = new ZakaziTermin(ZakaziTermin.getTipAkcije());
                PrikazProstorija.aktivan = false;
            }
            else if (Izvestaj.aktivan)
            {
                Izvestaj.podesiKretanjeZaDugmeNazad();
            }
            else if (IstorijaBolesti.aktivan)
            {
                LekarProzor.getX().Content = new PacijentInfo();
                IstorijaBolesti.aktivan = false;
            }
            else if (IzmenaBolesti.aktivan)
            {
                IzmenaBolesti.podesiKretanjeZaDugmeNazad();
            }
            else if (Alergije.aktivan)
            {
                LekarProzor.getX().Content = new PacijentInfo();
                Alergije.aktivan = false;

            }
            else if (UvidUTerapije.aktivan)
            {
                LekarProzor.getX().Content = new PacijentInfo();
                UvidUTerapije.aktivan = false;
            }
            else if (TerapijeIzdavanjeRecpeta.aktivan)
            {
                if(TerapijeIzdavanjeRecpeta.getGridLekovi().Visibility == Visibility.Visible)
                {
                    TerapijeIzdavanjeRecpeta.getGridLekovi().Visibility = Visibility.Hidden;
                    glavnaLabela.Content = "Izdavanje recepta";
                }
                else
                {
                    LekarProzor.getX().Content = new UvidUTerapije();
                    TerapijeIzdavanjeRecpeta.aktivan = false;
                }
            }
            else if (ZakazivanjeOperacije.aktivan)
            {
                ZakazivanjeOperacije.podesiKretanjeZaNazad();
            }
        }

        private void btnPretraga_Click(object sender, RoutedEventArgs e)
        {
            if (LekarTabovi.getTab().SelectedIndex == 0)
            {
                if (LekarTabovi.getRasporedPretraga().Visibility == Visibility.Visible)
                {
                    LekarTabovi.getRasporedPretraga().Visibility = Visibility.Hidden;
                }
                else
                {
                    LekarTabovi.getRasporedPretraga().Visibility = Visibility.Visible;
                }


            }
            else if (LekarTabovi.getTab().SelectedIndex == 1)
            {
                if (LekarTabovi.getPacijentiPretraga().Visibility == Visibility.Visible)
                {
                    LekarTabovi.getPacijentiPretraga().Visibility = Visibility.Hidden;
                }
                else
                {
                    LekarTabovi.getPacijentiPretraga().Visibility = Visibility.Visible;
                }
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {

        }

        private void odobriLekove_Click(object sender, RoutedEventArgs e)
        {
            if (gridRULekovi.Visibility == Visibility.Hidden)
            {
                sacuvajStareVrednosti();
            }
            gridRULekovi.Visibility = Visibility.Hidden;
            glavnaLabela.Content = "Odobravanje lekova";
            gridOdobravanjeLekova.Visibility = Visibility.Visible;
            btnNazad.Visibility = Visibility.Visible;
            btnPretraga.Visibility = Visibility.Hidden;
        }

        private void btnInfoLek_Click(object sender, RoutedEventArgs e)
        {
            glavnaLabela.Content = "Informacije o leku";
            gridInfoLek.Visibility = Visibility.Visible;
        }

        private void sacuvajStareVrednosti()
        {
            prethodniSadrzajGlavneLabele = (String)glavnaLabela.Content;
            btnNazadVisibility = btnNazad.Visibility;
            btnPretragaVisibility = btnPretraga.Visibility;
        }

        private void podesiKretanjeUnazadZaOdobravanjeLekova()
        {
            if (gridInfoLek.Visibility == Visibility.Visible)
            {
                gridInfoLek.Visibility = Visibility.Hidden;
                glavnaLabela.Content = "Odobravanje lekova";
            }
            else if(gridRUZamenskiLekovi.Visibility == Visibility.Visible)
            {
                gridRUZamenskiLekovi.Visibility = Visibility.Hidden;
                gridInfoLek.Visibility = Visibility.Visible;
                glavnaLabela.Content = "Informacije o leku";
            }
            else
            {
                gridOdobravanjeLekova.Visibility = Visibility.Hidden;
                glavnaLabela.Content = prethodniSadrzajGlavneLabele;
                btnNazad.Visibility = btnNazadVisibility;
                btnPretraga.Visibility = btnPretragaVisibility;
            }
           
        }

        private void RULekovi_Click(object sender, RoutedEventArgs e)
        {
            if (gridOdobravanjeLekova.Visibility == Visibility.Hidden)
            {
                sacuvajStareVrednosti();
            }
            ucitavanjePostojecihLekova();
            gridOdobravanjeLekova.Visibility = Visibility.Hidden;
            gridRULekovi.Visibility = Visibility.Visible;
            glavnaLabela.Content = "Pregled i izmena lekova";
            btnNazad.Visibility = Visibility.Visible;
            btnPretraga.Visibility = Visibility.Hidden;
        }

        private void ucitavanjePostojecihLekova()
        {
            dataGridPostojeciLekovi.ItemsSource = LekKontroler.ucitajSve();
        }

        private void podesiKretanjeUnazadZaRULekova()
        {
            if (gridIzmenaLekova.Visibility == Visibility.Visible)
            {
                glavnaLabela.Content = "Pregled i izmena lekova";
                gridIzmenaLekova.Visibility = Visibility.Hidden;
            }
            else if (gridRUZamenskiLekovi.Visibility == Visibility.Visible)
            {
                gridRUZamenskiLekovi.Visibility = Visibility.Hidden;
                gridIzmenaLekova.Visibility = Visibility.Visible;
                glavnaLabela.Content = "Informacije o leku";
            }
            else
            {
                gridRULekovi.Visibility = Visibility.Hidden;
                glavnaLabela.Content = prethodniSadrzajGlavneLabele;
                btnNazad.Visibility = btnNazadVisibility;
                btnPretraga.Visibility = btnPretragaVisibility;

            }
        }

        private void btnIzmeniPostojeciLek_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridPostojeciLekovi.SelectedIndex != -1)
            {
                Lek izabraniLek = (Lek)dataGridPostojeciLekovi.SelectedItem;
                lblRUNacinUpotrebe.Content = izabraniLek.getNacinUpotrebeString();
                lblRUTipLeka.Content = izabraniLek.getTipString();
                lblRUProizvodjac.Content = izabraniLek.proizvodjac;
                lblRUNazivLeka.Content = izabraniLek.naziv;
                gridIzmenaLekova.Visibility = Visibility.Visible;
                glavnaLabela.Content = "Izmena leka";
            }
        }

        private void btnRUZamenskiLekovi_Click(object sender, RoutedEventArgs e)
        {
            gridIzmenaLekova.Visibility = Visibility.Hidden;
            btnDodajZamenskiLek.Visibility = Visibility.Visible;
            gridRUZamenskiLekovi.Visibility = Visibility.Visible;
            glavnaLabela.Content = "Zamenski lekovi";
        }

        private void btnZamenskiLekovi_Click(object sender, RoutedEventArgs e)
        {
            gridRUZamenskiLekovi.Visibility = Visibility.Visible;
            btnDodajZamenskiLek.Visibility = Visibility.Hidden;
            gridInfoLek.Visibility = Visibility.Hidden;
            glavnaLabela.Content = "Zamenski lekovi";
        }

        private void btnDodajZamenskiLek_Click(object sender, RoutedEventArgs e)
        {
            gridDodavanjeZamenskogLeka.Visibility = Visibility.Visible;
            glavnaLabela.Content = "Dodavanje zamenskog leka";
        }
    }
}
