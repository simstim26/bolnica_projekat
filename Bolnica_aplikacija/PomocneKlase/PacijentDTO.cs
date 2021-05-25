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
   public class PacijentDTO 
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