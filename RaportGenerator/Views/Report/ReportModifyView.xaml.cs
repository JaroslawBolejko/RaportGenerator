using RaportGenerator.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace RaportGenerator.Views.Report
{
    /// <summary>
    /// Interaction logic for ReportModifyView.xaml
    /// </summary>
    public partial class ReportModifyView : Window
    {
        public ReportModifyView()
        {
            InitializeComponent();
        }

        public ReportModel report;
        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            setMonthItemSource();
            cmbReportMonth.SelectedIndex = getInitialMonthIndex(report.ReportForMonth);
            txtClientName.Text = report.CompanyName;
            txtRaportNumber.Text = report.ReportNumber;
            txtDeviceName.Text = report.DeviceName;
            txtDeviceSerialNumber.Text = report.DeviceSerialNumber;
            txtContractNumber.Text = report.ContractNumber;
            txtPrevStateBlack.Text = report.PrevPrintsStateBlack.ToString();
            txtPrevStateColor.Text = report.PrevPrintsStateColor.ToString();
            txtCurrentStateBlack.Text = report.CurrentPrintsStateBlack.ToString();
            txtCurrentStateColor.Text = report.CurrentPrintsStateColor.ToString();
            txtCurrentPrintCounterBlack.Text = report.BlackPrintsAmount.ToString();
            txtCurrentPrintCounterColor.Text = report.ColorPrintsAmount.ToString();
            txtRaportOwner.Text = "Marek Jary";// docelowo admin
            dpDateOfService.DataContext = new { ReportModel = new ReportModel { ReportDocumentDate = report.ReportDocumentDate } };
        }

        public void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
        }

        private void setMonthItemSource()
        {
            cmbReportMonth.ItemsSource = new CultureInfo("pl-PL").DateTimeFormat.MonthNames
          .Where(m => !string.IsNullOrEmpty(m))
          .ToList();
        }

        private int getInitialMonthIndex(string monthName)
        {
            int monthIndex = monthName switch
            {
                "styczeń" => 0,
                "luty" => 1,
                "marzec" => 2,
                "kwiecień" => 3,
                "maj" => 4,
                "czerwiec" => 5,
                "lipiec" => 6,
                "sierpień" => 7,
                "wrzesień" => 8,
                "październik" => 9,
                "listopad" => 10,
                "grudzień" => 11,
                _ => throw new ArgumentOutOfRangeException("Podany miesiąc nie istnieje!"),
            };

            return monthIndex;
        }
    }
}
