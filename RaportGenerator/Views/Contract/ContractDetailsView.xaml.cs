using Microsoft.Identity.Client;
using RaportGenerator.DataAccess.Entities;
using RaportGenerator.Models;
using System.Linq;
using System.Windows;
using System.Windows.Documents;

namespace RaportGenerator.Views
{
    /// <summary>
    /// Interaction logic for ContractDetailsView.xaml
    /// </summary>
    public partial class ContractDetailsView : Window
    {
        public ContractDetailsView()
        {
            InitializeComponent();
        }

        public ContractModel contract;

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (contract != null && contract.ContractId != 0)
            {
                txtContractName.Text = contract.Name;
                txtContractNumber.Text = contract.Number;
                txtContractDate.Text = contract.ContractDate;
                txtIsValid.Text = getIsValidString();
                txtDeviceList.Text = getDeviceList();
                txtTerm.Text = contract.Term;
                txtClientName.Text = contract.Client.CompanyName;
                txtClientTaxNumber.Text = contract.Client.TaxNumber;
                txtClientStreet.Text = contract.Client.Street;
                txtClientNumber.Text = contract.Client.Number;
                txtClientApartmentNumber.Text = contract.Client.ApartmentNumber;
                txtClientZipCode.Text = contract.Client.ZipCode;
                txtClientCity.Text = contract.Client.City;
                txtClientEmail.Text = contract.Client.Email;
                txtClientTelefonNumber.Text = contract.Client.TelefonNumber;
                txtClientBankAccountNumber.Text = contract.Client.BankAccountNumber;
            }
        }

        private string getIsValidString()
            => contract.ContractDevices.FirstOrDefault(x => x.ContractId == contract.ContractId).IsValid ? "Tak" : "Nie";
        private string getDeviceList()
            =>  string.Join(", ", contract.Devices.Select(x=>x.DeviceDisplayName));
    }
}
