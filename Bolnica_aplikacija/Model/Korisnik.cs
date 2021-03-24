// File:    Korisnik.cs
// Author:  User
// Created: Monday, March 22, 2021 7:07:21 PM
// Purpose: Definition of Class Korisnik

using System;

namespace Model
{
   public abstract class Korisnik
   {
      protected String korisnickoIme;
      protected String lozinka;
      protected String ime;
      protected String prezime;
      protected DateTime datumRodjenja;
      protected String email;
      protected String brojTelefona;
      protected String jmbg;
      
      public void Prijava(String korisnickoIme, String lozinka)
      {
         throw new NotImplementedException();
      }
   
   }
}