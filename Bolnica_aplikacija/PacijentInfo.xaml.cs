using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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
using Model;

namespace Bolnica_aplikacija
{
    /// <summary>
    /// Interaction logic for PacijentInfo.xaml
    /// </summary>
    public partial class PacijentInfo : UserControl
    {
        public static TabControl tab;
        public PacijentInfo()
        {
            InitializeComponent();
            tab = this.tabInfo;
            lblIme.Content += PrikazPacijenata.GetPacijent().ime;
            ucitajPodatke();
        }

        private void btnNazad_Click(object sender, RoutedEventArgs e)
        {
            Content = new PrikazPacijenata();
        }

        private void btnZakazi_Click(object sender, RoutedEventArgs e)
        {
            LekarProzor.getX().Content = new ZakaziTermin();
        }

        public static TabControl getPregledTab()
        {
            return tab;
        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridTerminiPacijenta.SelectedIndex != -1)
            {
                Termin izabraniTermin = (Termin)dataGridTerminiPacijenta.SelectedItem;
                var sviTermini = JsonSerializer.Deserialize<List<Termin>>(File.ReadAllText("Datoteke/probaTermini.txt"));
                foreach (Termin termin in sviTermini)
                {
                    if (izabraniTermin.idTermina.Equals(termin.idTermina))
                    {
                        termin.idPacijenta = "";
                    }
                }
                string jsonString = JsonSerializer.Serialize(sviTermini);
                File.WriteAllText("Datoteke/probaTermini.txt", jsonString);
            }
            ucitajPodatke();
        }

        private void ucitajPodatke()
        {
            var sviTermini = JsonSerializer.Deserialize<List<Termin>>(File.ReadAllText("Datoteke/probaTermini.txt"));
            List<Termin> terminiPacijenta = new List<Termin>();
            foreach (Termin termin in sviTermini)
            {
                if (termin.idPacijenta.Equals(PrikazPacijenata.GetPacijent().id))
                {
                    terminiPacijenta.Add(termin);
                }
            }
            dataGridTerminiPacijenta.ItemsSource = terminiPacijenta;
        }
    }
}
