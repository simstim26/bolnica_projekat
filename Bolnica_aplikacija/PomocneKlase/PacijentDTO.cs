// File:    Pacijent.cs
// Author:  User
// Created: Monday, March 22, 2021 7:07:23 PM
// Purpose: Definition of Class Pacijent

using Bolnica_aplikacija.PacijentModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows.Controls;

namespace Model
{
   public class PacijentDTO : Korisnik
   {
      public String id { get; set;}
      public String idBolnice { get; set; }
      public bool jeGost { get; set; }
      public Termin[] termin;

        public bool anketa { get; set; }

      public bool jeLogickiObrisan { get; set; }

        public PacijentDTO (String idBolnice, bool gost, String korisnickoIme, String lozinka, String jmbg, String ime, String prezime, DateTime datumRodj, String adresa, String email, String telefon)
        {
            this.jmbg = jmbg;
            this.ime = ime;
            this.prezime = prezime;
            this.idBolnice = idBolnice;
            this.jeGost = gost;
            this.korisnickoIme = korisnickoIme;
            this.lozinka = lozinka;
            this.jmbg = jmbg;
            this.ime = ime;
            this.prezime = prezime;
            this.datumRodjenja = datumRodj;
            this.adresa = adresa;
            this.email = email;
            this.brojTelefona = telefon;
        }

        public PacijentDTO() { }

        public PacijentDTO(String idPacijenta)
        {
            this.id = idPacijenta;
        }     
       
   }
}