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

        ProstorijaRepozitorijum prostorijaRepozitorijum = new ProstorijaRepozitorijum();
        //StavkaServis stavkaServis = new StavkaServis();

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

        public void premestiStavku(String prostorijaIzKojeSePrebacujeId, String prostorijaUKojuSePrebacujeId, String stavkaId)
        {
            var prostorijaIz = nadjiProstorijuPoId(prostorijaIzKojeSePrebacujeId);
            var prostorijaU = nadjiProstorijuPoId(prostorijaUKojuSePrebacujeId);
            var stavkaKojaSePrebacuje = StavkaKontroler.pronadjiStavkuPoId(stavkaId);
            var prostorije = prostorijaRepozitorijum.ucitajSve();
            Bolnica_aplikacija.UpravnikProzor upravnikProzor = Bolnica_aplikacija.UpravnikProzor.getInstance();
            var stavkaIz = new Stavka();
            int kolicina = Int32.Parse(upravnikProzor.textBoxKolicinaZaPremestanjeU.Text);
            var prostorijeZauzete = ProstorijaZauzetoKontroler.ucitajSve();
            var termini = TerminKontroler.ucitajSve();

            foreach(Stavka stavka in prostorijaIz.Stavka)
            {
                if(stavka.id == stavkaId)
                {
                    stavkaIz = stavka;
                }
            }

            

            if (kolicina <= stavkaIz.kolicina)
            {
                if (stavkaIz.kolicina - kolicina == 0)
                {
                    prostorijaIz.Stavka.Remove(stavkaIz);

                    foreach (Stavka s in prostorijaU.Stavka)
                    {
                        if (s.id == stavkaId)
                        {
                            s.kolicina += kolicina;

                            foreach (Prostorija p in prostorije)
                            {
                                if (p.id == prostorijaIz.id)
                                {
                                    p.Stavka = prostorijaIz.Stavka;

                                    if (stavkaKojaSePrebacuje.jeStaticka == true)
                                    {
                                      

                                        var datumPocetka = upravnikProzor.datumPocetka.SelectedDate.Value;
                                        var datumKraja = upravnikProzor.datumKraja.SelectedDate.Value;

                                        if (datumKraja >= datumPocetka)
                                        {

                                            foreach (Termin t in termini)
                                            {
                                                if (t.idProstorije == p.id)
                                                {
                                                    if(t.datum >= datumPocetka && t.datum <= datumKraja && t.idPacijenta != "")
                                                    {
                                                        Console.WriteLine("TERMIN POSTOJI U OVOM VREMENSKOM PERIODU");
                                                        return;
                                                    }
                                                }
                                            }

                                            ProstorijaZauzeto prostorijaZaZauzimanje = new ProstorijaZauzeto();
                                            prostorijaZaZauzimanje.idProstorije = p.id;
                                            prostorijaZaZauzimanje.datumPocetka = datumPocetka;
                                            prostorijaZaZauzimanje.datumKraja = datumKraja;
                                            prostorijeZauzete.Add(prostorijaZaZauzimanje);
                                        }
                                    }
                                }
                            }

                            foreach (Prostorija p in prostorije)
                            {
                                if (p.id == prostorijaU.id)
                                {
                                    p.Stavka = prostorijaU.Stavka;

                                    if (stavkaKojaSePrebacuje.jeStaticka == true)
                                    {
                                        var datumPocetka = upravnikProzor.datumPocetka.SelectedDate.Value;
                                        var datumKraja = upravnikProzor.datumKraja.SelectedDate.Value;

                                        if (datumKraja >= datumPocetka)
                                        {

                                            foreach (Termin t in termini)
                                            {
                                                if (t.idProstorije == p.id)
                                                {
                                                    if (t.datum >= datumPocetka && t.datum <= datumKraja && t.idPacijenta != "")
                                                    {
                                                        Console.WriteLine("TERMIN POSTOJI U OVOM VREMENSKOM PERIODU");
                                                        return;
                                                    }
                                                }
                                            }

                                            ProstorijaZauzeto prostorijaZaZauzimanje = new ProstorijaZauzeto();
                                            prostorijaZaZauzimanje.idProstorije = p.id;
                                            prostorijaZaZauzimanje.datumPocetka = datumPocetka;
                                            prostorijaZaZauzimanje.datumKraja = datumKraja;
                                            prostorijeZauzete.Add(prostorijaZaZauzimanje);
                                        }
                                    }
                                }
                            }

                            if (stavkaKojaSePrebacuje.jeStaticka == true)
                            {
                                ProstorijaZauzetoKontroler.upisi(prostorijeZauzete);
                            }

                            prostorijaRepozitorijum.upisi(prostorije);
                            upravnikProzor.dataGridInventarProstorije.ItemsSource = prostorijaIz.Stavka;
                            return;
                        }
                    }

                    var stavka = new Stavka();
                    stavka = stavkaIz;
                    stavka.kolicina = kolicina;
                    prostorijaU.Stavka.Add(stavka);
                    foreach (Prostorija p in prostorije)
                    {
                        if (p.id == prostorijaIz.id)
                        {
                            p.Stavka = prostorijaIz.Stavka;
                            if (stavkaKojaSePrebacuje.jeStaticka == true)
                            {
                                var datumPocetka = upravnikProzor.datumPocetka.SelectedDate.Value;
                                var datumKraja = upravnikProzor.datumKraja.SelectedDate.Value;

                                if (datumKraja >= datumPocetka)
                                {

                                    foreach (Termin t in termini)
                                    {
                                        if (t.idProstorije == p.id)
                                        {
                                            if (t.datum >= datumPocetka && t.datum <= datumKraja && t.idPacijenta != "")
                                            {
                                                Console.WriteLine("TERMIN POSTOJI U OVOM VREMENSKOM PERIODU");
                                                return;
                                            }
                                        }
                                    }

                                    ProstorijaZauzeto prostorijaZaZauzimanje = new ProstorijaZauzeto();
                                    prostorijaZaZauzimanje.idProstorije = p.id;
                                    prostorijaZaZauzimanje.datumPocetka = datumPocetka;
                                    prostorijaZaZauzimanje.datumKraja = datumKraja;
                                    prostorijeZauzete.Add(prostorijaZaZauzimanje);
                                }
                            }
                        }
                    }

                    foreach (Prostorija p in prostorije)
                    {
                        if (p.id == prostorijaU.id)
                        {
                            p.Stavka = prostorijaU.Stavka;
                            if (stavkaKojaSePrebacuje.jeStaticka == true)
                            {
                                var datumPocetka = upravnikProzor.datumPocetka.SelectedDate.Value;
                                var datumKraja = upravnikProzor.datumKraja.SelectedDate.Value;

                                if (datumKraja >= datumPocetka)
                                {

                                    foreach (Termin t in termini)
                                    {
                                        if (t.idProstorije == p.id)
                                        {
                                            if (t.datum >= datumPocetka && t.datum <= datumKraja && t.idPacijenta != "")
                                            {
                                                Console.WriteLine("TERMIN POSTOJI U OVOM VREMENSKOM PERIODU");
                                                return;
                                            }
                                        }
                                    }

                                    ProstorijaZauzeto prostorijaZaZauzimanje = new ProstorijaZauzeto();
                                    prostorijaZaZauzimanje.idProstorije = p.id;
                                    prostorijaZaZauzimanje.datumPocetka = datumPocetka;
                                    prostorijaZaZauzimanje.datumKraja = datumKraja;
                                    prostorijeZauzete.Add(prostorijaZaZauzimanje);
                                }
                            }
                        }
                    }

                    if (stavkaKojaSePrebacuje.jeStaticka == true)
                    {
                        ProstorijaZauzetoKontroler.upisi(prostorijeZauzete);
                    }

                    prostorijaRepozitorijum.upisi(prostorije);
                    upravnikProzor.dataGridInventarProstorije.ItemsSource = prostorijaIz.Stavka;
                    return;
                }
                else
                {
                    foreach(Stavka s in prostorijaIz.Stavka)
                    {
                        if(s.id == stavkaIz.id)
                        {
                            s.kolicina -= kolicina;
                        }
                    }

                    foreach (Stavka s in prostorijaU.Stavka)
                    {
                        if (s.id == stavkaId)
                        {
                            s.kolicina += kolicina;

                            foreach (Prostorija p in prostorije)
                            {
                                if (p.id == prostorijaIz.id)
                                {
                                    p.Stavka = prostorijaIz.Stavka;

                                    if (stavkaKojaSePrebacuje.jeStaticka == true)
                                    {
                                        var datumPocetka = upravnikProzor.datumPocetka.SelectedDate.Value;
                                        var datumKraja = upravnikProzor.datumKraja.SelectedDate.Value;

                                        if (datumKraja >= datumPocetka)
                                        {
                                            foreach (Termin t in termini)
                                            {
                                                if (t.idProstorije == p.id)
                                                {
                                                    if (t.datum >= datumPocetka && t.datum <= datumKraja && t.idPacijenta != "")
                                                    {
                                                        Console.WriteLine("TERMIN POSTOJI U OVOM VREMENSKOM PERIODU");
                                                        return;
                                                    }
                                                }
                                            }

                                            ProstorijaZauzeto prostorijaZaZauzimanje = new ProstorijaZauzeto();
                                            prostorijaZaZauzimanje.idProstorije = p.id;
                                            prostorijaZaZauzimanje.datumPocetka = datumPocetka;
                                            prostorijaZaZauzimanje.datumKraja = datumKraja;
                                            prostorijeZauzete.Add(prostorijaZaZauzimanje);
                                        }
                                    }
                                }
                            }

                            foreach (Prostorija p in prostorije)
                            {
                                if (p.id == prostorijaU.id)
                                {
                                    p.Stavka = prostorijaU.Stavka;

                                    if (stavkaKojaSePrebacuje.jeStaticka == true)
                                    {
                                        var datumPocetka = upravnikProzor.datumPocetka.SelectedDate.Value;
                                        var datumKraja = upravnikProzor.datumKraja.SelectedDate.Value;

                                        if (datumKraja >= datumPocetka)
                                        {
                                            foreach (Termin t in termini)
                                            {
                                                if (t.idProstorije == p.id)
                                                {
                                                    if (t.datum >= datumPocetka && t.datum <= datumKraja && t.idPacijenta != "")
                                                    {
                                                        Console.WriteLine("TERMIN POSTOJI U OVOM VREMENSKOM PERIODU");
                                                        return;
                                                    }
                                                }
                                            }

                                            ProstorijaZauzeto prostorijaZaZauzimanje = new ProstorijaZauzeto();
                                            prostorijaZaZauzimanje.idProstorije = p.id;
                                            prostorijaZaZauzimanje.datumPocetka = datumPocetka;
                                            prostorijaZaZauzimanje.datumKraja = datumKraja;
                                            prostorijeZauzete.Add(prostorijaZaZauzimanje);
                                        }
                                    }
                                }
                            }

                            if (stavkaKojaSePrebacuje.jeStaticka == true)
                            {
                                ProstorijaZauzetoKontroler.upisi(prostorijeZauzete);
                            }

                            prostorijaRepozitorijum.upisi(prostorije);
                            upravnikProzor.dataGridInventarProstorije.ItemsSource = prostorijaIz.Stavka;
                            return;
                        }
                    }

                    var stavka = new Stavka();
                    stavka = stavkaKojaSePrebacuje;
                    stavka.kolicina = kolicina;
                    prostorijaU.Stavka.Add(stavka);

                    foreach (Prostorija p in prostorije)
                    {
                        if (p.id == prostorijaIz.id)
                        {
                            p.Stavka = prostorijaIz.Stavka;
                            if (stavkaKojaSePrebacuje.jeStaticka == true)
                            {
                                var datumPocetka = upravnikProzor.datumPocetka.SelectedDate.Value;
                                var datumKraja = upravnikProzor.datumKraja.SelectedDate.Value;

                                if (datumKraja >= datumPocetka)
                                {
                                    foreach (Termin t in termini)
                                    {
                                        if (t.idProstorije == p.id)
                                        {
                                            if (t.datum >= datumPocetka && t.datum <= datumKraja && t.idPacijenta != "")
                                            {
                                                Console.WriteLine("TERMIN POSTOJI U OVOM VREMENSKOM PERIODU");
                                                return;
                                            }
                                        }
                                    }

                                    ProstorijaZauzeto prostorijaZaZauzimanje = new ProstorijaZauzeto();
                                    prostorijaZaZauzimanje.idProstorije = p.id;
                                    prostorijaZaZauzimanje.datumPocetka = datumPocetka;
                                    prostorijaZaZauzimanje.datumKraja = datumKraja;
                                    prostorijeZauzete.Add(prostorijaZaZauzimanje);
                                }
                            }
                        }
                    }

                    foreach (Prostorija p in prostorije)
                    {
                        if (p.id == prostorijaU.id)
                        {
                            p.Stavka = prostorijaU.Stavka;
                            if (stavkaKojaSePrebacuje.jeStaticka == true)
                            {
                                var datumPocetka = upravnikProzor.datumPocetka.SelectedDate.Value;
                                var datumKraja = upravnikProzor.datumKraja.SelectedDate.Value;

                                if (datumKraja >= datumPocetka)
                                {
                                    foreach (Termin t in termini)
                                    {
                                        if (t.idProstorije == p.id)
                                        {
                                            if (t.datum >= datumPocetka && t.datum <= datumKraja && t.idPacijenta != "")
                                            {
                                                Console.WriteLine("TERMIN POSTOJI U OVOM VREMENSKOM PERIODU");
                                                return;
                                            }
                                        }
                                    }

                                    ProstorijaZauzeto prostorijaZaZauzimanje = new ProstorijaZauzeto();
                                    prostorijaZaZauzimanje.idProstorije = p.id;
                                    prostorijaZaZauzimanje.datumPocetka = datumPocetka;
                                    prostorijaZaZauzimanje.datumKraja = datumKraja;
                                    prostorijeZauzete.Add(prostorijaZaZauzimanje);
                                }
                            }
                        }
                    }

                    if (stavkaKojaSePrebacuje.jeStaticka == true)
                    {
                        ProstorijaZauzetoKontroler.upisi(prostorijeZauzete);
                    }

                    prostorijaRepozitorijum.upisi(prostorije);
                    upravnikProzor.dataGridInventarProstorije.ItemsSource = prostorijaIz.Stavka;
                    return;
                   
                }
            }
        }

        public void dodajStavku(String prostorijaId, String stavkaId)
        {
            var prostorija = nadjiProstorijuPoId(prostorijaId);
            Console.WriteLine(prostorija.broj);
            var prostorije = prostorijaRepozitorijum.ucitajSve();
            Bolnica_aplikacija.UpravnikProzor upravnikProzor = Bolnica_aplikacija.UpravnikProzor.getInstance();
            int kolicina = Int32.Parse(upravnikProzor.textBoxKolicinaZaPremestanje.Text);
            int suma = 0;
            var prostorijeZauzete = ProstorijaZauzetoKontroler.ucitajSve();
            var termini = TerminKontroler.ucitajSve();

            var novaStavka = StavkaKontroler.pronadjiStavkuPoId(stavkaId);

            foreach (Prostorija p in prostorije)
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

            if (suma + kolicina <= novaStavka.kolicina)
            {
                if (prostorija.Stavka.Count == 0)
                {
                    novaStavka.kolicina = Int32.Parse(upravnikProzor.textBoxKolicinaZaPremestanje.Text);
                    prostorija.Stavka.Add(novaStavka);
                    foreach (Prostorija p in prostorije)
                    {
                        if (p.id == prostorija.id)
                        {
                            p.Stavka = prostorija.Stavka;

                            if (novaStavka.jeStaticka == true)
                            {
                                var datumPocetka = upravnikProzor.datumPocetka.SelectedDate.Value;
                                var datumKraja = upravnikProzor.datumKraja.SelectedDate.Value;

                                if (datumKraja >= datumPocetka)
                                {

                                    foreach (Termin t in termini)
                                    {
                                        if (t.idProstorije == p.id)
                                        {
                                            if (t.datum >= datumPocetka && t.datum <= datumKraja && t.idPacijenta != "")
                                            {
                                                Console.WriteLine("TERMIN POSTOJI U OVOM VREMENSKOM PERIODU");
                                                return;
                                            }
                                        }
                                    }

                                    ProstorijaZauzeto prostorijaZaZauzimanje = new ProstorijaZauzeto();
                                    prostorijaZaZauzimanje.idProstorije = p.id;
                                    prostorijaZaZauzimanje.datumPocetka = datumPocetka;
                                    prostorijaZaZauzimanje.datumKraja = datumKraja;
                                    prostorijeZauzete.Add(prostorijaZaZauzimanje);
                                    ProstorijaZauzetoKontroler.upisi(prostorijeZauzete);
                                    break;
                                }
                            }
                        }
                    }

                    prostorijaRepozitorijum.upisi(prostorije);
                }
                else
                {
                    foreach (Stavka stavka in prostorija.Stavka)
                    {
                        if (stavka.id == stavkaId)
                        {
                            stavka.kolicina += Int32.Parse(upravnikProzor.textBoxKolicinaZaPremestanje.Text);

                            foreach (Prostorija p in prostorije)
                            {
                                if (p.id == prostorijaId)
                                {
                                    p.Stavka = prostorija.Stavka;

                                    if (novaStavka.jeStaticka == true)
                                    {
                                        var datumPocetka = upravnikProzor.datumPocetka.SelectedDate.Value;
                                        var datumKraja = upravnikProzor.datumKraja.SelectedDate.Value;

                                        if (datumKraja >= datumPocetka)
                                        {
                                            foreach (Termin t in termini)
                                            {
                                                if (t.idProstorije == p.id)
                                                {
                                                    if (t.datum >= datumPocetka && t.datum <= datumKraja && t.idPacijenta != "")
                                                    {
                                                        Console.WriteLine("TERMIN POSTOJI U OVOM VREMENSKOM PERIODU");
                                                        return;
                                                    }
                                                }
                                            }

                                            ProstorijaZauzeto prostorijaZaZauzimanje = new ProstorijaZauzeto();
                                            prostorijaZaZauzimanje.idProstorije = p.id;
                                            prostorijaZaZauzimanje.datumPocetka = datumPocetka;
                                            prostorijaZaZauzimanje.datumKraja = datumKraja;
                                            prostorijeZauzete.Add(prostorijaZaZauzimanje);
                                            ProstorijaZauzetoKontroler.upisi(prostorijeZauzete);
                                            break;
                                        }
                                    }
                                }
                            }

                            prostorijaRepozitorijum.upisi(prostorije);
                            return;

                        }
                    }

                    novaStavka.kolicina = Int32.Parse(upravnikProzor.textBoxKolicinaZaPremestanje.Text);
                    prostorija.Stavka.Add(novaStavka);

                    foreach (Prostorija prost in prostorije)
                    {
                        if (prost.id == prostorijaId)
                        {
                            prost.Stavka = prostorija.Stavka;
                            if (novaStavka.jeStaticka == true)
                            {
                                var datumPocetka = upravnikProzor.datumPocetka.SelectedDate.Value;
                                var datumKraja = upravnikProzor.datumKraja.SelectedDate.Value;

                                if (datumKraja >= datumPocetka)
                                {
                                    foreach (Termin t in termini)
                                    {
                                        if (t.idProstorije == prost.id)
                                        {
                                            if (t.datum >= datumPocetka && t.datum <= datumKraja && t.idPacijenta != "")
                                            {
                                                Console.WriteLine("TERMIN POSTOJI U OVOM VREMENSKOM PERIODU");
                                                return;
                                            }
                                        }
                                    }

                                    ProstorijaZauzeto prostorijaZaZauzimanje = new ProstorijaZauzeto();
                                    prostorijaZaZauzimanje.idProstorije = prost.id;
                                    prostorijaZaZauzimanje.datumPocetka = datumPocetka;
                                    prostorijaZaZauzimanje.datumKraja = datumKraja;
                                    prostorijeZauzete.Add(prostorijaZaZauzimanje);
                                    ProstorijaZauzetoKontroler.upisi(prostorijeZauzete);
                                    break;
                                }
                            }
                        }
                    }

                    if (novaStavka.jeStaticka == true)
                    {
                        ProstorijaZauzetoKontroler.upisi(prostorijeZauzete);
                    }

                    prostorijaRepozitorijum.upisi(prostorije);
                }
            }
            
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
    }


}
