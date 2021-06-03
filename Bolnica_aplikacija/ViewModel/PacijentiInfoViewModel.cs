using Bolnica_aplikacija.Komande;
using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.LekarStudent;
using Bolnica_aplikacija.Model;
using Bolnica_aplikacija.PacijentModel;
using Bolnica_aplikacija.View.LekarStudent;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Bolnica_aplikacija.ViewModel
{
    class PacijentiInfoViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        #region Polja -> pacijent i termin, selektovani termini iz tabela (prosli termini i buduci)

        private Pacijent pPacijent;
        public Pacijent pacijent
        {
            get
            {
                return pPacijent;
            }

            set
            {
                pPacijent = value;
                NotifyPropertyChanged("pacijent");
            }
        }
        private Termin pTermin;
        public Termin termin
        {
            get
            {
                return pTermin;
            }

            set
            {
                pTermin = value;
                NotifyPropertyChanged("termin");
            }
        }
        private PacijentTermin pProsliTermin;
        public PacijentTermin prosliTermin
        {
            get
            {
                return pProsliTermin;
            }

            set
            {
                pProsliTermin = value;
                NotifyPropertyChanged("prosliTermin");
            }
        }
        private PacijentTermin pBuduciTermin;
        public PacijentTermin buduciTermin
        {
            get
            {
                return pBuduciTermin;
            }

            set
            {
                pBuduciTermin = value;
                NotifyPropertyChanged("buduciTermin");
            }
        }

        private String pImeIzvestaja;
        public String imeIzvestaja
        {
            get
            {
                return pImeIzvestaja;
            }

            set
            {
                pImeIzvestaja = value;
                Regex rx = new Regex("[^A-Za-z0-9]");
                if (rx.IsMatch(pImeIzvestaja))
                {
                    greskaVis = true;
                    izvestajEnabled = false;
                }
                else
                {
                    greskaVis = false;
                    if (String.IsNullOrWhiteSpace(pImeIzvestaja))
                    {
                        izvestajEnabled = false;
                    }
                    else
                    {
                        izvestajEnabled = true;
                    }
                }
                NotifyPropertyChanged("imeIzvestaja");
            }
        }

        private bool pIzvestajEnabled;

        public bool izvestajEnabled
        {
            get
            {
                return pIzvestajEnabled;
            }
            set
            {
                pIzvestajEnabled = value;
                NotifyPropertyChanged("izvestajEnabled");
            }
        }

        private bool pGreskaVis;
        public bool greskaVis
        {
            get
            {
                return pGreskaVis;
            }
            set
            {
                pGreskaVis = value;
                NotifyPropertyChanged("greskaVis");
            }
        }

        #endregion

        #region pomocni stringovi za prikaz
        private String pImePrezime;
        public String imePrezime
        {
            get
            {
                return pImePrezime;
            }
            set
            {
                pImePrezime = value;
                NotifyPropertyChanged("imePrezime");
            }
        }
        private String pMestoRodjenja;
        public String mestoRodjenja
        {
            get
            {
                return pMestoRodjenja;
            }
            set
            {
                pMestoRodjenja = value;
                NotifyPropertyChanged("mestoRodjenja");
            }
        }

        private String pSoba;

        public String soba
        {
            get
            {
                return pSoba;
            }
            set
            {
                pSoba = value;
                NotifyPropertyChanged("soba");
            }
        }

        private String pDatum;
        public String datum
        {
            get
            {
                return pDatum;
            }

            set
            {
                pDatum = value;
                NotifyPropertyChanged("datum");
            }
        }
        #endregion

        #region Liste
        private ObservableCollection<PacijentTermin> pProsli;
        public ObservableCollection<PacijentTermin> prosli
        {
            get
            {
                return pProsli;
            }

            set
            {
                pProsli = value;
                NotifyPropertyChanged("prosli");
            }
        }
        private ObservableCollection<PacijentTermin> pBuduci;
        public ObservableCollection<PacijentTermin> buduci
        {
            get
            {
                return pBuduci;
            }

            set
            {
                pBuduci = value;
                NotifyPropertyChanged("buduci");
            }
        }


        #endregion

        #region grid bolnicko lecenje visibility
        private bool pGridBLecenjeVisibility;
        public bool gridBLecenjeVisibility
        {
            get
            {
                return pGridBLecenjeVisibility;
            }
            set
            {
                pGridBLecenjeVisibility = value;
                NotifyPropertyChanged("gridBLecenjeVisibility");
            }
        }
        #endregion

        #region konstruktor i pomocne metode

        public PacijentiInfoViewModel(String idPacijenta, String idTermina)
        {
            pacijent = PacijentKontroler.nadjiPacijenta(idPacijenta);
            imePrezime = pacijent.ime + " " + pacijent.prezime;
            mestoRodjenja = pacijent.mestoRodjenja + ", " + pacijent.drzavaRodjenja;
            zavrsiBolnickoLecenje();
            prikaziBolnickoLecenje();
            termin = TerminKontroler.nadjiTerminPoId(idTermina);
            ucitajProsleTermine();
            ucitajBuduceTermine();

            otkazi = new RelayCommand(izvrsiOtkazivanje);
            promeni = new RelayCommand(izvrsiPromenu);
            zakazi = new RelayCommand(izvrsiZakazivanje);
            izmenaBLecenja = new RelayCommand(izvrsiIzmenuBLecenja);
            alergija = new RelayCommand(izvrsiAlergija);
            terapija = new RelayCommand(izvrsiTerapija);
            istorija = new RelayCommand(izvrsiIstorija);
            izvestaj = new RelayCommand(izvrsiIzvestaj);
            prosliIzvestaj = new RelayCommand(izvrsiProsliIzvestaj);
            izgenerisi = new RelayCommand(izvrsiIzgenerisi);
        }
        private void ucitajProsleTermine()
        {
            prosli = new ObservableCollection<PacijentTermin>(PacijentKontroler.prikazProslihTerminaPacijenta(pacijent.id));
        }
        private void ucitajBuduceTermine()
        {
            buduci = new ObservableCollection<PacijentTermin>(PacijentKontroler.prikazBuducihTerminaPacijenta(pacijent.id));
        }
        private void prikaziBolnickoLecenje()
        {
            if (BolnickoLecenjeKontroler.proveriBolnickoLecenjeZaPacijenta(pacijent.id))
            {
                BolnickoLecenje bLecenje = BolnickoLecenjeKontroler.nadjiBolnickoLecenjeZaPacijenta(pacijent.id);
                gridBLecenjeVisibility = true;
                soba = bLecenje.bolnickaSoba.sprat + " " + bLecenje.bolnickaSoba.broj;
                datum= bLecenje.datumPocetka.AddDays(bLecenje.trajanje).ToString("dd.MM.yyyy");
            }
            else
            {
                gridBLecenjeVisibility = false;
            }
        }
        private void zavrsiBolnickoLecenje()
        {
            if (BolnickoLecenjeKontroler.proveriKrajBolnickogLecenje(pacijent.id))
            {
                BolnickoLecenjeKontroler.zavrsiBolnickoLecenje(pacijent.id);
            }
        }

        #endregion

        #region Komanda -> otkazivanje termina
        private RelayCommand pOtkazi;
        public RelayCommand otkazi
        {
            get
            {
                return pOtkazi;
            }
            set
            {
                pOtkazi = value;
            }
        }

        private void izvrsiOtkazivanje(object obj)
        {
            if (buduciTermin != null)
            {
                if (TerminKontroler.proveriTipTermina(KorisnikKontroler.getLekar(), buduciTermin.id))
                {
                    if (TerminKontroler.proveriDatumTermina(buduciTermin.id) <= 0)
                    {
                        MessageBox.Show("Nije moguće izvršiti otkazivanje termina 24h pred termin.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        PacijentKontroler.otkaziTerminPacijenta(buduciTermin.id);
                    }
                }
                else
                {
                    MessageBox.Show("Ne mozete otkazati operaciju!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Potrebno je izabrati termin.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            buduci = new ObservableCollection<PacijentTermin>(PacijentKontroler.prikazBuducihTerminaPacijenta(pacijent.id));
        }

        #endregion

        #region Komanda -> promena termina
        private RelayCommand pPromeni;
        public RelayCommand promeni
        {
            get
            {
                return pPromeni;
            }
            set
            {
                pPromeni = value;
            }
        }

        private void izvrsiPromenu(object obj)
        {
            if (buduciTermin != null)
            {
                if (TerminKontroler.proveriTipTermina(KorisnikKontroler.getLekar(), buduciTermin.id))
                {
                    if (TerminKontroler.proveriDatumTermina(buduciTermin.id) <= 0)
                    {
                        MessageBox.Show("Nije moguće izvršiti promenu termina 24h pred termin.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        LekarProzor.getX().Content = new ZakaziTermin(1, pacijent.id, buduciTermin.id);

                    }
                }
                else
                {
                    MessageBox.Show("Nije moguće promeniti operaciju!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
            else
            {
                MessageBox.Show("Potrebno je izabrati termin.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
        }
        #endregion

        #region Komanda -> zakazivanje termina
        private RelayCommand pZakazi;
        public RelayCommand zakazi
        {
            get
            {
                return pZakazi;
            }
            set
            {
                pZakazi = value;
            }
        }

        private void izvrsiZakazivanje(object obj)
        {
            LekarProzor.getX().Content = new ZakaziTermin(0, pacijent.id, "");
        }
        #endregion

        #region Komanda -> izmena B lecenja
        private RelayCommand pIzmenaBLecenja;
        public RelayCommand izmenaBLecenja
        {
            get
            {
                return pIzmenaBLecenja;
            }
            set
            {
                pIzmenaBLecenja = value;
            }
        }

        private void izvrsiIzmenuBLecenja(object obj)
        {
            LekarProzor.getX().Content = new IzmenaBLecenja(pacijent.id);
        }
        #endregion

        #region Komanda -> alergije
         private RelayCommand pAlergija;
        public RelayCommand alergija
        {
            get
            {
                return pAlergija;
            }
            set
            {
                pAlergija = value;
            }
        }

        private void izvrsiAlergija(object obj)
        {
            LekarProzor.getX().Content = new Alergije(pacijent.id);
        }
        #endregion

        #region Komanda -> terapije
        private RelayCommand pTerapija;
        public RelayCommand terapija
        {
            get
            {
                return pTerapija;
            }
            set
            {
                pTerapija = value;
            }
        }

        private void izvrsiTerapija(object obj)
        {
            LekarProzor.getX().Content = new UvidUTerapije(pacijent.id);
        }
        #endregion

        #region Komanda -> istorija
        private RelayCommand pIstorija;
        public RelayCommand istorija
        {
            get
            {
                return pIstorija;
            }
            set
            {
                pIstorija = value;
            }
        }

        private void izvrsiIstorija(object obj)
        {
            LekarProzor.getX().Content = new IstorijaBolesti(pacijent.id);
        }
        #endregion

        #region Komanda -> izvestaj
        private RelayCommand pIzvestaj;
        public RelayCommand izvestaj
        {
            get
            {
                return pIzvestaj;
            }
            set
            {
                pIzvestaj = value;
            }
        }

        private void izvrsiIzvestaj(object obj)
        {
            LekarProzor.getX().Content = new Izvestaj(pacijent.id, termin.idTermina);
        }
        #endregion

        #region Komanda -> prosli termin
        private RelayCommand pProsliIzvestaj;
        public RelayCommand prosliIzvestaj
        {
            get
            {
                return pProsliIzvestaj;
            }
            set
            {
                pProsliIzvestaj = value;
            }
        }

        private void izvrsiProsliIzvestaj(object obj)
        {
            if (prosliTermin != null)
            {
                LekarProzor.getX().Content = new ProsliTermini(prosliTermin.id);
            }
            else
            {
                MessageBox.Show("Potrebno je izabrati termin!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        #endregion

        #region Komanda -> izgenerisi
        private RelayCommand pIzgenerisi;
        public RelayCommand izgenerisi
        {
            get
            {
                return pIzgenerisi;
            }
            set
            {
                pIzgenerisi = value;
            }
        }

        private void izvrsiIzgenerisi(object obj)
        {
            PdfWriter writer = new PdfWriter(imeIzvestaja + ".pdf");
            PdfDocument doc = new PdfDocument(writer);
            doc.AddNewPage();
            Document d = new Document(doc);
            Paragraph zaglavljePacijent = new Paragraph();
            zaglavljePacijent.Add("Pacijent: " + imePrezime).SetBold();
            d.Add(zaglavljePacijent);
            foreach(PacijentTermin pacTermin in prosli)
            {
                Termin termin = TerminKontroler.nadjiTerminPoId(pacTermin.id);
                Lekar lekar = LekarKontroler.nadjiLekaraPoId(termin.idLekara);
                Lekar lekarUput = LekarKontroler.nadjiLekaraPoId(termin.idUputLekara);
                Terapija terapija = TerapijaKontroler.nadjiTerapijuPoId(termin.idTerapije);
                Paragraph paragraph = new Paragraph();
                Paragraph paragrafTermin = new Paragraph();
                paragrafTermin.Add(termin.getTipString() + " - " + pacTermin.datum + " " + pacTermin.satnica + "\n").SetBold();
                paragraph.Add("Lekar koji je odrzao termin: ");
                paragraph.Add(lekar.ime + " " + lekar.prezime + "\n");
                paragraph.Add("Specijalizacija: " + SpecijalizacijaKontroler.nadjiSpecijalizacijuPoId(lekar.idSpecijalizacije) + "\n");
                paragraph.Add("Dijagnoza: " + BolestKontroler.nadjiBolestPoId(termin.idBolesti).naziv + "\n");
                paragraph.Add("Terapija: " + LekKontroler.nadjiLekPoId(terapija.idLeka).naziv + "\n");
                paragraph.Add("Izdat uput za: " + TerminKontroler.nadjiTerminPoId(termin.idUputTermin).getTipString() + " kod :" + lekarUput.ime + " " + lekarUput.prezime + "\n");
                paragraph.Add("Izvestaj: " + termin.izvestaj + "\n");
                paragraph.Add("Izvestaj sa uputa: " + termin.izvestajUputa + "\n");
                paragraph.Add("Nacin upotrebe terapije: " + terapija.nacinUpotrebe + "\n");
                d.Add(paragrafTermin);
                d.Add(paragraph);
                LineSeparator ls = new LineSeparator(new SolidLine());
                d.Add(ls);

            }
            d.Close();
            MessageBox.Show("Uspesno je napravljen pdf", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        #endregion
    }

}