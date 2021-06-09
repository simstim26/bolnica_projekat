using Bolnica_aplikacija.Komande;
using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.View.SekretarStudent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Bolnica_aplikacija.ViewModel
{
    class SekretarFeedbackViewModel : BindableBase
    {     
       
        public SekretarFeedbackViewModel(SekretarFeedback parent, SekretarProzor pocetni)
        {

            this.parent = parent;
            this.pocetni = pocetni;

            SacuvajFeedback = new RelayCommand(sacuvajFeedback);
            OdustaniFeedback = new RelayCommand(odustaniFeedback);
            IzmenjenTekst = new RelayCommand(izmenjenTekst);


        }

        #region RelayCommand property
        public RelayCommand SacuvajFeedback { get; set; }
        public RelayCommand OdustaniFeedback { get; set; }
        public RelayCommand IzmenjenTekst { get; private set; }

        #endregion

        #region Polja

        SekretarFeedback parent;
        SekretarProzor pocetni;

        #endregion

        #region Komanda -> Sacuvaj feedback
        private void sacuvajFeedback(object arg)
        {
            PrijavaGreskeKontroler.sacuvaj(TxtFeedback);
            parent.Content = null;
            parent.Visibility = Visibility.Hidden;
            pocetni.PocetniEkranGrid.IsEnabled = true;
        }
        #endregion

        #region Komanda -> Odustani

        private void odustaniFeedback(object arg)
        {
            parent.Content = null;
            parent.Visibility = Visibility.Hidden;
            pocetni.PocetniEkranGrid.IsEnabled = true;

        }

        #endregion

        #region Tekst i button property

        private void izmenjenTekst(object arg)
        {
            SacuvajIsEnabled = !string.IsNullOrWhiteSpace(TxtFeedback);
        }

        private bool sacuvajIsEnabled;
        public bool SacuvajIsEnabled
        {
            get { return sacuvajIsEnabled; }
            set
            {
                sacuvajIsEnabled = value;
                OnPropertyChanged("SacuvajIsEnabled");
            }
        }

        private String txtFeedback;
        public String TxtFeedback
        {
            get { return txtFeedback; }
            set
            {
                txtFeedback = value;
                izmenjenTekst(txtFeedback);
                OnPropertyChanged("TxtFeedback");
            }
        }

        #endregion
    }
}
