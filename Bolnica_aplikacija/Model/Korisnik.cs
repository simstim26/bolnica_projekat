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
      public String email { get; set; }
      public String brojTelefona { get; set; }
      public String jmbg { get; set; }

        public static String[] Prijava(String korisnickoIme, String lozinka)
        {
            String[] retVal = { "",""};
            foreach (eksperiment e in JsonSerializer.Deserialize<List<eksperiment>>(File.ReadAllText("Datoteke/probaKorisnici.txt")))
            {
                if(e.korisnickoIme.Equals(korisnickoIme) && e.lozinka.Equals(lozinka))
                {
                    retVal[0] = e.tip;
                    retVal[1] = e.id;
                }
            }

            return retVal;

        }
   }
}