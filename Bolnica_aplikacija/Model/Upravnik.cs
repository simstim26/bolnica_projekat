// File:    Upravnik.cs
// Author:  User
// Created: Monday, March 22, 2021 7:07:23 PM
// Purpose: Definition of Class Upravnik

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows;

namespace Model
{
   public class Upravnik : Korisnik
   {
      public String id { get; set; }
      public String idBolnice { get; set; }

        public void NapraviProstoriju(Prostorija prostorija)
      {
            var prostorije = JsonSerializer.Deserialize<List<Prostorija>>(File.ReadAllText("Datoteke/probaProstorije.txt"));

            Bolnica_aplikacija.UpravnikProzor upravnikProzor = Bolnica_aplikacija.UpravnikProzor.getInstance();

            bool pronadjenBroj = false;

            String pat = @"^[0-9]+$";
            Regex r = new Regex(pat);
            Match m = r.Match(upravnikProzor.unosBrojaProstorije.Text.Replace(" ", ""));
            Match m1 = r.Match(upravnikProzor.unosSprata.Text.Replace(" ", ""));

            //provera da li broj prostorije vec postoji
            foreach (Prostorija p in prostorije)
            {
                if (p.broj == upravnikProzor.unosBrojaProstorije.Text)
                {
                    pronadjenBroj = true;
                    break;
                }
            }

            if (pronadjenBroj)
            {
                upravnikProzor.lblBrojPostojiDodaj.Visibility = Visibility.Visible;
            }
            else if (String.IsNullOrEmpty(upravnikProzor.unosBrojaProstorije.Text) || String.IsNullOrEmpty(upravnikProzor.unosSprata.Text) || upravnikProzor.cbTipProstorije.SelectedIndex == -1)
            {
                upravnikProzor.lblNijePopunjenoDodaj.Visibility = Visibility.Visible;
            }
            else if(!m.Success || !m1.Success)
            {
                upravnikProzor.lblNijePopunjenoIspravnoDodaj.Visibility = Visibility.Visible;
            }
            else
            {
                //Prostorija prostorija = new Prostorija();
                prostorija.id = (prostorije.Count + 1).ToString();
                prostorija.broj = upravnikProzor.unosBrojaProstorije.Text;
                prostorija.sprat = Int32.Parse(upravnikProzor.unosSprata.Text);
                prostorija.logickiObrisana = false;
                prostorija.dostupnost = true;
                //prostorija.idBolnice = upravnikProzor.upravnik.id;
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
                //upravnikProzor.prostorijeNeobrisane.Add(prostorija);
                string jsonString = JsonSerializer.Serialize(prostorije);
                File.WriteAllText("Datoteke/probaProstorije.txt", jsonString);
                upravnikProzor.gridDodajProstoriju.Visibility = Visibility.Hidden;
                upravnikProzor.gridProstorija.Visibility = Visibility.Visible;
                
                //dataGridProstorija.Items.Refresh();
            }
        }
    
      
      public List<Prostorija> ProcitajProstoriju()
      {
            //throw new NotImplementedException();
            var prostorije = JsonSerializer.Deserialize<List<Prostorija>>(File.ReadAllText("Datoteke/probaProstorije.txt"));
            List<Prostorija> prostorijeNeobrisane = new List<Prostorija>();
            foreach (Prostorija p in prostorije)
            {
                if (p.logickiObrisana == false)
                {
                    prostorijeNeobrisane.Add(p);
                }
            }

            return prostorijeNeobrisane;
        }

        public void AzurirajProstoriju(Prostorija prostorija)
        {
            //throw new NotImplementedException();

            //Bolnica_aplikacija.UpravnikProzor upravnikProzor = new Bolnica_aplikacija.UpravnikProzor();

            Bolnica_aplikacija.UpravnikProzor upravnikProzor = Bolnica_aplikacija.UpravnikProzor.getInstance();

            var prostorije = JsonSerializer.Deserialize<List<Prostorija>>(File.ReadAllText("Datoteke/probaProstorije.txt"));
            bool pronadjenBroj = false;

            String pat = @"^[0-9]+$";
            Regex r = new Regex(pat);
            Match m = r.Match(upravnikProzor.txtBrojProstorije.Text.Replace(" ", ""));
            Match m1 = r.Match(upravnikProzor.txtSpratProstorije.Text.Replace(" ", ""));
            Prostorija prostorijaZaIzmenu = new Prostorija();
            foreach(Prostorija p in prostorije)
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

            //provera da li broj prostorije vec postoji
            foreach (Prostorija p in prostorije)
            {
                if (p.broj == upravnikProzor.txtBrojProstorije.Text && p.broj != prostorija.broj)
                {
                    pronadjenBroj = true;
                    break;
                }
            }

            if (pronadjenBroj)
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

                string jsonString = JsonSerializer.Serialize(prostorije);
                File.WriteAllText("Datoteke/probaProstorije.txt", jsonString);

                upravnikProzor.gridIzmeniProstoriju.Visibility = Visibility.Hidden;
                upravnikProzor.gridProstorija.Visibility = Visibility.Visible;
                upravnikProzor.dataGridProstorija.Items.Refresh();
            }
        }
      
      public void ObrisiProstoriju(String idProstorija)
      {
            //throw new NotImplementedException();
            Prostorija prostorija = new Prostorija();
            var prostorije = JsonSerializer.Deserialize<List<Prostorija>>(File.ReadAllText("Datoteke/probaProstorije.txt"));
            foreach(Prostorija p in prostorije)
            {
                if(p.id == idProstorija)
                {
                    prostorija = p;
                }
            }

            prostorija.logickiObrisana = true;
            string jsonString = JsonSerializer.Serialize(prostorije);
            File.WriteAllText("Datoteke/probaProstorije.txt", jsonString);
            //prostorijeNeobrisane.Remove(prostorija);
            //Console.WriteLine(prostorijeNeobrisane.Count());

        }
      
      public void NapraviLek(Lek lek)
      {
         throw new NotImplementedException();
      }
      
      public void ProcitajLek(Lek lek)
      {
         throw new NotImplementedException();
      }
      
      public void AzurirajLek(Lek lek)
      {
         throw new NotImplementedException();
      }
      
      public void ObrisiLek(String idLeka)
      {
         throw new NotImplementedException();
      }
      
      public void NapraviStavku(Stavka stavka)
      {
         throw new NotImplementedException();
      }
      
      public void ProcitajStavku(Stavka stavka)
      {
         throw new NotImplementedException();
      }
      
      public void AzurirajStavku(Stavka stavka)
      {
         throw new NotImplementedException();
      }
      
      public void ObrisiStavku(String idStavke)
      {
         throw new NotImplementedException();
      }
      
      public void NapraviOsoblje()
      {
         throw new NotImplementedException();
      }
      
      public void ProcitajOsoblje()
      {
         throw new NotImplementedException();
      }
      
      public void AzurirajOsoblje()
      {
         throw new NotImplementedException();
      }
      
      public void ObrisiOsoblje()
      {
         throw new NotImplementedException();
      }
      
      public void EvidencijaPraktikanata()
      {
         throw new NotImplementedException();
      }
      
      public void FormiranjeSpiskaLekova()
      {
         throw new NotImplementedException();
      }
      
      public void NapraviZahtev()
      {
         throw new NotImplementedException();
      }
      
      public void ProcitajZahtev()
      {
         throw new NotImplementedException();
      }
      
      public void AzurirajZahtev()
      {
         throw new NotImplementedException();
      }
      
      public void ObrisiZahtev()
      {
         throw new NotImplementedException();
      }
   
   }
}