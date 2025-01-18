using Microsoft.EntityFrameworkCore;
using RaportGenerator.DataAccess;
using RaportGenerator.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RaportGenerator.Views
{
    /// <summary>
    /// Interaction logic for ContractHistoryListView.xaml
    /// </summary>
    public partial class ContractHistoryListView : UserControl
    {
        private readonly ContractListView _contractListView;
        public ContractHistoryListView()
        {
            InitializeComponent();
            gridContractListView.SelectionChanged += GridContracts_SelectionChanged;
            _contractListView = new ContractListView();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            gridContractListView.ItemsSource = _contractListView.FillGrid(true);
        }

        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            ContractModel contract = (ContractModel)gridContractListView.SelectedItem;

            if (contract == null)
            {
                MessageBox.Show("Wybierz umowę!");
                return;
            }

            ContractDetailsView page = new ContractDetailsView();
            page.contract = contract;
            page.ShowDialog();
            _contractListView.FillGrid(true);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            ContractModel contractToDelete = (ContractModel)gridContractListView.SelectedItem;

            if (contractToDelete == null || contractToDelete.ContractId == 0)
            {
                MessageBox.Show("Nie wybrano żadnej umowy do usunięcia.");
                return;
            }

            if (MessageBox.Show("Jesteś pewny, że chcesz trwale usunąć tą umowę?", "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
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
                         
                            db.Contracts.Remove(contract);
                            db.SaveChanges();

                            MessageBox.Show("Umowa została trwale skasowana!");
                            gridContractListView.ItemsSource = _contractListView.FillGrid(true);
                        }
                        else
                        {
                            MessageBox.Show($"Umowa z Id - {contractToDelete.ContractId}, nie znajduje się w bazie danych.");
                        }
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
            btnDelete.IsEnabled = gridContractListView.SelectedItem != null;
            btnDetails.IsEnabled = gridContractListView.SelectedItem != null;
        }
    }
}
