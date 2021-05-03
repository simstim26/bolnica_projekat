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
using System.Windows.Shapes;

namespace Bolnica_aplikacija.View.PacijentStudent
{
    /// <summary>
    /// Interaction logic for OceniteLekara.xaml
    /// </summary>
    public partial class OceniteLekara : Window
    {
        private String idPacijenta;
        public OceniteLekara(String idPacijenta)
        {
            InitializeComponent();
            this.idPacijenta = idPacijenta;
            popuniPolja();

        }

        private void popuniPolja()
        {
            popuniOcenu();
            popuniLekara();
        }

        private void popuniOcenu()
        {
            comboBoxOcena.Items.Add("1 (nedovoljan)");
            comboBoxOcena.Items.Add("2 (dovoljan)");
            comboBoxOcena.Items.Add("3 (dobar)");
            comboBoxOcena.Items.Add("4 (vrlo dobar)");
            comboBoxOcena.Items.Add("5 (odlican)");
            comboBoxOcena.SelectedIndex = 0;
        }

        private void popuniLekara()
        {
            String[] lekari = OcenaLekaraKontroler.pronadjiImenaLekara(idPacijenta).Split('|');

            lekari = lekari.Distinct().ToArray();

            for (int i = 0; i < lekari.Length; i++)
                comboBoxLekar.Items.Add(lekari[i]);

            comboBoxLekar.SelectedIndex = 0;

        }

    }
}
