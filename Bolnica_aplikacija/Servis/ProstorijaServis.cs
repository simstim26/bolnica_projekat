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
        //StavkaServis stavkaServis = new StavkaServis();

        public String nadjiBrojISprat(String idProstorije)
        {
            String povratnaVrednost = "";
            foreach (Prostorija prostorija in prostorijaRepozitorijum.ucitajSve())
            {
                if (idProstorije.Equals(prostorija.id))
                {
                    povratnaVrednost = prostorija.broj + " " + prostorija.sprat;
                    break;
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

        public void AzurirajProstoriju(Prostorija prostorija)
        {
            Bolnica_aplikacija.UpravnikProzor upravnikProzor = Bolnica_aplikacija.UpravnikProzor.getInstance();

            var prostorije = prostorijaRepozitorijum.ucitajSve();
            bool pronadjenBroj = false;
            bool istiBroj = false;

            String pat = @"^[0-9]+$";
            Regex r = new Regex(pat);
            Match m = r.Match(upravnikProzor.txtBrojProstorije.Text.Replace(" ", ""));
            Match m1 = r.Match(upravnikProzor.txtSpratProstorije.Text.Replace(" ", ""));
            Prostorija prostorijaZaIzmenu = new Prostorija();
            foreach (Prostorija p in prostorije)
            {
                if (p.id.Equals(prostorija.id))
                {
                    prostorijaZaIzmenu.id = p.id;
                    prostorijaZaIzmenu.idBolnice = p.idBolnice;
                    prostorijaZaIzmenu.logickiObrisana = p.logickiObrisana;
                    prostorijaZaIzmenu.sprat = p.sprat;
                    prostorijaZaIzmenu.tipProstorije = p.tipProstorije;
                    prostorijaZaIzmenu.broj = p.broj;
                    prostorijaZaIzmenu.dostupnost = p.dostupnost;
                    prostorijaZaIzmenu.Stavka = p.Stavka;

                    break;
                }
            }

            if (prostorijaZaIzmenu.broj == upravnikProzor.txtBrojProstorije.Text && prostorijaZaIzmenu.sprat.ToString() == upravnikProzor.txtSpratProstorije.Text)
            {
                istiBroj = true;
            }

            //provera da li broj prostorije vec postoji
            foreach (Prostorija p in prostorije)
            {
                if (p.broj == upravnikProzor.txtBrojProstorije.Text)// && p.broj != prostorija.broj)
                {
                    if (p.sprat.ToString() == upravnikProzor.txtSpratProstorije.Text)
                    {
                        pronadjenBroj = true;
                        break;
                    }

                }

            }


            if (pronadjenBroj && !istiBroj)
            {
                upravnikProzor.lblBrojPostoji.Visibility = Visibility.Visible;
            }
            else if (String.IsNullOrEmpty(upravnikProzor.txtBrojProstorije.Text) || String.IsNullOrEmpty(upravnikProzor.txtSpratProstorije.Text) || upravnikProzor.cbTipProstorijeIzmena.SelectedIndex == -1 || upravnikProzor.cbDostupnostProstorije.SelectedIndex == -1)
            {
                upravnikProzor.lblNijePopunjenoIzmeni.Visibility = Visibility.Visible;
            }
            else if (!m.Success || !m1.Success)
            {
                upravnikProzor.lblNijePopunjenoIspravnoIzmeni.Visibility = Visibility.Visible;
            }
            else
            {
                prostorijaZaIzmenu.broj = upravnikProzor.txtBrojProstorije.Text.Replace(" ", "");
                prostorijaZaIzmenu.sprat = Int32.Parse(upravnikProzor.txtSpratProstorije.Text.Replace(" ", ""));

                if (upravnikProzor.cbTipProstorijeIzmena.SelectedIndex == 0)
                {
                    prostorijaZaIzmenu.tipProstorije = TipProstorije.BOLNICKA_SOBA;
                }
                else if (upravnikProzor.cbTipProstorijeIzmena.SelectedIndex == 1)
                {
                    prostorijaZaIzmenu.tipProstorije = TipProstorije.OPERACIONA_SALA;
                }
                else if (upravnikProzor.cbTipProstorijeIzmena.SelectedIndex == 2)
                {
                    prostorijaZaIzmenu.tipProstorije = TipProstorije.SOBA_ZA_PREGLED;
                }

                if (upravnikProzor.cbDostupnostProstorije.SelectedIndex == 0)
                {
                    prostorijaZaIzmenu.dostupnost = true;
                }
                else if (upravnikProzor.cbDostupnostProstorije.SelectedIndex == 1)
                {
                    prostorijaZaIzmenu.dostupnost = false;
                }
                Console.WriteLine(m);

                foreach (Prostorija p in prostorije)
                {
                    if (p.id == prostorijaZaIzmenu.id)
                    {
                        p.id = prostorijaZaIzmenu.id;
                        p.idBolnice = prostorijaZaIzmenu.idBolnice;
                        p.logickiObrisana = prostorijaZaIzmenu.logickiObrisana;
                        p.sprat = prostorijaZaIzmenu.sprat;
                        p.tipProstorije = prostorijaZaIzmenu.tipProstorije;
                        p.broj = prostorijaZaIzmenu.broj;
                        p.dostupnost = prostorijaZaIzmenu.dostupnost;
                        p.Stavka = prostorijaZaIzmenu.Stavka;
                    }
                }

                //string jsonString = JsonSerializer.Serialize(prostorije);
                prostorijaRepozitorijum.upisi(prostorije);

                upravnikProzor.gridIzmeniProstoriju.Visibility = Visibility.Hidden;
                upravnikProzor.gridProstorija.Visibility = Visibility.Visible;
                upravnikProzor.dataGridProstorija.ItemsSource = prostorijaRepozitorijum.ucitajNeobrisane();
            }

        }

<<<<<<< Updated upstream
        public void NapraviProstoriju(Prostorija prostorija)
=======
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
>>>>>>> Stashed changes
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
            if (ZakaziPremestanje(prebacivanje))
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

            Console.WriteLine("ID OD FUNKCIJE KOJI VRACA: " + prostorija.id);

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

        public void pregledajProstorijeZaRenoviranje()
        {
            var prostorijeZaRenoviranje = prostorijaRepozitorijum.ucitajProstorijeZaRenoviranje();
            var prostorijeZaRenoviranjeIzmena = prostorijaRepozitorijum.ucitajProstorijeZaRenoviranje();
            foreach (ProstorijaRenoviranje p in prostorijeZaRenoviranje)
            {
                if (DateTime.Now >= p.datumPocetka && DateTime.Now < p.datumKraja)
                {
                    var prostorija = nadjiProstorijuPoId(p.idProstorije);
                    prostorija.dostupnost = false;
                    kopirajProstorijuIUpisi(prostorija);
                }

                if (p.datumKraja <= DateTime.Now)
                {
                    var prostorija = nadjiProstorijuPoId(p.idProstorije);
                    prostorijeZaRenoviranjeIzmena = obrisiRenoviranuIzListe(p, prostorijeZaRenoviranjeIzmena);
                    prostorija.dostupnost = true;
                    kopirajProstorijuIUpisi(prostorija);
                    prostorijaRepozitorijum.upisiProstorijeZaRenoviranje(prostorijeZaRenoviranjeIzmena);
                }  
                
            }
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
