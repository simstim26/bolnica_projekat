// File:    Prostorija.cs
// Author:  User
// Created: Monday, March 22, 2021 7:57:52 PM
// Purpose: Definition of Class Prostorija

using System;
using System.Collections.Generic;

namespace Model
{
   public class Prostorija
   {
      public String id { get; set; }
      public String idBolnice { get; set; }
      public TipProstorije tipProstorije { get; set; }
      public String broj { get; set; }
      public int sprat { get; set; }
      public bool dostupnost { get; set; }
      public bool logickiObrisana { get; set; }
      
      public System.Collections.ArrayList stavka;

      public List<Stavka> Stavka { get; set; }
      
      /// <summary>
      /// Property for collection of Stavka
      /// </summary>
      /// <pdGenerated>Default opposite class collection property</pdGenerated>
     /* public System.Collections.ArrayList Stavka
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
            stavka.Clear();*/
      //}
      public Termin[] termin;
   

      public static TipProstorije ConvertTip(String s)
      {
        if(s == "BOLNICKA_SOBA")
            {
                return TipProstorije.BOLNICKA_SOBA;
            }
        else if(s == "OPERACIONA_SALA")
            {
                return TipProstorije.OPERACIONA_SALA;
            }
        else if(s == "SOBA_ZA_PREGLED")
            {
                return TipProstorije.SOBA_ZA_PREGLED;
            }
            return TipProstorije.GRESKA;
      }
   }
}