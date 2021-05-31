using Bolnica_aplikacija.Komande;
using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.PacijentModel;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
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

        #region konstruktor i pomocne metode

        public PacijentiInfoViewModel(String idPacijenta, String idTermina)
        {
            pacijent = PacijentKontroler.nadjiPacijenta(idPacijenta);
            imePrezime = pacijent.ime + " " + pacijent.prezime;
            mestoRodjenja = pacijent.mestoRodjenja + ", " + pacijent.drzavaRodjenja;
            termin = TerminKontroler.nadjiTerminPoId(idTermina);
            ucitajProsleTermine();
            ucitajBuduceTermine();

            otkazi = new RelayCommand(izvrsiOtkazivanje);
            promeni = new RelayCommand(izvrsiPromenu);
            zakazi = new RelayCommand(izvrsiZakazivanje);
        }

        private void ucitajProsleTermine()
        {
            prosli = new ObservableCollection<PacijentTermin>(PacijentKontroler.prikazProslihTerminaPacijenta(pacijent.id));
        }

        private void ucitajBuduceTermine()
        {
            buduci = new ObservableCollection<PacijentTermin>(PacijentKontroler.prikazBuducihTerminaPacijenta(pacijent.id));
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
    }

}