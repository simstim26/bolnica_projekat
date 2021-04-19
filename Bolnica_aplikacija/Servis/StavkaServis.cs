using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.Repozitorijum;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Servis
{
    class StavkaServis
    {
        StavkaRepozitorijum stavkaRepozitorijum = new StavkaRepozitorijum();
        KorisnikKontroler korisnikKontroler = new KorisnikKontroler();
        UpravnikProzor upravnikProzor = UpravnikProzor.getInstance();
        public List<Stavka> UcitajSve()
        {
            return stavkaRepozitorijum.UcitajSve();
        }

        public void Upisi(List<Stavka> sveStavke)
        {
            stavkaRepozitorijum.Upisi(sveStavke);
        }

        public List<Stavka> UcitajNeobrisaneStavke()
        {
            return stavkaRepozitorijum.UcitajNeobrisaneStavke();
        }

        public void DodajStavku() //Da li ovo sve da stavim u UpravnikProzor?
        {
            var sveStavke = stavkaRepozitorijum.UcitajSve();
            Stavka stavka = new Stavka();

            stavka.id = (sveStavke.Count() + 1).ToString();
            stavka.idBolnice = KorisnikKontroler.GetUpravnik().idBolnice;
            stavka.kolicina = Int32.Parse(upravnikProzor.textBoxKolicina.Text);
            stavka.naziv = upravnikProzor.textBoxNaziv.Text;
            stavka.proizvodjac = upravnikProzor.textBoxProizvodjac.Text;
            stavka.jeLogickiObrisana = false;
            stavka.idProstorije = null;

            if (upravnikProzor.comboBoxTipOpreme.SelectedIndex == 1)
            {
                stavka.jeStaticka = true;
            }
            else if (upravnikProzor.comboBoxTipOpreme.SelectedIndex == 2)
            {
                stavka.jeStaticka = false;
            }

            if (upravnikProzor.checkBoxPotrosna.IsChecked == true)
            {
                stavka.jePotrosnaRoba = true;
            }
            else if (upravnikProzor.checkBoxPotrosna.IsChecked == false)
            {
                stavka.jePotrosnaRoba = false;
            }

            sveStavke.Add(stavka);
            stavkaRepozitorijum.Upisi(sveStavke);
        }

        public void IzbrisiStavku(Stavka stavkaZaBrisanje)
        {
            var stavke = stavkaRepozitorijum.UcitajNeobrisaneStavke();
            foreach (Stavka stavka in stavke)
            {
                if (stavka.id == stavkaZaBrisanje.id)
                {
                    stavka.jeLogickiObrisana = true;
                }
            }

            stavkaRepozitorijum.Upisi(stavke);
        }

        public void IzmeniStavku(Stavka stavkaZaIzmenu)
        {
            UpravnikProzor upravnikProzor = UpravnikProzor.getInstance();

            var stavke = stavkaRepozitorijum.UcitajSve();
            var stavkaKopija = new Stavka();

            foreach (Stavka stavka in stavke)
            {
                if (stavka.id == stavkaZaIzmenu.id)
                {
                    stavkaKopija = stavka;
                }
            }

            stavkaKopija.naziv = upravnikProzor.txtBoxNazivStavke.Text;
            stavkaKopija.proizvodjac = upravnikProzor.txtBoxProizvodjacStavke.Text;
            stavkaKopija.kolicina = Int32.Parse(upravnikProzor.txtBoxKolicinaStavke.Text);

            if (upravnikProzor.cbTipStavke.SelectedIndex == 0)
            {
                stavkaKopija.jeStaticka = true;
            }
            else if (upravnikProzor.cbTipStavke.SelectedIndex == 1)
            {
                stavkaKopija.jeStaticka = false;
            }


            if (upravnikProzor.checkBoxPotrosnaIzmeni.IsChecked == true)
            {
                stavkaKopija.jePotrosnaRoba = true;
            }
            else if (upravnikProzor.checkBoxPotrosnaIzmeni.IsChecked == false)
            {
                stavkaKopija.jePotrosnaRoba = false;
            }

            foreach (Stavka stavka in stavke)
            {
                if (stavkaKopija.id == stavka.id)
                {
                    stavka.kolicina = stavkaKopija.kolicina;
                    stavka.naziv = stavkaKopija.naziv;
                    stavka.proizvodjac = stavkaKopija.proizvodjac;
                    stavka.jeStaticka = stavkaKopija.jeStaticka;
                }
            }

            stavkaRepozitorijum.Upisi(stavke);
        }

        public Stavka pronadjiStavkuPoId(String id)
        {
            var stavke = stavkaRepozitorijum.UcitajSve();
            var stavka = new Stavka();
            foreach (Stavka s in stavke)
            {
                if (s.id == id)
                {
                    stavka = s;
                }
            }

            return stavka;
        }

    }
}
