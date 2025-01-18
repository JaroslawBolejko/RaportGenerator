using RaportGenerator.ViewModels;
using System;
using System.Windows;

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

        private void btnClient_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new ClientViewModel();
        }

        private void btnDevice_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new DeviceViewModel();
        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new ReportViewModel();
        }

        private void btnContract_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new ContractViewModel();
        }

        private void btnHistory_Click(object sender, RoutedEventArgs e)
        {
            btnReportHistory.Visibility = btnReportHistory.Visibility == Visibility.Visible
                ? Visibility.Collapsed
                : Visibility.Visible;

            btnContractHistoryOnly.Visibility = btnContractHistoryOnly.Visibility == Visibility.Visible
                ? Visibility.Collapsed
                : Visibility.Visible;
        }

        private void btnContractHistory_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new ContractHistoryViewModel();
        }

        private void btnReportHistory_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new ReportHistoryViewModel();
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
