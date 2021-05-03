﻿using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.PacijentModel;
using Bolnica_aplikacija.Servis;
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
            Dictionary<string, string> lekari = new Dictionary<string, string>();
            var prosliTermini = PacijentKontroler.prikazProslihTerminaPacijentaKodOcenjivanjaLekara(idPacijenta);
            foreach (PacijentTermin termin in prosliTermini)
            {
                lekari.Add(termin.id, termin.imeLekara);
            }

            var listaLekara = lekari.Values.Distinct().ToList();

            comboBoxLekar.ItemsSource = listaLekara;
            comboBoxLekar.SelectedIndex = 0;
         
        }

        private void btnOceni_Click(object sender, RoutedEventArgs e)
        {
            int ocena = 0;

            switch(comboBoxOcena.SelectedIndex)
            {
                case 0: ocena = 1; break;
                case 1: ocena = 2; break;
                case 2: ocena = 3; break;
                case 3: ocena = 4; break;
                case 4: ocena = 5; break;
                default: ocena = -1; break;
            }

            int brojOcena = OcenaLekaraKontroler.ucitajSve().Count + 1;

            OcenaLekaraKontroler.dodajOcenu(new Model.OcenaLekara("OL "+brojOcena.ToString(),comboBoxLekar.SelectedItem.ToString(),ocena,txtKomentar.Text));
        }
    }
}