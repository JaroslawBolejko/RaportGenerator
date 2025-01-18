using RaportGenerator.DataAccess.Entities;
using System.Collections.Generic;

namespace RaportGenerator.Models
{
    public class CompanyModel
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public Role CompanyRole { get; set; }
        public string TaxNumber { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string ApartmentNumber { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string TelefonNumber { get; set; }
        public string BankAccountNumber { get; set; }
        public  List<ReportModel> SupplierReports { get; set; }
        public List<ContractModel> SupplierContracts { get; set; }
    }
}
