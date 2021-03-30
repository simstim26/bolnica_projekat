// File:    Bolnica.cs
// Author:  User
// Created: Monday, March 22, 2021 7:56:33 PM
// Purpose: Definition of Class Bolnica

using System;

namespace Model
{
   public class Bolnica
   {
      private String id;
      private String naziv;
      private String adresa;
      private String grad;
      private String drzava;
      private String kontakt;
      
      public System.Collections.ArrayList sekretar;
      
      /// <summary>
      /// Property for collection of Sekretar
      /// </summary>
      /// <pdGenerated>Default opposite class collection property</pdGenerated>
      public System.Collections.ArrayList Sekretar
      {
         get
         {
            if (sekretar == null)
               sekretar = new System.Collections.ArrayList();
            return sekretar;
         }
         set
         {
            RemoveAllSekretar();
            if (value != null)
            {
               foreach (Sekretar oSekretar in value)
                  AddSekretar(oSekretar);
            }
         }
      }
      
      /// <summary>
      /// Add a new Sekretar in the collection
      /// </summary>
      /// <pdGenerated>Default Add</pdGenerated>
      public void AddSekretar(Sekretar newSekretar)
      {
         if (newSekretar == null)
            return;
         if (this.sekretar == null)
            this.sekretar = new System.Collections.ArrayList();
         if (!this.sekretar.Contains(newSekretar))
            this.sekretar.Add(newSekretar);
      }
      
      /// <summary>
      /// Remove an existing Sekretar from the collection
      /// </summary>
      /// <pdGenerated>Default Remove</pdGenerated>
      public void RemoveSekretar(Sekretar oldSekretar)
      {
         if (oldSekretar == null)
            return;
         if (this.sekretar != null)
            if (this.sekretar.Contains(oldSekretar))
               this.sekretar.Remove(oldSekretar);
      }
      
      /// <summary>
      /// Remove all instances of Sekretar from the collection
      /// </summary>
      /// <pdGenerated>Default removeAll</pdGenerated>
      public void RemoveAllSekretar()
      {
         if (sekretar != null)
            sekretar.Clear();
      }
      public System.Collections.ArrayList lekar;
      
      /// <summary>
      /// Property for collection of Lekar
      /// </summary>
      /// <pdGenerated>Default opposite class collection property</pdGenerated>
      public System.Collections.ArrayList Lekar
      {
         get
         {
            if (lekar == null)
               lekar = new System.Collections.ArrayList();
            return lekar;
         }
         set
         {
            RemoveAllLekar();
            if (value != null)
            {
               foreach (Lekar oLekar in value)
                  AddLekar(oLekar);
            }
         }
      }
      
      /// <summary>
      /// Add a new Lekar in the collection
      /// </summary>
      /// <pdGenerated>Default Add</pdGenerated>
      public void AddLekar(Lekar newLekar)
      {
         if (newLekar == null)
            return;
         if (this.lekar == null)
            this.lekar = new System.Collections.ArrayList();
         if (!this.lekar.Contains(newLekar))
            this.lekar.Add(newLekar);
      }
      
      /// <summary>
      /// Remove an existing Lekar from the collection
      /// </summary>
      /// <pdGenerated>Default Remove</pdGenerated>
      public void RemoveLekar(Lekar oldLekar)
      {
         if (oldLekar == null)
            return;
         if (this.lekar != null)
            if (this.lekar.Contains(oldLekar))
               this.lekar.Remove(oldLekar);
      }
      
      /// <summary>
      /// Remove all instances of Lekar from the collection
      /// </summary>
      /// <pdGenerated>Default removeAll</pdGenerated>
      public void RemoveAllLekar()
      {
         if (lekar != null)
            lekar.Clear();
      }
      public Upravnik upravnik;
      public System.Collections.ArrayList pacijent;
      
      /// <summary>
      /// Property for collection of Pacijent
      /// </summary>
      /// <pdGenerated>Default opposite class collection property</pdGenerated>
      public System.Collections.ArrayList Pacijent
      {
         get
         {
            if (pacijent == null)
               pacijent = new System.Collections.ArrayList();
            return pacijent;
         }
         set
         {
            RemoveAllPacijent();
            if (value != null)
            {
               foreach (Pacijent oPacijent in value)
                  AddPacijent(oPacijent);
            }
         }
      }
      
