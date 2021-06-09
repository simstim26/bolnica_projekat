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

namespace Bolnica_aplikacija.LekarStudent
{
    /// <summary>
    /// Interaction logic for TerapijeIzdavanjeRecpeta.xaml
    /// </summary>
    public partial class TerapijeIzdavanjeRecpeta : UserControl
    {
        private static Grid gridLekovi;
        public static bool aktivan;
        private static FrameworkElement fm = new FrameworkElement();
        public TerapijeIzdavanjeRecpeta(BolestTerapija bolestTerapija)
        {
            InitializeComponent();
            this.DataContext = bolestTerapija;
            fm.DataContext = this.DataContext;

            LekarProzor.getGlavnaLabela().Content = "Izdavanje recepta";
            Pacijent pacijent = PacijentKontroler.nadjiPacijenta(bolestTerapija.idPacijenta);
            lblRDatumR.Content = pacijent.datumRodjenja;
            lblRImePrezime.Content = pacijent.ime + " " + pacijent.prezime;
            lblRJmbg.Content = pacijent.jmbg;
            lblRPol.Content = pacijent.pol;

            txtDijagnoza.Text = bolestTerapija.nazivBolesti;
            txtKol.Text = bolestTerapija.kolicina;
            txtNacinUpotrebe.Text = TerapijaKontroler.nadjiTerapijuPoId(bolestTerapija.idTerapije).nacinUpotrebe;
            txtNazivLeka.Text = bolestTerapija.nazivTerapije;
            txtTrajanje.Text = TerapijaKontroler.nadjiTerapijuPoId(bolestTerapija.idTerapije).trajanje.ToString();
            txtDatum.Text = DateTime.Now.ToString("dd.MM.yyyy.");
            dataGridLekovi.ItemsSource = LekKontroler.ucitajSveSemTrenutnogNaTerapiji(bolestTerapija.idLeka);
            gridLekovi = gridDodavanjeLeka;
            aktivan = true;
            UvidUTerapije.aktivan = false;
        }

        public static FrameworkElement getFM()
        {
            return fm;
        }

        public static Grid getGridLekovi()
        {
            return gridLekovi;
        }

        private void btnDodavanjeLeka_Click(object sender, RoutedEventArgs e)
        {
            gridDodavanjeLeka.Visibility = Visibility.Visible;
            LekarProzor.getGlavnaLabela().Content = "Odabir leka";
        }

        private void btnPotvdiOdabirLeka_Click(object sender, RoutedEventArgs e)
        {
            if(dataGridLekovi.SelectedIndex != -1)
            {
                if(PacijentKontroler.proveriAlergijuNaLekZaPacijenta(((BolestTerapija)fm.DataContext).idPacijenta, ((Lek)dataGridLekovi.SelectedItem).id))
                {
                    MessageBox.Show("Pacijent je alergičan na dati lek.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);

                }
                else
                {
                    promeniTerapiju();
                }
            }
            else
            {
                MessageBox.Show("Potrebno je izabrati lek!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void promeniTerapiju()
        {
            Lek lek = (Lek)dataGridLekovi.SelectedItem;
            ((BolestTerapija)fm.DataContext).idLeka = lek.id;
            ((BolestTerapija)fm.DataContext).idTermina = null;
            ((BolestTerapija)fm.DataContext).izvestaj = null;
            ((BolestTerapija)fm.DataContext).kolicina = lek.kolicina.ToString();
            ((BolestTerapija)fm.DataContext).nazivTerapije = lek.naziv;
            txtKol.Text = ((BolestTerapija)fm.DataContext).kolicina;
            txtNacinUpotrebe.Text = lek.getNacinUpotrebeString();
            txtNazivLeka.Text = ((BolestTerapija)fm.DataContext).nazivTerapije;
            txtTrajanje.Text = TerapijaKontroler.nadjiTerapijuPoId(((BolestTerapija)fm.DataContext).idTerapije).trajanje.ToString();
            gridDodavanjeLeka.Visibility = Visibility.Hidden;
            LekarProzor.getGlavnaLabela().Content = "Izdavanje recepta";
        }

        private void btnPotvrdiRecept_Click(object sender, RoutedEventArgs e)
        {
            if(((BolestTerapija)fm.DataContext).idTermina == null)
            {
                TerapijaKontroler.dodajTerapiju(new TerapijaDTO("", ((BolestTerapija)fm.DataContext).idLeka, ((BolestTerapija)fm.DataContext).idPacijenta,
                    ((BolestTerapija)fm.DataContext).idBolesti, null, DateTime.Now, Convert.ToInt32(txtTrajanje.Text), txtNacinUpotrebe.Text));
            }
            else
            {
                Terapija terapija = TerapijaKontroler.nadjiTerapijuPoId(((BolestTerapija)fm.DataContext).idTerapije);
                TerapijaKontroler.azurirajTerapiju(new TerapijaDTO(terapija.id, ((BolestTerapija)fm.DataContext).idLeka, terapija.idPacijenta,
                   terapija.idBolesti, terapija.idTermina, DateTime.Now, Convert.ToInt32(txtTrajanje.Text), txtNacinUpotrebe.Text));
            }

            MessageBox.Show("Uspešno ste izdali recept!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
            aktivan = false;
            Content = new UvidUTerapije(((BolestTerapija)fm.DataContext).idPacijenta);
        }

        private void btnPonistiOdaberLeka_Click(object sender, RoutedEventArgs e)
        {
            gridDodavanjeLeka.Visibility = Visibility.Hidden;
            LekarProzor.getGlavnaLabela().Content = "Izdavanje recepta";

        }

        private void btnPonistiRecept_Click(object sender, RoutedEventArgs e)
        {
            aktivan = false;
            Content = new UvidUTerapije(((BolestTerapija)fm.DataContext).idPacijenta);
        }

        private void txtTrajanje_TextChanged(object sender, TextChangedEventArgs e)
        {
            Regex rx = new Regex("[^0-9]+");
            if (rx.IsMatch(txtTrajanje.Text))
            {
                lblGreskaTrajanje.Visibility = Visibility.Visible;
                potvrdaReceptaEnabled(false, !String.IsNullOrWhiteSpace(txtNacinUpotrebe.Text));
            }
            else
            {
                lblGreskaTrajanje.Visibility = Visibility.Hidden;
                potvrdaReceptaEnabled(true && !String.IsNullOrWhiteSpace(txtTrajanje.Text), !String.IsNullOrWhiteSpace(txtNacinUpotrebe.Text));
            }
        }

        private void txtNacinUpotrebe_TextChanged(object sender, TextChangedEventArgs e)
        {
            Regex rx = new Regex("[^0-9]+");
            potvrdaReceptaEnabled(!rx.IsMatch(txtTrajanje.Text) && !String.IsNullOrWhiteSpace(txtTrajanje.Text), !String.IsNullOrWhiteSpace(txtNacinUpotrebe.Text));
        }

        private void potvrdaReceptaEnabled(bool trajanjeOK, bool nacinUpotrebeOK)
        {
            btnPotvrdiRecept.IsEnabled = trajanjeOK  && nacinUpotrebeOK;
        }
    }
}
