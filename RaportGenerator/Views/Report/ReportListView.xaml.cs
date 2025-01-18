using Azure;
using Microsoft.EntityFrameworkCore;
using RaportGenerator.DataAccess;
using RaportGenerator.DataAccess.Entities;
using RaportGenerator.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RaportGenerator.Views
{
    /// <summary>
    /// Interaction logic for ReportListView.xaml
    /// </summary>
    public partial class ReportListView : UserControl
    {
        private readonly RaportGeneratorContext _db;
        private List<ReportModel> _reportList;

        public ReportListView()
        {
            InitializeComponent();
            _db = new RaportGeneratorContext();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FillGrid();
        }

        private void FillGrid()
        {
            gridReports.ItemsSource = MapDataFromDb();
        }

        private List<ReportModel> MapDataFromDb()
        {
            var list = _db.Reports
                .Include(x=>x.Device)
                .Include(x => x.Company)
                    .ThenInclude(x=>x.Contracts)
                    .ThenInclude(x=>x.ContractDevices)
                .Select(x => new
                {
                    ReportId = x.Id,
                    ReportName = x.Name,
                    ReportNumber = x.Number,
                    DeviceName = x.Device.DeviceName,
                    DeviceSerialNumber = x.Device.SerialNumber,
                    CompanyName = x.Company.CompanyName,
                    ReportDocumentDate = x.DateOfDocument,
                    ReportForMonth = x.ReportForMonth,
                    PrevPrintsStateBlack = x.PrevPrintsStateBlack,
                    PrevPrintsStateColor = x.PrevPrintsStateColor,
                    CurrentPrintsStateBlack = x.CurrentPrintsStateBlack,
                    CurrenPrintstStateColor = x.CurrentPrintsStateColor,
                    Device = x.Device
                })
                .OrderBy(x => x.ReportId)
                .ToList();

            List<ReportModel> modelList = new List<ReportModel>();

            foreach (var item in list)
            {
                ReportModel model = new ReportModel()
                {
                    Id = item.ReportId,
                    ReportName = item.ReportName,
                    ReportNumber = item.ReportNumber,
                    DeviceName = item.DeviceName,
                    DeviceSerialNumber = item.DeviceSerialNumber,
                    CompanyName = item.CompanyName,
                    ReportDocumentDate = item.ReportDocumentDate.ToString("dd.MM.yyyy"),
                    ReportForMonth = item.ReportForMonth,
                    PrevPrintsStateBlack = item.PrevPrintsStateBlack,
                    PrevPrintsStateColor = item.PrevPrintsStateColor,
                    CurrentPrintsStateBlack = item.CurrentPrintsStateBlack,
                    CurrentPrintsStateColor = item.CurrenPrintstStateColor,
                    Device = item.Device
                };

                modelList.Add(model);
            }

            _reportList = modelList;
            return modelList;
        }

        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            ReportModel report = (ReportModel)gridReports.SelectedItem;

            if (report == null)
            {
                MessageBox.Show("Wybierz raport!");
                return;
            }

            ReportDetailsView page = new ReportDetailsView();
            page.CurrentReport = report;
            page.AllReports = _reportList;
            page.ShowDialog();
        }
        //po show dialog odswierzyc liste-ppobrac dane z bazy tak jak dodawaniu klientów
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            ReportAddSelectionWindow companySelectionWindow = new ReportAddSelectionWindow();
            bool? result = companySelectionWindow.ShowDialog();
            
            if (result == true)
            {
                Company selectedCompany = companySelectionWindow.SelectedCompany;
                Device selectedDevice = companySelectionWindow.SelectedDevice;
                ReportAddView page = new ReportAddView(selectedCompany, selectedDevice, _reportList);
                page.ShowDialog();
                FillGrid();
                return;
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            //DeviceModel deviceModel = (DeviceModel)gridDevices.SelectedItem;
            //if (deviceModel != null && deviceModel.Id != 0)
            //{
            //    DeviceModifyView page = new DeviceModifyView();
            //    page.device = deviceModel;
            //    page.ShowDialog();
            //    FillGrid();
            //}
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            //DeviceModel deviceModel = (DeviceModel)gridDevices.SelectedItem;
            //if (deviceModel != null && deviceModel.Id != 0)
            //{
            //    if (MessageBox.Show("Jesteś pewny, że chcesz usuńąć to urządzenie?", "Odpowiedz", MessageBoxButton.YesNo
            //         , MessageBoxImage.Warning) == MessageBoxResult.Yes)
            //    {
            //        Device deviceFromDb = db.Devices.Find(deviceModel.Id);
            //        db.Devices.Remove(deviceFromDb);
            //        db.SaveChanges();
            //        MessageBox.Show("Urządzenie zostało usunięte!");
            //        FillGrid();
            //    }
            //}
        }
    }
}
