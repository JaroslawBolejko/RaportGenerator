using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaportGenerator.DataAccess.Entities
{
    public class Contract : EntityBase
    {
        public int? CompanyId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        public DateTime ContractDate { get; set; }

        [Required]
        public string Term { get; set; }

        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }

        public List<ContractDevice> ContractDevices { get; set; }
    }
}
