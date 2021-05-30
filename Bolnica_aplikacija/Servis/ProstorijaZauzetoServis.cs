using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.PomocneKlase;
using Bolnica_aplikacija.Repozitorijum;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Servis
{
    class ProstorijaZauzetoServis
    {
        ProstorijaZauzetoRepozitorijum prostorijaZauzetoRepozitorijum = new ProstorijaZauzetoRepozitorijum();

        public List<ProstorijaZauzeto> ucitajSve()
        {
            return prostorijaZauzetoRepozitorijum.ucitajSve();
        }

        public void upisi(List<ProstorijaZauzeto> sveProstorije)
        {
            prostorijaZauzetoRepozitorijum.upisi(sveProstorije);
        }

        public void zauzmiProstorije()
        {
            var prostorijeZaZauzimanje = ucitajSve();
            var prostorije = ProstorijaKontroler.ucitajSve();

            foreach (ProstorijaZauzeto pz in prostorijeZaZauzimanje)
            {
                if (pz.idProstorijeUKojuSePrebacuje != null)
                {

                    prebaciAkoNijeZavrseno(pz, prostorije);
                }
                else
                {
                    dodajAkoNijeZavrseno(pz,  prostorije);
                }
            }
            upisi(prostorijeZaZauzimanje);
            ProstorijaKontroler.upisi(prostorije);
        }

        public void proveriDaLiJeProsaoDatum(ProstorijaZauzeto pz, Prostorija p)
        {
            if (DateTime.Now >= pz.datumPocetka && DateTime.Now < pz.datumKraja)
            {
                p.dostupnost = false;
            }

            if (DateTime.Now >= pz.datumKraja)
            {
                p.dostupnost = true;
                pz.jeZavrseno = true;
            }
        }

        public bool proveriDaLiPostojiStavka(ProstorijaZauzeto pz, Prostorija p)
        {
            foreach (Stavka s in p.Stavka)
            {
                if (s.id == pz.idStavke)
                {
                    s.kolicina += pz.kolicinaStavke;
                    return true;
                }
            }
            return false;
        }

        public void dodajStavkuUProstoriju(ProstorijaZauzeto pz, Prostorija p)
        {
            var stavka = StavkaKontroler.pronadjiStavkuPoId(pz.idStavke);
            stavka.kolicina = pz.kolicinaStavke;
            p.Stavka.Add(stavka);
        }

        public void dodajStavkuUProstorijuPraznu(ProstorijaZauzeto pz, Prostorija p)
        {
            var stavka = StavkaKontroler.pronadjiStavkuPoId(pz.idStavke);
            stavka.kolicina = pz.kolicinaStavke;
            p.Stavka = new List<Stavka>();
            p.Stavka.Add(stavka);
        }

        private void proveriDaLiJeJosUvekZauzeta(ProstorijaZauzeto pz, Prostorija p)
        {
            if (DateTime.Now >= pz.datumPocetka && DateTime.Now < pz.datumKraja)
            {
                p.dostupnost = false;
            }
        }

        public void proveriDaLiJeDatumDrugeProsao(ProstorijaZauzeto pz, Prostorija p)
        {
            if (DateTime.Now >= pz.datumKraja)
            {
                bool postoji = false;
                p.dostupnost = true;
                pz.jeZavrseno = true;
                if (p.Stavka != null)
                {
                    postoji = proveriDaLiPostojiStavka(pz, p);
                    if (postoji == false)
                    {
                        dodajStavkuUProstoriju(pz, p);
                    }
                }
                else
                {
                    dodajStavkuUProstorijuPraznu(pz, p);
                }
            }
        }

        public void prebaciAkoNijeZavrseno(ProstorijaZauzeto pz, List<Prostorija> prostorije)
        {
            //var prostorije = ProstorijaServis.getInstance().ucitajSve();
            if (pz.jeZavrseno == false)
            {
                foreach (Prostorija p in prostorije)
                {
                    if (p.id == pz.idProstorije)
                    {
                        proveriDaLiJeProsaoDatum(pz, p);
                    }
                    if (p.id == pz.idProstorijeUKojuSePrebacuje)
                    {
                        proveriDaLiJeJosUvekZauzeta(pz, p);
                        proveriDaLiJeDatumDrugeProsao(pz, p);
                    }
                }
            }
        }

        public void dodajAkoNijeZavrseno(ProstorijaZauzeto pz, List<Prostorija> prostorije)
        {
            //var prostorije = ProstorijaServis.getInstance().ucitajSve();
            if (pz.jeZavrseno == false)
            {
                foreach (Prostorija p in prostorije)
                {
                    if (p.id == pz.idProstorije)
                    {
                        proveriDaLiJeJosUvekZauzeta(pz, p);
                        proveriDaLiJeDatumDrugeProsao(pz, p);
                    }
                }
            }
        }
    } 
}
