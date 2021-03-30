// File:    Sekretar.cs
// Author:  User
// Created: Monday, March 22, 2021 7:07:22 PM
// Purpose: Definition of Class Sekretar

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows.Controls;

namespace Model
{
   public class Sekretar : Korisnik
   {
      public String id { get; set; }
      public String idBolnice { get; set; }

        // IZMENA: Pacijent pacijent izmenjem u stringove podataka koji se upisuju, static
        public static void NapraviPacijenta(String id, String idBolnice, bool gost, String korisnickoIme, String loznika, String jmbg, String ime, String prezime, DateTime datumRodj, string adresa, string email, string telefon, DataGrid tabelaPacijenti, List<Pacijent> pacijenti)
      {
        
            Pacijent pacijent = new Pacijent();
            List<Pacijent> sviPacijenti = pacijenti;

            pacijent.id = id;
            pacijent.idBolnice = idBolnice;
            pacijent.jeGost = gost;
            pacijent.korisnickoIme = korisnickoIme;
            pacijent.lozinka = loznika;
            pacijent.jmbg = jmbg;
            pacijent.ime = ime;
            pacijent.prezime = prezime;
            pacijent.datumRodjenja = datumRodj;
            pacijent.adresa = adresa;
            pacijent.email = email;
            pacijent.brojTelefona = telefon;

            sviPacijenti.Add(pacijent);
      
          
            string jsonString = JsonSerializer.Serialize(sviPacijenti);
            File.WriteAllText("Datoteke/probaPacijenti.txt", jsonString);

            ProcitajPacijenta(tabelaPacijenti);
            //throw new NotImplementedException();
        }
      
      
        //Izbrisan Pacijent pacijent
        public static void ProcitajPacijenta(DataGrid tabelaPacijenti)
      {
            List<Pacijent> ucitaniPacijenti = JsonSerializer.Deserialize<List<Pacijent>>(File.ReadAllText("Datoteke/probaPacijenti.txt"));
            List<Pacijent> neobrisaniPacijenti = new List<Pacijent>();
           
            foreach (Pacijent p in ucitaniPacijenti)
            {
                if (!p.jeLogickiObrisan)
                {
                    neobrisaniPacijenti.Add(p);
                }
            }

            tabelaPacijenti.ItemsSource = neobrisaniPacijenti;
            tabelaPacijenti.Items.Refresh();
            //throw new NotImplementedException();
        }
      
      public static void AzurirajPacijenta(String id, String idBolnice, bool gost, String korisnickoIme, String loznika, String jmbg, String ime, String prezime, DateTime datumRodj, string adresa, string email, string telefon, DataGrid tabelaPacijenti, List<Pacijent> pacijenti)
      {
            foreach(Pacijent izmeniP in pacijenti)
            {
                if (izmeniP.id.Equals(id))
                {
                    Console.WriteLine("Nadjen!");
                    izmeniP.id = id;
                    izmeniP.idBolnice = idBolnice;
                    izmeniP.jeGost = gost;
                    izmeniP.korisnickoIme = korisnickoIme;
                    izmeniP.lozinka = loznika;
                    izmeniP.jmbg = jmbg;
                    izmeniP.ime = ime;
                    izmeniP.prezime = prezime;
                    izmeniP.datumRodjenja = datumRodj;
                    izmeniP.adresa = adresa;
                    izmeniP.email = email;
                    izmeniP.brojTelefona = telefon;
                }
            }

            string jsonString = JsonSerializer.Serialize(pacijenti);
            File.WriteAllText("Datoteke/probaPacijenti.txt", jsonString);

            ProcitajPacijenta(tabelaPacijenti);

            // throw new NotImplementedException();
      }
      
      public static void ObrisiPacijenta(String idPacijenta, List<Pacijent> pacijenti)
      {

            //Pronadji pacijenta za brisanje i postavi mu jeLogickiObrisan na true
            foreach (Pacijent p in pacijenti)
            {

                if (p.id.Equals(idPacijenta))
                {
                    p.jeLogickiObrisan = true;

                }
            }

            string jsonString = JsonSerializer.Serialize(pacijenti);
            File.WriteAllText("Datoteke/probaPacijenti.txt", jsonString);


        }
      
      public void NapraviTermin(Termin termin)
      {
         throw new NotImplementedException();
      }
      
      public void ProcitajTermin(Termin termin)
      {
         throw new NotImplementedException();
      }
      
      public void AzurirajTermin(Termin termin)
      {
         throw new NotImplementedException();
      }
      
      public void ObrisiTermin(String idTermina)
      {
         throw new NotImplementedException();
      }
      
      public void PrikazSala()
      {
         throw new NotImplementedException();
      }
      
      public void PretragaSala()
      {
         throw new NotImplementedException();
      }
      
      public void PrikazInventara()
      {
         throw new NotImplementedException();
      }
      
      public void PlacanjePregleda()
      {
         throw new NotImplementedException();
      }
      
      public void PretragaInventara()
      {
         throw new NotImplementedException();
      }
      
      public void PrikazLekara()
      {
         throw new NotImplementedException();
      }
      
      public void PretragaLekara()
      {
         throw new NotImplementedException();
      }
      
      public void ObradaHitnogSlucaja()
      {
         throw new NotImplementedException();
      }
   
   }
}