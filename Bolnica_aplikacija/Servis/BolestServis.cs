﻿using Bolnica_aplikacija.Model;
using Bolnica_aplikacija.Repozitorijum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Servis
{
    class BolestServis
    {
        private BolestRepozitorijum bolestRepozitorijum = new BolestRepozitorijum();

        public void azurirajTerapijuZaBolest(String idBolesti, String idTerapije)
        {
            List<Bolest> bolesti = bolestRepozitorijum.ucitajSve();
            foreach (Bolest bolest in bolesti)
            {
                if (idBolesti.Equals(bolest.id))
                {
                    bolest.idTerapije = idTerapije;
                    break;
                }
            }
            bolestRepozitorijum.upisi(bolesti);
        }
        public String napraviBolest(String nazivBolesti, String idPacijenta, String idTerapije)
        {
            var sveBolesti = bolestRepozitorijum.ucitajSve();
            Bolest bolest = new Bolest();
            bolest.id = (sveBolesti.Count + 1).ToString();
            bolest.naziv = nazivBolesti;
            bolest.idPacijenta = idPacijenta;
            bolest.idTerapije = idTerapije;
            sveBolesti.Add(bolest);
            bolestRepozitorijum.upisi(sveBolesti);

            return bolest.id;
        }

    }
}
