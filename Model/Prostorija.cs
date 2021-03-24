// File:    Prostorija.cs
// Author:  User
// Created: Monday, March 22, 2021 7:57:52 PM
// Purpose: Definition of Class Prostorija

using System;

namespace Model
{
   public class Prostorija
   {
      private String id;
      private String idBolnice;
      private TipProstorije tipProstorije;
      private String broj;
      private int sprat;
      private bool dostupnost;
      
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
      public Termin[] termin;
   
   }
}