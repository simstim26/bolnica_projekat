// File:    Termin.cs
// Author:  User
// Created: Monday, March 22, 2021 8:52:17 PM
// Purpose: Definition of Class Termin

using System;

namespace Model
{
   public class Termin
   {
      private int tip;
      private DateTime datum;
      private DateTime satnica;
      private String idTermina;
      
      public Prostorija prostorija;
      public Pacijent pacijent;
      public Lekar[] lekar;
   
   }
}