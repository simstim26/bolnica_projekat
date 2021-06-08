using Bolnica_aplikacija.Kontroler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bolnica_aplikacija.View.LekarStudent
{
    /// <summary>
    /// Interaction logic for ProsliTermini.xaml
    /// </summary>
    public partial class ProsliTermini : UserControl
    {
        public static bool aktivan;
        private static FrameworkElement fm = new FrameworkElement();
        public ProsliTermini(String idTermina)
        {
            InitializeComponent();
            LekarProzor.getGlavnaLabela().Content = "Izveštaj sa termina";
            fm.DataContext = idTermina;
            PacijentInfo.aktivanPacijentInfo = false;
            aktivan = true;

            lblJmbg.Content = TerminKontroler.nadjiPacijentaZaTermin(idTermina).jmbg;
            lblImePrezime.Content = TerminKontroler.nadjiPacijentaZaTermin(idTermina).ime + " " + TerminKontroler.nadjiPacijentaZaTermin(idTermina).prezime;
            lblDatumRodjenja.Content = TerminKontroler.nadjiPacijentaZaTermin(idTermina).datumRodjenja.ToString("dd.MM.yyyy.");

            lblLekar.Content = LekarKontroler.nadjiLekaraPoId(TerminKontroler.nadjiTerminPoId(idTermina).idLekara).ime + " " + LekarKontroler.nadjiLekaraPoId(TerminKontroler.nadjiTerminPoId(idTermina).idLekara).prezime + ", " + SpecijalizacijaKontroler.nadjiSpecijalizacijuPoId(LekarKontroler.nadjiLekaraPoId(TerminKontroler.nadjiTerminPoId(idTermina).idLekara).idSpecijalizacije);

            txtDijagnoza.Text = dijagnoza();
            txtTerapija.Text = terapija();
            txtIzvestaj.Text = izvestaj();
            txtUputLekar.Text = lekarUput();
        }

        public static FrameworkElement getFM()
        {
            return fm;
        }

        private String izvestaj()
        {
            String povratnaVrednost = "";

            if(String.IsNullOrWhiteSpace(TerminKontroler.nadjiTerminPoId((String)fm.DataContext).izvestaj))
            {
                povratnaVrednost = "Nema izveštaja sa termina.\n";
            }
            else
            {
                povratnaVrednost = TerminKontroler.nadjiTerminPoId((String)fm.DataContext).izvestaj;

                if (!String.IsNullOrWhiteSpace(TerminKontroler.nadjiTerminPoId((String)fm.DataContext).idUputLekara))
                {
                    povratnaVrednost += "\n\nIzveštaj sa uputa:\n";
                    if (String.IsNullOrWhiteSpace(TerminKontroler.nadjiTerminPoId((String)fm.DataContext).izvestajUputa))
                    {
                        povratnaVrednost += "Nema podataka\n";
                    }
                    else
                    {
                        povratnaVrednost += TerminKontroler.nadjiTerminPoId((String)fm.DataContext).izvestajUputa;
                    }
                }


                if (!String.IsNullOrWhiteSpace(TerminKontroler.nadjiTerminPoId((String)fm.DataContext).idTerapije))
                {
                    povratnaVrednost += "\n\nRecept (način upotrebe):\n";

                    if (String.IsNullOrWhiteSpace(TerapijaKontroler.nadjiTerapijuPoId(TerminKontroler.nadjiTerminPoId((String)fm.DataContext).idTerapije).nacinUpotrebe))
                    {
                        povratnaVrednost += "Nema podataka\n";
                    }
                    else
                    {
                        povratnaVrednost += TerapijaKontroler.nadjiTerapijuPoId(TerminKontroler.nadjiTerminPoId((String)fm.DataContext).idTerapije).nacinUpotrebe;
                    }
                }
                else
                {
                    povratnaVrednost += "Nema načina upotrebe\n";
                }
            }

            return povratnaVrednost;
        }

        private String lekarUput()
        {
            String povratnaVrednost;
            if (String.IsNullOrWhiteSpace(TerminKontroler.nadjiTerminPoId((String)fm.DataContext).idUputLekara))
            {
                povratnaVrednost = "Nema podataka\n";
            }
            else
            {
                povratnaVrednost = LekarKontroler.nadjiLekaraPoId(TerminKontroler.nadjiTerminPoId((String)fm.DataContext).idUputLekara).ime + " " + LekarKontroler.nadjiLekaraPoId(TerminKontroler.nadjiTerminPoId((String)fm.DataContext).idUputLekara).prezime + ", " + SpecijalizacijaKontroler.nadjiSpecijalizacijuPoId(LekarKontroler.nadjiLekaraPoId(TerminKontroler.nadjiTerminPoId((String)fm.DataContext).idUputLekara).idSpecijalizacije);

            }

            return povratnaVrednost;
        }

        private String dijagnoza()
        {
            String povratnaVrednost;
            if (String.IsNullOrWhiteSpace(TerminKontroler.nadjiTerminPoId((String)fm.DataContext).idBolesti))
            {
                povratnaVrednost = "Nema podataka\n";
            }
            else
            {
                povratnaVrednost = BolestKontroler.nadjiBolestPoId(TerminKontroler.nadjiTerminPoId((String)fm.DataContext).idBolesti).naziv;
            }

            return povratnaVrednost;
        }

        private String terapija()
        {
            String povratnaVrednost;
            if (String.IsNullOrWhiteSpace(TerminKontroler.nadjiTerminPoId((String)fm.DataContext).idTerapije))
            {
                povratnaVrednost = "Nema podataka\n";
            }
            else
            {
                povratnaVrednost = LekKontroler.nadjiLekPoId(TerapijaKontroler.nadjiTerapijuPoId((TerminKontroler.nadjiTerminPoId((String)fm.DataContext).idTerapije)).idLeka).naziv; 
            }

            return povratnaVrednost;
        }
    }
}
