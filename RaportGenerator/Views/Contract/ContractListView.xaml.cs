using Microsoft.EntityFrameworkCore;
using RaportGenerator.DataAccess;
using RaportGenerator.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RaportGenerator.Views
{
    /// <summary>
    /// Interaction logic for ContractListView.xaml
    /// </summary>
    public partial class ContractListView : UserControl
    {
        private readonly RaportGeneratorContext _db;
        private ObservableCollection<ContractModel> _contracts;

        public ContractListView()
        {
            InitializeComponent();
            _db = new RaportGeneratorContext();

            gridContracts.SelectionChanged += GridContracts_SelectionChanged;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            gridContracts.ItemsSource = FillGrid(false);
        }

        public ObservableCollection<ContractModel> FillGrid(bool showHistoryItems)
        {
           return _contracts = showHistoryItems
                ? new ObservableCollection<ContractModel>(MapDataFromDb()
                    .Where(x => !x.IsValid)
                    .OrderBy(x => x.ContractDate))
                : new ObservableCollection<ContractModel>(MapDataFromDb()
                    .Where(x => x.IsValid)
                    .OrderBy(x => x.ContractDate));
        }

        private List<ContractModel> MapDataFromDb()
        {
            var contracts = _db.Contracts
                .Include(x => x.Company)
                .Include(x => x.ContractDevices)
                    .ThenInclude(x => x.Device)
                .ToList();

            var list = contracts
                .Select(a => new
                {
                    ContractId = a.Id,
                    ContractName = a.Name,
                    ContractNumber = a.Number,
                    ContractDate = a.ContractDate,
                    Term = a.Term,
                    Client = a.Company,
                    IsValid = a.ContractDevices.Where(x => x.ContractId == a.Id).First().IsValid,
                    ContractDevices = a.ContractDevices,
                    Devices = a.ContractDevices.Select(d => new DeviceModel
                    {
                        Id = d.DeviceId,
                        DeviceName = d.Device.DeviceName,
                        SerialNumber = d.Device.SerialNumber,
                        //ColorPrintStatus = d.Device.ColorPrintCounter,
                        //BlackWhitePrintStatus = d.Device.BlackWhitePrintCounter,
                    }).ToList()
                }).OrderByDescending(x => x.ContractDate).ToList();

            List<ContractModel> modelList = new List<ContractModel>();

            foreach (var item in list)
            {
                ContractModel model = new ContractModel()
                {
                    ContractId = item.ContractId,
                    Name = item.ContractName,
                    Number = item.ContractNumber,
                    ContractDate = item.ContractDate.ToString("MM.dd.yyyy"),
                    Term = item.Term,
                    IsValid = item.IsValid,
                    ContractDevices = item.ContractDevices,
                    Client = new CompanyModel
                    {
                        CompanyId = item.Client.Id,
                        CompanyName = item.Client.CompanyName,
                        CompanyRole = item.Client.CompanyRole,
                        TaxNumber = item.Client.TaxNumber,
                        Street = item.Client.Street,
                        Number = item.Client.Number,
                        ApartmentNumber = item.Client.ApartmentNumber,
                        ZipCode = item.Client.ZipCode,
                        City = item.Client.City,
                        Email = item.Client.Email,
                        TelefonNumber = item.Client.TelefonNumber,
                        BankAccountNumber = item.Client.BankAccountNumber,
                    },
                    Devices = item.Devices,
                };

                modelList.Add(model);
            }

            return modelList;
        }

        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            ContractModel contract = (ContractModel)gridContracts.SelectedItem;

            if (contract == null)
            {
                MessageBox.Show("Wybierz umowę!");
                return;
            }

            ContractDetailsView page = new ContractDetailsView();
            page.contract = contract;
            page.ShowDialog();
            gridContracts.ItemsSource = FillGrid(false);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            ContractAddView page = new ContractAddView();
            page.ShowDialog();
            gridContracts.ItemsSource = FillGrid(false);
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            ContractModel contract = (ContractModel)gridContracts.SelectedItem;

            if (contract == null)
            {
                MessageBox.Show("Wybierz umowę!");
                return;
            }

            if (contract != null && contract.ContractId != 0)
            {
                ContractModifyView page = new ContractModifyView();
                page.contract = contract;
                page.ShowDialog();
                gridContracts.ItemsSource = FillGrid(false);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            ContractModel contractToDelete = (ContractModel)gridContracts.SelectedItem;

            if (contractToDelete == null || contractToDelete.ContractId == 0)
            {
                MessageBox.Show("Nie wybrano żadnej umowy do przeniesienia do historii.");
                return;
            }

            if (MessageBox.Show("Jesteś pewny, że chcesz przenieść tą umowę do historii?", "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    using (RaportGeneratorContext db = new RaportGeneratorContext())
                    {
                        var contract = db.Contracts
                                         .Include(c => c.ContractDevices)
                                         .FirstOrDefault(c => c.Id == contractToDelete.ContractId);

                        if (contract != null)
                        {
                            foreach (var contractDevice in contract.ContractDevices)
                            {
                                contractDevice.IsValid = false;
                                contractDevice.AssociatedDate = DateTime.Now;
                            }

                            db.SaveChanges();

                            MessageBox.Show("Umowa została oznaczona jako nieaktywna i została przeniesiona do historii!");
                        }
                        else
                        {
                            MessageBox.Show($"Umowa z Id - {contractToDelete.ContractId}, nie znajduje się w bazie danych.");
                        }
                    }

                    var updatedContract = _contracts.FirstOrDefault(c => c.ContractId == contractToDelete.ContractId);
                    if (updatedContract != null)
                    {
                        updatedContract.IsValid = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Wystąpił błąd podczas usuwania umowy: {ex.Message}");
                }
            }
        }

        private void GridContracts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnDelete.IsEnabled = gridContracts.SelectedItem != null;
            btnDetails.IsEnabled = gridContracts.SelectedItem != null;
            btnUpdate.IsEnabled = gridContracts.SelectedItem != null;
        }
    }
}
