using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.PomocneKlase;
using Bolnica_aplikacija.Repozitorijum;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Bolnica_aplikacija.Servis
{
    class ProstorijaServis
    {
        private static ProstorijaServis instance;
        public static ProstorijaServis getInstance()
        {
            if(instance == null)
            {
                instance = new ProstorijaServis();
            }

            return instance;
        }

        ProstorijaRepozitorijum prostorijaRepozitorijum = new ProstorijaRepozitorijum();

        public String nadjiBrojISprat(String idProstorije)
        {
            Prostorija prostorija = nadjiProstorijuPoId(idProstorije);
            return prostorija.broj + " " + prostorija.sprat;
        }

        public List<Prostorija> pronadjiSlobodneBolnickeSobe()
        {
            List<Prostorija> povratnaVrednost = new List<Prostorija>();

            foreach(Prostorija prostorija in pronadjiBolnickeSobe())
            {
                if (proveriBrojKreveta(prostorija) > 0)
                    povratnaVrednost.Add(prostorija);
            }

            return povratnaVrednost;
        }

        private int proveriBrojKreveta(Prostorija prostorija)
        {
            if (prostorija.Stavka == null || prostorija.Stavka.Count == 0)
                return 0;

            foreach(Stavka stavka in prostorija.Stavka)
            {
                if (stavka.naziv.ToLower().Equals("krevet"))
                {
                    return stavka.kolicina - prostorija.brojZauzetihKreveta;
                }
            }

            return 0;
        }

        public List<Prostorija> pronadjiBolnickeSobe()
        {
            List<Prostorija> povratnaVrednost = new List<Prostorija>();

            foreach(Prostorija prostorija in ucitajNeobrisane())
            {
                if(prostorija.dostupnost && prostorija.tipProstorije == TipProstorije.BOLNICKA_SOBA)
                {
                    povratnaVrednost.Add(prostorija);
                }
            }
            return povratnaVrednost;
        }

        public List<Prostorija> ucitajSve()
        {
            return prostorijaRepozitorijum.ucitajSve();
        }

        public List<Prostorija> ucitajNeobrisane()
        {
            return prostorijaRepozitorijum.ucitajNeobrisane();
        }

        public void upisi(List<Prostorija> sveProstorije)
        {
            prostorijaRepozitorijum.upisi(sveProstorije);
        }

        public bool AzurirajProstoriju(ProstorijaDTO prostorija)
        {
            var prostorije = prostorijaRepozitorijum.ucitajSve();
            Prostorija prostorijaZaIzmenu = nadjiProstorijuPoId(prostorija.id);
            prostorija.Stavka = prostorijaZaIzmenu.Stavka;
            bool istiBroj = proveriIstiBroj(prostorijaZaIzmenu, prostorija);
            bool pronadjenBroj = proveriDaLiBrojISpratVecPostoje(prostorija);

            if (pronadjenBroj && !istiBroj)
            {
                return false;
            }
            else
            {
                prostorijaZaIzmenu = kopirajProstorijuDTOuProstoriju(prostorija);
                kopirajProstorijuIUpisi(prostorijaZaIzmenu);
                return true;
            }

        }

        public Prostorija kopirajProstorijuDTOuProstoriju(ProstorijaDTO prostorijaDTO)
        {
            Prostorija p = new Prostorija();
            p.id = prostorijaDTO.id;
            p.idBolnice = prostorijaDTO.idBolnice;
            p.logickiObrisana = false;
            p.sprat = prostorijaDTO.sprat;
            p.tipProstorije = prostorijaDTO.tipProstorije;
            p.broj = prostorijaDTO.broj;
            p.dostupnost = prostorijaDTO.dostupnost;
            p.Stavka = prostorijaDTO.Stavka;
            return p;
        }

        public ProstorijaDTO kopirajProstorijuuProstorijuDTO(Prostorija prostorija)
        {
            ProstorijaDTO p = new ProstorijaDTO();
            p.id = prostorija.id;
            p.idBolnice = prostorija.idBolnice;
            p.logickiObrisana = false;
            p.sprat = prostorija.sprat;
            p.tipProstorije = prostorija.tipProstorije;
            p.broj = prostorija.broj;
            p.dostupnost = prostorija.dostupnost;
            p.Stavka = prostorija.Stavka;
            return p;
        }


        public bool proveriIstiBroj(Prostorija prostorija, ProstorijaDTO prostorijaDTO)
        {
            if (prostorija.broj == prostorijaDTO.broj && prostorija.sprat == prostorijaDTO.sprat)
            {
                return true;
            }
            return false;
        }

        public bool proveriDaLiBrojISpratVecPostoje(ProstorijaDTO prostorijaDTO)
        {
            var prostorije = prostorijaRepozitorijum.ucitajSve();
            foreach (Prostorija p in prostorije)
            {
                if (p.broj == prostorijaDTO.broj)// && p.broj != prostorija.broj)
                {
                    if (p.sprat == prostorijaDTO.sprat)
                    {
                        return true;
                    }

                }
            }
            return false;
        }

        public void azurirajBrojZauzetihKreveta(string id)
        {
            List<Prostorija> prostorije = ucitajNeobrisane();
            foreach(Prostorija prostorija in prostorije)
            {
                if (id.Equals(prostorija.id))
                {
                    ++prostorija.brojZauzetihKreveta;
                    break;
                }
            }
            upisi(prostorije);
        }

        public bool ProveriProstoriju(ProstorijaDTO prostorija)
        {
            bool pronadjenBroj = false;
            bool pronadjenSprat = false;

            pronadjenBroj = proveriBrojProstorije(prostorija.broj);
            pronadjenSprat = proveriSpratProstorije(prostorija.sprat);

            if (pronadjenBroj && pronadjenSprat)// && !istiBroj)
            {
                return false;
            }
            else
            {
                napraviProstoriju(prostorija);
                return true;
            }
        }

        private bool proveriBrojProstorije(String brojProstorije)
        {
            var prostorije = ProstorijaKontroler.ucitajSve();
            foreach (Prostorija p in prostorije)
            {
                if (p.broj == brojProstorije)
                {
                    return true;
                }
            }
            return false;
        }

        private bool proveriSpratProstorije(int spratProstorije)
        {
            var prostorije = ProstorijaKontroler.ucitajSve();
            foreach (Prostorija p in prostorije)
            {
                if (p.sprat == spratProstorije)
                {
                    return true;
                }
            }
            return false;
        }

        private void napraviProstoriju(ProstorijaDTO prostorijaDTO)
        {
            var prostorije = prostorijaRepozitorijum.ucitajSve();
            Prostorija prostorija = new Prostorija();

            prostorija.id = (prostorije.Count + 1).ToString();
            prostorija.broj = prostorijaDTO.broj;
            prostorija.sprat = prostorijaDTO.sprat;
            prostorija.logickiObrisana = false;
            prostorija.dostupnost = true;
            prostorija.tipProstorije = prostorijaDTO.tipProstorije;

            prostorije.Add(prostorija);
            prostorijaRepozitorijum.upisi(prostorije);
        }

        public void ObrisiProstoriju(String idProstorija)
        {

            Prostorija prostorija = new Prostorija();
            var prostorije = prostorijaRepozitorijum.ucitajSve();

            foreach (Prostorija p in prostorije)
            {
                if (p.id == idProstorija)
                {
                    prostorija = p;
                }
            }

            prostorija.logickiObrisana = true;
            prostorijaRepozitorijum.upisi(prostorije);

        }

        public bool postojeTerminiZaPeriodPremestanja(DateTime datumPocetka, DateTime datumKraja, Prostorija p)
        {
            foreach (Termin t in TerminKontroler.ucitajSve())
            {
                if (t.idProstorije == p.id)
                {
                    if (t.datum >= datumPocetka && t.datum <= datumKraja && t.idPacijenta != "")
                    {
                        Console.WriteLine("TERMIN POSTOJI U OVOM VREMENSKOM PERIODU");
                        return true;
                    }
                }
            }

            return false;
        }


        public void premestiStavku(ProstorijaPrebacivanjeDTO prebacivanje)
        {
            var prostorijaIz = nadjiProstorijuPoId(prebacivanje.idProstorijeIzKojeSePrebacuje);
            var prostorijaU = nadjiProstorijuPoId(prebacivanje.idProstorijeUKojuSePrebacuje);
            int kolicina = prebacivanje.kolicinaStavke;
            var stavkaId = prebacivanje.idStavke;

            if (kolicina <= StavkaServis.getInstance().pronadjiStavkuIzProstorijePoId(prostorijaIz, prebacivanje.idStavke).kolicina)
            {
                manipulisanjeStavkamaProstorijeIzKojeSePrebacuje(kolicina, prostorijaIz, StavkaServis.getInstance().pronadjiStavkuIzProstorijePoId(prostorijaIz, stavkaId));
                if (StavkaServis.getInstance().pronadjiStavkuPoId(stavkaId).jeStaticka)
                {
                    premestiStatickuStavku(prebacivanje);
                }
                else
                {
                    premestiDinamickuStavku(prebacivanje);
                }
                return;
            }
        }

        private void premestiDinamickuStavku(ProstorijaPrebacivanjeDTO prebacivanje)
        {
            Stavka stavkaKojaSePrebacuje = StavkaServis.getInstance().pronadjiStavkuPoId(prebacivanje.idStavke);
            var prostorijaU = nadjiProstorijuPoId(prebacivanje.idProstorijeUKojuSePrebacuje);
            var prostorijaIz = nadjiProstorijuPoId(prebacivanje.idProstorijeIzKojeSePrebacuje);
            var stavka = StavkaServis.getInstance().pronadjiStavkuIzProstorijePoId(prostorijaU, prebacivanje.idStavke);

            if (stavka != null)
            {
                dodavanjeKolicineStavke(prebacivanje, stavka);
                return;
            }
            else
            {
                stavka = dodavanjeNoveStavke(prebacivanje);
            }
        }

        private void manipulisanjeStavkamaProstorijeIzKojeSePrebacuje(int kolicina, Prostorija prostorijaIz, Stavka stavkaIz)
        {
            if (stavkaIz.kolicina - kolicina == 0)
            {
                obrisiStavkuIzProstorije(stavkaIz.id, prostorijaIz);
            }
            else
            {
                smanjiKolicinuStavkeIzProstorije(prostorijaIz, stavkaIz, kolicina);
            }
        }

        private void premestiStatickuStavku(ProstorijaPrebacivanjeDTO prebacivanje)
        {
            var prostorijaIz = nadjiProstorijuPoId(prebacivanje.idProstorijeIzKojeSePrebacuje);
            if (ZakaziPremestanjeU(prebacivanje))
            {
                kopirajProstorijuIUpisi(prostorijaIz);
                return;
            }
        }

        private void dodavanjeKolicineStavke(ProstorijaPrebacivanjeDTO prebacivanje, Stavka stavka)
        {
            stavka.kolicina += prebacivanje.kolicinaStavke;
            var prostorijaU = nadjiProstorijuPoId(prebacivanje.idProstorijeUKojuSePrebacuje);
            var prostorijaUIzmenjena = dodajKolicinuStavkeUProstoriju(prostorijaU, stavka);
            var prostorijaIz = nadjiProstorijuPoId(prebacivanje.idProstorijeIzKojeSePrebacuje);
            kopirajStavke(prostorijaU);
            kopirajProstorijuIUpisi(prostorijaIz);
            UpravnikProzor.getInstance().dataGridInventarProstorije.ItemsSource = prostorijaIz.Stavka;
            return;
        }

        private Prostorija dodajKolicinuStavkeUProstoriju(Prostorija prostorija, Stavka stavka)
        {
            foreach (Stavka s in prostorija.Stavka)
            {
                if (s.id == stavka.id)
                {
                    s.kolicina = stavka.kolicina;
                }
            }

            return prostorija;
        }

        private Stavka dodavanjeNoveStavke(ProstorijaPrebacivanjeDTO prebacivanje)
        {
            Stavka stavka = StavkaServis.getInstance().pronadjiStavkuPoId(prebacivanje.idStavke);
            stavka.kolicina = prebacivanje.kolicinaStavke;
            var prostorijaU = nadjiProstorijuPoId(prebacivanje.idProstorijeUKojuSePrebacuje);
            var prostorijaIz = nadjiProstorijuPoId(prebacivanje.idProstorijeIzKojeSePrebacuje);
            prostorijaU.Stavka.Add(stavka);
            kopirajStavke(prostorijaU);
            kopirajProstorijuIUpisi(prostorijaIz);
            UpravnikProzor.getInstance().dataGridInventarProstorije.ItemsSource = prostorijaIz.Stavka;
            return stavka;
        }

        private void smanjiKolicinuStavkeIzProstorije(Prostorija prostorijaIz, Stavka stavkaIz, int kolicina)
        {
            foreach (Stavka s in prostorijaIz.Stavka)
            {
                if (s.id == stavkaIz.id)
                {
                    s.kolicina -= kolicina;
                    break;
                }
            }
            kopirajProstorijuIUpisi(prostorijaIz);
        }

        private void obrisiStavkuIzProstorije(string stavkaId, Prostorija prostorijaIz)
        {
            foreach (Stavka stavka in prostorijaIz.Stavka)
            {
                if (stavka.id == stavkaId)
                {
                    prostorijaIz.Stavka.Remove(stavka);
                    break;
                }
            }
            kopirajProstorijuIUpisi(prostorijaIz);
        }   

        public void kopirajStavke(Prostorija prostorijaIzKojeSeKopira)
        {
            var prostorije = prostorijaRepozitorijum.ucitajSve();

            foreach (Prostorija pros in prostorije)
            {
                if (pros.id == prostorijaIzKojeSeKopira.id)
                {
                    pros.Stavka = prostorijaIzKojeSeKopira.Stavka;
                    break;
                }
            }
            prostorijaRepozitorijum.upisi(prostorije);
        }

        public void dodajStavku(ProstorijaPrebacivanjeDTO prebacivanje)
        {
            var prostorijaUKojuSePrebacuje = nadjiProstorijuPoId(prebacivanje.idProstorijeUKojuSePrebacuje);
            var novaStavka = StavkaServis.getInstance().pronadjiStavkuPoId(prebacivanje.idStavke);
            var stavkaId = prebacivanje.idStavke;
            var kolicina = prebacivanje.kolicinaStavke;

            if (saberiKolicinuStavkeUSvimProstorijama(stavkaId) + kolicina <= novaStavka.kolicina)
            {
                var stavka = StavkaServis.getInstance().pronadjiStavkuIzProstorijePoId(prostorijaUKojuSePrebacuje, stavkaId);

                if (novaStavka.jeStaticka == true)
                {
                    if (ZakaziPremestanje(prebacivanje))
                    {
                        return;
                    }
                }
                else
                {
                    if (stavka == null)
                    {
                        novaStavka.kolicina = kolicina;
                        prostorijaUKojuSePrebacuje.Stavka.Add(novaStavka);
                        kopirajStavke(prostorijaUKojuSePrebacuje);
                    }
                    else
                    {
                        stavka.kolicina += kolicina;
                        kopirajStavke(prostorijaUKojuSePrebacuje);
                    }

                }
            }

        }

        private int saberiKolicinuStavkeUSvimProstorijama(string stavkaId)
        {
            int suma = 0;
            foreach (Prostorija p in prostorijaRepozitorijum.ucitajSve())
            {
                if (p.Stavka != null)
                {
                    foreach (Stavka s in p.Stavka)
                    {
                        if (s.id == stavkaId)
                        {
                            suma += s.kolicina;
                        }
                    }
                }
            }
            return suma;
        }

        private bool ZakaziPremestanjeU(ProstorijaPrebacivanjeDTO prebacivanje)
        {
            var prostorijeZauzete = ProstorijaZauzetoKontroler.ucitajSve();

            int kolicina = prebacivanje.kolicinaStavke;
            DateTime datumPocetka = prebacivanje.datumPocetka;
            DateTime datumKraja = prebacivanje.datumKraja;
            Prostorija prostorijaIz = nadjiProstorijuPoId(prebacivanje.idProstorijeIzKojeSePrebacuje);
            Prostorija prostorijaU = nadjiProstorijuPoId(prebacivanje.idProstorijeUKojuSePrebacuje);

            if (datumKraja >= datumPocetka)
            {
                if (!postojeTerminiZaPeriodPremestanja(datumPocetka, datumKraja, prostorijaIz))
                {
                    if (!postojeTerminiZaPeriodPremestanja(datumPocetka, datumKraja, prostorijaU))
                    {
                        ProstorijaZauzeto prostorijaZaZauzimanje = new ProstorijaZauzeto(prostorijaIz.id, prostorijaU.id, datumPocetka, datumKraja,
                        false, prebacivanje.idStavke, kolicina);
                        prostorijeZauzete.Add(prostorijaZaZauzimanje);
                        ProstorijaZauzetoKontroler.upisi(prostorijeZauzete);
                    }
                }

                return true;
            }
            return false;
        }

        private bool ZakaziPremestanje(ProstorijaPrebacivanjeDTO prebacivanje)
        {
            var prostorijeZauzete = ProstorijaZauzetoKontroler.ucitajSve();
            int kolicina = prebacivanje.kolicinaStavke;
            DateTime datumPocetka = prebacivanje.datumPocetka;
            DateTime datumKraja = prebacivanje.datumKraja;
            var prostorijaIzKojeSePrebacuje = nadjiProstorijuPoId(prebacivanje.idProstorijeIzKojeSePrebacuje);
            var prostorijaUKojuSePrebacuje = nadjiProstorijuPoId(prebacivanje.idProstorijeUKojuSePrebacuje);
            
            if (datumKraja >= datumPocetka)
            {
                if (!postojeTerminiZaPeriodPremestanja(datumPocetka, datumKraja, prostorijaIzKojeSePrebacuje))
                {
                    if (!postojeTerminiZaPeriodPremestanja(datumPocetka, datumKraja, prostorijaUKojuSePrebacuje))
                    {
                        ProstorijaZauzeto prostorijaZaZauzimanje = new ProstorijaZauzeto(prostorijaIzKojeSePrebacuje.id, prostorijaUKojuSePrebacuje.id, datumPocetka, datumKraja,
                        false, prebacivanje.idStavke, kolicina);
                        prostorijeZauzete.Add(prostorijaZaZauzimanje);
                        ProstorijaZauzetoKontroler.upisi(prostorijeZauzete);
                    }
                }

                return true;
            }
            return false;
        }

        public Prostorija nadjiProstorijuPoId(String id)
        {
            var prostorije = prostorijaRepozitorijum.ucitajSve();
            var prostorija = new Prostorija();

            foreach(Prostorija p in prostorije)
            {
                if(p.id.Equals(id))
                {
                    prostorija.id = p.id;
                    prostorija.idBolnice = p.idBolnice;
                    prostorija.logickiObrisana = p.logickiObrisana;
                    prostorija.sprat = p.sprat;
                    prostorija.tipProstorije = p.tipProstorije;
                    prostorija.broj = p.broj;
                    prostorija.dostupnost = p.dostupnost;
                    prostorija.Stavka = p.Stavka;

                }
            }
            return prostorija;
        }

        public List<Stavka> dobaviStavkeIzProstorije(Prostorija prostorija)
        {
            var prostorije = prostorijaRepozitorijum.ucitajNeobrisane();
            var stavke = new List<Stavka>();

            foreach(Prostorija p in prostorije)
            {
                if (p.id == prostorija.id)
                {
                    stavke = p.Stavka;
                }
            }

            return stavke;
        }

        public void zakaziRenoviranje(ProstorijaRenoviranje prostorija)
        {
            var prostorijeZaRenoviranje = prostorijaRepozitorijum.ucitajProstorijeZaRenoviranje();
            prostorijeZaRenoviranje.Add(prostorija);
            prostorijaRepozitorijum.upisiProstorijeZaRenoviranje(prostorijeZaRenoviranje);
        }

        /*public void zakaziRenoviranjeProsirivanje(ProstorijaRenoviranje prostorija)
        {
            var prostorijeZaRenoviranje = prostorijaRepozitorijum.ucitajProstorijeZaRenoviranje();
            prostorijeZaRenoviranje.Add(prostorija);
            prostorijaRepozitorijum.upisiProstorijeZaRenoviranje(prostorijeZaRenoviranje);
        }*/

        public void pregledajProstorijeZaRenoviranje()
        {
            var prostorijeZaRenoviranje = prostorijaRepozitorijum.ucitajProstorijeZaRenoviranje();

            foreach (ProstorijaRenoviranje p in prostorijeZaRenoviranje)
            {
                if (DateTime.Now >= p.datumPocetka && DateTime.Now < p.datumKraja)
                {
                    zauzmiProstorijuZaRenoviranje(nadjiProstorijuPoId(p.idProstorije));
                }

                if (p.datumKraja <= DateTime.Now)
                {
                    if (p.tipRenoviranja == 1)
                    {
                        Prostorija prostorija = nadjiProstorijuPoId(p.idProstorije);
                        ProstorijaDTO prostorijaNova = new ProstorijaDTO(postaviIDProstorija(), prostorija.idBolnice, prostorija.tipProstorije,
                            prostorija.broj + "-a", prostorija.sprat, true, false, 0, null);
                        noviBrojSobe(prostorijaNova, prostorija);
                    }
                    else if (p.tipRenoviranja == 2)
                    {
                        spojProstorije(p);
                    }
                    oslobodiProstorijuPosleRenoviranja(nadjiProstorijuPoId(p.idProstorije), p);
                }  
            }
        }

        public void spojProstorije(ProstorijaRenoviranje p)
        {
            Prostorija prostorija = nadjiProstorijuPoId(p.idProstorije);
            Prostorija prostorijaDruga = nadjiProstorijuPoId(p.idProstorijeKojaSeSpaja);
            ObrisiProstoriju(prostorijaDruga.id);
            prostorija.Stavka = new List<Stavka>();
            kopirajProstorijuIUpisi(prostorija);
        }

        public void noviBrojSobe(ProstorijaDTO prostorijaNova, Prostorija prostorija)
        {
            bool provera = ProveriProstoriju(prostorijaNova);
            if (!provera)
            {
                Random random = new Random();
                prostorijaNova.broj = prostorija.broj + "-" + (new Random()).Next(0, 100).ToString();
                noviBrojSobe(prostorijaNova, prostorija);
            }
        }

        public String postaviIDProstorija()
        {
            var prostorije = prostorijaRepozitorijum.ucitajSve();
            return (prostorije.Count + 1).ToString();
        }

        public void zauzmiProstorijuZaRenoviranje(Prostorija prostorija)
        {
            prostorija.dostupnost = false;
            kopirajProstorijuIUpisi(prostorija);
        }

        public void oslobodiProstorijuPosleRenoviranja(Prostorija prostorija, ProstorijaRenoviranje prostorijaRenoviranje)
        {
            var prostorijeZaRenoviranjeIzmena = prostorijaRepozitorijum.ucitajProstorijeZaRenoviranje();
            prostorijeZaRenoviranjeIzmena = obrisiRenoviranuIzListe(prostorijaRenoviranje, prostorijeZaRenoviranjeIzmena);
            prostorija.dostupnost = true;
            kopirajProstorijuIUpisi(prostorija);
            prostorijaRepozitorijum.upisiProstorijeZaRenoviranje(prostorijeZaRenoviranjeIzmena);
        }

        public void kopirajProstorijuIUpisi(Prostorija p)
        {
            var prostorije = ucitajSve();
            foreach (Prostorija prostorija in prostorije)
            {
                if (prostorija.id.Equals(p.id))
                {
                    prostorija.id = p.id;
                    prostorija.idBolnice = p.idBolnice;
                    prostorija.logickiObrisana = p.logickiObrisana;
                    prostorija.sprat = p.sprat;
                    prostorija.tipProstorije = p.tipProstorije;
                    prostorija.broj = p.broj;
                    prostorija.dostupnost = p.dostupnost;
                    prostorija.Stavka = p.Stavka;
                    break;
                }
            }
            upisi(prostorije);
        }

        public List<ProstorijaRenoviranje> obrisiRenoviranuIzListe(ProstorijaRenoviranje prostorijaZaBrisanje, List<ProstorijaRenoviranje> prostorijeZaRenoviranje)
        {
            foreach (ProstorijaRenoviranje p in prostorijeZaRenoviranje)
            {
                if (p.idProstorije.Equals(prostorijaZaBrisanje.idProstorije) && p.datumPocetka.Equals(prostorijaZaBrisanje.datumPocetka) &&
                    p.datumKraja.Equals(prostorijaZaBrisanje.datumPocetka) && p.razlogRenoviranja.Equals(prostorijaZaBrisanje.razlogRenoviranja))
                {
                    prostorijeZaRenoviranje.Remove(p);
                    break;
                }
            }

            return prostorijeZaRenoviranje;
        }
    }


}
