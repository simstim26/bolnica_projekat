// File:    Terapija.cs
// Author:  User
// Created: Wednesday, March 24, 2021 1:23:21 PM
// Purpose: Definition of Class Terapija

using System;

namespace Model
{
   public class Terapija
   {
      public String id { get; set; }
      public String idLeka { get; set; }
      public String idPacijenta { get; set; }
      public String idBolesti { get; set; }
      public String idTermina { get; set; }
      public DateTime datumPocetka { get; set; }
      public int trajanje { get; set; }
      public String nacinUpotrebe { get; set; }
    
      public Terapija() { }
      public Terapija(String id, String idLeka, String idPacijenta, String idBolesti, String idTermina, DateTime datumPocetka, 
          int trajanje, String nacinUpotrebe)
      {
            this.id = id;
            this.idLeka = idLeka;
            this.idPacijenta = idPacijenta;
            this.idBolesti = idBolesti;
            this.idTermina = idTermina;
            this.datumPocetka = datumPocetka;
            this.trajanje = trajanje;
            this.nacinUpotrebe = nacinUpotrebe;
      }

      public void kopiraj(Terapija terapija)
      {
            id = terapija.id;
            idLeka = terapija.idLeka;
            idPacijenta = terapija.idPacijenta;
            idBolesti = terapija.idBolesti;
            idTermina = terapija.idTermina;
            datumPocetka = terapija.datumPocetka;
            trajanje = terapija.trajanje;
            nacinUpotrebe = terapija.nacinUpotrebe;
      }

    }
}