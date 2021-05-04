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

        public void NapraviProstoriju(Prostorija prostorija)
        {
            var prostorije = ProstorijaKontroler.ucitajSve(); //.GetInstance().ucitajSve();
                                                              // Prostorija prostorija = new Prostorija();

            Bolnica_aplikacija.UpravnikProzor upravnikProzor = Bolnica_aplikacija.UpravnikProzor.getInstance();

            bool pronadjenBroj = false;
            bool pronadjenSprat = false;
            bool istiBroj = false;

            String pat = @"^[0-9]+$";
            Regex r = new Regex(pat);
            Match m = r.Match(upravnikProzor.unosBrojaProstorije.Text.Replace(" ", ""));
            Match m1 = r.Match(upravnikProzor.unosSprata.Text.Replace(" ", ""));

            /*if (prostorija.broj == upravnikProzor.txtBrojProstorije.Text && prostorija.sprat.ToString() == upravnikProzor.txtSpratProstorije.Text)
            {
                istiBroj = true;
            }*/

            //provera da li broj prostorije vec postoji
            foreach (Prostorija p in prostorije)
            {
                if (p.broj == upravnikProzor.unosBrojaProstorije.Text)
                {
                    pronadjenBroj = true;
                    break;
                }
            }

            foreach (Prostorija p in prostorije)
            {
                if (p.sprat == Int32.Parse(upravnikProzor.unosSprata.Text))
                {
                    pronadjenSprat = true;
                    break;
                }
            }

            if (pronadjenBroj && pronadjenSprat)// && !istiBroj)
            {
                upravnikProzor.lblBrojPostojiDodaj.Visibility = Visibility.Visible;
            }
            else if (String.IsNullOrEmpty(upravnikProzor.unosBrojaProstorije.Text) || String.IsNullOrEmpty(upravnikProzor.unosSprata.Text) || upravnikProzor.cbTipProstorije.SelectedIndex == -1)
            {
                upravnikProzor.lblNijePopunjenoDodaj.Visibility = Visibility.Visible;
            }
            else if (!m.Success || !m1.Success)
            {
                upravnikProzor.lblNijePopunjenoIspravnoDodaj.Visibility = Visibility.Visible;
            }
            else
            {
                Console.WriteLine(prostorije.Count);
                prostorija.id = (prostorije.Count + 1).ToString();
                prostorija.broj = upravnikProzor.unosBrojaProstorije.Text;
                prostorija.sprat = Int32.Parse(upravnikProzor.unosSprata.Text);
                prostorija.logickiObrisana = false;
                prostorija.dostupnost = true;

                if (upravnikProzor.cbTipProstorije.SelectedIndex == 0)
                {
                    prostorija.tipProstorije = TipProstorije.BOLNICKA_SOBA;
                }
                else if (upravnikProzor.cbTipProstorije.SelectedIndex == 1)
                {
                    prostorija.tipProstorije = TipProstorije.OPERACIONA_SALA;
                }
                else if (upravnikProzor.cbTipProstorije.SelectedIndex == 2)
                {
                    prostorija.tipProstorije = TipProstorije.SOBA_ZA_PREGLED;
                }

                prostorije.Add(prostorija);
                prostorijaRepozitorijum.upisi(prostorije);
                upravnikProzor.gridDodajProstoriju.Visibility = Visibility.Hidden;
                upravnikProzor.gridProstorija.Visibility = Visibility.Visible;

            }
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


        public void premestiStavku(String prostorijaIzKojeSePrebacujeId, String prostorijaUKojuSePrebacujeId, String stavkaId)
        {
            var prostorijaIz = nadjiProstorijuPoId(prostorijaIzKojeSePrebacujeId);
            var prostorijaU = nadjiProstorijuPoId(prostorijaUKojuSePrebacujeId);

            if (Int32.Parse(UpravnikProzor.getInstance().textBoxKolicinaZaPremestanjeU.Text) <= StavkaKontroler.pronadjiStavkuIzProstorijePoId(prostorijaIz, stavkaId).kolicina)
            {
                manipulisanjeStavkamaProstorijeIzKojeSePrebacuje(stavkaId, prostorijaIz, StavkaKontroler.pronadjiStavkuIzProstorijePoId(prostorijaIz, stavkaId));

                if (StavkaKontroler.pronadjiStavkuPoId(stavkaId).jeStaticka)
                {
                    premestiStatickuStavku(prostorijaIz, prostorijaU, StavkaKontroler.pronadjiStavkuPoId(stavkaId));
                }
                else
                {
                    premestiDinamickuStavku(prostorijaIz, prostorijaU, stavkaId);

                }
                return;
            }
        }

        private void premestiDinamickuStavku(Prostorija prostorijaIz, Prostorija prostorijaU, String stavkaId)
        {
            Stavka stavkaKojaSePrebacuje = StavkaKontroler.pronadjiStavkuPoId(stavkaId);
            var stavka = StavkaKontroler.pronadjiStavkuIzProstorijePoId(prostorijaU, stavkaId);

            if (stavka != null)
            {
                dodavanjeKolicineStavke(prostorijaIz, prostorijaU, stavka);
                return;
            }
            else
            {
                stavka = dodavanjeNoveStavke(prostorijaIz, prostorijaU, stavkaKojaSePrebacuje);
            }
        }

        private void manipulisanjeStavkamaProstorijeIzKojeSePrebacuje(string stavkaId, Prostorija prostorijaIz, Stavka stavkaIz)
        {
            if (stavkaIz.kolicina - Int32.Parse(UpravnikProzor.getInstance().textBoxKolicinaZaPremestanjeU.Text) == 0)
            {
                obrisiStavkuIzProstorije(stavkaId, prostorijaIz);
            }
            else
            {
                smanjiKolicinuStavkeIzProstorije(prostorijaIz, stavkaIz);
            }
        }

        private void premestiStatickuStavku(Prostorija prostorijaIz, Prostorija prostorijaU, Stavka stavkaKojaSePrebacuje)
        {
            if (ZakaziPremestanje(stavkaKojaSePrebacuje.id, prostorijaU, prostorijaIz))
            {
                kopirajProstorijuIUpisi(prostorijaIz);
                return;
            }
        }

        private void dodavanjeKolicineStavke(Prostorija prostorijaIz, Prostorija prostorijaU, Stavka stavka)
        {
            stavka.kolicina += Int32.Parse(UpravnikProzor.getInstance().textBoxKolicinaZaPremestanjeU.Text);
            kopirajStavke(prostorijaU);
            kopirajProstorijuIUpisi(prostorijaIz);
            UpravnikProzor.getInstance().dataGridInventarProstorije.ItemsSource = prostorijaIz.Stavka;
            return;
        }

        private Stavka dodavanjeNoveStavke(Prostorija prostorijaIz, Prostorija prostorijaU, Stavka stavkaKojaSePrebacuje)
        {
            Stavka stavka = stavkaKojaSePrebacuje;
            stavka.kolicina = Int32.Parse(UpravnikProzor.getInstance().textBoxKolicinaZaPremestanjeU.Text);
            prostorijaU.Stavka.Add(stavka);
            kopirajStavke(prostorijaU);
            kopirajProstorijuIUpisi(prostorijaIz);
            UpravnikProzor.getInstance().dataGridInventarProstorije.ItemsSource = prostorijaIz.Stavka;
            return stavka;
        }

        private void smanjiKolicinuStavkeIzProstorije(Prostorija prostorijaIz, Stavka stavkaIz)
        {
            foreach (Stavka s in prostorijaIz.Stavka)
            {
                if (s.id == stavkaIz.id)
                {
                    s.kolicina -= Int32.Parse(UpravnikProzor.getInstance().textBoxKolicinaZaPremestanjeU.Text);
                    break;
                }
            }
            kopirajProstorijuIUpisi(prostorijaIz);
        }

        private static void obrisiStavkuIzProstorije(string stavkaId, Prostorija prostorijaIz)
        {
            foreach (Stavka stavka in prostorijaIz.Stavka)
            {
                if (stavka.id == stavkaId)
                {
                    prostorijaIz.Stavka.Remove(stavka);
                    break;
                }
            }
        }

        /*private static Stavka NewMethod(string stavkaId, Prostorija prostorijaIz, Stavka stavkaIz)
        {
            foreach (Stavka stavka in prostorijaIz.Stavka)
            {
                if (stavka.id == stavkaId)
                {
                    stavkaIz = stavka;
                }
            }

            return stavkaIz;
        }*/

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

        public void dodajStavku(String prostorijaId, String stavkaId)
        {
            var prostorijaUKojuSePrebacuje = nadjiProstorijuPoId(prostorijaId);

            var novaStavka = StavkaKontroler.pronadjiStavkuPoId(stavkaId);

            if (saberiKolicinuStavkeUSvimProstorijama(stavkaId) + Int32.Parse(UpravnikProzor.getInstance().textBoxKolicinaZaPremestanje.Text) <= novaStavka.kolicina)
            {
                var stavka = StavkaKontroler.pronadjiStavkuIzProstorijePoId(prostorijaUKojuSePrebacuje, stavkaId);

                if (novaStavka.jeStaticka == true)
                {
                    if (ZakaziPremestanje(stavkaId, prostorijaUKojuSePrebacuje, prostorijaUKojuSePrebacuje))
                    {
                        return;
                    }
                }
                else
                {
                    if (stavka == null)
                    {
                        novaStavka.kolicina = Int32.Parse(UpravnikProzor.getInstance().textBoxKolicinaZaPremestanje.Text);
                        prostorijaUKojuSePrebacuje.Stavka.Add(novaStavka);
                        kopirajStavke(prostorijaUKojuSePrebacuje);
                    }
                    else
                    {
                        stavka.kolicina += Int32.Parse(UpravnikProzor.getInstance().textBoxKolicinaZaPremestanje.Text);
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
                if (p.Stavka.Count != 0)
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

        private bool ZakaziPremestanje(string stavkaId, Prostorija prostorijaUKojuSePrebacuje, Prostorija prostorijaIzKojeSePrebacuje)
        {
            UpravnikProzor upravnikProzor = UpravnikProzor.getInstance();
            var prostorijeZauzete = ProstorijaZauzetoKontroler.ucitajSve();

            int kolicina;
            DateTime datumPocetka;
            DateTime datumKraja;

            if (upravnikProzor.textBoxKolicinaZaPremestanje.Text == "")
            {
                kolicina = Int32.Parse(upravnikProzor.textBoxKolicinaZaPremestanjeU.Text);
                datumPocetka = upravnikProzor.datumPocetkaU.SelectedDate.Value;
                datumKraja = upravnikProzor.datumKrajaU.SelectedDate.Value;
            }
            else
            {
                kolicina = Int32.Parse(upravnikProzor.textBoxKolicinaZaPremestanje.Text);
                datumPocetka = upravnikProzor.datumPocetka.SelectedDate.Value;
                datumKraja = upravnikProzor.datumKraja.SelectedDate.Value;
            }
            
            
            if (datumKraja >= datumPocetka)
            {
                if (!postojeTerminiZaPeriodPremestanja(datumPocetka, datumKraja, prostorijaIzKojeSePrebacuje))
                {
                    if (!postojeTerminiZaPeriodPremestanja(datumPocetka, datumKraja, prostorijaUKojuSePrebacuje))
                    {
                        ProstorijaZauzeto prostorijaZaZauzimanje = new ProstorijaZauzeto(prostorijaIzKojeSePrebacuje.id, prostorijaUKojuSePrebacuje.id, datumPocetka, datumKraja,
                        false, stavkaId, kolicina);
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
