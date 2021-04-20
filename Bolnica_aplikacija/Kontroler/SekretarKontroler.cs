using Bolnica_aplikacija.Model;
using Bolnica_aplikacija.Servis;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Bolnica_aplikacija.Kontroler
{
    class SekretarKontroler
    {
        public static void NapraviPacijenta(String idBolnice, bool gost, String korisnickoIme, String lozinka, String jmbg, String ime, String prezime, DateTime datumRodj, string adresa, string email, string telefon, List<Alergija> alergije)
        {
            SekretarServis.NapraviPacijenta(idBolnice, gost, korisnickoIme, lozinka, jmbg, ime, prezime, datumRodj, adresa, email, telefon, alergije);
        }

        public static List<Pacijent> ProcitajPacijente()
        {
            return SekretarServis.ProcitajPacijente();
        }

        public static void AzurirajPacijenta(String id, String idBolnice, bool gost, String korisnickoIme, String lozinka, String jmbg, String ime, String prezime, DateTime datumRodj, string adresa, string email, string telefon, List<Alergija> alergije)
        {
            SekretarServis.AzurirajPacijenta(id, idBolnice, gost, korisnickoIme, lozinka, jmbg, ime, prezime, datumRodj, adresa, email, telefon, alergije);
        }

        public static void ObrisiPacijenta(String idPacijenta)
        {
            SekretarServis.ObrisiPacijenta(idPacijenta);
        }
    }
}
