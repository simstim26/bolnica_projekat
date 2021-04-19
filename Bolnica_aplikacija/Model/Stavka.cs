// File:    Stavka.cs
// Author:  User
// Created: Monday, March 22, 2021 8:04:15 PM
// Purpose: Definition of Class Stavka

using System;

namespace Model
{
   public class Stavka
   {
        public String id { get; set; }
        public String naziv { get; set; }
        public int kolicina { get; set; }
        public String proizvodjac { get; set; }
        public String idProstorije { get; set; }
        public String idBolnice { get; set; }
        public bool jeStaticka { get; set; }
        public bool jeLogickiObrisana { get; set; }
        public bool jePotrosnaRoba { get; set; }

    }
}