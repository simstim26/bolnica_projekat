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
    /// Interaction logic for PremestiUProstoriju.xaml
    /// </summary>
    public partial class PremestiUProstoriju : UserControl
    {
        public PremestiUProstoriju()
        {
            InitializeComponent();
            var stavka = (Stavka)InventarPogled.dobaviDataGridInventar().SelectedItem;
            textBoxKolicinaZaPremestanje.Clear();
            comboBoxProstorijeZaPremestanje.SelectedIndex = -1;
            datumPocetka.SelectedDate = null;
            datumKraja.SelectedDate = null;

            if (stavka.jeStaticka == true)
            {
                textStaticka1.Visibility = Visibility.Visible;
                textStaticka2.Visibility = Visibility.Visible;
                datumPocetka.Visibility = Visibility.Visible;
                datumKraja.Visibility = Visibility.Visible;
            }
            else
            {
                textStaticka1.Visibility = Visibility.Hidden;
                textStaticka2.Visibility = Visibility.Hidden;
                datumPocetka.Visibility = Visibility.Hidden;
                datumKraja.Visibility = Visibility.Hidden;
            }

            textBoxStavkaZaPremestanje.Text = stavka.naziv;


            Dictionary<string, string> prostorije = new Dictionary<string, string>();
            var neobrisaneProstorije = ProstorijaKontroler.ucitajNeobrisane();
            foreach (Prostorija p in neobrisaneProstorije)
            {
                prostorije.Add(p.id, p.broj + " " + p.sprat);
            }


            comboBoxProstorijeZaPremestanje.ItemsSource = prostorije;
        }

        private void btnPremesti_Click(object sender, RoutedEventArgs e)
        {
            /*var prostorijaId = (KeyValuePair<string, string>)comboBoxProstorijeZaPremestanje.SelectedItem;
            var stavka = (Stavka)InventarPogled.dobaviDataGridInventar().SelectedItem;*/
            DateTime datumPocetkaPremestanja;
            DateTime datumKrajaPremestanja;
            svaPolja.Visibility = Visibility.Hidden;
            neispravanDatum.Visibility = Visibility.Hidden;
            kolicinaBroj.Visibility = Visibility.Hidden;

            String pat = @"^[0-9]+$";
            Regex r = new Regex(pat);
            Match m = r.Match(textBoxKolicinaZaPremestanje.Text.Replace(" ", ""));

            if (comboBoxProstorijeZaPremestanje.SelectedIndex != -1 && !String.IsNullOrEmpty(textBoxKolicinaZaPremestanje.Text) && m.Success)
            {
                var prostorijaId = (KeyValuePair<string, string>)comboBoxProstorijeZaPremestanje.SelectedItem;
                var stavka = (Stavka)InventarPogled.dobaviDataGridInventar().SelectedItem;
                if (stavka.jeStaticka)
                {
                    
                    if (!datumPocetka.SelectedDate.HasValue || !datumKraja.SelectedDate.HasValue)
                    {
                        svaPolja.Visibility = Visibility.Visible;
                    }
                    else if (datumPocetka.SelectedDate.Value < System.DateTime.Now.AddDays(-1) || datumKraja.SelectedDate.Value < datumPocetka.SelectedDate.Value)
                    {
                        neispravanDatum.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        datumPocetkaPremestanja = datumPocetka.SelectedDate.Value;
                        datumKrajaPremestanja = datumKraja.SelectedDate.Value;
                        ProstorijaPrebacivanjeDTO prebacivanje = new ProstorijaPrebacivanjeDTO(stavka.id, null, prostorijaId.Key, Int32.Parse(textBoxKolicinaZaPremestanje.Text),
                     datumPocetkaPremestanja, datumKrajaPremestanja);
                        ProstorijaKontroler.dodajStavku(prebacivanje);
                        InventarPogled.dobaviDataGridInventar().ItemsSource = StavkaKontroler.UcitajNeobrisaneStavke();
                        GlavniProzor.DobaviProzorZaIzmenu().Children.Clear();
                        GlavniProzor.DobaviProzorZaIzmenu().Children.Add(new InventarPogled());
                    }
                    /*else
                    {
                        datumPocetkaPremestanja = datumPocetka.SelectedDate.Value;
                        datumKrajaPremestanja = datumKraja.SelectedDate.Value;
                        ProstorijaPrebacivanjeDTO prebacivanje = new ProstorijaPrebacivanjeDTO(stavka.id, null, prostorijaId.Key, Int32.Parse(textBoxKolicinaZaPremestanje.Text),
                             datumPocetkaPremestanja, datumKrajaPremestanja);
                        ProstorijaKontroler.dodajStavku(prebacivanje);
                        InventarPogled.dobaviDataGridInventar().ItemsSource = StavkaKontroler.UcitajNeobrisaneStavke();
                        GlavniProzor.DobaviProzorZaIzmenu().Children.Clear();
                        GlavniProzor.DobaviProzorZaIzmenu().Children.Add(new InventarPogled());
                    }*/
                }
                else
                {
                    datumPocetkaPremestanja = System.DateTime.MinValue;
                    datumKrajaPremestanja = System.DateTime.MinValue;
                    ProstorijaPrebacivanjeDTO prebacivanje = new ProstorijaPrebacivanjeDTO(stavka.id, null, prostorijaId.Key, Int32.Parse(textBoxKolicinaZaPremestanje.Text),
                     datumPocetkaPremestanja, datumKrajaPremestanja);
                    ProstorijaKontroler.dodajStavku(prebacivanje);
                    InventarPogled.dobaviDataGridInventar().ItemsSource = StavkaKontroler.UcitajNeobrisaneStavke();
                    GlavniProzor.DobaviProzorZaIzmenu().Children.Clear();
                    GlavniProzor.DobaviProzorZaIzmenu().Children.Add(new InventarPogled());
                }
/*
                datumPocetkaPremestanja = datumPocetka.SelectedDate.Value;
                datumKrajaPremestanja = datumKraja.SelectedDate.Value;*/
                
            }
            else if (!m.Success)
            {
                kolicinaBroj.Visibility = Visibility.Visible;
            }
            else
            {
                svaPolja.Visibility = Visibility.Visible;
            }

            

            /*if (datumPocetka.SelectedDate != null && datumKraja.SelectedDate != null)
            {
                datumPocetkaPremestanja = datumPocetka.SelectedDate.Value;
                datumKrajaPremestanja = datumKraja.SelectedDate.Value;
            }
            else
            {
                datumPocetkaPremestanja = System.DateTime.MinValue;
                datumKrajaPremestanja = System.DateTime.MinValue;
            }

            ProstorijaPrebacivanjeDTO prebacivanje = new ProstorijaPrebacivanjeDTO(stavka.id, null, prostorijaId.Key, Int32.Parse(textBoxKolicinaZaPremestanje.Text),
                datumPocetkaPremestanja, datumKrajaPremestanja);
            ProstorijaKontroler.dodajStavku(prebacivanje);
            InventarPogled.dobaviDataGridInventar().ItemsSource = StavkaKontroler.UcitajNeobrisaneStavke();
            GlavniProzor.DobaviProzorZaIzmenu().Children.Clear();
            GlavniProzor.DobaviProzorZaIzmenu().Children.Add(new InventarPogled());*/
        }

        private void btnOtkaziPremestanje_Click(object sender, RoutedEventArgs e)
        {
            GlavniProzor.DobaviProzorZaIzmenu().Children.Clear();
            GlavniProzor.DobaviProzorZaIzmenu().Children.Add(new InventarPogled());
        }
    }
}
