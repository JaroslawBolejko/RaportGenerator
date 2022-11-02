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

namespace RaportGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void generateRaport_Click(object sender, RoutedEventArgs e)
        {
            GenerateRaport generateRaportWindow = new GenerateRaport();
            generateRaportWindow.Show();
            this.Close();
        }

        private void raportHistory_Click(object sender, RoutedEventArgs e)
        {
            RaportsHistory raportsHistoryWindow = new RaportsHistory();
            raportsHistoryWindow.Show();
            this.Close();
        }
    }
}
