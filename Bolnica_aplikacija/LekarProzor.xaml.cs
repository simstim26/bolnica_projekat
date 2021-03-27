﻿using System;
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
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace Bolnica_aplikacija
{
    /// <summary>
    /// Interaction logic for LekarProzor.xaml
    /// </summary>
    public partial class LekarProzor : Window
    {
        private static ContentControl x;
        private Model.Lekar lekar;

        public LekarProzor(Model.Lekar lekar)
        {
            InitializeComponent();
            this.contentControl.Content = new LekarTabovi();
            x = this.contentControl;
            lblImePrezime.Content = lekar.ime + " " + lekar.prezime;
            lblprosecnaOcena.Content += " " + lekar.prosecnaOcena;
            this.lekar = lekar;
            Console.WriteLine(lekar.id);
        }

        public static ContentControl getX()
        {
            return x;
        }

        private void meniOdjava_Click(object sender, RoutedEventArgs e)
        {
            Prijava prijava = new Prijava();
            string jsonString = JsonSerializer.Serialize(lekar);
            File.WriteAllText("Datoteke/proba.txt", jsonString);
            this.Close();
            prijava.ShowDialog();
        }
    }
}
