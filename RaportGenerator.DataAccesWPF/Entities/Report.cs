using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaportGenerator.DataAccess.Entities
{
    public enum Print
    {
        Czarno_Biały = 0,
        Kolor = 1
    }
    public class Report : EntityBase
    {
        [Required]
        public int? ClientId { get; set; }

        [Required]
        public int? SupplierId { get; set; }

        [Required]
        public int DeviceId { get; set; }

        [Required]
        public int ContractId { get; set; }

        [MaxLength(50)]
        public string Name { get; set; } = "Rapoprt Wydruków";

        [Required]
        public string Number { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime DateOfDokument { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime DateOfService { get; set; }

    
        [ForeignKey("ClientId")]
        public virtual Company Client { get; set; }

        [ForeignKey("LaundryId")]
        public virtual Company Supplier { get; set; }

        public Device Device { get; set; }
        public Contract Contract { get; set; }
        public Print Print { get; set; }
    }
}
