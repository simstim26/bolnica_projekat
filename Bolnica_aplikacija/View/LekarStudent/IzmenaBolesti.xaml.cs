using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.PomocneKlase;
using Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bolnica_aplikacija.LekarStudent
{
    /// <summary>
    /// Interaction logic for IzmenaBolesti.xaml
    /// </summary>
    public partial class IzmenaBolesti : System.Windows.Controls.UserControl
    {
        public static bool aktivan;
        private static Grid gridRecept;
        private static Grid gridLekovi;
        private static FrameworkElement fm = new FrameworkElement();
        public IzmenaBolesti(BolestTerapija bolestTerapija)
        {
            InitializeComponent();
            LekarProzor.getGlavnaLabela().Content = "Ažuriranje bolesti";
            this.DataContext = bolestTerapija;
            fm.DataContext = this.DataContext;

            aktivan = true;
            this.txtNaziv.Text = bolestTerapija.nazivBolesti;
            this.txtTerapija.Text = bolestTerapija.nazivTerapije;
            this.txtIzvestaj.Text = bolestTerapija.izvestaj;

            Pacijent pacijent = PacijentKontroler.nadjiPacijenta(bolestTerapija.idPacijenta);

            this.lblRJmbg.Content = pacijent.jmbg;
            this.lblRImePrezime.Content = pacijent.ime + " " + pacijent.prezime;
            this.lblRDatumR.Content = pacijent.datumRodjenja.ToString("dd.MM.yyyy.");

            gridRecept = gridIzmenaTerapije;
            gridLekovi = gridOdabirLeka;
            this.btnPotvrdi.IsEnabled = false;
        }

        public static FrameworkElement getFM()
        {
            return fm;
        }

        public static void podesiKretanjeZaDugmeNazad()
        {
            if (gridLekovi.Visibility == Visibility.Visible)
            {
                gridLekovi.Visibility = Visibility.Hidden;
                gridRecept.Visibility = Visibility.Visible;
                LekarProzor.getGlavnaLabela().Content = "Izdavanje recepta";
            }
            else if (gridRecept.Visibility == Visibility.Visible)
            {
                gridRecept.Visibility = Visibility.Hidden;
                LekarProzor.getGlavnaLabela().Content = "Ažuriranje bolesti";
            }
            else
            {
                LekarProzor.getX().Content = new IstorijaBolesti(((BolestTerapija)fm.DataContext).idPacijenta);
                IzmenaBolesti.aktivan = false;
            }
        }

        private void btnPotvrdi_Click(object sender, RoutedEventArgs e)
        {
            TerminKontroler.azuriranjeIzvestajaZaTermin(txtIzvestaj.Text, ((BolestTerapija)fm.DataContext).idTermina);
            Content = new IstorijaBolesti(((BolestTerapija)fm.DataContext).idPacijenta);

        }

        private void txtIzvestaj_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnPotvrdi.IsEnabled = !String.IsNullOrWhiteSpace(txtIzvestaj.Text);
        }

        private void btnIzmeniTerapiju_Click(object sender, RoutedEventArgs e)
        {
            this.gridIzmenaTerapije.Visibility = Visibility.Visible;
            LekarProzor.getGlavnaLabela().Content = "Izdavanje recepta";
            txtDijagnoza.Text = ((BolestTerapija)fm.DataContext).nazivBolesti;
            txtNazivLeka.Text = ((BolestTerapija)fm.DataContext).nazivTerapije;
            Terapija terapija = TerapijaKontroler.nadjiTerapijuPoId(((BolestTerapija)fm.DataContext).idTerapije);
            txtTrajanje.Text = (terapija.trajanje).ToString();
            txtNacinUpotrebe.Text = terapija.nacinUpotrebe;
            txtKol.Text = (LekKontroler.nadjiLekPoId(terapija.idLeka).kolicina).ToString();
            txtDatum.Text = terapija.datumPocetka.ToString("dd.MM.yyyy.");
        }

        private void btnDodavanjeLeka_Click(object sender, RoutedEventArgs e)
        {
            this.gridIzmenaTerapije.Visibility = Visibility.Hidden;
            LekarProzor.getGlavnaLabela().Content = "Odabir leka";
            this.gridOdabirLeka.Visibility = Visibility.Visible;
            dataGridLekovi.ItemsSource = LekKontroler.ucitajSveSemTrenutnogNaTerapiji(
                TerapijaKontroler.nadjiTerapijuPoId(((BolestTerapija)fm.DataContext).idTerapije).idLeka);
        }

        private void btnPotvdiOdabirLeka_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridLekovi.SelectedIndex != -1)
            {
                if (PacijentKontroler.proveriAlergijuNaLekZaPacijenta(((BolestTerapija)fm.DataContext).idPacijenta, ((Lek)dataGridLekovi.SelectedItem).id))
                {
                    System.Windows.MessageBox.Show("Pacijent je alergičan na dati lek.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                    
                }
                else
                {
                    promeniTerapiju();
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Potrebno je izabrati lek!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void promeniTerapiju()
        {
            Lek lek = (Lek)dataGridLekovi.SelectedItem;
            LekarProzor.getGlavnaLabela().Content = "Izdavanje recepta";

            ((BolestTerapija)fm.DataContext).idLeka = lek.id;
            ((BolestTerapija)fm.DataContext).nazivTerapije = lek.naziv;

            this.txtTerapija.Text = ((BolestTerapija)fm.DataContext).nazivTerapije;
            this.gridIzmenaTerapije.Visibility = Visibility.Visible;
            this.gridOdabirLeka.Visibility = Visibility.Hidden;

            txtNazivLeka.Text = ((BolestTerapija)fm.DataContext).nazivTerapije;
            txtNacinUpotrebe.Text = lek.getNacinUpotrebeString();
            txtKol.Text = lek.kolicina.ToString();
            txtDatum.Text = DateTime.Now.ToString("dd.MM.yyyy.");
        }
        private void btnPotvrdiRecept_Click(object sender, RoutedEventArgs e)
        {
            this.gridIzmenaTerapije.Visibility = Visibility.Hidden;
            LekarProzor.getGlavnaLabela().Content = "Ažuriranje bolesti";
            if (((BolestTerapija)fm.DataContext).idTerapije != null)
            {
                Terapija terapija = TerapijaKontroler.nadjiTerapijuPoId(((BolestTerapija)fm.DataContext).idTerapije);
                TerapijaKontroler.azurirajTerapiju(new TerapijaDTO(terapija.id, ((BolestTerapija)fm.DataContext).idLeka, terapija.idPacijenta, terapija.idBolesti,
                    terapija.idTermina, DateTime.Now, Convert.ToInt32(txtTrajanje.Text), txtNacinUpotrebe.Text));

            }
            else
            {

                String idTerapije = TerapijaKontroler.dodajTerapiju(new TerapijaDTO("", ((BolestTerapija)fm.DataContext).idLeka, ((BolestTerapija)fm.DataContext).idPacijenta,
                   ((BolestTerapija)fm.DataContext).idBolesti, ((BolestTerapija)fm.DataContext).idTermina, DateTime.Now, Convert.ToInt32(txtTrajanje.Text), txtNacinUpotrebe.Text));

                TerminKontroler.azuriranjeTerapijeZaTermin(((BolestTerapija)fm.DataContext).idTermina, idTerapije);
                BolestKontroler.azurirajTerapijuZaBolest(((BolestTerapija)fm.DataContext).idBolesti, idTerapije);
            }
        }

        private void btnPonisti_Click(object sender, RoutedEventArgs e)
        {
            aktivan = false;
            Content = new IstorijaBolesti(((BolestTerapija)fm.DataContext).idPacijenta);
        }

        private void txtTrajanje_TextChanged(object sender, TextChangedEventArgs e)
        {
            Regex rx = new Regex("[^0-9]+");
            if (rx.IsMatch(txtTrajanje.Text))
            {
                lblGreskaTrajanje.Visibility = Visibility.Visible;
                potvrdaReceptEnable(false, !String.IsNullOrWhiteSpace(txtNacinUpotrebe.Text));
            }
            else
            {
                lblGreskaTrajanje.Visibility = Visibility.Hidden;
                potvrdaReceptEnable(true && !String.IsNullOrEmpty(txtTrajanje.Text), !String.IsNullOrWhiteSpace(txtNacinUpotrebe.Text));
            }
        }

        private void txtNacinUpotrebe_TextChanged(object sender, TextChangedEventArgs e)
        {
            Regex rx = new Regex("[^0-9]+");
            potvrdaReceptEnable(!rx.IsMatch(txtTrajanje.Text) && !String.IsNullOrEmpty(txtTrajanje.Text), !String.IsNullOrWhiteSpace(txtNacinUpotrebe.Text));
        }

        private void potvrdaReceptEnable(bool trajanjeOK, bool nacinUpotrebeOK)
        {
            btnPotvrdiRecept.IsEnabled = trajanjeOK && nacinUpotrebeOK;
        }
    }
}
