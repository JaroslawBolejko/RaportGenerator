using RaportGenerator.DataAccess;
using RaportGenerator.DataAccess.Entities;
using RaportGenerator.Models;
using System.Windows;

namespace RaportGenerator.Views
{
    /// <summary>
    /// Interaction logic for ContractModifyView.xaml
    /// </summary>
    public partial class ContractModifyView : Window
    {
        public ContractModifyView()
        {
            InitializeComponent();
            Loaded += Window_Loaded;

        }

        public ContractModel contract;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (contract != null && contract.ContractId != 0)
            {
                txtContractName.Text = contract.Name;
                datePicker.DataContext = new { ContractModel = new ContractModel { ContractDate = contract.ContractDate } };
                txtTerm.Text = contract.Term;
            }
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
                    if (contract != null && contract.ContractId != 0)
                    {
                        Contract contractToUpdate = new Contract
                        {
                            Id = contract.ContractId,
                            Name = txtContractName.Text,
                            ContractDate = datePicker.SelectedDate.Value,
                            Term = txtTerm.Text,
                            CompanyId = contract.Client.CompanyId,
                        };

                        db.Contracts.Update(contractToUpdate);
                        db.SaveChanges();
                        MessageBox.Show("Edycja zakończona sukcesem!");
                        this.Close();
                    }
                }
            }
        }

        private bool validateIncomingData()
        {
            if (txtContractName.Text.Trim() == "")
            {
                MessageBox.Show("Podaj nazwę umowy!");
                return true;
            }


            if (txtTerm.Text.Trim() == "")
            {
                MessageBox.Show("Podaj warunki umowy!");
                return true;
            }

            if (!datePicker.SelectedDate.HasValue)
            {
                MessageBox.Show("Podaj datę zawarcia umowy!");
                return true;
            }

            return false;
        }
    }
}
