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
    /// Interaction logic for PremestiUDruguProstoriju.xaml
    /// </summary>
    public partial class PremestiUDruguProstoriju : UserControl
    {
        public PremestiUDruguProstoriju()
        {
            //TODO: UBACITI STAVKE KAD SE UBACE
            InitializeComponent();
            var prostorija = (Prostorija)ProstorijePogled.dobaviDataGridProstorija().SelectedItem;
            var stavka = (Stavka)PogledajInventar.dobaviDataGridProstorijaInventar().SelectedItem;

            textBoxStavkaZaPremestanjeU.Text = stavka.naziv;
            textBoxKolicinaZaPremestanjeU.Clear();
            comboBoxProstorijeZaPremestanjeU.SelectedIndex = -1;
            datumPocetkaU.SelectedDate = null;
            datumKrajaU.SelectedDate = null;




            if (stavka.jeStaticka == true)
            {
                textStaticka1U.Visibility = Visibility.Visible;
                textStaticka2U.Visibility = Visibility.Visible;
                datumPocetkaU.Visibility = Visibility.Visible;
                datumKrajaU.Visibility = Visibility.Visible;
            }
            else
            {
                textStaticka1U.Visibility = Visibility.Hidden;
                textStaticka2U.Visibility = Visibility.Hidden;
                datumPocetkaU.Visibility = Visibility.Hidden;
                datumKrajaU.Visibility = Visibility.Hidden;
            }

            Dictionary<string, string> prostorije = new Dictionary<string, string>();
            var neobrisaneProstorije = ProstorijaKontroler.ucitajNeobrisane();
            foreach (Prostorija p in neobrisaneProstorije)
            {
                if (p.id != prostorija.id)
                {
                    prostorije.Add(p.id, p.broj + " " + p.sprat);
                }

            }
            comboBoxProstorijeZaPremestanjeU.ItemsSource = prostorije;
        }

        private void btnPremestiU_Click(object sender, RoutedEventArgs e)
        {
            svePoljaMorajuBitiPopunjena.Visibility = Visibility.Hidden;
            kolicinaBroj.Visibility = Visibility.Hidden;
            neispravanDatum.Visibility = Visibility.Hidden;
            String pat = @"^[0-9]+$";
            Regex r = new Regex(pat);
            Match m = r.Match(textBoxKolicinaZaPremestanjeU.Text.Replace(" ", ""));

            if (((Stavka)PogledajInventar.dobaviDataGridProstorijaInventar().SelectedItem).jeStaticka)
            {
                if (String.IsNullOrEmpty(textBoxKolicinaZaPremestanjeU.Text) || datumPocetkaU.SelectedDate == null || datumKrajaU.SelectedDate == null || comboBoxProstorijeZaPremestanjeU.SelectedIndex == -1)
                {
                    svePoljaMorajuBitiPopunjena.Visibility = Visibility.Visible;
                }
                else if (!m.Success)
                {
                    kolicinaBroj.Visibility = Visibility.Visible;
                    textBoxKolicinaZaPremestanjeU.Clear();
                }
                else if (datumKrajaU.SelectedDate < datumPocetkaU.SelectedDate || datumPocetkaU.SelectedDate.Value <= System.DateTime.Today.AddDays(-1))
                {
                    neispravanDatum.Visibility = Visibility.Visible;
                }
                else
                {
                    var prostorijaIz = (Prostorija)ProstorijePogled.dobaviDataGridProstorija().SelectedItem;
                    var selektovaniIndeks = ProstorijePogled.dobaviDataGridProstorija().SelectedIndex;

                    var prostorijaU = (KeyValuePair<string, string>)comboBoxProstorijeZaPremestanjeU.SelectedItem;
                    var stavka = (Stavka)PogledajInventar.dobaviDataGridProstorijaInventar().SelectedItem;
                    var stavke = ProstorijaKontroler.dobaviStavkeIzProstorije(prostorijaIz);
                    DateTime datumPocetkaPremestanja;
                    DateTime datumKrajaPremestanja;

                    if (datumPocetkaU.SelectedDate != null && datumKrajaU.SelectedDate != null)
                    {
                        datumPocetkaPremestanja = datumPocetkaU.SelectedDate.Value;
                        datumKrajaPremestanja = datumKrajaU.SelectedDate.Value;
                    }
                    else
                    {
                        datumPocetkaPremestanja = System.DateTime.MinValue;
                        datumKrajaPremestanja = System.DateTime.MinValue;
                    }

                    ProstorijaPrebacivanjeDTO prebacivanje = new ProstorijaPrebacivanjeDTO(stavka.id, prostorijaIz.id, prostorijaU.Key, Int32.Parse(textBoxKolicinaZaPremestanjeU.Text), datumPocetkaPremestanja, datumKrajaPremestanja);

                    ProstorijaKontroler.premestiStavku(prebacivanje);
                    var prostorije = ProstorijaKontroler.ucitajNeobrisane();
                    ProstorijePogled.dobaviDataGridProstorija().ItemsSource = prostorije;
                    ProstorijePogled.dobaviDataGridProstorija().SelectedIndex = selektovaniIndeks;
                    PogledajInventar.dobaviDataGridProstorijaInventar().ItemsSource = ((Prostorija)ProstorijePogled.dobaviDataGridProstorija().SelectedItem).Stavka;
                    GlavniProzor.DobaviProzorZaIzmenu().Children.Clear();
                    GlavniProzor.DobaviProzorZaIzmenu().Children.Add(new PogledajInventar());
                }
            }
            else
            {
                if (String.IsNullOrEmpty(textBoxKolicinaZaPremestanjeU.Text) || comboBoxProstorijeZaPremestanjeU.SelectedIndex == -1)
                {
                    svePoljaMorajuBitiPopunjena.Visibility = Visibility.Visible;
                }
                else if (!m.Success)
                {
                    kolicinaBroj.Visibility = Visibility.Visible;
                    textBoxKolicinaZaPremestanjeU.Clear();
                }
                else if (datumKrajaU.SelectedDate < datumPocetkaU.SelectedDate || datumPocetkaU.SelectedDate <= System.DateTime.Now)
                {
                    neispravanDatum.Visibility = Visibility.Visible;
                }
                else
                {
                    var prostorijaIz = (Prostorija)ProstorijePogled.dobaviDataGridProstorija().SelectedItem;
                    var selektovaniIndeks = ProstorijePogled.dobaviDataGridProstorija().SelectedIndex;

                    var prostorijaU = (KeyValuePair<string, string>)comboBoxProstorijeZaPremestanjeU.SelectedItem;
                    var stavka = (Stavka)PogledajInventar.dobaviDataGridProstorijaInventar().SelectedItem;
                    var stavke = ProstorijaKontroler.dobaviStavkeIzProstorije(prostorijaIz);
                    DateTime datumPocetkaPremestanja;
                    DateTime datumKrajaPremestanja;

                    if (datumPocetkaU.SelectedDate != null && datumKrajaU.SelectedDate != null)
                    {
                        datumPocetkaPremestanja = datumPocetkaU.SelectedDate.Value;
                        datumKrajaPremestanja = datumKrajaU.SelectedDate.Value;
                    }
                    else
                    {
                        datumPocetkaPremestanja = System.DateTime.MinValue;
                        datumKrajaPremestanja = System.DateTime.MinValue;
                    }

                    ProstorijaPrebacivanjeDTO prebacivanje = new ProstorijaPrebacivanjeDTO(stavka.id, prostorijaIz.id, prostorijaU.Key, Int32.Parse(textBoxKolicinaZaPremestanjeU.Text), datumPocetkaPremestanja, datumKrajaPremestanja);

                    ProstorijaKontroler.premestiStavku(prebacivanje);
                    var prostorije = ProstorijaKontroler.ucitajNeobrisane();
                    ProstorijePogled.dobaviDataGridProstorija().ItemsSource = prostorije;
                    ProstorijePogled.dobaviDataGridProstorija().SelectedIndex = selektovaniIndeks;
                    PogledajInventar.dobaviDataGridProstorijaInventar().ItemsSource = ((Prostorija)ProstorijePogled.dobaviDataGridProstorija().SelectedItem).Stavka;
                    GlavniProzor.DobaviProzorZaIzmenu().Children.Clear();
                    GlavniProzor.DobaviProzorZaIzmenu().Children.Add(new PogledajInventar());
                }
            }

            

        }

        private void btnOtkaziPremestanjeU_Click(object sender, RoutedEventArgs e)
        {
            GlavniProzor.DobaviProzorZaIzmenu().Children.Clear();
            GlavniProzor.DobaviProzorZaIzmenu().Children.Add(new ProstorijePogled());
        }
    }
}
