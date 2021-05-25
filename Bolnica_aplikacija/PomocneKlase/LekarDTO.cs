using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.PomocneKlase
{
    class LekarDTO : Korisnik
    {
        public String id { get; set; }
        public int brojSlobodnihDana { get; set; }
        public double prosecnaOcena { get; set; }
        public String idBolnice { get; set; }
        public String idSpecijalizacije { get; set; }
        public DateTime pocetakGodisnjegOdmora { get; set; }
        public DateTime pocetakRadnogVremena { get; set; }
        public DateTime krajRadnogVremena { get; set; }
        public bool jeNaGodisnjemOdmoru { get; set; }
        public bool jeLogickiObrisan { get; set; }
        public List<Notifikacija> notifikacije { get; set; }

        public LekarDTO(String idBolnice, String ime, String prezime, String jmbg, DateTime datumRodjenja, String mestoRodjenja, String drzavaRodjenja, String pol,
                        String adresa, String email, String telefon, String korisnickoIme, String lozinka, String brojZdravKnjizice, String zanimanje,
                        String bracnoStanje, DateTime pocetakRadnogVremena, DateTime krajRadnogVrmena, String idSpecijalizacije)
        {
            this.prosecnaOcena = 0.0;
            this.jeNaGodisnjemOdmoru = false;
            this.notifikacije = new List<Notifikacija>();
            this.jeLogickiObrisan = false;

            this.idBolnice = idBolnice;
            this.ime = ime;
            this.prezime = prezime;
            this.jmbg = jmbg;
            this.datumRodjenja = datumRodjenja;
            this.mestoRodjenja = mestoRodjenja;
            this.drzavaRodjenja = drzavaRodjenja;
            this.pol = pol;
            this.adresa = adresa;
            this.email = email;
            this.brojTelefona = telefon;
            this.korisnickoIme = korisnickoIme;
            this.lozinka = lozinka;
            this.brojZdravstveneKnjizice = brojZdravstveneKnjizice;
            this.zanimanje = zanimanje;
            this.bracniStatus = bracnoStanje;
            this.pocetakRadnogVremena = pocetakRadnogVremena;
            this.krajRadnogVremena = krajRadnogVremena;
            this.idSpecijalizacije = idSpecijalizacije;
            
        }
    }
}
