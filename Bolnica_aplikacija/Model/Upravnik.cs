// File:    Upravnik.cs
// Author:  User
// Created: Monday, March 22, 2021 7:07:23 PM
// Purpose: Definition of Class Upravnik

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows;

namespace Model
{
   public class Upravnik : Korisnik
   {
      public String id { get; set; }
      public String idBolnice { get; set; }
   
   }
}