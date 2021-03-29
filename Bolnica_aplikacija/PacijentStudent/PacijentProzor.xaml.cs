using Bolnica_aplikacija.PacijentModel;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace Bolnica_aplikacija.PacijentStudent
{
    /// <summary>
    /// Interaction logic for PacijentProzor.xaml
    /// </summary>
    public partial class PacijentProzor : Window
    {

        private String idPacijenta;

        public PacijentProzor(String id)
        {
            InitializeComponent();
            this.idPacijenta = id;


            this.Height = (System.Windows.SystemParameters.PrimaryScreenHeight * 0.92);
            this.Width = (System.Windows.SystemParameters.PrimaryScreenWidth * 0.75);
            this.MinHeight = (System.Windows.SystemParameters.PrimaryScreenHeight * 0.92);
            this.MinWidth = (System.Windows.SystemParameters.PrimaryScreenWidth * 0.75);
            WindowState = WindowState.Maximized;

            this.DataContext = this;

            CenterWindow();

            dataGridTermin.Loaded += SetMinWidths;
            dataGridTermin.Height = System.Windows.SystemParameters.PrimaryScreenHeight - 300;

            Pacijent.ProcitajTermin(dataGridTermin, this.idPacijenta);

        }

        private void CenterWindow()
        {

            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);

        }

        public void SetMinWidths(object source, EventArgs e)
        {
            foreach (var column in dataGridTermin.Columns)
            {
                column.MinWidth = column.ActualWidth;
                column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
        }

        private void btnOdjava_Click(object sender, RoutedEventArgs e)
        {
            Prijava prijava = new Prijava();
            this.Close();
            prijava.ShowDialog();
        }

        private void btnZakaziPregled_Click_1(object sender, RoutedEventArgs e)
        {
            PacijentZakaziTermin zakaziTermin = new PacijentZakaziTermin(dataGridTermin, this.idPacijenta);
            zakaziTermin.Owner = this;
            zakaziTermin.ShowDialog();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double newHeight = e.NewSize.Height;
            dataGridTermin.Height = newHeight - 300;
        }

        private void btnOtkaziPregled_Click(object sender, RoutedEventArgs e)
        {
            PotvrdaProzor pprozor = new PotvrdaProzor();
            pprozor.Owner = this;
            pprozor.ShowDialog();

            if (pprozor.GetPovratnaVrednost() == 1)
                if (dataGridTermin.SelectedIndex != -1)
                {
                    Pacijent.ObrisiTermin(dataGridTermin, this.idPacijenta);
                }
            
        }

        private void btnIzmeniTermin_Click(object sender, RoutedEventArgs e)
        {
            if(dataGridTermin.SelectedIndex != -1)
            {
                //TO DO: izmena termina
                IzmenaTerminaPacijent izmenaTermina = new IzmenaTerminaPacijent(dataGridTermin, this.idPacijenta);
                izmenaTermina.Owner = this;
                izmenaTermina.ShowDialog();

            }
        }
    }
}
