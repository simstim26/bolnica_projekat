// File:    Termin.cs
// Author:  User
// Created: Monday, March 22, 2021 8:52:17 PM
// Purpose: Definition of Class Termin

using System;

namespace Model
{
   public class Termin
   {
        private TipTermina tip;
        private DateTime datum;
        private DateTime satnica;
        private String idTermina;
      
        public Prostorija prostorija;
        public String idPacijenta;
        public String idLekara;
      
        public TipTermina getTip()
        {
            return tip;
        }

        public DateTime getDatum()
        {
            return datum;
        }

        public DateTime getSatnica()
        {
            return satnica;
        }

        public String getIdTermina()
        {
            return idTermina;
        }

        public void setTipTermina(TipTermina tip)
        {
            this.tip = tip;
        }

        public void setDatum(DateTime datum)
        {
            this.datum = datum;
        }

        public void setSatnica(DateTime satnica)
        {
            this.satnica = satnica;
        }

        public void setIdTermina(String idTermina)
        {
            this.idTermina = idTermina;
        }

        //public Pacijent pacijent;
      //public Lekar[] lekar;
   
   }
}