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
            //var stavke = StavkaKontroler.UcitajSve();
            bool postoji = false;

            foreach (ProstorijaZauzeto pz in prostorijeZaZauzimanje)
            {
                if (pz.idProstorijeUKojuSePrebacuje != null)
                {
                    if (pz.jeZavrseno == false)
                    {
                        foreach (Prostorija p in prostorije)
                        {
                            postoji = false;
                            if (p.id == pz.idProstorije)
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

                            if (p.id == pz.idProstorijeUKojuSePrebacuje)
                            {
                                if (DateTime.Now >= pz.datumPocetka && DateTime.Now < pz.datumKraja)
                                {
                                    p.dostupnost = false;
                                }

                                if (DateTime.Now >= pz.datumKraja)
                                {
                                    p.dostupnost = true;
                                    pz.jeZavrseno = true;

                                    if (p.Stavka.Count != 0)
                                    {
                                        foreach (Stavka s in p.Stavka)
                                        {
                                            if (s.id == pz.idStavke)
                                            {
                                                s.kolicina += pz.kolicinaStavke;
                                                postoji = true;
                                            }
                                        }

                                        if (postoji == false)
                                        {
                                            var stavka = StavkaKontroler.pronadjiStavkuPoId(pz.idStavke);
                                            stavka.kolicina = pz.kolicinaStavke;
                                            p.Stavka.Add(stavka);
                                        }
                                    }
                                    else
                                    {
                                        var stavka = StavkaKontroler.pronadjiStavkuPoId(pz.idStavke);
                                        stavka.kolicina = pz.kolicinaStavke;
                                        p.Stavka.Add(stavka);
                                    }

                                }
                            }

                        }
                    }
                }
                else
                {
                    if (pz.jeZavrseno == false)
                    {
                        foreach (Prostorija p in prostorije)
                        {
                            if (p.id == pz.idProstorije)
                            {
                                if (DateTime.Now >= pz.datumPocetka && DateTime.Now < pz.datumKraja)
                                {
                                    p.dostupnost = false;
                                }

                                if (DateTime.Now >= pz.datumKraja)
                                {
                                    p.dostupnost = true;
                                    pz.jeZavrseno = true;

                                    if (p.Stavka.Count != 0)
                                    {
                                        foreach (Stavka s in p.Stavka)
                                        {
                                            if (s.id == pz.idStavke)
                                            {
                                                s.kolicina += pz.kolicinaStavke;
                                                postoji = true;
                                            }
                                        }

                                        if (postoji == false)
                                        {
                                            var stavka = StavkaKontroler.pronadjiStavkuPoId(pz.idStavke);
                                            stavka.kolicina = pz.kolicinaStavke;
                                            p.Stavka.Add(stavka);
                                        }
                                    }
                                    else
                                    {
                                        var stavka = StavkaKontroler.pronadjiStavkuPoId(pz.idStavke);
                                        stavka.kolicina = pz.kolicinaStavke;
                                        p.Stavka.Add(stavka);
                                    }
                                }

                            }
                        }

                    }
                }
            }
            upisi(prostorijeZaZauzimanje);
            ProstorijaKontroler.upisi(prostorije);
        }
    }
}
