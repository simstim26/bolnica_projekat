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
        private Termin pProsliTermin;
        public Termin prosliTermin
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
        private Termin pBuduciTermin;
        public Termin buduciTermin
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
    }

}