﻿using Bolnica_aplikacija.Kontroler;
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

namespace Bolnica_aplikacija.PacijentStudent
{
    /// <summary>
    /// Interaction logic for NotifikacijaProzor.xaml
    /// </summary>
    public partial class NotifikacijaProzor : Window
    {
        
        private String idNotifikacije;

        public NotifikacijaProzor(String idNotifikacije)
        {
            InitializeComponent();

            this.idNotifikacije = idNotifikacije;

            popuniNotifikaciju();
            
        }

        void popuniNotifikaciju()
        {
            lblNazivObavestenja.Content = NotifikacijaKontroler.getNotifikacija(idNotifikacije).nazivNotifikacije;
            lblVremeObavestenja.Content = NotifikacijaKontroler.getNotifikacija(idNotifikacije).vremeNotifikovanja.ToString("HH:mm");
            txtPoruka.Text = NotifikacijaKontroler.getNotifikacija(idNotifikacije).porukaNotifikacije;
        }

        private void btnPotvrda_Click(object sender, RoutedEventArgs e)
        {
            if(checkBoxProcitano.IsChecked == true)
            {
                NotifikacijaKontroler.procitajNotifikaciju(idNotifikacije);
            }
        }
    }
}
