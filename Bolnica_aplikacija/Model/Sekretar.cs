// File:    Sekretar.cs
// Author:  User
// Created: Monday, March 22, 2021 7:07:22 PM
// Purpose: Definition of Class Sekretar

using Bolnica_aplikacija.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows.Controls;

namespace Model
{
   public class Sekretar : Korisnik
   {
      public String id { get; set; }
      public String idBolnice { get; set; }
  
      public void NapraviTermin(Termin termin)
      {
         throw new NotImplementedException();
      }
      
      public void ProcitajTermin(Termin termin)
      {
         throw new NotImplementedException();
      }
      
      public void AzurirajTermin(Termin termin)
      {
         throw new NotImplementedException();
      }
      
      public void ObrisiTermin(String idTermina)
      {
         throw new NotImplementedException();
      }
      
      public void PrikazSala()
      {
         throw new NotImplementedException();
      }
      
      public void PretragaSala()
      {
         throw new NotImplementedException();
      }
      
      public void PrikazInventara()
      {
         throw new NotImplementedException();
      }
      
      public void PlacanjePregleda()
      {
         throw new NotImplementedException();
      }
      
      public void PretragaInventara()
      {
         throw new NotImplementedException();
      }
      
      public void PrikazLekara()
      {
         throw new NotImplementedException();
      }
      
      public void PretragaLekara()
      {
         throw new NotImplementedException();
      }
      
      public void ObradaHitnogSlucaja()
      {
         throw new NotImplementedException();
      }
   
   }
}