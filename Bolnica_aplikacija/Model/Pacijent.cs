// File:    Pacijent.cs
// Author:  User
// Created: Monday, March 22, 2021 7:07:23 PM
// Purpose: Definition of Class Pacijent

using Bolnica_aplikacija.PacijentModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows.Controls;

namespace Model
{
   public class Pacijent : Korisnik
   {
      public String id { get; set;}
      public String idBolnice { get; set; }
      public bool jeGost { get; set; }
      public String adresa { get; set; }
      public Termin[] termin;

        public bool anketa { get; set; }

      public bool jeLogickiObrisan { get; set; }

        //String idBolnice, bool gost, String korisnickoIme, String lozinka, String jmbg, String ime, String prezime, DateTime datumRodj, string adresa, string email, string telefon
        public Pacijent (String idBolnice, bool gost, String korisnickoIme, String lozinka, String jmbg, String ime, String prezime, DateTime datumRodj, String adresa, String email, String telefon)
        {
            this.idBolnice = idBolnice;
            this.jeGost = gost;
            this.korisnickoIme = korisnickoIme;
            this.lozinka = lozinka;         
            this.jmbg = jmbg;
            this.ime = ime;
            this.prezime = prezime;
            this.datumRodjenja = datumRodj;
            this.adresa = adresa;
            this.email = email;
            this.brojTelefona = telefon;
        }
        public Pacijent() { }

        public Pacijent(String idPacijenta)
        {
            this.id = idPacijenta;
        }
          
        public String[] toString()
        {
            String[] stringovi = { jmbg, ime, prezime };
            return stringovi;
        }
      public static void NapraviTermin(DataGrid dataGridSlobodniTermini, DataGrid dataGrid, String idPacijenta)
      {
            //throw new NotImplementedException();

            List<Termin> sviTermini;
            try
            {
                sviTermini = JsonSerializer.Deserialize<List<Termin>>(File.ReadAllText("Datoteke/probaTermini.txt"));
            }
            catch(Exception e)
            {
                sviTermini = new List<Termin>();
                Console.WriteLine(e.Message);
            }
            
            PacijentTermin pacijentTermin = (PacijentTermin)dataGridSlobodniTermini.SelectedItem;

            foreach (Termin termin in sviTermini)
            {
                if (pacijentTermin.id.Equals(termin.idTermina))
                {
                    termin.idPacijenta = idPacijenta;
                    termin.tip = TipTermina.PREGLED;
                    string jsonString = JsonSerializer.Serialize(sviTermini);
                    File.WriteAllText("Datoteke/probaTermini.txt", jsonString);
                    break;
                }
            }

            ProcitajTermin(dataGrid, idPacijenta);
        }
      
      public static void ProcitajTermin(DataGrid dataGrid, string idPacijenta)
      {

            List<Termin> sviTermini;
            List<Prostorija> sveProstorije;
            List<Lekar> sviLekari;

            try
            {

                sviTermini = JsonSerializer.Deserialize<List<Termin>>(File.ReadAllText("Datoteke/probaTermini.txt"));
                sveProstorije = JsonSerializer.Deserialize<List<Prostorija>>(File.ReadAllText("Datoteke/probaProstorije.txt"));
                sviLekari = JsonSerializer.Deserialize<List<Lekar>>(File.ReadAllText("Datoteke/probaLekari.txt"));

            }
            catch(Exception e)
            {
                sviTermini = new List<Termin>();
                sveProstorije = new List<Prostorija>();
                sviLekari = new List<Lekar>();

                Console.WriteLine(e.Message);
            }
            

            List<Termin> terminiPacijenta = new List<Termin>();
            List<PacijentTermin> terminiPacijentaIspravni = new List<PacijentTermin>();

            foreach (Termin termin in sviTermini)
            {
                DateTime terminDatum = termin.datum;
                DateTime danasnjiDatum = DateTime.Today;

                int rezultat = DateTime.Compare(terminDatum, danasnjiDatum);

                if(rezultat < 0)
                {

                }
                else
                {
                    PacijentTermin pacijentTermin = new PacijentTermin();
                    foreach (Prostorija prostorija in sveProstorije)
                    {
                        if (prostorija.id.Equals(termin.idProstorije))
                            pacijentTermin.lokacija = "Sprat " + prostorija.sprat + ", broj " + prostorija.broj;

                    }

                    foreach (Lekar lekar in sviLekari)
                    {
                        if (lekar.id.Equals(termin.idLekara))
                        {
                            pacijentTermin.imeLekara = lekar.ime + " " + lekar.prezime;
                        }
                    }

                    if (termin.idPacijenta.Equals(idPacijenta))
                    {
                        //terminiPacijenta.Add(termin);
                        String[] datumBezVremena = termin.datum.Date.ToString().Split(' ');
                        pacijentTermin.datum = datumBezVremena[0];
                        switch (termin.tip)
                        {
                            case TipTermina.OPERACIJA: pacijentTermin.napomena = "Operacija"; break;
                            case TipTermina.PREGLED: pacijentTermin.napomena = "Pregled"; break;
                            default: break;
                        }
                        String satnica = termin.satnica.ToString("HH:mm");
                        pacijentTermin.satnica = satnica;
                        pacijentTermin.id = termin.idTermina;

                        terminiPacijentaIspravni.Add(pacijentTermin);

                    }
                } 
            }

            dataGrid.ItemsSource = terminiPacijentaIspravni;


        }
      
