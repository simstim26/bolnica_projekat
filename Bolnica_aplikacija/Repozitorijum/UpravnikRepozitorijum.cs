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
    class UpravnikRepozitorijum
    {
        public List<Upravnik> UcitajSve()
        {
            List<Upravnik> sviUpravnici;

            try
            {
                sviUpravnici = JsonSerializer.Deserialize<List<Upravnik>>(File.ReadAllText("Datoteke/Upravnici.txt"));
            }
            catch(Exception e)
            {
                sviUpravnici = new List<Upravnik>();
            }

            return sviUpravnici;
        }

       
    }
}
