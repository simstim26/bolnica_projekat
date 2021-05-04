using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.PomocneKlase
{
    class Logovanje
    {
        public String idKorisnika { get; set; }            // aka id logovanja - svaki korisnik ima jedno logovanje
        public int brojUzastopnihIzmena { get; set; }      // broj promena koji ce se povecavati ili resetovati u skladu sa vremenskim intervalom
        //Termin termin { get; set; }
        public DateTime vremeIzmene { get; set; }          // pocetak vremenskog intervala u kom se sme izvrsiti odredjen broj izmena termina

        public Logovanje(String idKorisnika, DateTime vremeIzmene, int brojUzastopnihIzmena)
        {
            this.idKorisnika = idKorisnika;
            this.vremeIzmene = vremeIzmene;
            //this.termin = termin;
            this.brojUzastopnihIzmena = brojUzastopnihIzmena;

        }

    }
}
