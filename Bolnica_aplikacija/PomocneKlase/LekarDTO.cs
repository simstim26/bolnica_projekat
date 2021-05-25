using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.PomocneKlase
{
    class LekarDTO : Lekar
    { 
        public LekarDTO(String idBolnice, String ime, String prezime, String jmbg, DateTime datumRodjenja, String mestoRodjenja, String drzavaRodjenja, String pol,
                        String adresa, String email, String telefon, String korisnickoIme, String lozinka, String brojZdravKnjizice, String zanimanje,
                        String bracnoStanje, DateTime pocetakRadnogVremena, DateTime krajRadnogVremena, String idSpecijalizacije, double ocena, bool jeNaGodisnjemOdmoru,
                        List<Notifikacija> notifikacije, int brojSlobodnihDana, DateTime pocetakGodisnjegOdmora, bool jeLogickiObrisan)
        {
                  
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
            this.brojZdravstveneKnjizice = brojZdravKnjizice;
            this.zanimanje = zanimanje;
            this.bracniStatus = bracnoStanje;
            this.pocetakRadnogVremena = pocetakRadnogVremena;
            this.krajRadnogVremena = krajRadnogVremena;
            this.idSpecijalizacije = idSpecijalizacije;
           
            this.prosecnaOcena = ocena;
            this.jeNaGodisnjemOdmoru = jeNaGodisnjemOdmoru;
            this.notifikacije = notifikacije;
            this.brojSlobodnihDana = brojSlobodnihDana;
            this.pocetakGodisnjegOdmora = pocetakGodisnjegOdmora;
            this.jeLogickiObrisan = jeLogickiObrisan;


        }

        public LekarDTO()
        {

        }
    }
}
