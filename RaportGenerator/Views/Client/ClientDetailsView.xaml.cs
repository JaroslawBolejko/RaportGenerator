using RaportGenerator.DataAccess.Entities;
using System.Linq;
using System.Windows;

namespace RaportGenerator.Views
{
    /// <summary>
    /// Interaction logic for ClientDetailsView.xaml
    /// </summary>
    public partial class ClientDetailsView : Window
    {
        public Company company;
        public ClientDetailsView()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (company != null && company.Id != 0)
            {
                txtCompanyName.Text = company.CompanyName;
                txtCompanyTaxNumber.Text = company.TaxNumber;
                txtCompanyStreet.Text = company.Street;
                txtCompanyStreetNumber.Text = company.Number;
                txtCompanyApartmentNumber.Text = company.ApartmentNumber;
                txtCompanyZipCode.Text = company.ZipCode;
                txtCompanyCity.Text = company.City;
                txtCompanyEmail.Text = company.Email;
                txtCompanyTelefonNumber.Text = company.TelefonNumber;
                txtCompanyAccountNumber.Text = company.BankAccountNumber;
               // txtCompanyContracts.Text = string.Join(", ", company.Contracts.Where(x=>x.ContractDevices.Select(x=>x.IsValid)).Select(x => x.Number));
               // txtCompanyDevices.Text = string.Join(", ", company.Contracts.SelectMany(x => x.Devices.Select(x=>x.DeviceName)));
            }
        }
    }
}
