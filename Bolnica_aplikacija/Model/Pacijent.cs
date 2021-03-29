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
      
        public Pacijent (String jmbg, String ime, String prezime)
        {
            this.jmbg = jmbg;
            this.ime = ime;
            this.prezime = prezime;
        }
        public Pacijent() { }

        public String[] toString()
        {
            String[] stringovi = { jmbg, ime, prezime };
            return stringovi;
        }
      public static void NapraviTermin(DataGrid dataGridSlobodniTermini, DataGrid dataGrid, String idPacijenta)
      {
            //throw new NotImplementedException();

            
            
            var sviTermini = JsonSerializer.Deserialize<List<Termin>>(File.ReadAllText("Datoteke/probaTermini.txt"));
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
            var sviTermini = JsonSerializer.Deserialize<List<Termin>>(File.ReadAllText("Datoteke/probaTermini.txt"));
            var sveProstorije = JsonSerializer.Deserialize<List<Prostorija>>(File.ReadAllText("Datoteke/probaProstorije.txt"));
            var sviLekari = JsonSerializer.Deserialize<List<Lekar>>(File.ReadAllText("Datoteke/probaLekari.txt"));

            List<Termin> terminiPacijenta = new List<Termin>();
            List<PacijentTermin> terminiPacijentaIspravni = new List<PacijentTermin>();

            foreach (Termin termin in sviTermini)
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
                    String[] satnicaString = termin.satnica.ToString().Split(' ');
                    String[] sat = satnicaString[1].Split(':');
                    pacijentTermin.satnica = sat[0] + ':' + sat[1];
                    pacijentTermin.id = termin.idTermina;

                    terminiPacijentaIspravni.Add(pacijentTermin);

                }
            }

            dataGrid.ItemsSource = terminiPacijentaIspravni;


        }
      
      public static void AzurirajTermin(DataGrid dataGridSlobodniTermini, DataGrid dataGrid, string idPacijenta)
      {

            //throw new NotImplementedException();

            //za azuriranje trebam>>> prikazani svi slobodni termini ok
            //treba izbrisati id pacijenta sa izabranog termina u dataGrid 
            //treba ubaciti id pacijenta na izabrani termin u dataGridSlobodniTermini
            //azurirati prikaz dataGrid-a
            //
            //
            //
            PacijentTermin izabraniTermin = (PacijentTermin)dataGrid.SelectedItem;
            var sviTermini = JsonSerializer.Deserialize<List<Termin>>(File.ReadAllText("Datoteke/probaTermini.txt"));
            foreach (Termin termin in sviTermini)
            {
                if (izabraniTermin.id.Equals(termin.idTermina))
                {
                    termin.idPacijenta = "";
                }
            }

            string jsonString = JsonSerializer.Serialize(sviTermini);
            File.WriteAllText("Datoteke/probaTermini.txt", jsonString);

            PacijentTermin pacijentTermin = (PacijentTermin)dataGridSlobodniTermini.SelectedItem;

            foreach (Termin termin in sviTermini)
            {
                if (pacijentTermin.id.Equals(termin.idTermina))
                {
                    termin.idPacijenta = idPacijenta;
                    string jsonString2 = JsonSerializer.Serialize(sviTermini);
                    File.WriteAllText("Datoteke/probaTermini.txt", jsonString2);
                    break;
                }
            }

            ProcitajTermin(dataGrid, idPacijenta);


        }
      
      public static void ObrisiTermin(DataGrid dataGrid, string idPacijenta)
      {
            //throw new NotImplementedException();

            
            
                PacijentTermin izabraniTermin = (PacijentTermin)dataGrid.SelectedItem;
                var sviTermini = JsonSerializer.Deserialize<List<Termin>>(File.ReadAllText("Datoteke/probaTermini.txt"));
                foreach (Termin termin in sviTermini)
                {
                    if (izabraniTermin.id.Equals(termin.idTermina))
                    {
                        termin.idPacijenta = "";
                    }
                }

                string jsonString = JsonSerializer.Serialize(sviTermini);
                File.WriteAllText("Datoteke/probaTermini.txt", jsonString);

            

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
      
      public ZdravstveniKarton zdravstveniKarton;
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