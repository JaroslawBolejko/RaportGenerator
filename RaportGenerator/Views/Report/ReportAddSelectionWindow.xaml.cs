using Microsoft.EntityFrameworkCore;
using RaportGenerator.DataAccess;
using RaportGenerator.DataAccess.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RaportGenerator.Views
{
    /// <summary>
    /// Interaction logic for ReportAddSelectionWindow.xaml
    /// </summary>
    public partial class ReportAddSelectionWindow : Window
    {
        private ObservableCollection<Company> _companies;
        private ObservableCollection<Device> _devices;

        public ReportAddSelectionWindow()
        {
            InitializeComponent();
            using (RaportGeneratorContext db = new RaportGeneratorContext())
            {
                _companies = new ObservableCollection<Company>(db.Companies
                    .Include(company => company.Contracts)
                        .ThenInclude(contract => contract.ContractDevices)
                    .Where(company => company.Contracts
                        .Any(contract => contract.ContractDevices
                            .Any(contract => contract.IsValid)))
                    .Where(x => x.CompanyRole != Role.Admin)
                    .ToList());
                CompanyComboBox.ItemsSource = _companies;
            }

            DeviceComboBox.IsEnabled = false;
            DeviceComboBox.Style = (Style)FindResource("DisabledComboBoxStyle");
        }

        public Company SelectedCompany { get; private set; }
        public Device SelectedDevice { get; private set; }

        private void CompanyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedCompany = (Company)CompanyComboBox.SelectedItem;
            if (SelectedCompany != null)
            {
                using (RaportGeneratorContext db = new RaportGeneratorContext())
                {
                    List<int> contractIdList = _companies
                        .SelectMany(x=>x.Contracts)
                        .Where(x=>x.CompanyId == SelectedCompany.Id)
                        .Select(x=>x.Id)
                        .ToList();
                    _devices = new ObservableCollection<Device>(db.ContractDevices
                        .Include(x => x.Device)
                        .Where(x => contractIdList.Contains(x.ContractId) && x.IsValid)
                        .Select(x=>x.Device)
                        .ToList());

                    DeviceComboBox.ItemsSource = _devices;
                }
                DeviceComboBox.IsEnabled = true;
                DeviceComboBox.Style = (Style)FindResource("BaseComboBoxStyle");
            }
            else
            {
                DeviceComboBox.IsEnabled = false;
                DeviceComboBox.ItemsSource = null;
                DeviceComboBox.Style = (Style)FindResource("DisabledComboBoxStyle");
            }
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedDevice = (Device)DeviceComboBox.SelectedItem;
            if (SelectedCompany != null && SelectedDevice != null)
            {
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Wybierz firmę i urządzenie!");
            }
        }
    }
}
