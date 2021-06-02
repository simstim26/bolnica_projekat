using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.PomocneKlase;
using Bolnica_aplikacija.Repozitorijum;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Servis
{
    class StavkaServis
    {
        StavkaRepozitorijum stavkaRepozitorijum = new StavkaRepozitorijum();
        private static StavkaServis instance;
        public static StavkaServis getInstance()
        {
            if (instance == null)
            {
                instance = new StavkaServis();
            }

            return instance;
        }

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

        public void DodajStavku(StavkaDTO stavkaDTO)
        {
            var sveStavke = stavkaRepozitorijum.UcitajSve();
            Stavka stavka = new Stavka(dodajIDStavke(), stavkaDTO.naziv, stavkaDTO.kolicina, 
                stavkaDTO.proizvodjac, null, stavkaDTO.jeStaticka, false, stavkaDTO.jePotrosnaRoba);
            sveStavke.Add(stavka);
            stavkaRepozitorijum.Upisi(sveStavke);
        }

        public String dodajIDStavke()
        {
            var sveStavke = stavkaRepozitorijum.UcitajSve();
            return (sveStavke.Count() + 1).ToString();
        }

        public void IzbrisiStavku(Stavka stavkaZaBrisanje)
        {
            var stavke = stavkaRepozitorijum.UcitajNeobrisaneStavke();
            var prostorije = ProstorijaKontroler.ucitajSve();
            foreach (Stavka stavka in stavke)
            {
                if (stavka.id == stavkaZaBrisanje.id)
                {
                    foreach (Prostorija p in prostorije)
                    {
                        obrisiStavkuIzProstorije(stavka, p);
                    }
                    stavka.jeLogickiObrisana = true;
                }
            }
            ProstorijaKontroler.upisi(prostorije);
            stavkaRepozitorijum.Upisi(stavke);
        }

        public void obrisiStavkuIzProstorije(Stavka stavka, Prostorija p)
        {
            if (p.Stavka != null)
            {
                foreach (Stavka s in p.Stavka)
                {
                    if (s.id == stavka.id)
                    {
                        p.Stavka.Remove(s);
                        break;
                    }
                }
            }
        }


        public void IzmeniStavku(StavkaDTO stavkaZaIzmenu)
        {
            var stavka = pronadjiStavkuPoId(stavkaZaIzmenu.id);
            stavka = kopirajStavkuDTOuStavku(stavkaZaIzmenu, stavka);
            kopirajStavkuIUpisi(stavka);
            IzmeniStavkuUProstorijama(stavka);
        }

        public void IzmeniStavkuUProstorijama(Stavka stavka)
        {
            var prostorije = ProstorijaServis.getInstance().ucitajSve();
            foreach(Prostorija p in prostorije)
            {
                if(p.Stavka != null)
                {
                    foreach (Stavka s in p.Stavka)
                    {
                        if (s.id == stavka.id)
                        {
                            kopirajStavkuUProstoriju(s, stavka);
                            break;
                        }
                    }
                }
            }
            ProstorijaServis.getInstance().upisi(prostorije);
        }

        public Stavka kopirajStavkuDTOuStavku(StavkaDTO stavkaDTO, Stavka stavka)
        {
            stavka.naziv = stavkaDTO.naziv;
            stavka.kolicina = stavkaDTO.kolicina;
            stavka.proizvodjac = stavkaDTO.proizvodjac;
            stavka.jeStaticka = stavkaDTO.jeStaticka;
            stavka.jePotrosnaRoba = stavkaDTO.jePotrosnaRoba;
            return stavka;
        }

        public void kopirajStavkuUProstoriju(Stavka staraStavka, Stavka novaStavka)
        {
            staraStavka.naziv = novaStavka.naziv;
            staraStavka.proizvodjac = novaStavka.proizvodjac;
            staraStavka.jeStaticka = novaStavka.jeStaticka;
            staraStavka.jePotrosnaRoba = novaStavka.jePotrosnaRoba;
        }

        public void kopirajStavkuIUpisi(Stavka stavka)
        {
            var stavke = stavkaRepozitorijum.UcitajSve();
            foreach (Stavka s in stavke)
            {
                if (s.id.Equals(stavka.id))
                {
                    s.id = stavka.id;
                    s.naziv = stavka.naziv;
                    s.kolicina = stavka.kolicina;
                    s.proizvodjac = stavka.proizvodjac;
                    s.prostorija = stavka.prostorija;
                    s.idBolnice = stavka.idBolnice;
                    s.jeStaticka = stavka.jeStaticka;
                    s.jeLogickiObrisana = stavka.jeLogickiObrisana;
                    s.jePotrosnaRoba = stavka.jePotrosnaRoba;
                    break;
                }
            }
            stavkaRepozitorijum.Upisi(stavke);
        }

        public Stavka pronadjiStavkuPoId(String id)
        {
            var stavke = stavkaRepozitorijum.UcitajSve();
            Stavka stavka = null;
            foreach (Stavka s in stavke)
            {
                if (s.id == id)
                {
                    stavka = s;
                }
            }

            return stavka;
        }

        public Stavka pronadjiStavkuIzProstorijePoId(Prostorija prostorija, String stavkaId)
        {
            Stavka stavka = null;

            if(prostorija.Stavka != null)
            {
                foreach (Stavka s in prostorija.Stavka)
                {
                    if (s.id == stavkaId)
                    {
                        stavka = s;
                        return stavka;
                    }
                }
            }
            

            return stavka;
        }

        public List<Stavka> ucitajStatickeStavke()
        {
            return stavkaRepozitorijum.ucitajStatickeStavke();
        }

        public List<Stavka> ucitajDinamickeStavke()
        {
            return stavkaRepozitorijum.ucitajDinamickeStavke();
        }

        public List<Stavka> poredjajListuStavkiPoKoliciniRastuce(List<Stavka> stavke)
        {
            return stavke.OrderBy(o => o.kolicina).ToList();  
        }

        public List<Stavka> poredjajListuStavkiPoKoliciniOpadajuce(List<Stavka> stavke)
        {
            return stavke.OrderByDescending(o => o.kolicina).ToList();
        }

        public List<Stavka> poredjajListuStavkiPoNazivuOpadajuce(List<Stavka> stavke)
        {
            return stavke.OrderByDescending(o => o.naziv).ToList();
        }

        public List<Stavka> poredjajListuStavkiPoNazivuRastuce(List<Stavka> stavke)
        {
            return stavke.OrderBy(o => o.naziv).ToList();
        }

        public List<Stavka> pretraziStavku(String kriterijum, List<Stavka> stavke)
        {
            List<Stavka> stavkePretraga = new List<Stavka>();
            foreach(Stavka s in stavke)
            {
                if (s.naziv.Contains(kriterijum))
                {
                    stavkePretraga.Add(s);
                }
                    
            }
            return stavkePretraga;
        }

    }
}
