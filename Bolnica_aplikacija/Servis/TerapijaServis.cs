using Bolnica_aplikacija.Repozitorijum; 
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Servis
{
    class TerapijaServis
    {
        private TerapijaRepozitorijum terapijaRepozitorijum = new TerapijaRepozitorijum();
        public void azurirajTerapiju(String idTerapije, String idLeka, String nacinUpotrebe, int trajanje
            , DateTime datumPropisivanja)
        {
            Terapija terapija = nadjiTerapijuPoId(idTerapije);
            terapija.idLeka = idLeka;
            terapija.nacinUpotrebe = nacinUpotrebe;
            terapija.trajanje = trajanje;
            terapija.datumPocetka = datumPropisivanja;
            terapijaRepozitorijum.azurirajTerapiju(terapija);
        }

        public Terapija nadjiTerapijuPoId(String idTerapije)
        {
            Terapija povratnaVrednost = new Terapija();

            foreach(Terapija terapija in terapijaRepozitorijum.ucitajSve())
            {
                if (idTerapije.Equals(terapija.id))
                {
                    povratnaVrednost.id = terapija.id;
                    povratnaVrednost.idBolesti = terapija.idBolesti;
                    povratnaVrednost.idPacijenta = terapija.idPacijenta;
                    povratnaVrednost.idLeka = terapija.idLeka;
                    povratnaVrednost.idTermina = terapija.idTermina;
                    povratnaVrednost.nacinUpotrebe = terapija.nacinUpotrebe;
                    povratnaVrednost.trajanje = terapija.trajanje;
                    povratnaVrednost.datumPocetka = terapija.datumPocetka;
                }
            }

            return povratnaVrednost;
        }

        public void dodajTerapiju(Terapija terapija)
        {
            List<Terapija> sveTerapije = terapijaRepozitorijum.ucitajSve();
            terapija.id = (sveTerapije.Count + 1).ToString();

            sveTerapije.Add(terapija);
            terapijaRepozitorijum.upisi(sveTerapije);
        }
        public Terapija nadjiTerapijuZaTermin(String idTermina)
        {
            foreach(Terapija t in terapijaRepozitorijum.ucitajSve())
            {
                if (t.idTermina != null && t.idTermina.Equals(idTermina))
                {
                    return t;
                }
            }

            return null;
        }

        public void dodajIdBolestiZaTerapiju(String idTerapije, String idBolesti)
        {
            foreach(Terapija t in terapijaRepozitorijum.ucitajSve())
            {
                if (idTerapije.Equals(t.id))
                {
                    t.idBolesti = idBolesti;
                    terapijaRepozitorijum.azurirajTerapiju(t);
                }
            }
        }
    }
}
