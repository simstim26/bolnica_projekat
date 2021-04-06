using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.Repozitorijum;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Bolnica_aplikacija.Servis
{
    class ProstorijaServis
    {
     
        ProstorijaRepozitorijum prostorijaRepozitorijum = new ProstorijaRepozitorijum();

        public List<Prostorija> ucitajSve()
        {
            return prostorijaRepozitorijum.ucitajSve();
        }

        public void upisi(List<Prostorija> sveProstorije)
        {
            prostorijaRepozitorijum.upisi(sveProstorije);
        }

        public void NapraviProstoriju(Prostorija prostorija)
        {
            var prostorije = ProstorijaKontroler.ucitajSve(); //.GetInstance().ucitajSve();
           // Prostorija prostorija = new Prostorija();

            Bolnica_aplikacija.UpravnikProzor upravnikProzor = Bolnica_aplikacija.UpravnikProzor.getInstance();

            bool pronadjenBroj = false;
            bool pronadjenSprat = false;
            bool istiBroj = false;

            String pat = @"^[0-9]+$";
            Regex r = new Regex(pat);
            Match m = r.Match(upravnikProzor.unosBrojaProstorije.Text.Replace(" ", ""));
            Match m1 = r.Match(upravnikProzor.unosSprata.Text.Replace(" ", ""));

            /*if (prostorija.broj == upravnikProzor.txtBrojProstorije.Text && prostorija.sprat.ToString() == upravnikProzor.txtSpratProstorije.Text)
            {
                istiBroj = true;
            }*/

            //provera da li broj prostorije vec postoji
            foreach (Prostorija p in prostorije)
            {
                if (p.broj == upravnikProzor.unosBrojaProstorije.Text)
                {
                    pronadjenBroj = true;
                    break;
                }
            }

            foreach (Prostorija p in prostorije)
            {
                if (p.sprat == Int32.Parse(upravnikProzor.unosSprata.Text))
                {
                    pronadjenSprat = true;
                    break;
                }
            }

            if (pronadjenBroj && pronadjenSprat)// && !istiBroj)
            {
                upravnikProzor.lblBrojPostojiDodaj.Visibility = Visibility.Visible;
            }
            else if (String.IsNullOrEmpty(upravnikProzor.unosBrojaProstorije.Text) || String.IsNullOrEmpty(upravnikProzor.unosSprata.Text) || upravnikProzor.cbTipProstorije.SelectedIndex == -1)
            {
                upravnikProzor.lblNijePopunjenoDodaj.Visibility = Visibility.Visible;
            }
            else if (!m.Success || !m1.Success)
            {
                upravnikProzor.lblNijePopunjenoIspravnoDodaj.Visibility = Visibility.Visible;
            }
            else
            {
                //Prostorija prostorija = new Prostorija();
                prostorija.id = (prostorije.Count + 1).ToString();
                prostorija.broj = upravnikProzor.unosBrojaProstorije.Text;
                prostorija.sprat = Int32.Parse(upravnikProzor.unosSprata.Text);
                prostorija.logickiObrisana = false;
                prostorija.dostupnost = true;
                //prostorija.idBolnice = upravnikProzor.upravnik.id;
                if (upravnikProzor.cbTipProstorije.SelectedIndex == 0)
                {
                    prostorija.tipProstorije = TipProstorije.BOLNICKA_SOBA;
                }
                else if (upravnikProzor.cbTipProstorije.SelectedIndex == 1)
                {
                    prostorija.tipProstorije = TipProstorije.OPERACIONA_SALA;
                }
                else if (upravnikProzor.cbTipProstorije.SelectedIndex == 2)
                {
                    prostorija.tipProstorije = TipProstorije.SOBA_ZA_PREGLED;
                }

                prostorije.Add(prostorija);
                //upravnikProzor.prostorijeNeobrisane.Add(prostorija);
                ProstorijaKontroler.upisi(prostorije);
                upravnikProzor.gridDodajProstoriju.Visibility = Visibility.Hidden;
                upravnikProzor.gridProstorija.Visibility = Visibility.Visible;

            }
        }
    }
}
