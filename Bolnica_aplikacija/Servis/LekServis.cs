using Bolnica_aplikacija.Model;
using Bolnica_aplikacija.Repozitorijum;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Servis
{
    class LekServis
    {
        private static LekServis instance;
        public static LekServis getInstance()
        {
            if (instance == null)
            {
                instance = new LekServis();
            }

            return instance;
        }
        private LekRepozitorijum lekRepozitorijum = new LekRepozitorijum();


        public List<Lek> ucitajSveSemTrenutnogNaTerapiji(String idLeka)
        {
            List<Lek> povratnaVrednost = new List<Lek>();

            if (idLeka == null)
            {
                return lekRepozitorijum.ucitajSve();
            }

            foreach (Lek lek in ucitajSve())
            {
                if (!idLeka.Equals(lek.id))
                {
                    povratnaVrednost.Add(lek);
                }
            }

            return povratnaVrednost;
        }
        public Lek nadjiLekPoId(String idLeka)
        {
            Lek povratnaVrednost = new Lek();

            foreach (Lek lek in ucitajSve())
            {
                if (idLeka != null && idLeka.Equals(lek.id))
                {
                    povratnaVrednost.id = lek.id;
                    povratnaVrednost.kolicina = lek.kolicina;
                    povratnaVrednost.nacinUpotrebe = lek.nacinUpotrebe;
                    povratnaVrednost.naziv = lek.naziv;
                    povratnaVrednost.proizvodjac = lek.proizvodjac;
                    povratnaVrednost.tip = lek.tip;

                    break;
                }
            }

            return povratnaVrednost;
        }

        public List<Lek> ucitajSve()
        {
            return lekRepozitorijum.ucitajSve();
        }

        public List<NacinUpotrebe> nacinUpotrebeLeka()
        {
            List<NacinUpotrebe> nacinUpotrebe = Enum.GetValues(typeof(NacinUpotrebe)).Cast<NacinUpotrebe>().ToList();
            return nacinUpotrebe;
        }

        public List<TipLeka> tipLeka()
        {
            List<TipLeka> tipLeka = Enum.GetValues(typeof(TipLeka)).Cast<TipLeka>().ToList();
            return tipLeka;
        }

        public void napraviLek(LekZaOdobravanje lek)
        {
            var lekoviZaOdobravanje = lekRepozitorijum.ucitajLekoveZaOdobravanje();
            lekoviZaOdobravanje.Add(lek);
            lekRepozitorijum.upisiLekoveZaObradu(lekoviZaOdobravanje);
        }

        public void dodajLekuLekare(List<String> idLekari, LekZaOdobravanje lek)
        {
            lek.lekariKojimaJePoslatLek = idLekari;
        }
    }
}
