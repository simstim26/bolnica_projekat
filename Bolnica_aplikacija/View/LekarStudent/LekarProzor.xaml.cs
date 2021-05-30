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
using Bolnica_aplikacija.Model;
using Bolnica_aplikacija.PomocneKlase;
using Bolnica_aplikacija.View.LekarStudent;
using Bolnica_aplikacija.ViewModel;

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

            this.DataContext = new LekZaOdobravanjeViewModel();

            this.contentControl.Content = new LekarTabovi();
            x = this.contentControl;
            lblGlavna.Content = "Zdravo korporacija";
            glavnaLabela = this.lblGlavna;
            nazad = this.btnNazad;
            pretraga = this.btnPretraga;
            btnNazad.Visibility = Visibility.Hidden;
            if (!LekKontroler.proveriLekoveZaOdobravanjeZaLogovanogLekara(KorisnikKontroler.getLekar().id))
            {
                postojeLekoviZaOdobravanje.Visibility = Visibility.Visible;
            }
            btnDodavanjeSastojka.IsEnabled = false;
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
                LekarProzor.getX().Content = new PacijentInfo(((String[])PacijentInfo.getFM().DataContext)[0],
                    ((String[])PacijentInfo.getFM().DataContext)[1]);
                PacijentInfo.getTab().SelectedIndex = 2;
                PacijentInfo.aktivanPacijentInfo = true;
                ZakaziTermin.aktivan = false;
            }
            else if (PrikazProstorija.aktivan)
            {
                LekarProzor.getX().Content = new ZakaziTermin(ZakaziTermin.getTipAkcije(), ((String[])ZakaziTermin.getFM().DataContext)[0]
                    , ((String[])ZakaziTermin.getFM().DataContext)[1]);
                PrikazProstorija.aktivan = false;
            }
            else if (Izvestaj.aktivan)
            {
                Izvestaj.podesiKretanjeZaDugmeNazad();
            }
            else if (IstorijaBolesti.aktivan)
            {
                LekarProzor.getX().Content = new PacijentInfo(((String[])PacijentInfo.getFM().DataContext)[0],
                                   ((String[])PacijentInfo.getFM().DataContext)[1]);
                IstorijaBolesti.aktivan = false;
            }
            else if (IzmenaBolesti.aktivan)
            {
                IzmenaBolesti.podesiKretanjeZaDugmeNazad();
            }
            else if (Alergije.aktivan)
            {
                LekarProzor.getX().Content = new PacijentInfo(((String[])PacijentInfo.getFM().DataContext)[0],
                                   ((String[])PacijentInfo.getFM().DataContext)[1]);
                Alergije.aktivan = false;

            }
            else if (UvidUTerapije.aktivan)
            {
                LekarProzor.getX().Content = new PacijentInfo(((String[])PacijentInfo.getFM().DataContext)[0],
                   ((String[])PacijentInfo.getFM().DataContext)[1]);
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
                    LekarProzor.getX().Content = new UvidUTerapije(((BolestTerapija)TerapijeIzdavanjeRecpeta.getFM().DataContext).idPacijenta);
                    TerapijeIzdavanjeRecpeta.aktivan = false;
                }
            }
            else if (ZakazivanjeOperacije.aktivan)
            {
                ZakazivanjeOperacije.podesiKretanjeZaNazad();
            }
            else if (IzmenaBLecenja.aktivan)
            {
                LekarProzor.getX().Content = new PacijentInfo(((String[])PacijentInfo.getFM().DataContext)[0],
                   ((String[])PacijentInfo.getFM().DataContext)[1]);
                IzmenaBLecenja.aktivan = false;
            }
            else if (ProsliTermini.aktivan)
            {
                LekarProzor.getX().Content = new PacijentInfo(((String[])PacijentInfo.getFM().DataContext)[0],
                                  ((String[])PacijentInfo.getFM().DataContext)[1]);
                PacijentInfo.getTab().SelectedIndex = 1;
                ProsliTermini.aktivan = false;
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

        private void odobriLekove_Click(object sender, RoutedEventArgs e)
        {

            if (gridRULekovi.Visibility == Visibility.Hidden)
            {
                sacuvajStareVrednosti();
            }
            gridRULekovi.Visibility = Visibility.Hidden;
            gridDodavanjeZamenskogLeka.Visibility = Visibility.Hidden;
            gridRUZamenskiLekovi.Visibility = Visibility.Hidden;
            gridIzmenaLekova.Visibility = Visibility.Hidden;

            glavnaLabela.Content = "Odobravanje lekova";
            gridOdobravanjeLekova.Visibility = Visibility.Visible;
            btnNazad.Visibility = Visibility.Visible;
            btnPretraga.Visibility = Visibility.Hidden;

        }

        private void btnInfoLek_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridLekoviZaOdobravanje.SelectedIndex != -1)
            {
                glavnaLabela.Content = "Informacije o leku";
                gridInfoLek.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Potrebno je izabrati lek!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void sacuvajStareVrednosti()
        {
            prethodniSadrzajGlavneLabele = (String)glavnaLabela.Content;
            btnNazadVisibility = btnNazad.Visibility;
            btnPretragaVisibility = btnPretraga.Visibility;
        }

        private void podesiKretanjeUnazadZaOdobravanjeLekova()
        {
          
            if(gridRUZamenskiLekovi.Visibility == Visibility.Visible)
            {
                gridRUZamenskiLekovi.Visibility = Visibility.Hidden;
                gridInfoLek.Visibility = Visibility.Visible;
                glavnaLabela.Content = "Informacije o leku";
            }
            else if(gridPropratnaPoruka.Visibility == Visibility.Visible)
            {
                txtPropratnaPoruka.Text = "";
                gridPropratnaPoruka.Visibility = Visibility.Hidden;
            }
            else if (gridInfoLek.Visibility == Visibility.Visible)
            {
                gridInfoLek.Visibility = Visibility.Hidden;
                glavnaLabela.Content = "Odobravanje lekova";
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
            gridRUZamenskiLekovi.Visibility = Visibility.Hidden;
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
           
            if (gridRUZamenskiLekovi.Visibility == Visibility.Visible)
            {
                if (gridDodavanjeZamenskogLeka.Visibility == Visibility.Visible)
                {
                    gridDodavanjeZamenskogLeka.Visibility = Visibility.Hidden;
                    glavnaLabela.Content = "Zamenski lekovi";

                }
                else
                {
                    gridRUZamenskiLekovi.Visibility = Visibility.Hidden;
                    gridIzmenaLekova.Visibility = Visibility.Visible;
                    glavnaLabela.Content = "Informacije o leku";
                }
            }
            else if (gridIzmenaLekova.Visibility == Visibility.Visible)
            {
                glavnaLabela.Content = "Pregled i izmena lekova";
                gridIzmenaLekova.Visibility = Visibility.Hidden;
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
                listRUSastojci.ItemsSource = izabraniLek.sastojci;

            }
            else
            {
                MessageBox.Show("Potrebno je izabrati lek!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnRUZamenskiLekovi_Click(object sender, RoutedEventArgs e)
        {
            gridIzmenaLekova.Visibility = Visibility.Hidden;
            btnDodajZamenskiLek.Visibility = Visibility.Visible;
            btnObrisiZamenskiLek.Visibility = Visibility.Visible;
            gridRUZamenskiLekovi.Visibility = Visibility.Visible;
            glavnaLabela.Content = "Zamenski lekovi";
            dataGridZamenskiLekovi.ItemsSource = ((Lek)dataGridPostojeciLekovi.SelectedItem).zamenskiLekovi;
        }

        private void btnZamenskiLekovi_Click(object sender, RoutedEventArgs e)
        {
            gridRUZamenskiLekovi.Visibility = Visibility.Visible;
            btnDodajZamenskiLek.Visibility = Visibility.Hidden;
            btnObrisiZamenskiLek.Visibility = Visibility.Hidden;
            glavnaLabela.Content = "Zamenski lekovi";
            dataGridZamenskiLekovi.ItemsSource = ((LekZaOdobravanje)dataGridLekoviZaOdobravanje.SelectedItem).zamenskiLekovi;
        }

        private void btnDodajZamenskiLek_Click(object sender, RoutedEventArgs e)
        {
            gridDodavanjeZamenskogLeka.Visibility = Visibility.Visible;
            glavnaLabela.Content = "Dodavanje zamenskog leka";
            dataGridDodavanjeZamenskogLeka.ItemsSource = LekKontroler.ucitajSveLekoveBezZamenskih(((Lek)dataGridPostojeciLekovi.SelectedItem).id);
        }

        private void PopupBox_Opened(object sender, RoutedEventArgs e)
        {
            if (!LekKontroler.proveriLekoveZaOdobravanjeZaLogovanogLekara(KorisnikKontroler.getLekar().id))
            {
                odobriLekove.Content = "! Odobri lekova";
            }
            postojeLekoviZaOdobravanje.Visibility = Visibility.Hidden;
        }

        private void PopupBox_Closed(object sender, RoutedEventArgs e)
        {
            if (!LekKontroler.proveriLekoveZaOdobravanjeZaLogovanogLekara(KorisnikKontroler.getLekar().id))
            {
                postojeLekoviZaOdobravanje.Visibility = Visibility.Visible;
            }
            else
            {
                odobriLekove.Content = "Odobri lekova";

            }
        }

        private void btnOdbaci_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridLekoviZaOdobravanje.SelectedIndex != -1)
            {
                gridPropratnaPoruka.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Potrebno je izabrati lek!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
        }

        private void btnPotvrdaUnosaPropratnePoruke_Click(object sender, RoutedEventArgs e)
        {
           // LekZaOdobravanje lek = (LekZaOdobravanje)dataGridLekoviZaOdobravanje.SelectedItem;
            //lek.propratnaPoruka = txtPropratnaPoruka.Text;
            //LekKontroler.odbacivanjeLeka(lek);
            //gridPropratnaPoruka.Visibility = Visibility.Hidden;
        }

        private void btnDodavanjeSastojka_Click(object sender, RoutedEventArgs e)
        {
            int id = dataGridPostojeciLekovi.SelectedIndex;
            LekKontroler.dodajSastojak(((Lek)dataGridPostojeciLekovi.SelectedItem).id, txtDodavanjeSastojaka.Text);
            osveziPrikaz(id);
            txtDodavanjeSastojaka.Text = "";
        }

        private void txtDodavanjeSastojaka_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtDodavanjeSastojaka.Text))
            {
                btnDodavanjeSastojka.IsEnabled = false;
            }
            else
            {
                btnDodavanjeSastojka.IsEnabled = true;
            }
        }

        private void btnIzbrisiSastojak_Click(object sender, RoutedEventArgs e)
        {
            if(listRUSastojci.SelectedIndex != -1)
            {
                int id = dataGridPostojeciLekovi.SelectedIndex;
                LekKontroler.izbrisiSastojak(((Lek)dataGridPostojeciLekovi.SelectedItem).id, (String)listRUSastojci.SelectedItem);
                osveziPrikaz(id);
            }
            else
            {
                MessageBox.Show("Izaberite sastojak za brisanje", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void osveziPrikaz(int id)
        {
            dataGridPostojeciLekovi.ItemsSource = LekKontroler.ucitajSve();
            dataGridPostojeciLekovi.SelectedIndex = id;
            dataGridZamenskiLekovi.ItemsSource = ((Lek)dataGridPostojeciLekovi.SelectedItem).zamenskiLekovi;
            dataGridDodavanjeZamenskogLeka.ItemsSource = LekKontroler.ucitajSveLekoveBezZamenskih(((Lek)dataGridPostojeciLekovi.SelectedItem).id);
            listRUSastojci.ItemsSource = ((Lek)dataGridPostojeciLekovi.SelectedItem).sastojci;
        }

        private void btnPotvrdaZamenskogLeka_Click(object sender, RoutedEventArgs e)
        {
            if(dataGridDodavanjeZamenskogLeka.SelectedIndex != -1)
            {
                int id = dataGridPostojeciLekovi.SelectedIndex;
                LekKontroler.dodajZamenskiLek(((Lek)dataGridPostojeciLekovi.SelectedItem).id,(Lek)dataGridDodavanjeZamenskogLeka.SelectedItem);
                osveziPrikaz(id);     
            }
            else
            {
                MessageBox.Show("Izaberite lek za dodavanje.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnObrisiZamenskiLek_Click(object sender, RoutedEventArgs e)
        {
            if(dataGridZamenskiLekovi.SelectedIndex != -1)
            {
                int id = dataGridPostojeciLekovi.SelectedIndex;
                LekKontroler.obrisiZamenskiLek(((Lek)dataGridPostojeciLekovi.SelectedItem).id, ((Lek)dataGridZamenskiLekovi.SelectedItem).id);
                osveziPrikaz(id);
            }
            else
            {
                MessageBox.Show("Izaberite lek za brisanje.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
