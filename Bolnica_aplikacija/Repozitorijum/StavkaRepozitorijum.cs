using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Repozitorijum
{
    class StavkaRepozitorijum
    {
        public List<Stavka> UcitajSve()
        {
            List<Stavka> sveStavke;
            try
            {
                sveStavke = JsonSerializer.Deserialize<List<Stavka>>(File.ReadAllText("Datoteke/Stavke.txt"));
            }
            catch (Exception e)
            {
                sveStavke = new List<Stavka>();
            }

            return sveStavke;
        }

        public List<Stavka> UcitajNeobrisaneStavke()
        {
            var sveStavke = UcitajSve();
            List<Stavka> neobrisaneStavke = new List<Stavka>();

            foreach(Stavka stavka in sveStavke)
            {
                if (stavka.jeLogickiObrisana == false)
                {
                    neobrisaneStavke.Add(stavka);
                }
            }

            return neobrisaneStavke;
        }

        public void Upisi(List<Stavka> sveStavke)
        {
            var formatiranje = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            string jsonString = JsonSerializer.Serialize(sveStavke, formatiranje);
            File.WriteAllText("Datoteke/Stavke.txt", jsonString);
        }

        public List<Stavka> ucitajStatickeStavke()
        {
            var sveStavke = UcitajNeobrisaneStavke();
            List<Stavka> statickeStavke = new List<Stavka>();

            foreach(Stavka s in sveStavke)
            {
                if (s.jeStaticka)
                {
                    statickeStavke.Add(s);
                }
            }
            return statickeStavke;
        }

        public List<Stavka> ucitajDinamickeStavke()
        {
            var sveStavke = UcitajNeobrisaneStavke();
            List<Stavka> dinamickeStavke = new List<Stavka>();

            foreach (Stavka s in sveStavke)
            {
                if (!s.jeStaticka)
                {
                    dinamickeStavke.Add(s);
                }
            }
            return dinamickeStavke;
        }
    }
}
