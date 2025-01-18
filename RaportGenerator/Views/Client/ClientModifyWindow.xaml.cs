using RaportGenerator.DataAccess;
using RaportGenerator.DataAccess.Entities;
using System.Windows;

namespace RaportGenerator.Views
{
    /// <summary>
    /// Interaction logic for ClientModifyWindow.xaml
    /// </summary>
    public partial class ClientModifyWindow : Window
    {
        public Company company;

        public ClientModifyWindow()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!validateIncomingData())
            {
                using (RaportGeneratorContext db = new RaportGeneratorContext())
                {
                    if (company != null && company.Id != 0)
                    {
                        Company companyToUpdate = new Company
                        {
                            Id = company.Id,
                            CompanyName = txtCompanyName.Text,
                            CompanyRole = Role.Client,
                            TaxNumber = txtCompanyTaxNumber.Text,
                            Street = txtCompanyStreet.Text,
                            Number = txtCompanyStreetNumber.Text,
                            ApartmentNumber = txtCompanyApartmentNumber.Text,
                            ZipCode = txtCompanyZipCode.Text,
                            City = txtCompanyCity.Text,
                            Email = txtCompanyEmail.Text,
                            TelefonNumber = txtCompanyTelefonNumber.Text,
                            BankAccountNumber = txtCompanyAccountNumber.Text,
                        };

                        db.Companies.Update(companyToUpdate);
                        db.SaveChanges();
                        MessageBox.Show("Edycja zakończona sukcesem!");
                        this.Close();
                    }
                    else
                    {
                        Company dpt = new Company
                        {
                            CompanyName = txtCompanyName.Text,
                            CompanyRole = Role.Client,
                            TaxNumber = txtCompanyTaxNumber.Text,
                            Street = txtCompanyStreet.Text,
                            Number = txtCompanyStreetNumber.Text,
                            ApartmentNumber = txtCompanyApartmentNumber.Text,
                            ZipCode = txtCompanyZipCode.Text,
                            City = txtCompanyCity.Text,
                            Email = txtCompanyEmail.Text,
                            TelefonNumber = txtCompanyTelefonNumber.Text,
                            BankAccountNumber = txtCompanyAccountNumber.Text,
                        };

                        db.Companies.Add(dpt);
                        db.SaveChanges();
                        txtCompanyName.Clear();
                        txtCompanyTaxNumber.Clear();
                        txtCompanyStreet.Clear();
                        txtCompanyStreetNumber.Clear();
                        txtCompanyApartmentNumber.Clear();
                        txtCompanyZipCode.Clear();
                        txtCompanyCity.Clear();
                        txtCompanyEmail.Clear();
                        txtCompanyTelefonNumber.Clear();
                        txtCompanyAccountNumber.Clear();

                        MessageBox.Show("Firma została dodadna do bazy danych!");
                        this.Close();
                    }

                }
            }
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
            }
        }

        private bool validateIncomingData()
        {
            if (txtCompanyName.Text.Trim() == "")
            {
                MessageBox.Show("Podaj nazwę firmy!");
                return true;
            }

            if (txtCompanyTaxNumber.Text.Trim() == "")
            {
                /// tu jest lipa jak wpisze powyzej 10 znaków to exception
                MessageBox.Show("Podaj NIP firmy!");
                return true;
            }

            if (txtCompanyStreet.Text.Trim() == "")
            {
                MessageBox.Show("Podaj ulicę, na której znajduje się firma!");
                return true;
            }

            if (txtCompanyStreetNumber.Text.Trim() == "")
            {
                MessageBox.Show("Podaj nazwę numer ulicy!");
                return true;
            }

            if (txtCompanyZipCode.Text.Trim() == "")
            {
                MessageBox.Show("Podaj nazwę kod pocztowy miasta, w którym znajduje się firma!");
                return true;
            }

            if (txtCompanyCity.Text.Trim() == "")
            {
                MessageBox.Show("Podaj miasto, w którym znajduje się firma!");
                return true;
            }

            if (txtCompanyEmail.Text.Trim() == "")
            {
                MessageBox.Show("Podaj email firmy!");
                return true;
            }

            if (txtCompanyTelefonNumber.Text.Trim() == "")
            {
                MessageBox.Show("Podaj numer telefinu!");
                return true;
            }

            if (txtCompanyAccountNumber.Text.Trim() == "")
            {
                MessageBox.Show("Podaj numer konta!");
                return true;
            }

            return false;
        }
    }
}
