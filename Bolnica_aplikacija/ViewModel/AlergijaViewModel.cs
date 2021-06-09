using Bolnica_aplikacija.Komande;
using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.Model;
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
    public class AlergijaViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private String pIdPacijenta;

        public String idPacijenta
        {
            get
            {
                return pIdPacijenta;
            }
            set
            {
                pIdPacijenta = value;
                NotifyPropertyChanged("idPacijenta");
            }
        }

        private ObservableCollection<Alergija> pAlergije;

        public ObservableCollection<Alergija> alergije
        {
            get
            {
                return pAlergije;
            }
            set
            {
                pAlergije = value;
                NotifyPropertyChanged("alergije");
            }
        }

        private Alergija pIzabranaAlergija;
        public Alergija izabranaAlergija
        {
            get
            {
                return pIzabranaAlergija;
            }
            set
            {
                pIzabranaAlergija = value;
                NotifyPropertyChanged("izabranaAlergija");
            }
        }

        private String pNaziv;
        public String naziv
        {
            get
            {
                return pNaziv;
            }
            set
            {
                pNaziv = value;
                if (String.IsNullOrWhiteSpace(pNaziv))
                {
                    btnDodajEnabled = false;
                }
                else
                {
                    if(AlergijaKontroler.proveriPostojanjeAlergije(idPacijenta, pNaziv.Trim()))
                    {
                        btnDodajEnabled = false;
                        greska = true;
                    }
                    else
                    {
                        btnDodajEnabled = true;
                        greska = false;
                    }
                }
                NotifyPropertyChanged("naziv");
            }
        }

        private bool pGreska;
        public bool greska
        {
            get
            {
                return pGreska;
            }
            set
            {
                pGreska = value;
                NotifyPropertyChanged("greska");
            }
        }

        private bool pBtnDodajEnabled;
        public bool btnDodajEnabled
        {
            get
            {
                return pBtnDodajEnabled;
            }
            set
            {
                pBtnDodajEnabled = value;
                NotifyPropertyChanged("btnDodajEnabled");
            }
        }

        public AlergijaViewModel(String idPacijenta)
        {
            this.idPacijenta = idPacijenta;
            ucitaj();
            dodavanje = new RelayCommand(izvrsiDodavanje);
            izbrisi = new RelayCommand(izvrsiBrisanje);
        }

        private void ucitaj()
        {
            alergije = new ObservableCollection<Alergija>(PacijentKontroler.procitajAlergije(idPacijenta));
        }

        #region Komanda -> dodavanje alergije
        private RelayCommand pDodavanje;
        public RelayCommand dodavanje
        {
            get
            {
                return pDodavanje;
            }
            set
            {
                pDodavanje = value;
            }
        }

        private void izvrsiDodavanje(object obj)
        {
            PacijentKontroler.napraviAlergiju(idPacijenta, naziv);
            ucitaj();
            naziv = "";
        }
        #endregion

        #region Komanda -> brisanje alergije
        private RelayCommand pIzbrisi;
        public RelayCommand izbrisi
        {
            get
            {
                return pIzbrisi;
            }
            set
            {
                pIzbrisi = value;
            }
        }

        private void izvrsiBrisanje(object obj)
        {
            if (izabranaAlergija != null)
            {
                AlergijaKontroler.obrisiAlergiju(izabranaAlergija);
                ucitaj();
            }
            else
            {
                MessageBox.Show("Potrebno je izabrati alergiju!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        #endregion

    }
}
