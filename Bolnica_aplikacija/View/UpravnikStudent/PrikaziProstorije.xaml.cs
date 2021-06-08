using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.PomocneKlase;
using Model;
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

namespace Bolnica_aplikacija.View.UpravnikStudent
{
    /// <summary>
    /// Interaction logic for PrikaziProstorije.xaml
    /// </summary>
    public partial class PrikaziProstorije : UserControl
    {
        public static DataGrid dataGridInventarProstorije;
        public PrikaziProstorije()
        {
            InitializeComponent();
            var stavka = (Stavka)InventarPogled.dobaviDataGridInventar().SelectedItem;
            var prostorijeTreba = new List<ProstorijaKolicina>();
            var prostorije = ProstorijaKontroler.ucitajNeobrisane();
            var kolicina = new List<int>();


            foreach (Prostorija p in prostorije)
            {
                if (p.Stavka != null)
                {
                    foreach (Stavka s in p.Stavka)
                    {

                        if (s.id == stavka.id)
                        {
                            ProstorijaKolicina prostorija = new ProstorijaKolicina();
                            prostorija.broj = p.broj;
                            prostorija.sprat = p.sprat;
                            prostorija.kolicina = s.kolicina;
                            prostorijeTreba.Add(prostorija);
                        }
                    }
                }
            }

            textBoxNazivStavkePoProstorijama.Text = stavka.naziv;
            textBoxProizvodjacStavkePoProstorijama.Text = stavka.proizvodjac;
            textBoxKolicinaStavkePoProstorijama.Text = stavka.kolicina.ToString();



            dataGridStavkaUProstorijama.ItemsSource = prostorijeTreba;
        }

        private void btnOtkaziPrikazNeki_Click(object sender, RoutedEventArgs e)
        {
            GlavniProzor.DobaviProzorZaIzmenu().Children.Clear();
            GlavniProzor.DobaviProzorZaIzmenu().Children.Add(new InventarPogled());
        }
    }
}
