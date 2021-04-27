// File:    Notifikacija.cs
// Author:  User
// Created: Wednesday, March 24, 2021 1:26:44 PM
// Purpose: Definition of Class Notifikacija

using System;

namespace Model
{
   public class Notifikacija
   {
        public String id { get; set; }
        public String nazivNotifikacije { get; set; }
        public DateTime vremeNotifikovanja { get; set; }
        public String porukaNotifikacije { get; set; }
        public String idKorisnika { get; set; }
        public DateTime datumNotifikovanja { get; set; }
        public bool jeProcitana { get; set; }
        
        public Notifikacija()
        {

        }
        
        public Notifikacija(String id, String nazivNotifikacije, DateTime vremeNotifikovanja, String porukaNotifikacije, String idKorisnika, DateTime datumNotifikovanja, bool jeProcitana)
        {
            this.id = id;
            this.nazivNotifikacije = nazivNotifikacije;
            this.vremeNotifikovanja = vremeNotifikovanja;
            this.porukaNotifikacije = porukaNotifikacije;
            this.idKorisnika = idKorisnika;
            this.datumNotifikovanja = datumNotifikovanja;
            this.jeProcitana = jeProcitana;
        }

    }
}