// File:    Pacijent.cs
// Author:  User
// Created: Monday, March 22, 2021 7:07:23 PM
// Purpose: Definition of Class Pacijent

using System;

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