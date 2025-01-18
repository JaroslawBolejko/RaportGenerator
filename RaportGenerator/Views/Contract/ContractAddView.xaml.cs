using Microsoft.EntityFrameworkCore;
using RaportGenerator.DataAccess;
using RaportGenerator.DataAccess.Entities;
using RaportGenerator.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace RaportGenerator.Views
{
    public partial class ContractAddView : Window
    {
        private ObservableCollection<Company> _companies;
        private ObservableCollection<DeviceModel> _devices;
        private List<ContractDevice> _contractDevices;
        private List<Device> _selectedDevices;
        private Company _client;

        public ContractAddView()
        {
            _selectedDevices = new List<Device>();
            _contractDevices = new List<ContractDevice>();
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {
            using (RaportGeneratorContext db = new RaportGeneratorContext())
            {
                _companies = new ObservableCollection<Company>(db.Companies.Where(x => x.CompanyRole != Role.Admin).ToList());
                _devices = new ObservableCollection<DeviceModel>(mapDevicesFromDb(db));
              
                companySearchBox.ItemsSource = _companies;
                devicesListBox.ItemsSource = _devices;
            }
            txtContractName.Text = "Umowa";
            txtContractNumber.Text = getDocumentNumber();
            datePicker.SelectedDate = DateTime.Now;
        }

        private List<DeviceModel> mapDevicesFromDb(RaportGeneratorContext db)
        {
            _contractDevices = db.ContractDevices.Include(x=>x.Contract).ToList();
            List<int> availableDevices = _contractDevices.Where(x=>!x.IsValid).Select(x=>x.DeviceId).ToList();
            List<Device> dataFromDb = db.Devices.Include(x=>x.ContractDevices).ToList();
            return dataFromDb.Select(device => new DeviceModel
            {
                Id = device.Id,
                DeviceName = device.DeviceName,
                SerialNumber = device.SerialNumber,
                //ColorPrintStatus = device.ColorPrintCounter,
               // BlackWhitePrintStatus = device.BlackWhitePrintCounter,
            }).ToList();
        }

        private void getSelectedDevices(ObservableCollection<DeviceModel> deviceModels)
        {
            _selectedDevices = deviceModels
                .Where(x => x.IsSelected)
                .Select(deviceModel => new Device
                {
                    Id = deviceModel.Id,
                    DeviceName = deviceModel.DeviceName,
                    SerialNumber = deviceModel.SerialNumber,
                    //ColorPrintCounter = deviceModel.ColorPrintStatus,
                    //BlackWhitePrintCounter = deviceModel.BlackWhitePrintStatus,
                }).ToList();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            getSelectedDevices(_devices);

            if (!validateIncomingData())
            {
                using (RaportGeneratorContext db = new RaportGeneratorContext())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            Contract contractToAdd = getContractToAdd();
                            db.Contracts.Add(contractToAdd);
                            db.SaveChanges();

                            addDeviceContracts(contractToAdd.Id, db);
                            db.SaveChanges();

                            transaction.Commit();
                            MessageBox.Show("Dodanie umowy zakończone sukcesem!");
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show($"Wystąpił błąd: {ex.Message}");
                        }
                    }
                }
            }
        }

        private void addDeviceContracts(int contractId, RaportGeneratorContext db)
        {
            var contractDevices = _selectedDevices.Select(device => new ContractDevice
            {
                ContractId = contractId,
                DeviceId = device.Id,
                IsValid = true,
                AssociatedDate = DateTime.Now,
            }).ToList();

            db.ContractDevices.AddRange(contractDevices);
        }

        private bool validateIncomingData()
        {
            if (string.IsNullOrWhiteSpace(txtContractName.Text))
            {
                MessageBox.Show("Podaj nazwę umowy!");
                return true;
            }

            if (string.IsNullOrWhiteSpace(txtContractNumber.Text))
            {
                MessageBox.Show("Podaj numer umowy!");
                return true;
            }

            if (string.IsNullOrWhiteSpace(txtTerm.Text))
            {
                MessageBox.Show("Podaj warunki umowy!");
                return true;
            }

            if (!datePicker.SelectedDate.HasValue)
            {
                MessageBox.Show("Podaj datę zawarcia umowy!");
                return true;
            }

            if (!_selectedDevices.Any())
            {
                MessageBox.Show("Musisz wybrać urządzenia!");
                return true;
            }

            return false;
        }

        private Contract getContractToAdd()
        {
            return new Contract
            {
                Name = txtContractName.Text,
                Number = txtContractNumber.Text,
                ContractDate = datePicker.SelectedDate.Value,
                Term = txtTerm.Text,
                CompanyId = _client?.Id,
            };
        }

        private string getDocumentNumber()
        {
            ContractDevice? lastContract = _contractDevices.OrderByDescending(x=>x.ContractId).FirstOrDefault();
            return CommonUtils.CommonUtils.GetDocumentNumber(lastContract?.Contract?.Number);
        }

        private void CompanySearchBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (companySearchBox.SelectedItem != null)
            {
                Company selectedCompany = (Company)companySearchBox.SelectedItem;
                _client = selectedCompany;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
