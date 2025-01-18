using Microsoft.EntityFrameworkCore;
using RaportGenerator.DataAccess;
using RaportGenerator.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RaportGenerator.Views
{
    /// <summary>
    /// Interaction logic for ClientListView.xaml
    /// </summary>
    public partial class ClientListView : UserControl
    {
        public ClientListView()
        {
            InitializeComponent();

            using (RaportGeneratorContext db = new RaportGeneratorContext())
            {
                List<Company> list = db.Companies
                    .Where(x =>x.CompanyRole != Role.Admin)
                    .Include(x=>x.Contracts)
                  //  .ThenInclude(c => c.Devices)
                    .AsNoTracking()
                    .ToList();
                gridClients.ItemsSource = list;
            }
        }

        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            Company dpt = (Company)gridClients.SelectedItem;
            
            if (dpt == null) 
            {
                MessageBox.Show("Wybierz klienta!");
                return;
            }

            ClientDetailsView page = new ClientDetailsView();
            page.company = dpt;
            page.ShowDialog();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            ClientModifyWindow page = new ClientModifyWindow();
            page.ShowDialog();

            using (RaportGeneratorContext db = new RaportGeneratorContext())
            {
                List<Company> list = db.Companies.OrderBy(x => x.Id).ToList();
                gridClients.ItemsSource = list;
            }

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Company dpt = (Company)gridClients.SelectedItem;
            ClientModifyWindow page = new ClientModifyWindow();
            page.company = dpt;
            page.ShowDialog();

            using (RaportGeneratorContext db = new RaportGeneratorContext())
            {
                gridClients.ItemsSource = db.Companies.OrderBy(x => x.Id).ToList();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Company companyToDelete = (Company)gridClients.SelectedItem;
            if (companyToDelete != null && companyToDelete.Id != 0)
            {
                if (MessageBox.Show("Jesteś pewny, że chcesz usuńąć tą firmę?", "Odpowiedz", MessageBoxButton.YesNo
                    , MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    using (RaportGeneratorContext db = new RaportGeneratorContext())
                    {
                        Company company = db.Companies.Find(companyToDelete.Id);
                        db.Companies.Remove(company);
                        db.SaveChanges();
                        MessageBox.Show("Firma została usunięta!");
                        gridClients.ItemsSource = db.Companies.ToList();
                    }
                }
            }
        }
    }
}
