using Microsoft.EntityFrameworkCore;
using RaportGenerator.DataAccess;
using RaportGenerator.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace RaportGenerator.Views
{
    /// <summary>
    /// Interaction logic for RaportDetailsView.xaml
    /// </summary>
    public partial class ReportDetailsView : Window
    {
        private int _prevStateBlack;
        private int _prevStateColor;
        private int _currentStateBlack;
        private int _currentStateColor;
        public ReportDetailsView()
        {
            InitializeComponent();
        }

        public ReportModel CurrentReport { get; set; }
        public List<ReportModel> AllReports { get; set; }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            setPrintCaounters();

            if (CurrentReport != null && CurrentReport.Id != 0)
            {
                txtReportMonth.Text = CurrentReport.ReportForMonth;
                txtClientName.Text = CurrentReport.CompanyName;
                txtRaportNumber.Text = CurrentReport.ReportNumber;
                txtDeviceName.Text = CurrentReport.DeviceName;
                txtDeviceSerialNumber.Text = CurrentReport.DeviceSerialNumber;
                txtContractNumber.Text = getContractNumber();
                txtPrevStateBlack.Text = _prevStateBlack.ToString();
                txtPrevStateColor.Text = _prevStateColor.ToString();
                txtCurrentStateBlack.Text = _currentStateBlack.ToString();
                txtCurrentStateColor.Text = _currentStateColor.ToString();
                txtCurrentPrintCounterBlack.Text = getCurentPrintCount(_prevStateBlack, _currentStateBlack);
                txtCurrentPrintCounterColor.Text = getCurentPrintCount(_prevStateColor, _currentStateColor);
                txtRaportOwner.Text = "Marek Jary";// docelowo admin
                txtDateOfService.Text = CurrentReport.ReportDocumentDate;
            }
        }

        private (int blackPrints, int colorPrints) getPreviousPrintCount()
        {
            if (CurrentReport.Id > 1)
            {
                //Tu jest bug do nuprawy trzeba z devica sciagąć
                //var preLastReport = AllReports.SkipLast(1).Last();
                return (CurrentReport.PrevPrintsStateBlack, CurrentReport.PrevPrintsStateColor);
            }
            else
            {
                return (0, 0);
            }
        }

        private string getCurentPrintCount(int previousState, int currentState)
            => (currentState - previousState).ToString();

        private void setPrintCaounters()
        {
            if (CurrentReport != null)
            {
                _prevStateBlack = getPreviousPrintCount().blackPrints;
                _prevStateColor = getPreviousPrintCount().colorPrints;
                _currentStateBlack = CurrentReport.CurrentPrintsStateBlack;
                _currentStateColor = CurrentReport.CurrentPrintsStateColor;
            }
        }

        private string? getContractNumber()
        {
            using RaportGeneratorContext db = new();
            return db.ContractDevices
                .Include(x => x.Contract)
                .FirstOrDefault(x => x.DeviceId == CurrentReport.Device.Id && x.IsValid)?
                .Contract?.Number;
        }
    }
}