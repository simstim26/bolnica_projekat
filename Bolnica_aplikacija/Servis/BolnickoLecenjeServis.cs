using Bolnica_aplikacija.Model;
using Bolnica_aplikacija.Repozitorijum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Servis
{
    class BolnickoLecenjeServis
    {
        BolnickoLecenjeRepozitorijum bolnickoLecenjeRepozitorijum = new BolnickoLecenjeRepozitorijum();
        private static BolnickoLecenjeServis instance = null;

        public static BolnickoLecenjeServis getInstance()
        {
            if (instance == null)
            {
                instance = new BolnickoLecenjeServis();
            }

            return instance;
        }

        public void napraviUputZaBolnickoLecenje(BolnickoLecenje bolnickoLecenje)
        {
            List<BolnickoLecenje> sviUputi = bolnickoLecenjeRepozitorijum.ucitajSve();
            bolnickoLecenje.id = (sviUputi.Count + 1).ToString();
            sviUputi.Add(bolnickoLecenje);
            bolnickoLecenjeRepozitorijum.upisi(sviUputi);

        }

        public BolnickoLecenje nadjiBolnickoLecenjeZaPacijenta(String idPacijenta)
        {
            foreach (BolnickoLecenje bolnickoLecenje in bolnickoLecenjeRepozitorijum.ucitajSve())
            {
                if (!bolnickoLecenje.jeZavrsen && bolnickoLecenje.pacijent.id.Equals(idPacijenta))
                {
                    bolnickoLecenje.bolnickaSoba = ProstorijaServis.getInstance().nadjiProstorijuPoId(bolnickoLecenje.bolnickaSoba.id);
                    bolnickoLecenje.pacijent = PacijentServis.getInstance().nadjiPacijenta(bolnickoLecenje.pacijent.id);
                    return bolnickoLecenje;
                }
            }

            return new BolnickoLecenje();
        }

        public bool proveriBolnickoLecenjeZaPacijenta(String idPacijenta)
        {
            return proveriDatum(nadjiBolnickoLecenjeZaPacijenta(idPacijenta).datumPocetka);
        }

        private bool proveriDatum(DateTime datumPocetka)
        {
            return DateTime.Compare(datumPocetka.Date, DateTime.Now.Date) <= 0;
        }

        public void zavrsiBolnickoLecenje(String idPacijenta)
        {
            BolnickoLecenje bolnickoLecenje = nadjiBolnickoLecenjeZaPacijenta(idPacijenta);
            bolnickoLecenje.jeZavrsen = true;
            bolnickoLecenjeRepozitorijum.azurirajBolnickoLecenje(bolnickoLecenje);
        }

        public void azurirajProstoriju(String idPacijenta, String idProstorije)
        {
            BolnickoLecenje bolnickoLecenje = nadjiBolnickoLecenjeZaPacijenta(idPacijenta);
            bolnickoLecenje.bolnickaSoba = ProstorijaServis.getInstance().nadjiProstorijuPoId(idProstorije);
            bolnickoLecenjeRepozitorijum.azurirajBolnickoLecenje(bolnickoLecenje);
        }

        public void azurirajTrajanje(String idPacijenta, int trajanje)
        {
            BolnickoLecenje bolnickoLecenje = nadjiBolnickoLecenjeZaPacijenta(idPacijenta);
            bolnickoLecenje.trajanje = trajanje;
            bolnickoLecenjeRepozitorijum.azurirajBolnickoLecenje(bolnickoLecenje);
        }

        public bool proveriKrajBolnickogLecenje(String idPacijenta)
        {
            BolnickoLecenje bolnickoLecenje = nadjiBolnickoLecenjeZaPacijenta(idPacijenta);
            return proveriKrajnjiDatum(bolnickoLecenje.datumPocetka.AddDays(bolnickoLecenje.trajanje));
        }

        private bool proveriKrajnjiDatum(DateTime datumKraja)
        {
            return DateTime.Compare(datumKraja.Date, DateTime.Now.Date) == 0;
        }

    }
}
