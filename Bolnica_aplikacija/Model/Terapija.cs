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

      public String dijagnoza { get; set; }
      public String idLeka { get; set; }
      public DateTime datumPocetka { get; set; }
      public int trajanje { get; set; }
      public String nacinUpotrebe { get; set; }

    }
}