      /// <summary>
      /// Add a new Pacijent in the collection
      /// </summary>
      /// <pdGenerated>Default Add</pdGenerated>
      public void AddPacijent(Pacijent newPacijent)
      {
         if (newPacijent == null)
            return;
         if (this.pacijent == null)
            this.pacijent = new System.Collections.ArrayList();
         if (!this.pacijent.Contains(newPacijent))
            this.pacijent.Add(newPacijent);
      }
      
      /// <summary>
      /// Remove an existing Pacijent from the collection
      /// </summary>
      /// <pdGenerated>Default Remove</pdGenerated>
      public void RemovePacijent(Pacijent oldPacijent)
      {
         if (oldPacijent == null)
            return;
         if (this.pacijent != null)
            if (this.pacijent.Contains(oldPacijent))
               this.pacijent.Remove(oldPacijent);
      }
      
      /// <summary>
      /// Remove all instances of Pacijent from the collection
      /// </summary>
      /// <pdGenerated>Default removeAll</pdGenerated>
      public void RemoveAllPacijent()
      {
         if (pacijent != null)
            pacijent.Clear();
      }
      public System.Collections.ArrayList prostorija;
      
      /// <summary>
      /// Property for collection of Prostorija
      /// </summary>
      /// <pdGenerated>Default opposite class collection property</pdGenerated>
      public System.Collections.ArrayList Prostorija
      {
         get
         {
            if (prostorija == null)
               prostorija = new System.Collections.ArrayList();
            return prostorija;
         }
         set
         {
            RemoveAllProstorija();
            if (value != null)
            {
               foreach (Prostorija oProstorija in value)
                  AddProstorija(oProstorija);
            }
         }
      }
      
      /// <summary>
      /// Add a new Prostorija in the collection
      /// </summary>
      /// <pdGenerated>Default Add</pdGenerated>
      public void AddProstorija(Prostorija newProstorija)
      {
         if (newProstorija == null)
            return;
         if (this.prostorija == null)
            this.prostorija = new System.Collections.ArrayList();
         if (!this.prostorija.Contains(newProstorija))
            this.prostorija.Add(newProstorija);
      }
      
      /// <summary>
      /// Remove an existing Prostorija from the collection
      /// </summary>
      /// <pdGenerated>Default Remove</pdGenerated>
      public void RemoveProstorija(Prostorija oldProstorija)
      {
         if (oldProstorija == null)
            return;
         if (this.prostorija != null)
            if (this.prostorija.Contains(oldProstorija))
               this.prostorija.Remove(oldProstorija);
      }
      
      /// <summary>
      /// Remove all instances of Prostorija from the collection
      /// </summary>
      /// <pdGenerated>Default removeAll</pdGenerated>
      public void RemoveAllProstorija()
      {
         if (prostorija != null)
            prostorija.Clear();
      }
      public System.Collections.ArrayList stavka;
      
      /// <summary>
      /// Property for collection of Stavka
      /// </summary>
      /// <pdGenerated>Default opposite class collection property</pdGenerated>
      public System.Collections.ArrayList Stavka
      {
         get
         {
            if (stavka == null)
               stavka = new System.Collections.ArrayList();
            return stavka;
         }
         set
         {
            RemoveAllStavka();
            if (value != null)
            {
               foreach (Stavka oStavka in value)
                  AddStavka(oStavka);
            }
         }
      }
      
      /// <summary>
      /// Add a new Stavka in the collection
      /// </summary>
      /// <pdGenerated>Default Add</pdGenerated>
      public void AddStavka(Stavka newStavka)
      {
         if (newStavka == null)
            return;
         if (this.stavka == null)
            this.stavka = new System.Collections.ArrayList();
         if (!this.stavka.Contains(newStavka))
            this.stavka.Add(newStavka);
      }
      
