using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.Model;
using Bolnica_aplikacija.PomocneKlase;
using Bolnica_aplikacija.Repozitorijum;
using Bolnica_aplikacija.Servis;
using Model;

namespace Bolnica_aplikacija
{
    /// <summary>
    /// Interaction logic for UpravnikProzor.xaml
    /// </summary>
    public partial class UpravnikProzor : Window
    {
        private static UpravnikProzor instance = null;

        public static UpravnikProzor getInstance()
        {
            if(instance == null)
            {
                instance = new UpravnikProzor();
            }
            
            return instance;  
        }

        private static Prostorija prostorija;
        private String lblId1;
        private String lblBrojProstorije1;
        private String lblSprat1;
        private String lblDostupnost1;

        private UpravnikProzor()
        {
            InitializeComponent();
            lblIme.Text = KorisnikKontroler.GetUpravnik().ime + " " + KorisnikKontroler.GetUpravnik().prezime;
            dataGridProstorija.ItemsSource = ProstorijaKontroler.ucitajNeobrisane();
        }

        private void tbLekovi_Checked(object sender, RoutedEventArgs e)
        {
            dataGridLekovi.ItemsSource = LekKontroler.ucitajSve();
            gridLekovi.Visibility = Visibility.Visible;
            vodoravniPravougaonik.Visibility = Visibility.Visible;
            /*horizontalniPravougaonik.Height = 33;
            horizontalniPravougaonik.Margin = new System.Windows.Thickness(19, 697, 0, 0);*/


            donjiPravougaonik.Visibility = Visibility.Visible;
        }

        private void btnOdjava_Click(object sender, RoutedEventArgs e)
        {
            Prijava prijava = new Prijava();
            this.Close();
            instance = null;
            prijava.ShowDialog();

        }

        private void btnDodajProstoriju_Click(object sender, RoutedEventArgs e)
        {
            gridProstorija.Visibility = Visibility.Hidden;
            gridDodajProstoriju.Visibility = Visibility.Visible;
            unosBrojaProstorije.Clear();
            unosSprata.Clear();
            cbTipProstorije.SelectedIndex = -1;
            lblBrojPostojiDodaj.Visibility = Visibility.Hidden;
            lblNijePopunjenoIspravnoDodaj.Visibility = Visibility.Hidden;
            lblNijePopunjenoDodaj.Visibility = Visibility.Hidden;
        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            gridDodajProstoriju.Visibility = Visibility.Hidden;
            gridProstorija.Visibility = Visibility.Visible;
        }

