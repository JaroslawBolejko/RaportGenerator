using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaportGenerator.DataAccess.Entities
{
    public class Report : EntityBase
    {
        public int? DeviceId { get; set; }
        public int? CompanyId { get; set; }
           
        [MaxLength(50)]
        public string Name { get; set; } = "Raport Wydruków";

        [Required]
        public string Number { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime DateOfDocument { get; set; }// data utworzenia - timestamp

        [Required]
        public string ReportForMonth{ get; set; }//raport za okres
       
        [Required]
        public int PrevPrintsStateBlack { get; set; }
        
        [Required]
        public int PrevPrintsStateColor { get; set; }
       
        [Required]
        public int CurrentPrintsStateBlack { get; set; }
        
        [Required]
        public int CurrentPrintsStateColor { get; set; }
        
        [ForeignKey("DeviceId")]
        public Device Device { get; set; }

        [ForeignKey("CompanyId")]
        public Company Company { get; set; }
    }
}
