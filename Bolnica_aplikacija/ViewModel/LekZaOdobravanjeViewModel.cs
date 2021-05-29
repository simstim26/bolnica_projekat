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

        public LekZaOdobravanjeViewModel()
        {
            ucitajPodatke();
            odobriLek = new RelayCommand(izvrsiOdobravanjeLeka);
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

    }
}
