// File:    Lek.cs
// Author:  User
// Created: Monday, March 22, 2021 8:10:25 PM
// Purpose: Definition of Class Lek

using System;

namespace Model
{
   public class Lek
   {
      public String id { get; set; }
      public String naziv { get; set; }
      public TipLeka tip { get; set; }
      public int kolicina { get; set; }
      public String proizvodjac { get; set; }
      public NacinUpotrebe nacinUpotrebe { get; set; }

    }
}