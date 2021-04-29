// File:    Lek.cs
// Author:  User
// Created: Monday, March 22, 2021 8:10:25 PM
// Purpose: Definition of Class Lek

using System;
using System.Collections.Generic;

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
      public List<String> sastojci { get; set; }
      public List<Lek> zamenskiLekovi { get; set; }

      public String getNacinUpotrebeString()
      {
            switch (nacinUpotrebe)
            {
                case NacinUpotrebe.REKTALNO:
                    return "Rektalno";
                case NacinUpotrebe.PER_OS:
                    return "Per os";
                case NacinUpotrebe.SUBLINGVALNO:
                    return "Sublingvalno";
                default:
                    return "";
            }
      }

      public String getTipString()
        {
            switch (tip)
            {
                case TipLeka.ANALGETIK:
                    return "Analgetik";
                case TipLeka.ANTIBIOTIK:
                    return "Antibiotik";
                case TipLeka.ANTIMALARIJSKI:
                    return "Antimalarijski";
                case TipLeka.ANTIPIRETIK:
                    return "Antipiretik";
                case TipLeka.ANTISEPTIK:
                    return "Antiseptik";
                case TipLeka.HORMONSKE_ZAMENE:
                    return "Hormonske zamene";
                case TipLeka.ORALNI_KONTRACEPTIV:
                    return "Oralni kontraceptiv";
                case TipLeka.STABILIZATORI_RASP:
                    return "Stabilizatori raspolozenja";
                case TipLeka.STATIN:
                    return "Statin";
                case TipLeka.STIMULANT:
                    return "Stimulant";
                case TipLeka.TRANKVILAJZER:
                    return "Trankvilajzer";
                default:
                    return "";
            }
        }

    }
}