      public static void AzurirajTermin(DataGrid dataGridSlobodniTermini, DataGrid dataGrid, string idPacijenta)
      {

            List<Termin> sviTermini;

            try
            {
                sviTermini = JsonSerializer.Deserialize<List<Termin>>(File.ReadAllText("Datoteke/probaTermini.txt"));
            }
            catch(Exception e)
            {
                sviTermini = new List<Termin>();
                Console.WriteLine(e.Message);
            }

            PacijentTermin izabraniTermin = (PacijentTermin)dataGrid.SelectedItem;
           
            foreach (Termin termin in sviTermini)
            {
                if (izabraniTermin.id.Equals(termin.idTermina))
                {
                    termin.idPacijenta = "";
                }
            }

            try
            {
                string jsonString = JsonSerializer.Serialize(sviTermini);
                File.WriteAllText("Datoteke/probaTermini.txt", jsonString);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            PacijentTermin pacijentTermin = (PacijentTermin)dataGridSlobodniTermini.SelectedItem;

            foreach (Termin termin in sviTermini)
            {
                if (pacijentTermin.id.Equals(termin.idTermina))
                {
                    termin.idPacijenta = idPacijenta;

                    try
                    {
                        string jsonString2 = JsonSerializer.Serialize(sviTermini);
                        File.WriteAllText("Datoteke/probaTermini.txt", jsonString2);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    
                    break;
                }
            }

            ProcitajTermin(dataGrid, idPacijenta);


        }
      
      public static void ObrisiTermin(DataGrid dataGrid, string idPacijenta)
      {
            //throw new NotImplementedException();

            if (dataGrid.SelectedIndex != -1)
            {
                List<Termin> sviTermini;
                PacijentTermin izabraniTermin = (PacijentTermin)dataGrid.SelectedItem;

                try
                {
                    sviTermini = JsonSerializer.Deserialize<List<Termin>>(File.ReadAllText("Datoteke/probaTermini.txt"));
                }
                catch(Exception e)
                {
                    sviTermini = new List<Termin>();
                    Console.WriteLine(e.Message);
                }
                
                foreach (Termin termin in sviTermini)
                {
                    if (izabraniTermin.id.Equals(termin.idTermina))
                    {
                        termin.idPacijenta = "";
                    }
                }

                try
                {
                    string jsonString = JsonSerializer.Serialize(sviTermini);
                    File.WriteAllText("Datoteke/probaTermini.txt", jsonString);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }

            ProcitajTermin(dataGrid, idPacijenta);

        }
      
      public void NapraviNotifikaciju()
      {
         throw new NotImplementedException();
      }
      
      public void AzurirajNotifikaciju()
      {
         throw new NotImplementedException();
      }
      
      public void ProcitajNotifikaciju()
      {
         throw new NotImplementedException();
      }
      
      public void ObrisiNotifikaciju()
      {
         throw new NotImplementedException();
      }
      
      public void OcenjivanjeLekara(int ocena)
      {
         throw new NotImplementedException();
      }
      
      public void PlacanjePregleda()
      {
         throw new NotImplementedException();
      }
      
      public void UvidUZdravstveniKarton()
      {
         throw new NotImplementedException();
      }
      
      public void UvitUAktivnuTerapiju()
      {
         throw new NotImplementedException();
      }
      
      public System.Collections.ArrayList notifikacija;
      
      /// <summary>
      /// Property for collection of Notifikacija
      /// </summary>
      /// <pdGenerated>Default opposite class collection property</pdGenerated>
      public System.Collections.ArrayList Notifikacija
      {
         get
         {
            if (notifikacija == null)
               notifikacija = new System.Collections.ArrayList();
            return notifikacija;
         }
         set
         {
            RemoveAllNotifikacija();
            if (value != null)
            {
               foreach (Notifikacija oNotifikacija in value)
                  AddNotifikacija(oNotifikacija);
            }
         }
      }
      
      /// <summary>
      /// Add a new Notifikacija in the collection
      /// </summary>
      /// <pdGenerated>Default Add</pdGenerated>
      public void AddNotifikacija(Notifikacija newNotifikacija)
      {
         if (newNotifikacija == null)
            return;
         if (this.notifikacija == null)
            this.notifikacija = new System.Collections.ArrayList();
         if (!this.notifikacija.Contains(newNotifikacija))
            this.notifikacija.Add(newNotifikacija);
      }
      
      /// <summary>
      /// Remove an existing Notifikacija from the collection
      /// </summary>
      /// <pdGenerated>Default Remove</pdGenerated>
      public void RemoveNotifikacija(Notifikacija oldNotifikacija)
      {
         if (oldNotifikacija == null)
            return;
         if (this.notifikacija != null)
            if (this.notifikacija.Contains(oldNotifikacija))
               this.notifikacija.Remove(oldNotifikacija);
      }
      
      /// <summary>
      /// Remove all instances of Notifikacija from the collection
      /// </summary>
      /// <pdGenerated>Default removeAll</pdGenerated>
      public void RemoveAllNotifikacija()
      {
         if (notifikacija != null)
            notifikacija.Clear();
      }
   
   }
}