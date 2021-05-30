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
    public class LekarTaboviViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region liste
        private ObservableCollection<Pacijent> pPacijenti;

        public ObservableCollection<Pacijent> pacijenti
        {
            get
            {
                return pPacijenti;
            }

            set
            {
                pPacijenti = value;
                NotifyPropertyChanged("pacijenti");
            }
        }

        private ObservableCollection<PacijentTermin> pRaspored;
        public ObservableCollection<PacijentTermin> raspored
        {
            get
            {
                return pRaspored;
            }

            set
            {
                pRaspored = value;
                NotifyPropertyChanged("raspored");
            }
        }
        #endregion

        #region izabrani termin i pacijent iz data grida
        private PacijentTermin pIzabraniTermin;
        public PacijentTermin izabraniTermin
        {
            get
            {
                return pIzabraniTermin;
            }

            set
            {
                pIzabraniTermin = value;
                NotifyPropertyChanged("izabraniTermin");
            }
        }

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


        #endregion

        #region konstruktori i pomocne metode

        public LekarTaboviViewModel()
        {
            ucitajPacijente();
            ucitajRaspored();
            otkazi = new RelayCommand(izvrsiOtkazivanje);
            infoRaspored = new RelayCommand(izvrsiInfoRaspored);
            infoPacijent = new RelayCommand(izvrsiInfoPacijent);
        }

        private void ucitajPacijente()
        {
            pacijenti = new ObservableCollection<Pacijent>(PacijentKontroler.prikazPacijenata());
        }
        private void ucitajRaspored()
        {
            raspored = new ObservableCollection<PacijentTermin>(LekarKontroler.prikaziZauzeteTermineZaLekara(KorisnikKontroler.getLekar()));
        }

        #endregion

        #region Komanda -> otkazivanje termina iz rasporeda
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
            if (izabraniTermin != null)
            {
                PacijentKontroler.otkaziTerminPacijenta(izabraniTermin.id);
                /*if (this.prvi.SelectedDate != null && this.prvi.SelectedDate != null)
                {
                    dataRaspored.ItemsSource = LekarKontroler.pretraziZauzeteTermineZaLekara(KorisnikKontroler.getLekar(), (DateTime)this.prvi.SelectedDate, (DateTime)this.drugi.SelectedDate);

                }
                else
                {
                    this.prvi.SelectedDate = null;
                    this.drugi.SelectedDate = null;
                    ucitajSve();
                }*/
                raspored = new ObservableCollection<PacijentTermin>(LekarKontroler.prikaziZauzeteTermineZaLekara(KorisnikKontroler.getLekar()));
            }
            else
            {
                MessageBox.Show("Potrebno je izabrati termin!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        #endregion

        #region Komanda -> info raspored
        private RelayCommand pInfoRaspored;

        public RelayCommand infoRaspored
        {
            get
            {
                return pInfoRaspored;
            }
            set
            {
                pInfoRaspored = value;
            }
        }

        private void izvrsiInfoRaspored(object obj)
        {
            if (izabraniTermin != null)
            {
                LekarProzor.getX().Content = new PacijentInfo(TerminKontroler.nadjiPacijentaZaTermin(izabraniTermin.id).id, izabraniTermin.id);
            }
            else
            {
                MessageBox.Show("Potrebno je izabrati termin!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        #endregion

        #region Komanda -> info pacijent
        private RelayCommand pInfoPacijent;
        public RelayCommand infoPacijent
        {
            get
            {
                return pInfoPacijent;
            }
            set
            {
                pInfoPacijent = value;
            }
        }

        private void izvrsiInfoPacijent(object obj)
        {
            if (pacijent != null)
            {
                LekarProzor.getX().Content = new PacijentInfo(pacijent.id, "");
            }
            else
            {
                MessageBox.Show("Potrebno je izabrati pacijenta!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        #endregion
    }
}
