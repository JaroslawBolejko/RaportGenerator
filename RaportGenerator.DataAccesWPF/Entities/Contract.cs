using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaportGenerator.DataAccess.Entities
{
    public class Contract : EntityBase
    {
        [Required]
        public int? ClientId { get; set; }

        [Required]
        public int? SupplierId { get; set; }

        [Required]
        public string Name  { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        public DateTime ContractDate { get; set; }

        [Required]
        public string  Term { get; set; }

        [ForeignKey("ClientId")]
        public virtual Company Client { get; set; }

        [ForeignKey("LaundryId")]
        public virtual Company Supplier { get; set; }
    }
}
