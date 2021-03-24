// File:    ZdravstveniKarton.cs
// Author:  User
// Created: Monday, March 22, 2021 8:21:33 PM
// Purpose: Definition of Class ZdravstveniKarton

using System;

namespace Model
{
   public class ZdravstveniKarton
   {
      public System.Collections.ArrayList terapija;
      
      /// <summary>
      /// Property for collection of Terapija
      /// </summary>
      /// <pdGenerated>Default opposite class collection property</pdGenerated>
      public System.Collections.ArrayList Terapija
      {
         get
         {
            if (terapija == null)
               terapija = new System.Collections.ArrayList();
            return terapija;
         }
         set
         {
            RemoveAllTerapija();
            if (value != null)
            {
               foreach (Terapija oTerapija in value)
                  AddTerapija(oTerapija);
            }
         }
      }
      
      /// <summary>
      /// Add a new Terapija in the collection
      /// </summary>
      /// <pdGenerated>Default Add</pdGenerated>
      public void AddTerapija(Terapija newTerapija)
      {
         if (newTerapija == null)
            return;
         if (this.terapija == null)
            this.terapija = new System.Collections.ArrayList();
         if (!this.terapija.Contains(newTerapija))
            this.terapija.Add(newTerapija);
      }
      
      /// <summary>
      /// Remove an existing Terapija from the collection
      /// </summary>
      /// <pdGenerated>Default Remove</pdGenerated>
      public void RemoveTerapija(Terapija oldTerapija)
      {
         if (oldTerapija == null)
            return;
         if (this.terapija != null)
            if (this.terapija.Contains(oldTerapija))
               this.terapija.Remove(oldTerapija);
      }
      
      /// <summary>
      /// Remove all instances of Terapija from the collection
      /// </summary>
      /// <pdGenerated>Default removeAll</pdGenerated>
      public void RemoveAllTerapija()
      {
         if (terapija != null)
            terapija.Clear();
      }
   
   }
}