      /// <summary>
      /// Remove an existing Stavka from the collection
      /// </summary>
      /// <pdGenerated>Default Remove</pdGenerated>
      public void RemoveStavka(Stavka oldStavka)
      {
         if (oldStavka == null)
            return;
         if (this.stavka != null)
            if (this.stavka.Contains(oldStavka))
               this.stavka.Remove(oldStavka);
      }
      
      /// <summary>
      /// Remove all instances of Stavka from the collection
      /// </summary>
      /// <pdGenerated>Default removeAll</pdGenerated>
      public void RemoveAllStavka()
      {
         if (stavka != null)
            stavka.Clear();
      }
      public System.Collections.ArrayList lek;
      
      /// <summary>
      /// Property for collection of Lek
      /// </summary>
      /// <pdGenerated>Default opposite class collection property</pdGenerated>
      public System.Collections.ArrayList Lek
      {
         get
         {
            if (lek == null)
               lek = new System.Collections.ArrayList();
            return lek;
         }
         set
         {
            RemoveAllLek();
            if (value != null)
            {
               foreach (Lek oLek in value)
                  AddLek(oLek);
            }
         }
      }
      
      /// <summary>
      /// Add a new Lek in the collection
      /// </summary>
      /// <pdGenerated>Default Add</pdGenerated>
      public void AddLek(Lek newLek)
      {
         if (newLek == null)
            return;
         if (this.lek == null)
            this.lek = new System.Collections.ArrayList();
         if (!this.lek.Contains(newLek))
            this.lek.Add(newLek);
      }
      
      /// <summary>
      /// Remove an existing Lek from the collection
      /// </summary>
      /// <pdGenerated>Default Remove</pdGenerated>
      public void RemoveLek(Lek oldLek)
      {
         if (oldLek == null)
            return;
         if (this.lek != null)
            if (this.lek.Contains(oldLek))
               this.lek.Remove(oldLek);
      }
      
      /// <summary>
      /// Remove all instances of Lek from the collection
      /// </summary>
      /// <pdGenerated>Default removeAll</pdGenerated>
      public void RemoveAllLek()
      {
         if (lek != null)
            lek.Clear();
      }
      public System.Collections.ArrayList specijalizacija;
      
      /// <summary>
      /// Property for collection of Specijalizacija
      /// </summary>
      /// <pdGenerated>Default opposite class collection property</pdGenerated>
      public System.Collections.ArrayList Specijalizacija
      {
         get
         {
            if (specijalizacija == null)
               specijalizacija = new System.Collections.ArrayList();
            return specijalizacija;
         }
         set
         {
            RemoveAllSpecijalizacija();
            if (value != null)
            {
               foreach (Specijalizacija oSpecijalizacija in value)
                  AddSpecijalizacija(oSpecijalizacija);
            }
         }
      }
      
      /// <summary>
      /// Add a new Specijalizacija in the collection
      /// </summary>
      /// <pdGenerated>Default Add</pdGenerated>
      public void AddSpecijalizacija(Specijalizacija newSpecijalizacija)
      {
         if (newSpecijalizacija == null)
            return;
         if (this.specijalizacija == null)
            this.specijalizacija = new System.Collections.ArrayList();
         if (!this.specijalizacija.Contains(newSpecijalizacija))
            this.specijalizacija.Add(newSpecijalizacija);
      }
      
      /// <summary>
      /// Remove an existing Specijalizacija from the collection
      /// </summary>
      /// <pdGenerated>Default Remove</pdGenerated>
      public void RemoveSpecijalizacija(Specijalizacija oldSpecijalizacija)
      {
         if (oldSpecijalizacija == null)
            return;
         if (this.specijalizacija != null)
            if (this.specijalizacija.Contains(oldSpecijalizacija))
               this.specijalizacija.Remove(oldSpecijalizacija);
      }
      
      /// <summary>
      /// Remove all instances of Specijalizacija from the collection
      /// </summary>
      /// <pdGenerated>Default removeAll</pdGenerated>
      public void RemoveAllSpecijalizacija()
      {
         if (specijalizacija != null)
            specijalizacija.Clear();
      }
   
   }
}