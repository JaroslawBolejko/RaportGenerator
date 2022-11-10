using System.ComponentModel.DataAnnotations;

namespace RaportGenerator.DataAccess.Entities
{
    public class Company : EntityBase
    {
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
        [Required]
        [StringLength(10)]
        public string TaxNumber { get; set; }
        [Required]
        [MaxLength(50)]
        public string Street { get; set; }
        [Required]
        [MaxLength(10)]
        public string Number { get; set; }
        public string ApartmentNumber { get; set; }
        [Required]
        [StringLength(6)]
        public string ZipCode { get; set; }
        [Required]
        [MaxLength(100)]
        public string City { get; set; }
        [MaxLength(100)]
        public string EMail { get; set; }
        [MaxLength(20)]
        public string TelefonNumber { get; set; }
        [MaxLength(28)]
        public string BankAccountNumber { get; set; }
    }
}
