using Bolnica_aplikacija.Komande;
using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.Model;
using Bolnica_aplikacija.PomocneKlase;
using Bolnica_aplikacija.View.SekretarStudent;
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
    class SekretarObavestenjaViewModel : BindableBase
    {
        public SekretarObavestenjaViewModel(SekretarProzor parent)
        {
            this.parent = parent;           
            buttonObavestenjaPovratak = new RelayCommand(btnPovratakObavestenja_Click);
            txtNaslovaObavestenjaPromenjen = new RelayCommand(txtNaslovObavestenja_TextChanged);
            txtSadrzajObavetenjaPromenjen = new RelayCommand(txtSadrzajObavestenja_TextChanged);
            btnDodajObavestenjeCommand = new RelayCommand(btnDodajObavestenje_Click);
            btnIzmeniObavestenjeCommand = new RelayCommand(btnIzmeniObavestenje_Click);
            btnIzbrisiObavestenjeCommand = new RelayCommand(btnIzbrisiObavestenje_Click);
            btnOdustaniIzmenaObavestenjaCommand = new RelayCommand(btnOdustaniIzmenaObavestenja_Click);
            dataGridObavestenjaIzmenaSelekcija = new RelayCommand(dataGridObavestenja_SelectionChanged);

            ucitajObavestenjaUTabelu();
        }

        #region RelayCommand property

        public RelayCommand buttonObavestenjaPovratak { get; set; }
        public RelayCommand txtNaslovaObavestenjaPromenjen { get; private set; }
        public RelayCommand txtSadrzajObavetenjaPromenjen { get; private set; }
        public RelayCommand btnDodajObavestenjeCommand { get; private set; }
        public RelayCommand btnIzmeniObavestenjeCommand { get; private set; }
        public RelayCommand btnIzbrisiObavestenjeCommand { get; private set; }
        public RelayCommand btnOdustaniIzmenaObavestenjaCommand { get; private set; }
        public RelayCommand dataGridObavestenjaIzmenaSelekcija { get; private set; }

        #endregion

        #region Polja

        SekretarProzor parent;
        private int tipAkcijeObavestenja; // 0 - dodaj, 1 - izmeni, 2 - ukloni
        private Obavestenje izabranoObavestenje;
        
        #endregion

        #region Pomocne funkcije
        private void proveriPopunjenostPoljaObavestenja()
        {
            if (tipAkcijeObavestenja == 0)
            {
                BtnDodajObavestenjeIsEnabled = !string.IsNullOrWhiteSpace(TxtNaslovObavestenja) &&
                                               !string.IsNullOrWhiteSpace(TxtSadrzajObavestenja);

            }
            else
            {
                BtnIzmeniObavestenjeIsEnabled = !string.IsNullOrWhiteSpace(TxtNaslovObavestenja) &&
                                                !string.IsNullOrWhiteSpace(TxtSadrzajObavestenja);

            }

        }

        private void ucitajObavestenjaUTabelu()
        {
            List<Obavestenje> svaObavestenja = ObavestenjeKontroler.ucitajObavestenja();
            obavestenja = new ObservableCollection<Obavestenje>(svaObavestenja);
        }

        private void ocistiPoljaObavestenja()
        {
            TxtSadrzajObavestenja = "";
            TxtNaslovObavestenja = "";
            BtnDodajObavestenjeIsEnabled = false;
            BtnIzmeniObavestenjeIsEnabled = false;
            BtnObrisiObavestenjeIsEnabled = false;

            BtnOdustaniIzmenaObavestenja = Visibility.Hidden;
        }

        #endregion

        #region Button property


        private bool btnDodajObavestenjeIsEnabled;
        public bool BtnDodajObavestenjeIsEnabled
        {
            get { return btnDodajObavestenjeIsEnabled; }
            set
            {
                btnDodajObavestenjeIsEnabled = value;
                OnPropertyChanged("BtnDodajObavestenjeIsEnabled");
            }
        }

        private bool btnIzmeniObavestenjeIsEnabled;
        public bool BtnIzmeniObavestenjeIsEnabled
        {
            get { return btnIzmeniObavestenjeIsEnabled; }
            set
            {
                btnIzmeniObavestenjeIsEnabled = value;
                OnPropertyChanged("BtnIzmeniObavestenjeIsEnabled");
            }
        }

        private bool btnObrisiObavestenjeIsEnabled;
        public bool BtnObrisiObavestenjeIsEnabled
        {
            get { return btnObrisiObavestenjeIsEnabled; }
            set
            {
                btnObrisiObavestenjeIsEnabled = value;
                OnPropertyChanged("BtnObrisiObavestenjeIsEnabled");
            }
        }

        private Visibility btnOdustaniIzmenaObavestenja = Visibility.Hidden;
        public Visibility BtnOdustaniIzmenaObavestenja
        {
            get { return btnOdustaniIzmenaObavestenja; }
            set
            {
                btnOdustaniIzmenaObavestenja = value;
                OnPropertyChanged("BtnOdustaniIzmenaObavestenja");
            }
        }
        #endregion

        #region Text property


        private String txtNaslovObavestenja;
        public String TxtNaslovObavestenja
        {
            get { return txtNaslovObavestenja; }
            set
            {
                txtNaslovObavestenja = value;
                txtNaslovObavestenja_TextChanged(txtNaslovObavestenja);
                OnPropertyChanged("TxtNaslovObavestenja");
            }
        }

        private String txtSadrzajObavestenja;
        public String TxtSadrzajObavestenja
        {
            get { return txtSadrzajObavestenja; }
            set
            {
                txtSadrzajObavestenja = value;
                txtNaslovObavestenja_TextChanged(txtSadrzajObavestenja);
                OnPropertyChanged("TxtSadrzajObavestenja");
            }
        }
        private void txtNaslovObavestenja_TextChanged(object obj)
        {
            proveriPopunjenostPoljaObavestenja();
        }

        private void txtSadrzajObavestenja_TextChanged(object obj)
        {
            proveriPopunjenostPoljaObavestenja();
        }

        #endregion

        #region Obavestenja i selekcija
        public ObservableCollection<Obavestenje> obavestenja { get; set; }

        private Obavestenje selektovanoObavestenje;
        public Obavestenje SelektovanoObavestenje
        {
            get { return selektovanoObavestenje; }
            set
            {
                selektovanoObavestenje = value;
                dataGridObavestenja_SelectionChanged(selektovanoObavestenje);
                OnPropertyChanged("SelektovanoObavestenje");
            }
        }

        private void dataGridObavestenja_SelectionChanged(object obj)
        {
            tipAkcijeObavestenja = 1;
            if (SelektovanoObavestenje != null)
            {
                izabranoObavestenje = SelektovanoObavestenje;
                TxtNaslovObavestenja = izabranoObavestenje.naslovObavestenja;
                TxtSadrzajObavestenja = izabranoObavestenje.sadrzajObavestenja;
                BtnObrisiObavestenjeIsEnabled = true;
                BtnOdustaniIzmenaObavestenja = Visibility.Visible;
            }
            else
            {
                BtnIzmeniObavestenjeIsEnabled = false;
                BtnObrisiObavestenjeIsEnabled = false;
                tipAkcijeObavestenja = 0;
            }

        }

        #endregion

        #region komanda -> Povratak na pocetni prozor
        private void btnPovratakObavestenja_Click(object obj)
        {
            ocistiPoljaObavestenja();

            parent.frame.Content = null;
            parent.PocetniEkranGrid.Visibility = Visibility.Visible;

        }

        #endregion

        #region komanda -> Dodaj obavestenje
        private void btnDodajObavestenje_Click(object obj)
        {
            tipAkcijeObavestenja = 0;
            String naslovObavestenja = TxtNaslovObavestenja;
            String sadrzajObavestenja = TxtSadrzajObavestenja;

            ObavestenjeKontroler.napraviObavestenje(new Obavestenje(naslovObavestenja, sadrzajObavestenja));
            ocistiPoljaObavestenja();
            ucitajObavestenjaUTabelu();
            parent.frame.Content = null;
            parent.frame.Content = new SekretarObavestenja(parent);
        }
        #endregion

        #region komanda -> Izmeni obavestenje

        private void btnIzmeniObavestenje_Click(object obj)
        {
            Obavestenje obavestenje = new Obavestenje(TxtNaslovObavestenja, TxtSadrzajObavestenja);
            obavestenje.id = selektovanoObavestenje.id;
            ObavestenjeKontroler.azurirajObavestenje(obavestenje);
            ocistiPoljaObavestenja();
            ucitajObavestenjaUTabelu();
            parent.frame.Content = null;
            parent.frame.Content = new SekretarObavestenja(parent);
        }

        #endregion

        #region komanda -> Obrisi obavestenje

        private void btnIzbrisiObavestenje_Click(object obj)
        {
            ObavestenjeKontroler.obrisiObavestenje(izabranoObavestenje.id);
            ocistiPoljaObavestenja();
            ucitajObavestenjaUTabelu();
            parent.frame.Content = null;
            parent.frame.Content = new SekretarObavestenja(parent);
        }

        #endregion

        #region Komanda -> Odustani od izmene
        private void btnOdustaniIzmenaObavestenja_Click(object obj)
        {
            SelektovanoObavestenje = null;
            ocistiPoljaObavestenja();
        }
        #endregion

    }
}
