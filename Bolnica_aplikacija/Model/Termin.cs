// File:    Termin.cs
// Author:  User
// Created: Monday, March 22, 2021 8:52:17 PM
// Purpose: Definition of Class Termin

using System;

namespace Model
{
   public class Termin
   {
        public TipTermina tip { get; set; }
        public DateTime datum { get; set; }
        public DateTime satnica { get; set; }
        public bool jeZavrsen { get; set; }
        public String idTermina { get; set; }
        public String idProstorije { get; set; }
        public String idPacijenta { get; set; }
        public String idLekara { get; set; }
        public String idTerapije { get; set; }
        public String idBolesti { get; set; }
        public String izvestaj { get; set; }
        public String izvestajUputa { get; set; }
        public String idUputLekara { get; set; }
        public String idUputTermin { get; set; }
        public bool jeHitan { get; set; }

        public void kopiraj(Termin termin)
        {
            tip = termin.tip;
            datum = termin.datum;
            satnica = termin.satnica;
            jeZavrsen = termin.jeZavrsen;
            idTermina = termin.idTermina;
            idProstorije = termin.idProstorije;
            idPacijenta = termin.idPacijenta;
            idLekara = termin.idLekara;
            idTerapije = termin.idTerapije;
            idBolesti = termin.idBolesti;
            izvestaj = termin.izvestaj;
            izvestajUputa = termin.izvestajUputa;
            idUputLekara = termin.idUputLekara;
            idUputTermin = termin.idUputTermin;
            jeHitan = termin.jeHitan;
        }

        public String getTipString()
        {
            if (tip == TipTermina.PREGLED)
            {
                return "Pregled";
            }
            else
                return "Operacija";
        }

    }
}