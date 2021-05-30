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
    public class LekZaOdobravanjeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<LekZaOdobravanje> pLekoviZaOdobravanje;
        public ObservableCollection<LekZaOdobravanje> lekoviZaOdobravanje
        {
            get
            {
                return pLekoviZaOdobravanje;
            }
            set
            {
                pLekoviZaOdobravanje = value;
                NotifyPropertyChanged("lekoviZaOdobravanje");
            }
        }

        private String pPropratnaPoruka;
        public String propratnaPoruka
        {
            get
            {
                return pPropratnaPoruka;
            }

            set
            {
                pPropratnaPoruka = value;
                NotifyPropertyChanged("propratnaPoruka");
            }
        }

        private bool pGridPropratnaPorukaVisibility = false;
        public bool gridPropratnaPorukaVisibility
        {
            get
            {
                return pGridPropratnaPorukaVisibility;
            }
            set
            {
                pGridPropratnaPorukaVisibility = value;
                NotifyPropertyChanged("gridPropratnaPorukaVisibility");
            }
        }


        public LekZaOdobravanjeViewModel()
        {
            ucitajPodatke();
            odobriLek = new RelayCommand(izvrsiOdobravanjeLeka);
            odbaciLek = new RelayCommand(izvrsiOdbacivanjeLeka);
        }

        private void ucitajPodatke()
        {
            lekoviZaOdobravanje = new ObservableCollection<LekZaOdobravanje>(LekKontroler.nadjiLekoveZaOdobravanjeZaLogovanogLekara(KorisnikKontroler.getLekar().id));
        }

        private LekZaOdobravanje izabraniLek;


        public LekZaOdobravanje pIzabraniLek
        {
            get
            {
                return izabraniLek;
            }

            set
            {
                izabraniLek = value;
            }
        }

        //Komande
        #region OdobravanjeLekaKomanda
        private RelayCommand pOdobrilek;

        public RelayCommand odobriLek
        {
            get
            {
                return pOdobrilek;
            }
            set
            {
                pOdobrilek = value; 
            }
        }

        private void izvrsiOdobravanjeLeka(object obj)
        {
            if (pIzabraniLek != null)
            {
                if (pIzabraniLek.brLekaraKojiSuodobriliLek + 1 < 2)
                {
                    LekKontroler.azurirajOdobravanje(pIzabraniLek);
                }
                else
                {
                    LekKontroler.dodajLek(pIzabraniLek);
                }

                ucitajPodatke();
            }
            else
            {
                MessageBox.Show("Potrebno je izabrati lek!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        #endregion

        #region OdbacivanjeLekaKomanda
        private RelayCommand pOdbaciLek;
        public RelayCommand odbaciLek
        {
            get
            {
                return pOdbaciLek;
            }

            set
            {
                pOdbaciLek = value;
            }
        }

        private void izvrsiOdbacivanjeLeka(object obj)
        {
            izabraniLek.propratnaPoruka = propratnaPoruka;
            LekKontroler.odbacivanjeLeka(izabraniLek);
            gridPropratnaPorukaVisibility = false;
            ucitajPodatke();
        }


        #endregion

    }
}
