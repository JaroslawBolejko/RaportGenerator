using Microsoft.EntityFrameworkCore;
using RaportGenerator.DataAccess;
using RaportGenerator.DataAccess.Entities;
using RaportGenerator.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RaportGenerator.Views
{
    /// <summary>
    /// Interaction logic for ReportAddView.xaml
    /// </summary>
    public partial class ReportAddView : Window
    {
        private readonly Company _selectedCompany;
        private readonly Device _selectedDevice;
        private readonly List<ReportModel> _reports;
        private readonly ReportModel? _lastReport;

        public ReportAddView(Company selectedCompany, Device selectedDevice, List<ReportModel> reports)
        {
            InitializeComponent();
            _selectedCompany = selectedCompany;
            _selectedDevice = selectedDevice;
            _reports = reports;
            _lastReport = _reports?.Where(x => x.Device.Id == _selectedDevice.Id).OrderByDescending(x => x.Id)?.FirstOrDefault();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtCompanyName.Text = _selectedCompany.CompanyName;
            txtRaportNumber.Text = getDocumentNumber();
            datePicker.SelectedDate = DateTime.Now;
            txtDeviceWithSerialNumber.Text = $"{_selectedDevice.DeviceName} {_selectedDevice.SerialNumber}";
            txtContractNumber.Text = getContractNumber();
            txtPrevStateBlack.Text = getPrevPrintState(false);
            txtPrevStateColor.Text = getPrevPrintState(true);
            setInitialMonthName();
            txtRaportOwner.Text = "Marek Jary";//Docelowo admin
        }

        private void txtCurrentStateBlack_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdatePrintCounter(true);
        }

        private void txtCurrentStateColor_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdatePrintCounter(false);
        }

        private void UpdatePrintCounter(bool isBlackPrint)
        {
            string txtPrevState = isBlackPrint ? txtPrevStateBlack.Text : txtPrevStateColor.Text;
            string txtCurrentState = isBlackPrint ? txtCurrentStateBlack.Text : txtCurrentStateColor.Text;

            if (int.TryParse(txtPrevState, out int prevState) &&
                int.TryParse(txtCurrentState, out int currentState))
            {
                int currentPrintCounter = currentState - prevState;

                TextBlock targetTextBox = isBlackPrint ? txtCurrentPrintCounterBlack : txtCurrentPrintCounterColor;
                targetTextBox.Text = currentPrintCounter.ToString();
            }
            else
            {
                TextBlock targetTextBox = isBlackPrint ? txtCurrentPrintCounterBlack : txtCurrentPrintCounterColor;
                targetTextBox.Text = "Wpisz liczbę wydruków w polu: Stan bieżący";
            }
        }

        private string getDocumentNumber()
            => CommonUtils.CommonUtils.GetDocumentNumber(_lastReport?.ReportNumber);

        private string getPrevPrintState(bool color)
        {
            if (_selectedDevice == null)
            {
                return "Najpierw wybierz urządzenie!";
            }

            if (_lastReport == null)
            {
                return "0";
            }

            return color ? _lastReport.CurrentPrintsStateColor.ToString() : _lastReport.CurrentPrintsStateBlack.ToString();

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (validateIncomingData())
            {
                using RaportGeneratorContext db = new();
                DataAccess.Entities.Report reportToAdd = new()
                {
                    Number = txtRaportNumber.Text,
                    DateOfDocument = datePicker.SelectedDate.Value,
                    ReportForMonth = cmbReportMonth.Text,
                    DeviceId = _selectedDevice.Id,
                    CompanyId = _selectedCompany.Id,
                    PrevPrintsStateBlack = int.Parse(txtPrevStateBlack.Text),
                    PrevPrintsStateColor = int.Parse(txtPrevStateColor.Text),
                    CurrentPrintsStateBlack = int.Parse(txtCurrentStateBlack.Text),
                    CurrentPrintsStateColor = int.Parse(txtCurrentStateColor.Text)
                };

                db.Reports.Add(reportToAdd);
                db.SaveChanges();
                MessageBox.Show("Dodanie raportu zakończone sukcesem!");
                this.Close();
            }
        }

        private bool validateIncomingData()
        {
            return true;
        }

        private string? getContractNumber()
        {
            using RaportGeneratorContext db = new();
            return db.ContractDevices
                .Include(x => x.Contract)
                .FirstOrDefault(x => x.DeviceId == _selectedDevice.Id && x.IsValid)?
                .Contract?.Number;
        }

        private void setInitialMonthName()
        {
            int lastMonthIndex = 11;
            int monthNumber = DateTime.Now.Month;
            cmbReportMonth.ItemsSource = new CultureInfo("pl-PL").DateTimeFormat.MonthNames
             .Where(m => !string.IsNullOrEmpty(m))
             .ToList();
            cmbReportMonth.SelectedIndex = monthNumber == 1 ? lastMonthIndex : monthNumber - 2;
        }
    }
}
