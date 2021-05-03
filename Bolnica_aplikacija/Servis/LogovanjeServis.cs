using Bolnica_aplikacija.PomocneKlase;
using Bolnica_aplikacija.Repozitorijum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Servis
{
    class LogovanjeServis
    {
        private static LogovanjeServis instance;
        private static LogovanjeRepozitorijum logovanjeRepozitorijum = new LogovanjeRepozitorijum();

        public static LogovanjeServis getInstance()
        {
            if(instance == null)
            {
                instance = new LogovanjeServis();
            }

            return instance;
        }

        public List<Logovanje> ucitajSve()
        {
            return logovanjeRepozitorijum.ucitajSve();
        }

        public void dodajLogovanje(Logovanje logovanje)
        {
            logovanjeRepozitorijum.dodajLogovanje(logovanje);
        }

        public bool proveriPostojanjeLogovanja(String idKorisnika)
        {
            foreach(Logovanje logovanje in ucitajSve())
            {
                if(logovanje.idKorisnika.Equals(idKorisnika))
                {
                    return true;
                }
            }

            return false;
        }

        public bool proveriVremePostojecegLogovanja(String idKorisnika)
        {
            bool povratnaVrednost = false;
            foreach(Logovanje logovanje in ucitajSve())
            {
                if(logovanje.idKorisnika.Equals(idKorisnika))
                {
                    DateTime vreme = DateTime.Now;

                    int rezultatPoredjenja = DateTime.Compare(vreme, logovanje.vremeIzmene.AddHours(1));

                    if (rezultatPoredjenja < 0)
                    {
                        povratnaVrednost = false;
                        Console.WriteLine("manji je");
                    }
                    else if (rezultatPoredjenja == 0)
                    {
                        povratnaVrednost = false;
                        Console.WriteLine("jednako");
                    }
                    else
                    {
                        povratnaVrednost = true;
                        Console.WriteLine("WUHU");
                    }
                }
            }

            return povratnaVrednost;
        }

        public void resetujLogovanje(String idKorisnika)
        {
            var svaLogovanja = ucitajSve();
            foreach (Logovanje logovanje in svaLogovanja)
            {
                if (logovanje.idKorisnika.Equals(idKorisnika))
                {
                    logovanje.brojUzastopnihIzmena = 1;
                    logovanje.vremeIzmene = DateTime.Now;
                    break;
                }
            }
            logovanjeRepozitorijum.upisi(svaLogovanja);
        }

        public void uvecajBrojIzmena(String idKorisnika)
        {
            var svaLogovanja = ucitajSve();
            foreach (Logovanje logovanje in svaLogovanja)
            {
                if (logovanje.idKorisnika.Equals(idKorisnika))
                {
                    logovanje.brojUzastopnihIzmena = logovanje.brojUzastopnihIzmena + 1;
                    break;
                }
            }
            logovanjeRepozitorijum.upisi(svaLogovanja);
        }

        public int getBrojUzastopnihIzmena(String idKorisnika)
        {
            var svaLogovanja = ucitajSve();
            foreach(Logovanje logovanje in svaLogovanja)
            {
                if(logovanje.idKorisnika.Equals(idKorisnika))
                {
                    return logovanje.brojUzastopnihIzmena;
                }
            }

            return -1;
        }

        public DateTime getVremeIzmene(String idKorisnika)
        {
            var svaLogovanja = ucitajSve();
            foreach(Logovanje logovanje in svaLogovanja)
            {
                if(logovanje.idKorisnika.Equals(idKorisnika))
                {
                    return logovanje.vremeIzmene;
                }
            }

            return new DateTime();
        }

    }
}
