using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Model
{
    public class LekZaOdobravanje : Lek
    {
        private static LekZaOdobravanje instance = null;

        public static LekZaOdobravanje getInstance()
        {
            if (instance == null)
            {
                instance = new LekZaOdobravanje();
            }

            return instance;
        }

        public int brLekaraKojiSuodobriliLek { get; set; }
        public List<String> lekariKojimaJePoslatLek { get; set; }
        public bool odobren { get; set; }

        public String propratnaPoruka { get; set; }

        public LekZaOdobravanje()
        {

        }

        public LekZaOdobravanje(String naziv, TipLeka tipLeka, int kolicina, String proizvodjac, NacinUpotrebe nacinUpotrebe)
        {
            this.naziv = naziv;
            this.tip = tipLeka;
            this.kolicina = kolicina;
            this.proizvodjac = proizvodjac;
            this.nacinUpotrebe = nacinUpotrebe;
            this.brLekaraKojiSuodobriliLek = 0;
            this.odobren = false;
        }

        public void kopiraj(LekZaOdobravanje lekZaOdobravanje)
        {
            id = lekZaOdobravanje.id;
            naziv = lekZaOdobravanje.naziv;
            tip = lekZaOdobravanje.tip;
            kolicina = lekZaOdobravanje.kolicina;
            proizvodjac = lekZaOdobravanje.proizvodjac;
            nacinUpotrebe = lekZaOdobravanje.nacinUpotrebe;
            brLekaraKojiSuodobriliLek = lekZaOdobravanje.brLekaraKojiSuodobriliLek;
            odobren = lekZaOdobravanje.odobren;
            sastojci = lekZaOdobravanje.sastojci;
            zamenskiLekovi = lekZaOdobravanje.zamenskiLekovi;
            lekariKojimaJePoslatLek = lekZaOdobravanje.lekariKojimaJePoslatLek;
            propratnaPoruka = lekZaOdobravanje.propratnaPoruka;
        }
    }
}
