using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.PacijentModel;
using Bolnica_aplikacija.PomocneKlase;
using Bolnica_aplikacija.View.PacijentStudent;
using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace Bolnica_aplikacija.PacijentStudent
{
    /// <summary>
    /// Interaction logic for ProzorPacijent.xaml
    /// </summary>
    public partial class ProzorPacijent : Window
    {

        //private String idPacijenta;
        private DispatcherTimer tajmer;
        public List<KeyValuePair<String, int>> ValueList { get;  private set; }

        public ProzorPacijent()
        {
            InitializeComponent();
            this.DataContext = this;

            SetScreenSize();
            CenterWindow();
            SetDataGridBounds();
            SetLabelPacijentContent();

            postaviLabeleDatuma();

            PopuniTermine();
            popuniTerapije();
            setujTajmer();
            proveraAnkete();
            popuniObavestenja();
            napraviGraf();
            popuniMesecneTermine();

            PomocnaKlasaKalendar.popuniKalendar(calendar);

        }

        private void postaviLabeleDatuma()
        {
            DateTime datum = DateTime.Now;
            lblDatum.Content = lblDatum.Content + " " + datum.ToString("dd/MM/yyyy");

            switch (datum.Month)
            {
                case 1:
                    lblMesec.Content = "Januar"; break;
                case 2:
                    lblMesec.Content = "Februar"; break;
                case 3:
                    lblMesec.Content = "Mart"; break;
                case 4:
                    lblMesec.Content = "April"; break;
                case 5:
                    lblMesec.Content = "Maj"; break;
                case 6:
                    lblMesec.Content = "Jun"; break;
                case 7:
                    lblMesec.Content = "Jul"; break;
                case 8:
                    lblMesec.Content = "Avgust"; break;
                case 9:
                    lblMesec.Content = "Septembar"; break;
                case 10:
                    lblMesec.Content = "Oktobar"; break;
                case 11:
                    lblMesec.Content = "Novembar"; break;
                case 12:
                    lblMesec.Content = "Decembar"; break;
                default:
                    lblMesec.Content = ""; break;

            }

        }

        private void popuniMesecneTermine()
        {
            dataGridIzvrseniTermini.ItemsSource = TerminKontroler.pronadjiOdradjeneTerminePacijenta(KorisnikKontroler.GetPacijent().id);
        }

        private void napraviGraf()
        {
            ValueList = new List<KeyValuePair<string, int>>();

            int[] brojOdradjenihTermina = { 0,0,0,0,0,0,0,0,0,0,0,0};
            for(int i=1; i <= 12; i++)
            {
                brojOdradjenihTermina[i-1] = PomocnaKlasaProvere.prebrojTermine(KorisnikKontroler.GetPacijent().id, i);
            }

            ValueList.Add(new KeyValuePair<string, int>("JAN", brojOdradjenihTermina[0]));
            ValueList.Add(new KeyValuePair<string, int>("FEB", brojOdradjenihTermina[1]));
            ValueList.Add(new KeyValuePair<string, int>("MAR", brojOdradjenihTermina[2]));
            ValueList.Add(new KeyValuePair<string, int>("APR", brojOdradjenihTermina[3]));
            ValueList.Add(new KeyValuePair<string, int>("MAJ", brojOdradjenihTermina[4]));
            ValueList.Add(new KeyValuePair<string, int>("JUN", brojOdradjenihTermina[5]));
            ValueList.Add(new KeyValuePair<string, int>("JUL", brojOdradjenihTermina[6])); 
            ValueList.Add(new KeyValuePair<string, int>("AVG", brojOdradjenihTermina[7]));
            ValueList.Add(new KeyValuePair<string, int>("SEP", brojOdradjenihTermina[8]));
            ValueList.Add(new KeyValuePair<string, int>("OKT", brojOdradjenihTermina[9]));
            ValueList.Add(new KeyValuePair<string, int>("NOV", brojOdradjenihTermina[10]));
            ValueList.Add(new KeyValuePair<string, int>("DEC", brojOdradjenihTermina[11]));
        }

        private void PopuniTermine()
        { 
            dataGridTermin.ItemsSource = PacijentKontroler.prikazPacijentovihTermina(KorisnikKontroler.GetPacijent().id);
        }

        private void popuniTerapije()
        {
            dataGridTerapije.ItemsSource = PacijentKontroler.ucitajAktivneTerapije(KorisnikKontroler.GetPacijent().id);
        }

        private void popuniObavestenja()
        {
            dataGridObavestenja.ItemsSource = NotifikacijaKontroler.getNoveNotifikacijeKorisnika(KorisnikKontroler.GetPacijent().id);
        }

        private void SetLabelPacijentContent()
        {
            lblPacijent.Content = lblPacijent.Content + " " +
                KorisnikKontroler.GetPacijent().ime + " " +
                KorisnikKontroler.GetPacijent().prezime;
        }

        private void SetScreenSize()
        {
            this.Height = (System.Windows.SystemParameters.PrimaryScreenHeight * 0.92);
            this.Width = (System.Windows.SystemParameters.PrimaryScreenWidth * 0.75);
            this.MinHeight = (System.Windows.SystemParameters.PrimaryScreenHeight * 0.92);
            this.MinWidth = (System.Windows.SystemParameters.PrimaryScreenWidth * 0.75);
            WindowState = WindowState.Maximized;
        }

        private void SetDataGridBounds()
        {
            dataGridTermin.Loaded += SetMinWidths;
            dataGridTermin.Height = System.Windows.SystemParameters.PrimaryScreenHeight - 370;
        }

        private void CenterWindow()
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }

        public void SetMinWidths(object source, EventArgs e)
        {
            foreach (var column in dataGridTermin.Columns)
            {
                column.MinWidth = column.ActualWidth;
                column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
            foreach (var column in dataGridTerapije.Columns)
            {
                column.MinWidth = column.ActualWidth;
                column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
            foreach (var column in dataGridObavestenja.Columns)
            {
                column.MinWidth = column.ActualWidth;
                column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
            foreach (var column in dataGridIzvrseniTermini.Columns)
            {
                column.MinWidth = column.ActualWidth;
                column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double newHeight = e.NewSize.Height;
            dataGridTermin.Height = newHeight - 370;
            dataGridTerapije.Height = newHeight - 370;
            double newWidth = e.NewSize.Width;
        }

        private void btnOtkaziPregled_Click(object sender, RoutedEventArgs e)
        {
            if(proveraUzastopnihIzmena())
            {
                if (dataGridTermin.SelectedIndex != -1)
                {
                    PacijentTermin izabraniTermin = (PacijentTermin)dataGridTermin.SelectedItem;

                    if (izabraniTermin.napomena.Equals("Pregled"))
                    {
                        if (TerminKontroler.proveriDatumTermina(izabraniTermin.id) <= 0)
                        {
                            MessageBox.Show("Nije moguće izvršiti otkazivanje termina 24h pred termin.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else
                        {
                            if (izabraniTermin.idSpecijalizacije.Equals("0"))
                            {
                                PotvrdaProzor pprozor = new PotvrdaProzor();
                                pprozor.Owner = this;
                                pprozor.ShowDialog();

                                if (pprozor.GetPovratnaVrednost() == 1)
                                {
                                    PacijentKontroler.otkaziTerminPacijenta(izabraniTermin.id);
                                    //ANTI TROL
                                    PomocnaKlasaProvere.antiTrolMetoda(KorisnikKontroler.GetPacijent().id);

                                    //Kalendar
                                    PomocnaKlasaKalendar.azurirajKalendar(calendar);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Potrebno je da se konsultujete sa Vašim specijalistom kako biste otkazali ovaj termin pregleda.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Potrebno je da se konsultujete sa Vašim lekarom kako biste otkazali termin operacije.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Molimo odaberite termin koji želite da otkažete.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                PopuniTermine();
            }
            else
            {
                MessageBox.Show("Poštovani, izvršili ste previše izmena u kratkom vremenskom periodu. Molimo sačekajte.", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnIzmeniTermin_Click(object sender, RoutedEventArgs e)
        {
            if(proveraUzastopnihIzmena())
            {
                if (dataGridTermin.SelectedIndex != -1)
                {

                    PacijentTermin izabraniTermin = (PacijentTermin)dataGridTermin.SelectedItem;

                    if (izabraniTermin.napomena.Equals("Pregled"))
                    {
                        if (TerminKontroler.proveriDatumTermina(izabraniTermin.id) <= 0)
                        {
                            MessageBox.Show("Nije moguće izvršiti promenu termina 24h pred termin.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else
                        {
                            if (izabraniTermin.idSpecijalizacije.Equals("0"))
                            {
                                IzmenaTerminaPacijent izmenaTermina = new IzmenaTerminaPacijent(dataGridTermin, calendar);
                                izmenaTermina.Owner = this;
                                izmenaTermina.ShowDialog();
                            }
                            else
                            {
                                MessageBox.Show("Potrebno je da se konsultujete sa Vašim specijalistom oko izmene termina pregleda.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Za izmenu termina operacije je potrebno da se konsultujete sa Vašim lekarom.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);

                    }

                }
                else
                {
                    MessageBox.Show("Molimo odaberite termin koji želite da izmenite.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Poštovani, izvršili ste previše izmena u kratkom vremenskom periodu. Molimo sačekajte.", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnZakaziPregled_Click_1(object sender, RoutedEventArgs e)
        {
            if(proveraUzastopnihIzmena())
            {
                PacijentZakaziTermin zakaziTermin = new PacijentZakaziTermin(dataGridTermin, calendar);
                zakaziTermin.Owner = this;
                zakaziTermin.ShowDialog();
            }
            else
            {
                MessageBox.Show("Poštovani, izvršili ste previše izmena u kratkom vremenskom periodu. Molimo sačekajte.", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void menuItemOdjava_Click(object sender, RoutedEventArgs e)
        {
            Prijava prijava = new Prijava();
            this.Close();
            prijava.Show();
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            menuItemOdjava.RaiseEvent(new RoutedEventArgs(MenuItem.ClickEvent));
        }

        private void setujTajmer()
        {
            tajmer = new DispatcherTimer();
            tajmer.Interval = TimeSpan.FromMinutes(1);
            tajmer.Tick += timer_Tick;
            tajmer.Start();
        }

        void timer_Tick(Object sender, EventArgs e)
        {

            DateTime trenutnoVremeiDatum = DateTime.Now.AddHours(1);
            String trenutnoVreme = trenutnoVremeiDatum.ToString("HH:mm");
            String trenutanDatum = trenutnoVremeiDatum.ToString("dd.MM.yyyy.");

            bool proveraDatumaIVremena = NotifikacijaKontroler.proveriVreme(trenutnoVreme, trenutanDatum, KorisnikKontroler.GetPacijent().id);

            if(!proveraDatumaIVremena)
            {
                //Console.WriteLine("NESTO JE USPELO");

                var zaProveriti = NotifikacijaKontroler.getNoveNotifikacijeKorisnika(KorisnikKontroler.GetPacijent().id);

                if(zaProveriti.Count > 1)
                {
                    //Console.WriteLine("ovo je onaj slucaj sa vise obavestenja");
                }
                else if (zaProveriti.Count == 1)
                {
                    //NotifikacijaProzor notifikacijaProzor = new NotifikacijaProzor(zaProveriti.ElementAt(0).id, KorisnikKontroler.GetPacijent().id);
                    //notifikacijaProzor.ShowDialog();

                }
                else
                {
                    //Console.WriteLine("SAD JE VALJDA PRAZNO");
                }
            }

        }

        bool proveraUzastopnihIzmena()
        {
            bool povratnaVrednost = false;
            int brojPonavljanja = LogovanjeKontroler.getBrojUzastopnihPonavljanja(KorisnikKontroler.GetPacijent().id);
            bool proveraDatuma = PomocnaKlasaProvere.uporediDatumSaDanasnjim(LogovanjeKontroler.getVremeIzmene(KorisnikKontroler.GetPacijent().id));

            if(brojPonavljanja > 3 )
            {
                if(proveraDatuma)
                {
                    LogovanjeKontroler.resetujLogovanje(KorisnikKontroler.GetPacijent().id);
                    povratnaVrednost = true;
                }
                else
                {
                    povratnaVrednost = false;
                }
            }
            else
            {
                povratnaVrednost = true;
            }
            return povratnaVrednost;

        }

        private void btnOceniLekara_Click(object sender, RoutedEventArgs e)
        {
            OceniteLekara oceniteLekara = new OceniteLekara(KorisnikKontroler.GetPacijent().id);
            oceniteLekara.Owner = this;
            oceniteLekara.ShowDialog();
        }

        //zbog testiranja

        private void btnPlacanje_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnPodsetnik_Click(object sender, RoutedEventArgs e)
        {
            Obavestenje obavestenje = new Obavestenje(dataGridObavestenja, 0);
            obavestenje.ShowDialog();
        }

        private void proveraAnkete()
        {
            int mesec = DateTime.Now.Month;
            bool proveraMeseca = false;

            switch(mesec)
            {
                case 4:     proveraMeseca = true;    break;
                case 8:     proveraMeseca = true;    break;
                case 12:    proveraMeseca = true;    break;
                default:    proveraMeseca = false;    break;
            }

            if(proveraMeseca)
            {
                //proveriti stanje anketa polja u pacijentu

                if(!PacijentKontroler.proveriStanjeAnkete(KorisnikKontroler.GetPacijent().id))
                {
                    OceniteBolnicu oceniteBolnicu = new OceniteBolnicu(KorisnikKontroler.GetPacijent().id);
                    oceniteBolnicu.ShowDialog();

                }
            }
        }

        private void btnKreirajObavestenje_Click(object sender, RoutedEventArgs e)
        {
            Obavestenje obavestenje = new Obavestenje(dataGridObavestenja, 0);
            obavestenje.ShowDialog();
        }

        private void btnIzmeniObavestenje_Click(object sender, RoutedEventArgs e)
        {
            //to do: implementirati izmenu postojeceg obavestenja

            if (dataGridObavestenja.SelectedIndex != -1)
            {

                Obavestenje obavestenje = new Obavestenje(dataGridObavestenja, 1);
                obavestenje.ShowDialog();
                
            }
            else
            {
                MessageBox.Show("Molimo odaberite obaveštenje koje želite da izmenite.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }


        }

        private void btnIzvestaj_Click(object sender, RoutedEventArgs e)
        {
            String imeIzvestaja = "Nedeljne terapije za pacijenta";

            PdfWriter writer = new PdfWriter(imeIzvestaja + ".pdf");
            PdfDocument document = new PdfDocument(writer);
            document.AddNewPage();
            Document d = new Document(document);
            FontProgram fontProgram = FontProgramFactory.CreateFont();
            PdfFont font = PdfFontFactory.CreateFont(fontProgram, "Cp1250");
            d.SetFont(font);

            Paragraph zaglavlje = new Paragraph();
            zaglavlje.Add("Pacijent: " + KorisnikKontroler.GetPacijent().ime + " " + KorisnikKontroler.GetPacijent().prezime).SetBold();
            d.Add(zaglavlje);
            int brojac = 1;
            foreach(TerapijaPacijent terapija in TerapijaKontroler.ucitajTrenutneTerapijePacijentaTP(KorisnikKontroler.GetPacijent().id))
            {
                Paragraph paragraf = new Paragraph();
                Paragraph terapijaParagraf = new Paragraph();

                terapijaParagraf.Add("Terapija broj " + brojac + ":\n").SetBold();
                paragraf.Add("Naziv oboljenja: "+terapija.nazivOboljenja);
                paragraf.Add("\nIzdat lek za upotrebu: " + terapija.nazivLeka + "\n");
                paragraf.Add("Dodatno:\n");
                paragraf.Add(terapija.opisTerapije);

                d.Add(terapijaParagraf);
                d.Add(paragraf);
                LineSeparator ls = new LineSeparator(new SolidLine());
                d.Add(ls);

                brojac += 1;
            
            }
            d.Close();

            MessageBox.Show("Uspešno je napravljen izveštaj o nedeljnim terapijama!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            PrijavaProblema prijava = new PrijavaProblema();
            prijava.ShowDialog();
        }

        private void btnPretraga_Click(object sender, RoutedEventArgs e)
        {
            int indikator = -2;

            if (!Regex.IsMatch(txtPretraga.Text, @"^[\p{L}\p{M}' \.\-]+$"))
            {
                indikator = -2;
            }
            else
                indikator = 0;

            if(!PomocnaKlasaProvere.proveraPretrage(indikator))
            {
                PopuniTermine();
            }
            else
                dataGridTermin.ItemsSource = PacijentKontroler.filtrirajTermineSve(indikator, txtPretraga.Text);

        }

        private void txtPretraga_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtPretraga.Text.Length == 0)
            {
                PopuniTermine();
            }
        }
    }
}
