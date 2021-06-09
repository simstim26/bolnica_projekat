// File:    Korisnik.cs
// Author:  User
// Created: Monday, March 22, 2021 7:07:21 PM
// Purpose: Definition of Class Korisnik

using Bolnica_aplikacija.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Model
{
   public abstract class Korisnik
   {
      public String korisnickoIme { get; set; }
      public String lozinka { get; set; }
      public String ime { get; set; }
      public String prezime { get; set; }
      public DateTime datumRodjenja { get; set; }
      public String adresa { get; set; }
      public String email { get; set; }
      public String brojTelefona { get; set; }
      public String jmbg { get; set; }
      public String mestoRodjenja { get; set; }
      public String drzavaRodjenja { get; set; }
      public String pol { get; set; }
      public String brojZdravstveneKnjizice { get; set; }
      public String bracniStatus { get; set; }
      public String zanimanje { get; set; }

   }
}