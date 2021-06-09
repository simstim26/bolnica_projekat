using Bolnica_aplikacija.Komande;
using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.Model;
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
    public class LekZaOdobravanjeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Liste
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

        private ObservableCollection<Lek> pLekovi;
        public ObservableCollection<Lek> lekovi
        {
            get
            {
                return pLekovi;
            }
            set
            {
                pLekovi = value;
                NotifyPropertyChanged("lekovi");
            }
        }

        private ObservableCollection<Lek> pZamenski;

        public ObservableCollection<Lek> zamenski
        {
            get
            {
                return pZamenski;
            }
            set
            {
                pZamenski = value;
                NotifyPropertyChanged("zamenski");
            }
        }

        private ObservableCollection<Lek> pZamenskiDodavanje;

        public ObservableCollection<Lek> zamenskiDodavanje
        {
            get
            {
                return pZamenskiDodavanje;
            }
            set
            {
                pZamenskiDodavanje = value;
                NotifyPropertyChanged("zamenskiDodavanje");
            }
        }
        #endregion

        #region text box-evi

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

        private String pSastojak;

        public String sastojak
        {
            get
            {
                return pSastojak;
            }
            set
            {
                pSastojak = value;
                if (String.IsNullOrWhiteSpace(pSastojak))
                {
                    btnDodaj = false;
                }
                else
                {
                    if (LekKontroler.postojiSastojak(izabraniPostojeciLek.id, sastojak.Trim()))
                    {
                        btnDodaj = false;
                        sasGr = true;
                    }
                    else
                    {
                        btnDodaj = true;
                        sasGr = false;
                    }
                }
                NotifyPropertyChanged("sastojak");
            }
        }

        private bool pSasGr;
        public bool sasGr
        {
            get
            {
                return pSasGr;
            }
            set
            {
                pSasGr = value;
                NotifyPropertyChanged("sasGr");
            }
        }

        private bool pBtnPotvrdaPrijave;

        public bool btnPotvrdaPrijave
        {
            get
            {
                return pBtnPotvrdaPrijave;
            }

            set
            {
                pBtnPotvrdaPrijave = value;
                NotifyPropertyChanged("btnPotvrdaPrijave");
            }
        }

        private String pPrijavaGreskeTekst;

        public String prijavaGreskeTekst
        {
            get
            {
                return pPrijavaGreskeTekst;
            }

            set
            {
                pPrijavaGreskeTekst = value;
                if (String.IsNullOrWhiteSpace(pPrijavaGreskeTekst))
                {
                    btnPotvrdaPrijave = false;
                }
                else
                {
                    btnPotvrdaPrijave = true;
                }
                NotifyPropertyChanged("prijavaGreskeTekst");
            }
        }
        #endregion

        #region dugmad - visibility, isEnabled
        private bool pBtnDodajVisibility;
        public bool btnDodajVisibility
        {
            get
            {
                return pBtnDodajVisibility;
            }

            set
            {
                pBtnDodajVisibility = value;
                NotifyPropertyChanged("btnDodajVisibility");
            }
        }

        private bool pBtnObrisiVisibility;
        public bool btnObrisiVisibility
        {
            get
            {
                return pBtnObrisiVisibility;
            }

            set
            {
                pBtnObrisiVisibility = value;
                NotifyPropertyChanged("btnObrisiVisibility");
            }
        }

        private bool pBtnDodaj;
        public bool btnDodaj
        {
            get
            {
                return pBtnDodaj;
            }

            set
            {
                pBtnDodaj = value;
                NotifyPropertyChanged("btnDodaj");
            }
        }

        #endregion

        #region gridovi
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

        private bool pGridIzmenaLekovaVisibility = false;
        public bool gridIzmenaLekovaVisibility
        {
            get
            {
                return pGridIzmenaLekovaVisibility;
            }
            set
            {
                pGridIzmenaLekovaVisibility = value;
                NotifyPropertyChanged("gridIzmenaLekovaVisibility");
            }
        }

        private bool pGridZamenskiLekoviVisibility = false;

        public bool gridZamenskiLekoviVisibility
        {
            get
            {
                return pGridZamenskiLekoviVisibility;
            }

            set
            {
                pGridZamenskiLekoviVisibility = value;
                NotifyPropertyChanged("gridZamenskiLekoviVisibility");
            }
        }

        private bool pGridDodavanjeZamenskihLekovaVisibility = false;

        public bool gridDodavanjeZamenskihLekovaVisibility
        {
            get
            {
                return pGridDodavanjeZamenskihLekovaVisibility;
            }

            set
            {
                pGridDodavanjeZamenskihLekovaVisibility = value;
                NotifyPropertyChanged("gridDodavanjeZamenskihLekovaVisibility");
            }
        }

        private bool pGridInfoLek = false;
        public bool gridInfoLek
        {
            get
            {
                return pGridInfoLek;
            }
            set
            {
                pGridInfoLek = value;
                NotifyPropertyChanged("gridInfoLek");
            }
        }

        private bool pGridPrijavaGreske = false;

        public bool gridPrijavaGreske
        {
            get
            {
                return pGridPrijavaGreske;
            }
            set
            {
                pGridPrijavaGreske = value;
                NotifyPropertyChanged("gridPrijavaGreske");
            }
        }
        #endregion

        #region konstruktor i pomocne metode
        public LekZaOdobravanjeViewModel()
        {
            ucitajPodatke();
            ucitajPostojeceLekove();
            odobriLek = new RelayCommand(izvrsiOdobravanjeLeka);
            odbaciLek = new RelayCommand(izvrsiOdbacivanjeLeka);
            prikaziGridIzmenaLeka = new RelayCommand(izvrsiPrikazIzmenaLeka);
            prikazGridPropratnaPoruka = new RelayCommand(IzvrsiPrikazPropratnePoruke);
            prikazZamenskiLekoviRU = new RelayCommand(izvrsiPrikazZamenskihLekovaRU);
            prikazZamenskiLekovi = new RelayCommand(izvrsiPrikazZamenskihLekova);
            dodavanjeSastojaka = new RelayCommand(izvrsiDodavanjeSastojka);
            brisanjeSastojaka = new RelayCommand(izvrsiBrisanjeSastojaka);
            dodavanjeZamenskihLekovaPrikaz = new RelayCommand(izvrsidodavanjeZamenskihLekovaPrikaz);
            dodavanjeZamenskogLeka = new RelayCommand(izvrsiDodavanjeZamenskogLeka);
            brisanjeZamenskogLeka = new RelayCommand(izvrsiBrisanjeZamenskogLeka);
            ponistiZamenskiLek = new RelayCommand(izvrsiPonistavanjeZamenskogLeka);
            infoLek = new RelayCommand(izvrsiInfoLek);
            potvrdaPrijaveGreske = new RelayCommand(izvrsiPotvrduPrijaveGreske);
            prikazPrijave = new RelayCommand(izvrsiPrikazPrijave);
            ponistavanjePrikaza = new RelayCommand(izvrsiPonistavanjePrikaza);
        }

    private void ucitajPodatke()
        {
            lekoviZaOdobravanje = new ObservableCollection<LekZaOdobravanje>(LekKontroler.nadjiLekoveZaOdobravanjeZaLogovanogLekara(KorisnikKontroler.getLekar().id));
        }

        private void ucitajPostojeceLekove()
        {
            lekovi = new ObservableCollection<Lek>(LekKontroler.ucitajSve());
        }

        #endregion

        #region polja -> izabrani lekovi iz data grid-ova

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
                NotifyPropertyChanged("pIzabraniLek");
            }
        }

        private Lek pIzabraniPostojeciLek;
        public Lek izabraniPostojeciLek
        {
            get
            {
                return pIzabraniPostojeciLek;
            }

            set
            {
                pIzabraniPostojeciLek = value;
                NotifyPropertyChanged("izabraniPostojeciLek");
            }
        }

        private String pIzabraniSastojak;

        public String izabraniSastojak
        {
            get
            {
                return pIzabraniSastojak;
            }
            set
            {
                pIzabraniSastojak = value;
                NotifyPropertyChanged("izabraniSastojak");
            }
        }

        private Lek pIzabraniLekZaDodavanje;
        public Lek izabraniLekZaDodavanje
        {
            get
            {
                return pIzabraniLekZaDodavanje;
            }
            set
            {
                pIzabraniLekZaDodavanje = value;
                NotifyPropertyChanged("izabraniLekZaDodavanje");
            }
        }

        private Lek pIzabraniLekZaBrisanje;
        public Lek izabraniLekZaBrisanje
        {
            get
            {
                return pIzabraniLekZaBrisanje;
            }

            set
            {
                pIzabraniLekZaBrisanje = value;
                NotifyPropertyChanged("izabraniLekZaBrisanje");
            }
        }
        #endregion

        #region prikaz zamenskih lekova

        private RelayCommand pPrikazZamenskiLekoviRU;
        public RelayCommand prikazZamenskiLekoviRU
        {
            get
            {
                return pPrikazZamenskiLekoviRU;
            }

            set
            {
                pPrikazZamenskiLekoviRU = value;
            }
        }

        private void izvrsiPrikazZamenskihLekovaRU(object obj)
        {
            gridIzmenaLekovaVisibility = false;
            btnDodajVisibility = true;
            btnObrisiVisibility = true;
            gridZamenskiLekoviVisibility = true;
            LekarProzor.getGlavnaLabela().Content = "Zamenski lekovi";
            if (izabraniPostojeciLek.zamenskiLekovi != null)
                zamenski = new ObservableCollection<Lek>(izabraniPostojeciLek.zamenskiLekovi);
            else
                zamenski = new ObservableCollection<Lek>();
        }

        private RelayCommand pPrikazZamenskiLekovi;
        public RelayCommand prikazZamenskiLekovi
        {
            get
            {
                return pPrikazZamenskiLekovi;
            }

            set
            {
                pPrikazZamenskiLekovi = value;
            }
        }

        private void izvrsiPrikazZamenskihLekova(object obj)
        {
            btnDodajVisibility = false;
            btnObrisiVisibility = false;
            gridZamenskiLekoviVisibility = true;
            LekarProzor.getGlavnaLabela().Content = "Zamenski lekovi";
            if (izabraniLek.zamenskiLekovi != null)
                zamenski = new ObservableCollection<Lek>(izabraniLek.zamenskiLekovi);
            else
                zamenski = new ObservableCollection<Lek>();
        }

        private RelayCommand pDodavanjeZamenskihLekovaPrikaz;
        public RelayCommand dodavanjeZamenskihLekovaPrikaz
        {
            get
            {
                return pDodavanjeZamenskihLekovaPrikaz;
            }

            set
            {
                pDodavanjeZamenskihLekovaPrikaz = value;
            }
        }

        private void izvrsidodavanjeZamenskihLekovaPrikaz(object obj)
        {
            gridDodavanjeZamenskihLekovaVisibility = true;
            zamenskiDodavanje = new ObservableCollection<Lek>(LekKontroler.ucitajSveLekoveBezZamenskih(izabraniPostojeciLek.id));
        }

        #endregion

        #region prikaz propratne poruke -> odbacivanje leka
        private RelayCommand pPrikaziGridPropratnaPoruka;
        public RelayCommand prikazGridPropratnaPoruka
        {
            get
            {
                return pPrikaziGridPropratnaPoruka;
            }

            set
            {
                pPrikaziGridPropratnaPoruka = value;
            }
        }

        private void IzvrsiPrikazPropratnePoruke(object obj)
        {
            if (izabraniLek != null)
            {
                gridPropratnaPorukaVisibility = true;
            }
            else
            {
                MessageBox.Show("Potrebno je izabrati lek!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        #endregion

        #region izmena leka - prikaz

        private RelayCommand pPrikaziGridIzmenaLeka;
        public RelayCommand prikaziGridIzmenaLeka
        {
            get
            {
                return pPrikaziGridIzmenaLeka;
            }
            set
            {
                pPrikaziGridIzmenaLeka = value;
            }
        }

        private void izvrsiPrikazIzmenaLeka(object obj)
        {
            if (izabraniPostojeciLek != null)
            {
                gridIzmenaLekovaVisibility = true;
                LekarProzor.getGlavnaLabela().Content = "Izmena leka";
            }
            else
            {
                MessageBox.Show("Potrebno je izabrati lek!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        #endregion

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

        #region dodavanje sastojaka
        private RelayCommand pDodavanjeSastojaka;
        public RelayCommand dodavanjeSastojaka
        {
            get
            {
                return pDodavanjeSastojaka;
            }

            set
            {
                pDodavanjeSastojaka = value;
            }
        }

        private void izvrsiDodavanjeSastojka(object obj)
        {
            LekKontroler.dodajSastojak(izabraniPostojeciLek.id, sastojak);
            String id = izabraniPostojeciLek.id;
            ucitajPostojeceLekove();
            izabraniPostojeciLek = LekKontroler.nadjiLekPoId(id);
            sastojak = "";
        }
        #endregion

        #region brisanje sastojaka
        private RelayCommand pBrisanjeSastojaka;
        public RelayCommand brisanjeSastojaka
        {
            get
            {
                return pBrisanjeSastojaka;
            }

            set
            {
                pBrisanjeSastojaka = value;
            }
        }

        private void izvrsiBrisanjeSastojaka(object obj)
        {
            if (izabraniSastojak != null)
            {
                String id = izabraniPostojeciLek.id;
                LekKontroler.izbrisiSastojak(izabraniPostojeciLek.id, izabraniSastojak);
                ucitajPostojeceLekove();
                izabraniPostojeciLek = LekKontroler.nadjiLekPoId(id);
            }
            else
            {
                MessageBox.Show("Izaberite sastojak za brisanje", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        #endregion

        #region dodavanje zamenskog leka
        private RelayCommand pDodavanjeZamenskogLeka;
        public RelayCommand dodavanjeZamenskogLeka
        {
            get
            {
                return pDodavanjeZamenskogLeka;
            }

            set
            {
                pDodavanjeZamenskogLeka = value;
            }
        }

        private void izvrsiDodavanjeZamenskogLeka(object obj)
        {
            if (izabraniLekZaDodavanje != null)
            {
                String id = izabraniPostojeciLek.id;
                LekKontroler.dodajZamenskiLek(izabraniPostojeciLek.id, izabraniLekZaDodavanje);
                ucitajPostojeceLekove();
                izabraniPostojeciLek = LekKontroler.nadjiLekPoId(id);
                zamenski = new ObservableCollection<Lek>(izabraniPostojeciLek.zamenskiLekovi);
                zamenskiDodavanje =new ObservableCollection<Lek>(LekKontroler.ucitajSveLekoveBezZamenskih(izabraniPostojeciLek.id));
            }
            else
            {
                MessageBox.Show("Izaberite lek za dodavanje.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        #endregion

        #region brisanje zamenskog leka
        private RelayCommand pBrisanjeZamenskogLeka;
        public RelayCommand brisanjeZamenskogLeka
        {
            get
            {
                return pBrisanjeZamenskogLeka;
            }
            set
            {
                pBrisanjeZamenskogLeka = value;
            }
        }

        private void izvrsiBrisanjeZamenskogLeka(object obj)
        {
            if (izabraniLekZaBrisanje != null)
            {
                String id = izabraniPostojeciLek.id;
                LekKontroler.obrisiZamenskiLek(izabraniPostojeciLek.id, izabraniLekZaBrisanje.id);
                ucitajPostojeceLekove();
                izabraniPostojeciLek = LekKontroler.nadjiLekPoId(id);
                zamenski = new ObservableCollection<Lek>(izabraniPostojeciLek.zamenskiLekovi);
                zamenskiDodavanje = new ObservableCollection<Lek>(LekKontroler.ucitajSveLekoveBezZamenskih(izabraniPostojeciLek.id));
            }
            else
            {
                MessageBox.Show("Izaberite lek za brisanje.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        #endregion

        #region ponistavanje dodavanja zamenskog leka
        private RelayCommand pPonistiZamenskiLek;
        public RelayCommand ponistiZamenskiLek
        {
            get
            {
                return pPonistiZamenskiLek;
            }

            set
            {
                pPonistiZamenskiLek = value;
            }
        }

        private void izvrsiPonistavanjeZamenskogLeka(object obj)
        {
            gridDodavanjeZamenskihLekovaVisibility = false;
        }

        #endregion

        #region info lek
        private RelayCommand pInfoLek;

        public RelayCommand infoLek
        {
            get
            {
                return pInfoLek;
            }
            set
            {
                pInfoLek = value;
            }
        }

        private void izvrsiInfoLek(object obj)
        {
            if (izabraniLek != null)
            {
                LekarProzor.getGlavnaLabela().Content = "Informacije o leku";
                gridInfoLek = true;
            }
            else
            {
                MessageBox.Show("Potrebno je izabrati lek!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        #endregion

        #region Komanda ->potvrda prijave greske
        private RelayCommand pPotvrdaPrijaveGreske;

        public RelayCommand potvrdaPrijaveGreske
        {
            get
            {
                return pPotvrdaPrijaveGreske;
            }
            set
            {
                pPotvrdaPrijaveGreske = value;
            }
        }

        private void izvrsiPotvrduPrijaveGreske(object obj)
        {
            PrijavaGreskeKontroler.sacuvaj(prijavaGreskeTekst);
            MessageBox.Show("Greska je uspesno prijavljena!", "informacija", MessageBoxButton.OK, MessageBoxImage.Information);
            gridPrijavaGreske = false;
        }

        private RelayCommand pPrikazPrijave;

        public RelayCommand prikazPrijave
        {
            get
            {
                return pPrikazPrijave;
            }
            set
            {
                pPrikazPrijave = value;
            }
        }

        private void izvrsiPrikazPrijave(object obj)
        {
            gridPrijavaGreske = true;
        }

        private RelayCommand pPonistavanjePrikaza;

        public RelayCommand ponistavanjePrikaza
        {
            get
            {
                return pPonistavanjePrikaza;
            }
            set
            {
                pPonistavanjePrikaza = value;
            }
        }

        private void izvrsiPonistavanjePrikaza(object obj)
        {
            gridPrijavaGreske = false;
        }
        #endregion
    }
}
