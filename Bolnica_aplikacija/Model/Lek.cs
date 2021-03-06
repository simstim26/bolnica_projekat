// File:    Lek.cs
// Author:  User
// Created: Monday, March 22, 2021 8:10:25 PM
// Purpose: Definition of Class Lek

using Bolnica_aplikacija.Model;
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

      public Lek() { }
      public Lek(LekZaOdobravanje lek, String id)
      {
            this.id = id;
            naziv = lek.naziv;
            tip = lek.tip;
            kolicina = lek.kolicina;
            proizvodjac = lek.proizvodjac;
            nacinUpotrebe = lek.nacinUpotrebe;
            sastojci = lek.sastojci;
            zamenskiLekovi = lek.zamenskiLekovi;
      }
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

      public void kopiraj(Lek lek)
      {
            id = lek.id;
            naziv = lek.naziv;
            tip = lek.tip;
            kolicina = lek.kolicina;
            proizvodjac = lek.proizvodjac;
            sastojci = lek.sastojci;
            zamenskiLekovi = lek.zamenskiLekovi;
            nacinUpotrebe = lek.nacinUpotrebe;
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