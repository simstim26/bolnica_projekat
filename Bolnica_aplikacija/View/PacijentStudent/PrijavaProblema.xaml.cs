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
    /// Interaction logic for PrijavaProblema.xaml
    /// </summary>
    public partial class PrijavaProblema : Window
    {
        public PrijavaProblema()
        {
            InitializeComponent();
            CenterWindow();
        }

        private void btnPosalji_Click(object sender, RoutedEventArgs e)
        {
            if (!txtProblem.Text.Equals(""))
            {
                MessageBox.Show("Vaš komentar je uspešno zabeležen!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
                PrijavaGreskeKontroler.sacuvaj(txtProblem.Text);
            }
                
            else
                MessageBox.Show("Molimo unesite Vaš komentar.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);

            this.Close();

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

    }
}