        private void btnIzmeniProstoriju_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridProstorija.SelectedIndex != -1)
            {

                lblBrojPostoji.Visibility = Visibility.Hidden;
                lblNijePopunjenoIspravnoIzmeni.Visibility = Visibility.Hidden;
                lblNijePopunjenoIzmeni.Visibility = Visibility.Hidden;
                prostorija = (Prostorija)dataGridProstorija.SelectedItem;
                ProstorijaKontroler.pregledajProstorijeZaRenoviranje();

                //lblId1 = lblId.Text;
                gridIzmeniProstoriju.Visibility = Visibility.Visible;


                //lblId.Text += prostorija.id;
                txtBrojProstorije.Text = prostorija.broj;
                txtSpratProstorije.Text = prostorija.sprat.ToString();

                //Prikaz combo boxa za dostupnost--------------------
                if (prostorija.dostupnost == true)
                {
                    cbDostupnostProstorije.SelectedIndex = 0;
                }
                else if (prostorija.dostupnost == false)
                {
                    cbDostupnostProstorije.SelectedIndex = 1;
                }


                //Prikaz combo boxa za tip prostorije----------------
                if (prostorija.tipProstorije == TipProstorije.BOLNICKA_SOBA)
                {
                    cbTipProstorijeIzmena.SelectedIndex = 0;
                }
                else if (prostorija.tipProstorije == TipProstorije.OPERACIONA_SALA)
                {
                    cbTipProstorijeIzmena.SelectedIndex = 1;
                }
                else if (prostorija.tipProstorije == TipProstorije.SOBA_ZA_PREGLED)
                {
                    cbTipProstorijeIzmena.SelectedIndex = 2;
                }
            }
        }

        private void btnPotvrdiIzmenu_Click(object sender, RoutedEventArgs e)
        {
            String pat = @"^[0-9]+$";
            Regex r = new Regex(pat);
            Match m = r.Match(txtBrojProstorije.Text.Replace(" ", ""));
            Match m1 = r.Match(txtSpratProstorije.Text.Replace(" ", ""));

            if (String.IsNullOrEmpty(txtBrojProstorije.Text) || String.IsNullOrEmpty(txtSpratProstorije.Text) ||
                cbTipProstorijeIzmena.SelectedIndex == -1 || cbDostupnostProstorije.SelectedIndex == -1)
            {
                lblNijePopunjenoIzmeni.Visibility = Visibility.Visible;
            }
            else if (!m.Success || !m1.Success)
            {
                lblNijePopunjenoIspravnoIzmeni.Visibility = Visibility.Visible;
            }
            else
            {
                ProstorijaDTO prostorija = new ProstorijaDTO();
                prostorija.id = ((Prostorija)dataGridProstorija.SelectedItem).id;
                prostorija.sprat = Int32.Parse(txtSpratProstorije.Text);
                prostorija.broj = txtBrojProstorije.Text;
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
     
                if (cbDostupnostProstorije.SelectedIndex == 0)
                {
                    prostorija.dostupnost = true;
                }
                else if (cbDostupnostProstorije.SelectedIndex == 1)
                {
                    prostorija.dostupnost = false;
                }

                bool provera = ProstorijaKontroler.AzurirajProstoriju(prostorija);
                if (!provera)
                {
                    lblBrojPostoji.Visibility = Visibility.Visible;
                }
                else
                {
                    gridIzmeniProstoriju.Visibility = Visibility.Hidden;
                    gridProstorija.Visibility = Visibility.Visible;
                    dataGridProstorija.ItemsSource = ProstorijaKontroler.ucitajNeobrisane();
                }
            }
            
            //lblId.Text = lblId1;
        }

        private void btnOtkaziIzmeni_Click(object sender, RoutedEventArgs e)
        {
            gridIzmeniProstoriju.Visibility = Visibility.Hidden;
            gridProstorija.Visibility = Visibility.Visible;
            //lblId.Text = lblId1;
        }

        private void btnIzbrisiProstoriju_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridProstorija.SelectedIndex != -1)
            {
                //Console.WriteLine(prostorijeNeobrisane.Count);
                prostorija = (Prostorija)dataGridProstorija.SelectedItem;
                ProstorijaKontroler.ObrisiProstoriju(prostorija.id);
                dataGridProstorija.ItemsSource = ProstorijaKontroler.ucitajNeobrisane();

            }
        }

        private void btnPotvrdi_Click(object sender, RoutedEventArgs e)
        {
            {
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
                    lblNijePopunjenoIspravnoDodaj.Visibility = Visibility.Visible;
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
                        lblNijePopunjenoIspravnoDodaj.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        gridDodajProstoriju.Visibility = Visibility.Hidden;
                        gridProstorija.Visibility = Visibility.Visible;
                    }

                }

                dataGridProstorija.ItemsSource = ProstorijaKontroler.ucitajNeobrisane();
            }
        }

        private void btnIzmeniStavku_Click(object sender, RoutedEventArgs e)
        {

            if (dataGridInventar.SelectedIndex != -1)
            {
                var stavka = (Stavka)dataGridInventar.SelectedItem;
                gridIzmeniStavku.Visibility = Visibility.Visible;

                //lblId1 = lblId.Text;
                gridIzmeniStavku.Visibility = Visibility.Visible;


                txtBoxKolicinaStavke.Text = stavka.kolicina.ToString();
                txtBoxNazivStavke.Text = stavka.naziv;
                txtBoxProizvodjacStavke.Text = stavka.proizvodjac;
                labelNepravilanUnosIzmena.Visibility = Visibility.Hidden;
                
                if(stavka.jePotrosnaRoba == true)
                {
                    checkBoxPotrosnaIzmeni.IsChecked = true;
                }
                else if(stavka.jePotrosnaRoba == false)
                {
                    checkBoxPotrosnaIzmeni.IsChecked = false;
                }
                
                if(stavka.jeStaticka == true)
                {
                    cbTipStavke.SelectedIndex = 0;
                }
                else if(stavka.jeStaticka == false)
                {
                    cbTipStavke.SelectedIndex = 1;
                }


            }

        }

        private void btnPotvrdiStavku_Click(object sender, RoutedEventArgs e)
        {
            labelNepravilnoUneseno.Visibility = Visibility.Hidden;
            if (StavkaKontroler.DodajStavku())
            {
                dataGridInventar.ItemsSource = StavkaKontroler.UcitajNeobrisaneStavke();
                gridDodajStavku.Visibility = Visibility.Hidden;
                gridInventar.Visibility = Visibility.Visible;
            }
            else if(!StavkaKontroler.DodajStavku())
            {
                labelNepravilnoUneseno.Visibility = Visibility.Visible;
            }    
        }

        private void btnDodajStavku_Click(object sender, RoutedEventArgs e)
        {
            gridDodajStavku.Visibility = Visibility.Visible;
            textBoxNaziv.Clear();
            textBoxKolicina.Clear();
            textBoxProizvodjac.Clear();
            comboBoxTipOpreme.SelectedIndex = -1;
            checkBoxPotrosna.IsChecked = false;
            labelNepravilnoUneseno.Visibility = Visibility.Hidden;


        }

        private void btnPotvrdiIzmenuStavke_Click(object sender, RoutedEventArgs e)
        {
            labelNepravilanUnosIzmena.Visibility = Visibility.Hidden;
            var stavka = (Stavka)dataGridInventar.SelectedItem;

            if(StavkaKontroler.IzmeniStavku(stavka))
            {
                gridIzmeniStavku.Visibility = Visibility.Hidden;
                dataGridInventar.ItemsSource = StavkaKontroler.UcitajNeobrisaneStavke();
                gridInventar.Visibility = Visibility.Visible;
            }
            else if(!StavkaKontroler.IzmeniStavku(stavka))
            {
                labelNepravilanUnosIzmena.Visibility = Visibility.Visible;
            }
            
        }

        private void btnOtkaziIzmenuStavke_Click(object sender, RoutedEventArgs e)
        {
            gridIzmeniStavku.Visibility = Visibility.Hidden;
            gridInventar.Visibility = Visibility.Visible;
        }

        private void btnObrisiStavku_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridInventar.SelectedIndex != -1)
            {
                StavkaKontroler.IzbrisiStavku((Stavka)dataGridInventar.SelectedItem);
                dataGridInventar.ItemsSource = StavkaKontroler.UcitajNeobrisaneStavke();

            }
        }

        private void btnPremestiUProstoriju_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridInventar.SelectedIndex != -1)
            {
                var stavka = (Stavka)dataGridInventar.SelectedItem;
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

                gridPremestiUProstoriju.Visibility = Visibility.Visible;

                textBoxStavkaZaPremestanje.Text = stavka.naziv;


                Dictionary<string, string> prostorije = new Dictionary<string, string>();
                var neobrisaneProstorije = ProstorijaKontroler.ucitajNeobrisane();
                foreach (Prostorija p in neobrisaneProstorije)
                {
                    prostorije.Add(p.id, p.broj + " " + p.sprat);
                }


                comboBoxProstorijeZaPremestanje.ItemsSource = prostorije;
            }
        }

        private void btnPremesti_Click(object sender, RoutedEventArgs e)
        {
            gridPremestiUProstoriju.Visibility = Visibility.Hidden;
           
            var prostorijaId = (KeyValuePair<string, string>)comboBoxProstorijeZaPremestanje.SelectedItem;
            var stavka = (Stavka)dataGridInventar.SelectedItem;
            DateTime datumPocetkaPremestanja;
            DateTime datumKrajaPremestanja;

            if (datumPocetka.SelectedDate != null && datumKraja.SelectedDate != null)
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
            dataGridInventar.ItemsSource = StavkaKontroler.UcitajNeobrisaneStavke();
            gridInventar.Visibility = Visibility.Visible;

        }

        private void btnOtkaziPremestanje_Click(object sender, RoutedEventArgs e)
        {
            gridPremestiUProstoriju.Visibility = Visibility.Hidden;
            gridInventar.Visibility = Visibility.Visible;
        }

        private void btnPogledajInventar_Click(object sender, RoutedEventArgs e)
        {
            if(dataGridProstorija.SelectedIndex != -1)
            {
                var prostorija = (Prostorija)dataGridProstorija.SelectedItem;
                var stavke = ProstorijaKontroler.dobaviStavkeIzProstorije(prostorija);
                gridPogledajInventar.Visibility = Visibility.Visible;
                gridProstorija.Visibility = Visibility.Hidden;
                dataGridInventarProstorije.ItemsSource = stavke;
            }
            
        }

        private void btnOtkaziPrikaz_Click(object sender, RoutedEventArgs e)
        {
            gridPogledajInventar.Visibility = Visibility.Hidden;
            gridProstorija.Visibility = Visibility.Visible;
        }

        private void btnPremestiUDruguProstoriju_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridInventarProstorije.SelectedIndex != -1)
            {
                gridPremestiIzProstorije.Visibility = Visibility.Visible;
                gridProstorija.Visibility = Visibility.Hidden;

                
                var prostorija = (Prostorija)dataGridProstorija.SelectedItem;
                var stavka = (Stavka)dataGridInventarProstorije.SelectedItem;

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
            
        }

        private void btnOtkaziPremestanjeU_Click(object sender, RoutedEventArgs e)
        {
            gridPremestiIzProstorije.Visibility = Visibility.Hidden;
            gridPogledajInventar.Visibility = Visibility.Visible;
        }

        private void btnPremestiU_Click(object sender, RoutedEventArgs e)
        {
            var prostorijaIz = (Prostorija)dataGridProstorija.SelectedItem;
            var selektovaniIndeks = dataGridProstorija.SelectedIndex;

            var prostorijaU = (KeyValuePair<string, string>)comboBoxProstorijeZaPremestanjeU.SelectedItem;
            var stavka = (Stavka)dataGridInventarProstorije.SelectedItem;
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
            dataGridProstorija.ItemsSource = prostorije;
            dataGridProstorija.SelectedIndex = selektovaniIndeks;
            dataGridInventarProstorije.ItemsSource = ((Prostorija)dataGridProstorija.SelectedItem).Stavka;
            gridPremestiIzProstorije.Visibility = Visibility.Hidden;
            gridPogledajInventar.Visibility = Visibility.Visible;

        }

        private void btnPogledajProstorije_Click(object sender, RoutedEventArgs e)
        {
            gridInventar.Visibility = Visibility.Hidden;
            gridStavkaUProstoriama.Visibility = Visibility.Visible;

            var stavka = (Stavka)dataGridInventar.SelectedItem;
            var prostorijeTreba = new List<ProstorijaKolicina>();
            var prostorije = ProstorijaKontroler.ucitajNeobrisane();
            var kolicina = new List<int>();
           

            foreach(Prostorija p in prostorije)
            {
                if (p.Stavka != null)
                {
                    foreach (Stavka s in p.Stavka)
                    {

                        if (s.id == stavka.id)
                        {
                            ProstorijaKolicina prostorija = new ProstorijaKolicina();
                            prostorija.broj = p.broj;
                            prostorija.sprat = p.sprat;
                            prostorija.kolicina = s.kolicina;
                            prostorijeTreba.Add(prostorija);
                        }
                    }
                } 
            }

            textBoxNazivStavkePoProstorijama.Text = stavka.naziv;
            textBoxProizvodjacStavkePoProstorijama.Text = stavka.proizvodjac;
            textBoxKolicinaStavkePoProstorijama.Text = stavka.kolicina.ToString();

           

            dataGridStavkaUProstorijama.ItemsSource = prostorijeTreba;
            
        }

        private void btnOtkaziPrikazNeki_Click(object sender, RoutedEventArgs e)
        {
            gridInventar.Visibility = Visibility.Visible;
            gridStavkaUProstoriama.Visibility = Visibility.Hidden;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridSviLekoviZaZamenski.SelectedIndex != -1)
            {
                List<Lek> sviLekovi = (List<Lek>)dataGridSviLekoviZaZamenski.ItemsSource;
                List<Lek> zamenSkiLekovi = (List<Lek>)dataGridZamenskiUbaceniLekovi.ItemsSource;
                zamenSkiLekovi.Add((Lek)dataGridSviLekoviZaZamenski.SelectedItem);
                sviLekovi.Remove((Lek)dataGridSviLekoviZaZamenski.SelectedItem);
                dataGridZamenskiUbaceniLekovi.Items.Refresh();
                dataGridSviLekoviZaZamenski.Items.Refresh();
            }
        }

        private void btnDodajLek_Click(object sender, RoutedEventArgs e)
        {
            LekZaOdobravanje noviLek = LekZaOdobravanje.getInstance();
            List<String> sastojci = new List<string>();
            noviLek.sastojci = sastojci;

            gridLekovi.Visibility = Visibility.Hidden;
            gridLekoviDodaj.Visibility = Visibility.Visible;
            cbTipLekaDodavanje.ItemsSource = LekKontroler.tipLeka();
            comboBoxNacinUpotrebe.ItemsSource = LekKontroler.nacinUpotrebeLeka();
        }

        private void btnDodajSastojke_Click(object sender, RoutedEventArgs e)
        {
            gridLekoviDodajSastojke.Visibility = Visibility.Visible;
            gridLekoviDodaj.Visibility = Visibility.Hidden;
            dataGridDodajSastojke.ItemsSource = LekZaOdobravanje.getInstance().sastojci;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            gridLekoviDodajSastojke.Visibility = Visibility.Hidden;
            gridLekoviDodaj.Visibility = Visibility.Visible;
        }

        private void btnDodajZamenskeLekove_Click(object sender, RoutedEventArgs e)
        {
            dataGridSviLekoviZaZamenski.ItemsSource = LekKontroler.ucitajSve();
            List<Lek> zamenskiLekovi = new List<Lek>();
            dataGridZamenskiUbaceniLekovi.ItemsSource = zamenskiLekovi;
            gridLekoviDodajZamenskeLekove.Visibility = Visibility.Visible;
            gridLekoviDodaj.Visibility = Visibility.Hidden;
        }

        private void btnPotvrdiZamenske_Click(object sender, RoutedEventArgs e)
        {

            LekZaOdobravanje noviLek = LekZaOdobravanje.getInstance();
            var provera = (List<Lek>)dataGridZamenskiUbaceniLekovi.ItemsSource;
            noviLek.zamenskiLekovi = (List<Lek>)dataGridZamenskiUbaceniLekovi.ItemsSource;
            gridLekoviDodajZamenskeLekove.Visibility = Visibility.Hidden;
            gridLekoviDodaj.Visibility = Visibility.Visible;
        }

        private void btnPotvrdiDodavanjeLekova_Click(object sender, RoutedEventArgs e)
        {
            gridLekoviDodaj.Visibility = Visibility.Hidden;
            dataGridLekovi.ItemsSource = LekKontroler.ucitajSve();
            gridLekovi.Visibility = Visibility.Visible;
            TipLeka tipLeka = (TipLeka)cbTipLekaDodavanje.SelectedItem;
            NacinUpotrebe nacinUpotrebe = (NacinUpotrebe)comboBoxNacinUpotrebe.SelectedItem;

            LekZaOdobravanje noviLek = LekZaOdobravanje.getInstance();

            noviLek.naziv = textBoxNazivLekaUnos.Text;
            noviLek.tip = tipLeka;
            noviLek.kolicina = Int32.Parse(textBoxKolicinaLekaUnos.Text);
            noviLek.proizvodjac = textBoxProizvodjacLekaUnos.Text;
            noviLek.nacinUpotrebe = nacinUpotrebe;
            noviLek.id = (LekKontroler.ucitajLekoveZaOdobravanje().Count() + 1).ToString();

            LekKontroler.napraviLek(noviLek);
            //noviLek.staviInstancuNaNull();

        }

        private void btnIzaberiLekare_Click(object sender, RoutedEventArgs e)
        {
            dataGridLekariZaSlanje.ItemsSource = LekarKontroler.ucitajSve();
            gridIzaberiLekareZaSlanje.Visibility = Visibility.Visible;
            gridLekoviDodaj.Visibility = Visibility.Hidden;

        }

        private void btnOtkaziLekare_Click(object sender, RoutedEventArgs e)
        {
            gridIzaberiLekareZaSlanje.Visibility = Visibility.Hidden;
            gridLekoviDodaj.Visibility = Visibility.Visible;
        }

        private void btnPotvrdiLekare_Click(object sender, RoutedEventArgs e)
        {
            List<String> lekariId = new List<String>();
            foreach(var selektovan in dataGridLekariZaSlanje.SelectedItems)
            {
                Lekar lekar = (Lekar)selektovan;
                lekariId.Add(lekar.id);
            }

            LekZaOdobravanje noviLek = LekZaOdobravanje.getInstance();
            noviLek.lekariKojimaJePoslatLek = lekariId;
        }

        private void OnChecked(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(dataGridLekariZaSlanje.SelectedIndex.ToString());
        }

        private void btnOtkaziDodavanjeLeka_Click(object sender, RoutedEventArgs e)
        {
            gridLekoviDodaj.Visibility = Visibility.Hidden;
            gridLekovi.Visibility = Visibility.Visible;
        }

        private void btnSastojakUnesi_Click(object sender, RoutedEventArgs e)
        {
            LekZaOdobravanje noviLek = LekZaOdobravanje.getInstance();
            String sastojak = textBoxUpisiSastojak.Text;
            noviLek.sastojci.Add(sastojak);
            dataGridDodajSastojke.Items.Refresh();
            textBoxUpisiSastojak.Clear();

        }

        private void btnOdbijeniZahtevi_Click(object sender, RoutedEventArgs e)
        {
            List<LekZaOdobravanje> lekovi = LekKontroler.ucitajOdbaceneLekove();
            dataGridOdbijeniLekovi.ItemsSource = lekovi;
            gridLekovi.Visibility = Visibility.Hidden;
            gridOdbijeniLekovi.Visibility = Visibility.Visible;
            
        }

        private void btnOtkaziOdbacenLek_Click(object sender, RoutedEventArgs e)
        {
            gridOdbijeniLekovi.Visibility = Visibility.Hidden;
            gridLekovi.Visibility = Visibility.Visible;
        }

        private void btnIzmeniLek_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridOdbijeniLekovi.SelectedIndex != -1)
            {
                gridLekoviIzmenaOdbijenih.Visibility = Visibility.Visible;
                gridOdbijeniLekovi.Visibility = Visibility.Hidden;
                cbTipLekaDodavanjeOdbijenog.ItemsSource = LekKontroler.tipLeka();
                comboBoxNacinUpotrebeOdbijenog.ItemsSource = LekKontroler.nacinUpotrebeLeka();

                LekZaOdobravanje odbijeniLek = (LekZaOdobravanje)dataGridOdbijeniLekovi.SelectedItem;

                textBoxNazivLekaUnosOdbijenog.Text = odbijeniLek.naziv;
                cbTipLekaDodavanjeOdbijenog.SelectedItem = odbijeniLek.tip;
                textBoxKolicinaLekaUnosOdbijenog.Text = odbijeniLek.kolicina.ToString();
                textBoxProizvodjacLekaUnosOdbijenog.Text = odbijeniLek.proizvodjac;
                comboBoxNacinUpotrebeOdbijenog.SelectedItem = odbijeniLek.nacinUpotrebe;
            }
        }

        private void btnOtkaziDodavanjeLekaOdbijenog_Click(object sender, RoutedEventArgs e)
        {
            gridLekoviIzmenaOdbijenih.Visibility = Visibility.Hidden;
            gridOdbijeniLekovi.Visibility = Visibility.Visible;
        }

        private void btnDodajSastojkeOdbijenog_Click(object sender, RoutedEventArgs e)
        {
            gridLekoviIzmeniSastojkeOdbijenog.Visibility = Visibility.Visible;
            gridLekoviIzmenaOdbijenih.Visibility = Visibility.Hidden;
            LekZaOdobravanje odbijeniLek = (LekZaOdobravanje)dataGridOdbijeniLekovi.SelectedItem;
            List<String> sastojci = odbijeniLek.sastojci;
            dataGridDodajSastojkeOdbijenog.ItemsSource = sastojci;
        }

        private void btnSastojakUnesiOdbijenog_Click(object sender, RoutedEventArgs e)
        {
            String sastojak = textBoxUpisiSastojakOdbijenog.Text;
            List<String> sastojci = (List<String>)dataGridDodajSastojkeOdbijenog.ItemsSource;
            sastojci.Add(sastojak);
            
            dataGridDodajSastojkeOdbijenog.ItemsSource = sastojci;
            dataGridDodajSastojkeOdbijenog.Items.Refresh();
            textBoxUpisiSastojakOdbijenog.Clear();
        }

        private void btnIzbrisiSastojakOdbijenog_Click(object sender, RoutedEventArgs e)
        {
            if(dataGridDodajSastojkeOdbijenog.SelectedIndex != -1)
            {
                List<String> sastojci = (List<String>)dataGridDodajSastojkeOdbijenog.ItemsSource;
                sastojci.Remove((String)dataGridDodajSastojkeOdbijenog.SelectedItem);
                dataGridDodajSastojkeOdbijenog.ItemsSource = sastojci;
                dataGridDodajSastojkeOdbijenog.Items.Refresh();
            }
        }

        private void btnPotvrdiSastojkeOdbijenog_Click(object sender, RoutedEventArgs e)
        {
            LekZaOdobravanje odbijeniLek = (LekZaOdobravanje)dataGridOdbijeniLekovi.SelectedItem;
            odbijeniLek.sastojci = (List<String>)dataGridDodajSastojkeOdbijenog.ItemsSource;
          
            gridLekoviIzmeniSastojkeOdbijenog.Visibility = Visibility.Hidden;
            gridLekoviIzmenaOdbijenih.Visibility = Visibility.Visible;
        }

        private void btnDodajZamenskeLekoveOdbijenog_Click(object sender, RoutedEventArgs e)
        {
            gridLekoviIzmenaOdbijenih.Visibility = Visibility.Hidden;
            gridLekoviIzmeniZamenskeLekoveOdbijenog.Visibility = Visibility.Visible;

            LekZaOdobravanje odbijeniLek = (LekZaOdobravanje)dataGridOdbijeniLekovi.SelectedItem;
            dataGridZamenskiUbaceniLekoviOdbijenog.ItemsSource = odbijeniLek.zamenskiLekovi;
            dataGridSviLekoviZaZamenskiOdbijenog.ItemsSource = LekKontroler.ucitajSve();
        }

        private void dodajZamenskiOdbijenog_Click(object sender, RoutedEventArgs e)
        {
            LekZaOdobravanje odbijeniLek = (LekZaOdobravanje)dataGridOdbijeniLekovi.SelectedItem;
            List<Lek> sviLekovi = (List<Lek>)dataGridSviLekoviZaZamenskiOdbijenog.ItemsSource;
            odbijeniLek.zamenskiLekovi.Add((Lek)dataGridSviLekoviZaZamenskiOdbijenog.SelectedItem);
            sviLekovi.Remove((Lek)dataGridSviLekoviZaZamenskiOdbijenog.SelectedItem);
            dataGridZamenskiUbaceniLekoviOdbijenog.ItemsSource = odbijeniLek.zamenskiLekovi;
            dataGridSviLekoviZaZamenskiOdbijenog.ItemsSource = sviLekovi;
            dataGridZamenskiUbaceniLekoviOdbijenog.Items.Refresh();
            dataGridSviLekoviZaZamenskiOdbijenog.Items.Refresh();
        }

        private void btnPotvrdiZamenskeOdbijenog_Click(object sender, RoutedEventArgs e)
        {
            LekZaOdobravanje odbijeniLek = (LekZaOdobravanje)dataGridOdbijeniLekovi.SelectedItem;
            odbijeniLek.zamenskiLekovi = (List<Lek>)dataGridZamenskiUbaceniLekoviOdbijenog.ItemsSource;
            gridLekoviIzmeniZamenskeLekoveOdbijenog.Visibility = Visibility.Hidden;
            gridLekoviIzmenaOdbijenih.Visibility = Visibility.Visible;
        }

        private void btnPotvrdiDodavanjeLekovaOdbijenog_Click(object sender, RoutedEventArgs e)
        {
            TipLeka tipLeka = (TipLeka)cbTipLekaDodavanjeOdbijenog.SelectedItem;
            NacinUpotrebe nacinUpotrebe = (NacinUpotrebe)comboBoxNacinUpotrebeOdbijenog.SelectedItem;

            LekZaOdobravanje odbijeniLek = (LekZaOdobravanje)dataGridOdbijeniLekovi.SelectedItem;
            LekKontroler.fizickiObrisiLekZaOdbacivanje(odbijeniLek);

            odbijeniLek.naziv = textBoxNazivLekaUnosOdbijenog.Text;
            odbijeniLek.tip = tipLeka;
            odbijeniLek.kolicina = Int32.Parse(textBoxKolicinaLekaUnosOdbijenog.Text);
            odbijeniLek.proizvodjac = textBoxProizvodjacLekaUnosOdbijenog.Text;
            odbijeniLek.nacinUpotrebe = nacinUpotrebe;
            odbijeniLek.id = (LekKontroler.ucitajLekoveZaOdobravanje().Count() + 1).ToString();

            LekKontroler.napraviCeoLek(odbijeniLek);
        }

        private void btnObrisiOdbacenLek_Click(object sender, RoutedEventArgs e)
        {
            if(dataGridOdbijeniLekovi.SelectedIndex != -1)
            {
                LekKontroler.fizickiObrisiLekZaOdbacivanje((LekZaOdobravanje)dataGridOdbijeniLekovi.SelectedItem);
                dataGridOdbijeniLekovi.ItemsSource = LekKontroler.ucitajOdbaceneLekove();
                dataGridOdbijeniLekovi.Items.Refresh();
            }
        }

        private void btnPonistiFiltriranje_Click(object sender, RoutedEventArgs e)
        {
            comboBoxFiltrirajStavku.SelectedIndex = -1;
            comboBoxKolicinaFiltriranje.SelectedIndex = -1;
            
            dataGridInventar.ItemsSource = StavkaKontroler.UcitajNeobrisaneStavke();
        }

        private void btnZakaziRenoviranje_Click(object sender, RoutedEventArgs e)
        {
            gridProstorija.Visibility = Visibility.Hidden;
            gridZakaziProstorijuZaRenoviranje.Visibility = Visibility.Visible;
            dataGridProstorijeZaRenoviranje.ItemsSource = ProstorijaKontroler.ucitajNeobrisane();
        }

        private void btnZakaziRenoviranjeProstorije_Click(object sender, RoutedEventArgs e)
        {
            if(dataGridProstorijeZaRenoviranje.SelectedItems.Count != 0)
            {
                foreach (var selektovan in dataGridProstorijeZaRenoviranje.SelectedItems)
                {
                    Prostorija selektovanaProstorija = (Prostorija)selektovan;
                    ProstorijaRenoviranje prostorijaZaRenoviranje = new ProstorijaRenoviranje(selektovanaProstorija.id, (DateTime)datumRenoviranjaOd.SelectedDate, 
                                                                    (DateTime)datumRenoviranjaDo.SelectedDate, textBoxRazlogRenoviranja.Text);
                    ProstorijaKontroler.zakaziRenoviranje(prostorijaZaRenoviranje);
                }
            }

            ProstorijaKontroler.pregledajProstorijeZaRenoviranje();
            gridZakaziProstorijuZaRenoviranje.Visibility = Visibility.Hidden;
            gridProstorija.Visibility = Visibility.Visible;
        }

        private void btnOtkaziRenoviranjeProstorije_Click(object sender, RoutedEventArgs e)
        {
            gridZakaziProstorijuZaRenoviranje.Visibility = Visibility.Hidden;
            gridProstorija.Visibility = Visibility.Visible;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPretraziStavku_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxFiltrirajStavku.SelectedIndex == -1)
            {
                if (comboBoxKolicinaFiltriranje.SelectedIndex == 0)
                {
                    dataGridInventar.ItemsSource = StavkaKontroler.poredjajListuStavkiPoKoliciniRastuce((List<Stavka>)dataGridInventar.ItemsSource);
                }
                if (comboBoxKolicinaFiltriranje.SelectedIndex == 1)
                {
                    dataGridInventar.ItemsSource = StavkaKontroler.poredjajListuStavkiPoKoliciniOpadajuce((List<Stavka>)dataGridInventar.ItemsSource);
                }
                if (comboBoxKolicinaFiltriranje.SelectedIndex == 2)
                {
                    dataGridInventar.ItemsSource = StavkaKontroler.poredjajListuStavkiPoNazivuRastuce((List<Stavka>)dataGridInventar.ItemsSource);
                }
                if (comboBoxKolicinaFiltriranje.SelectedIndex == 3)
                {
                    dataGridInventar.ItemsSource = StavkaKontroler.poredjajListuStavkiPoNazivuOpadajuce((List<Stavka>)dataGridInventar.ItemsSource);
                }

                if (textBoxPretraga.Text.Length != 0)
                {
                    dataGridInventar.ItemsSource = StavkaKontroler.pretraziStavku(textBoxPretraga.Text, (List<Stavka>)dataGridInventar.ItemsSource);
                }
            }
            else if (comboBoxFiltrirajStavku.SelectedIndex == 2)
            {
                dataGridInventar.ItemsSource = StavkaKontroler.UcitajNeobrisaneStavke();
                dataGridInventar.Items.Refresh();
                if (comboBoxKolicinaFiltriranje.SelectedIndex == 0)
                {
                    dataGridInventar.ItemsSource = StavkaKontroler.poredjajListuStavkiPoKoliciniRastuce((List<Stavka>)dataGridInventar.ItemsSource);
                }
                if (comboBoxKolicinaFiltriranje.SelectedIndex == 1)
                {
                    dataGridInventar.ItemsSource = StavkaKontroler.poredjajListuStavkiPoKoliciniOpadajuce((List<Stavka>)dataGridInventar.ItemsSource);
                }
                if (comboBoxKolicinaFiltriranje.SelectedIndex == 2)
                {
                    dataGridInventar.ItemsSource = StavkaKontroler.poredjajListuStavkiPoNazivuRastuce((List<Stavka>)dataGridInventar.ItemsSource);
                }
                if (comboBoxKolicinaFiltriranje.SelectedIndex == 3)
                {
                    dataGridInventar.ItemsSource = StavkaKontroler.poredjajListuStavkiPoNazivuOpadajuce((List<Stavka>)dataGridInventar.ItemsSource);
                }
                if (textBoxPretraga.Text.Length != 0)
                {
                    dataGridInventar.ItemsSource = StavkaKontroler.pretraziStavku(textBoxPretraga.Text, (List<Stavka>)dataGridInventar.ItemsSource);
                }
            }
            else if (comboBoxFiltrirajStavku.SelectedIndex == 0)
            {
                dataGridInventar.ItemsSource = StavkaKontroler.ucitajDinamickeStavke();
                dataGridInventar.Items.Refresh();
                if (comboBoxKolicinaFiltriranje.SelectedIndex == 0)
                {
                    dataGridInventar.ItemsSource = StavkaKontroler.poredjajListuStavkiPoKoliciniRastuce((List<Stavka>)dataGridInventar.ItemsSource);
                }
                if (comboBoxKolicinaFiltriranje.SelectedIndex == 1)
                {
                    dataGridInventar.ItemsSource = StavkaKontroler.poredjajListuStavkiPoKoliciniOpadajuce((List<Stavka>)dataGridInventar.ItemsSource);
                }
                if (comboBoxKolicinaFiltriranje.SelectedIndex == 2)
                {
                    dataGridInventar.ItemsSource = StavkaKontroler.poredjajListuStavkiPoNazivuRastuce((List<Stavka>)dataGridInventar.ItemsSource);
                }
                if (comboBoxKolicinaFiltriranje.SelectedIndex == 3)
                {
                    dataGridInventar.ItemsSource = StavkaKontroler.poredjajListuStavkiPoNazivuOpadajuce((List<Stavka>)dataGridInventar.ItemsSource);
                }
                if (textBoxPretraga.Text.Length != 0)
                {
                    dataGridInventar.ItemsSource = StavkaKontroler.pretraziStavku(textBoxPretraga.Text, (List<Stavka>)dataGridInventar.ItemsSource);
                }
            }
            else if (comboBoxFiltrirajStavku.SelectedIndex == 1)
            {
                dataGridInventar.ItemsSource = StavkaKontroler.ucitajStatickeStavke();
                dataGridInventar.Items.Refresh();
                if (comboBoxKolicinaFiltriranje.SelectedIndex == 0)
                {
                    dataGridInventar.ItemsSource = StavkaKontroler.poredjajListuStavkiPoKoliciniRastuce((List<Stavka>)dataGridInventar.ItemsSource);
                }
                if (comboBoxKolicinaFiltriranje.SelectedIndex == 1)
                {
                    dataGridInventar.ItemsSource = StavkaKontroler.poredjajListuStavkiPoKoliciniOpadajuce((List<Stavka>)dataGridInventar.ItemsSource);
                }
                if (comboBoxKolicinaFiltriranje.SelectedIndex == 2)
                {
                    dataGridInventar.ItemsSource = StavkaKontroler.poredjajListuStavkiPoNazivuRastuce((List<Stavka>)dataGridInventar.ItemsSource);
                }
                if (comboBoxKolicinaFiltriranje.SelectedIndex == 3)
                {
                    dataGridInventar.ItemsSource = StavkaKontroler.poredjajListuStavkiPoNazivuOpadajuce((List<Stavka>)dataGridInventar.ItemsSource);
                }
                if (textBoxPretraga.Text.Length != 0)
                {
                    dataGridInventar.ItemsSource = StavkaKontroler.pretraziStavku(textBoxPretraga.Text, (List<Stavka>)dataGridInventar.ItemsSource);
                }
            }
            dataGridInventar.Items.Refresh();


        }

        private void btnIzmenaLeka_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridLekovi.SelectedIndex != -1)
            {
                Lek lek = (Lek)dataGridLekovi.SelectedItem;
                gridLekoviIzmena.Visibility = Visibility.Visible;
                gridLekovi.Visibility = Visibility.Hidden;
                cbTipLekaDodavanjeIzmena.ItemsSource = LekKontroler.tipLeka();
                comboBoxNacinUpotrebeIzmena.ItemsSource = LekKontroler.nacinUpotrebeLeka();

                textBoxNazivLekaUnosIzmena.Text = lek.naziv;
                textBoxKolicinaLekaUnosIzmena.Text = lek.kolicina.ToString();
                textBoxProizvodjacLekaUnosIzmena.Text = lek.proizvodjac;
                comboBoxNacinUpotrebeIzmena.SelectedItem = lek.nacinUpotrebe;
                cbTipLekaDodavanjeIzmena.SelectedItem = lek.tip;
            }
        }

        private void btnDodajSastojkeIzmena_Click(object sender, RoutedEventArgs e)
        {
            gridLekoviIzmeniSastojke.Visibility = Visibility.Visible;
            gridLekoviIzmena.Visibility = Visibility.Hidden;
            dataGridDodajSastojkeIzmena.ItemsSource = ((Lek)dataGridLekovi.SelectedItem).sastojci;
        }

        private void btnPotvrdiSastojkeIzmena_Click(object sender, RoutedEventArgs e)
        {
            Lek lek = (Lek)dataGridLekovi.SelectedItem;
            lek.sastojci = (List<String>)dataGridDodajSastojkeIzmena.ItemsSource;

            gridLekoviIzmeniSastojke.Visibility = Visibility.Hidden;
            gridLekoviIzmena.Visibility = Visibility.Visible;
        }

        private void btnSastojakUnesiIzmena_Click(object sender, RoutedEventArgs e)
        {
            String sastojak = textBoxUpisiSastojakIzmena.Text;
            List<String> sastojci;
            if(((List<String>)dataGridDodajSastojkeIzmena.ItemsSource).Count == 0)
            {
                sastojci = new List<String>();
            }
            else
            {
                sastojci = (List<String>)dataGridDodajSastojkeIzmena.ItemsSource;
            }
            
            sastojci.Add(sastojak);

            dataGridDodajSastojkeIzmena.ItemsSource = sastojci;
            dataGridDodajSastojkeIzmena.Items.Refresh();
            textBoxUpisiSastojakIzmena.Clear();
        }

        private void btnIzbrisiSastojakIzmena_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridDodajSastojkeIzmena.SelectedIndex != -1)
            {
                List<String> sastojci = (List<String>)dataGridDodajSastojkeIzmena.ItemsSource;
                sastojci.Remove((String)dataGridDodajSastojkeIzmena.SelectedItem);
                dataGridDodajSastojkeIzmena.ItemsSource = sastojci;
                dataGridDodajSastojkeIzmena.Items.Refresh();
            }
        }

        private void btnDodajZamenskeLekoveIzmena_Click(object sender, RoutedEventArgs e)
        {
            gridLekoviIzmena.Visibility = Visibility.Hidden;
            gridLekoviIzmeniZamenskeLekove.Visibility = Visibility.Visible;
            dataGridSviLekoviZaZamenskiIzmena.ItemsSource = LekKontroler.ucitajSve();
            dataGridZamenskiUbaceniLekoviIzmena.ItemsSource = ((Lek)dataGridLekovi.SelectedItem).zamenskiLekovi;
        }

        private void dodajZamenskiIzmena_Click(object sender, RoutedEventArgs e)
        {
            Lek lek = (Lek)dataGridLekovi.SelectedItem;
            List<Lek> sviLekovi = (List<Lek>)dataGridSviLekoviZaZamenskiIzmena.ItemsSource;
            lek.zamenskiLekovi.Add((Lek)dataGridSviLekoviZaZamenskiIzmena.SelectedItem);
            sviLekovi.Remove((Lek)dataGridSviLekoviZaZamenskiIzmena.SelectedItem);
            dataGridZamenskiUbaceniLekoviIzmena.ItemsSource = lek.zamenskiLekovi;
            dataGridSviLekoviZaZamenskiIzmena.ItemsSource = sviLekovi;
            dataGridZamenskiUbaceniLekoviIzmena.Items.Refresh();
            dataGridSviLekoviZaZamenskiIzmena.Items.Refresh();
        }

        private void btnPotvrdiZamenskeIzmena_Click(object sender, RoutedEventArgs e)
        {
            Lek lek = (Lek)dataGridLekovi.SelectedItem;
            lek.zamenskiLekovi = (List<Lek>)dataGridZamenskiUbaceniLekoviIzmena.ItemsSource;
            gridLekoviIzmeniZamenskeLekove.Visibility = Visibility.Hidden;
            gridLekoviIzmena.Visibility = Visibility.Visible;
        }

        private void btnPotvrdiDodavanjeLekovaIzmena_Click(object sender, RoutedEventArgs e)
        {
            TipLeka tipLeka = (TipLeka)cbTipLekaDodavanjeIzmena.SelectedItem;
            NacinUpotrebe nacinUpotrebe = (NacinUpotrebe)comboBoxNacinUpotrebeIzmena.SelectedItem;

            Lek lek = (Lek)dataGridLekovi.SelectedItem;

            lek.naziv = textBoxNazivLekaUnosIzmena.Text;
            lek.tip = tipLeka;
            lek.kolicina = Int32.Parse(textBoxKolicinaLekaUnosIzmena.Text);
            lek.proizvodjac = textBoxProizvodjacLekaUnosIzmena.Text;
            lek.nacinUpotrebe = nacinUpotrebe;
            lek.zamenskiLekovi = (List<Lek>)dataGridZamenskiUbaceniLekoviIzmena.ItemsSource;
            lek.sastojci = (List<String>)dataGridDodajSastojkeIzmena.ItemsSource;

            LekKontroler.azurirajLek(lek);
            gridLekoviIzmena.Visibility = Visibility.Hidden;
            gridLekovi.Visibility = Visibility.Visible;
            dataGridLekovi.Items.Refresh();
        }

        private void btnObrisiLek_Click(object sender, RoutedEventArgs e)
        {
            Lek lek = (Lek)dataGridLekovi.SelectedItem;
            List<Lek> lekovi = (List<Lek>)dataGridLekovi.ItemsSource;
            lekovi.Remove(lek);
            dataGridLekovi.ItemsSource = lekovi;
            dataGridLekovi.Items.Refresh();
        }

        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {
            gridProstorija.Visibility = Visibility.Visible;
            
        }

        /*private void listInventar_Selected(object sender, RoutedEventArgs e)
        {
            dataGridInventar.ItemsSource = StavkaKontroler.UcitajNeobrisaneStavke();
            gridInventar.Visibility = Visibility.Visible;
            
        }

        private void listLekovi_Selected(object sender, RoutedEventArgs e)
        {
            dataGridLekovi.ItemsSource = LekKontroler.ucitajSve();
            
            gridLekovi.Visibility = Visibility.Visible;
        }

        private void listLekovi_Unselected(object sender, RoutedEventArgs e)
        {

        }*/

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            gridProstorija.Visibility = Visibility.Hidden;
            gridInventar.Visibility = Visibility.Hidden;
            gridLekovi.Visibility = Visibility.Hidden;
            if (listView.SelectedIndex == 1)
            {
                gridProstorija.Visibility = Visibility.Visible;
            }
            else if (listView.SelectedIndex == 2)
            {
                dataGridInventar.ItemsSource = StavkaKontroler.UcitajNeobrisaneStavke();
                gridInventar.Visibility = Visibility.Visible;
            }
            else if (listView.SelectedIndex == 3)
            {
                dataGridLekovi.ItemsSource = LekKontroler.ucitajSve();
                gridLekovi.Visibility = Visibility.Visible;
            }
        }

        private void btnOtkaziStavku_Click(object sender, RoutedEventArgs e)
        {
            gridDodajStavku.Visibility = Visibility.Hidden;
            gridInventar.Visibility = Visibility.Visible;
        }
    }
}
