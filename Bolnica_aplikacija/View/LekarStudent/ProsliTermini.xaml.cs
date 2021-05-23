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
        public ProsliTermini(String idTermina)
        {
            InitializeComponent();

            lblJmbg.Content = TerminKontroler.nadjiPacijentaZaTermin(idTermina).jmbg;
            lblImePrezime.Content = TerminKontroler.nadjiPacijentaZaTermin(idTermina).ime + " " + TerminKontroler.nadjiPacijentaZaTermin(idTermina).prezime;
            lblDatumRodjenja.Content = TerminKontroler.nadjiPacijentaZaTermin(idTermina).datumRodjenja.ToString("dd.MM.yyyy.");

            lblLekar.Content = LekarKontroler.nadjiLekaraPoId(TerminKontroler.nadjiTerminPoId(idTermina).idLekara).ime + " " + LekarKontroler.nadjiLekaraPoId(TerminKontroler.nadjiTerminPoId(idTermina).idLekara).prezime + ", " + SpecijalizacijaKontroler.nadjiSpecijalizacijuPoId(LekarKontroler.nadjiLekaraPoId(TerminKontroler.nadjiTerminPoId(idTermina).idLekara).idSpecijalizacije);

            txtDijagnoza.Text = BolestKontroler.nadjiBolestPoId(TerminKontroler.nadjiTerminPoId(idTermina).idBolesti).naziv;
            txtTerapija.Text = LekKontroler.nadjiLekPoId(TerapijaKontroler.nadjiTerapijuPoId((TerminKontroler.nadjiTerminPoId(idTermina).idTerapije)).idLeka).naziv;
            txtIzvestaj.Text = TerminKontroler.nadjiTerminPoId(idTermina).izvestaj + "\n\nIzveštaj sa uputa:\n" + TerminKontroler.nadjiTerminPoId(idTermina).izvestajUputa;
            txtUputLekar.Text = LekarKontroler.nadjiLekaraPoId(TerminKontroler.nadjiTerminPoId(idTermina).idUputLekara).ime + " " + LekarKontroler.nadjiLekaraPoId(TerminKontroler.nadjiTerminPoId(idTermina).idUputLekara).prezime + ", " + SpecijalizacijaKontroler.nadjiSpecijalizacijuPoId(LekarKontroler.nadjiLekaraPoId(TerminKontroler.nadjiTerminPoId(idTermina).idUputLekara).idSpecijalizacije);

        }
    }
}
