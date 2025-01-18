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
    /// Interaction logic for DeviceListView.xaml
    /// </summary>
    public partial class DeviceListView : UserControl
    {
        private readonly RaportGeneratorContext db;
        public DeviceListView()
        {
            InitializeComponent();
            db = new RaportGeneratorContext();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FillGrid();
        }

        private void FillGrid()
        {
            gridDevices.ItemsSource = MapDataFromDb();
        }

        private List<DeviceModel> MapDataFromDb()
        {
            var list = db.Devices
                .Include(x=>x.Reports)
                .Include(x=>x.ContractDevices)
                    .ThenInclude(x=>x.Contract)
                    .ThenInclude(x=>x.Company)
                .Select(a => new
            {
                deviceId = a.Id,
                deviceName = a.DeviceName,
                serialNumber = a.SerialNumber,
                colorPrintStatus = a.Reports.Where(r=>a.ContractDevices.Any(cd => cd.IsValid)).OrderByDescending(x=>x.Id).Select(x=>x.CurrentPrintsStateColor).FirstOrDefault(),
                blackWhitePrintStatus = a.Reports.Where(r => a.ContractDevices.Any(cd => cd.IsValid)).OrderByDescending(x => x.Id).Select(x => x.CurrentPrintsStateBlack).FirstOrDefault(),
                contractedCompany = a.ContractDevices.Where(x=>x.IsValid).Select(x=>x.Contract.Company.CompanyName).FirstOrDefault(),
            }).OrderBy(x => x.deviceId).ToList();

            List<DeviceModel> modelList = new List<DeviceModel>();

            foreach (var item in list)
            {
                DeviceModel model = new DeviceModel()
                {
                    Id = item.deviceId,
                    DeviceName = item.deviceName,
                    SerialNumber = item.serialNumber,
                    ColorPrintStatus = item.colorPrintStatus,
                    BlackWhitePrintStatus = item.blackWhitePrintStatus,
                    ContractedCompany = item.contractedCompany
                };

                modelList.Add(model);
            }

            return modelList;
        }

        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            DeviceModel device = (DeviceModel)gridDevices.SelectedItem;

            if (device == null)
            {
                MessageBox.Show("Wybierz urządzenie!");
                return;
            }

            DeviceDetailsView page = new DeviceDetailsView();
            page.device = device;
            page.ShowDialog();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            DeviceModifyView page = new DeviceModifyView();
            page.ShowDialog();
            FillGrid();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            DeviceModel device = (DeviceModel)gridDevices.SelectedItem;

            if (device == null)
            {
                MessageBox.Show("Wybierz urządzenie!");
                return;
            }

            if (device != null && device.Id != 0)
            {
                DeviceModifyView page = new DeviceModifyView();
                page.device = device;
                page.ShowDialog();
                FillGrid();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            DeviceModel deviceModel = (DeviceModel)gridDevices.SelectedItem;
            if (deviceModel != null && deviceModel.Id != 0)
            {
                if (MessageBox.Show("Jesteś pewny, że chcesz usuńąć to urządzenie?", "Odpowiedz", MessageBoxButton.YesNo
                     , MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    Device deviceFromDb = db.Devices.Find(deviceModel.Id);
                    db.Devices.Remove(deviceFromDb);
                    db.SaveChanges();
                    MessageBox.Show("Urządzenie zostało usunięte!");
                    FillGrid();
                }
            }
        }
    }
}