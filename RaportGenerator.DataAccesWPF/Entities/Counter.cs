using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaportGenerator.DataAccess.Entities
{
    public class Counter : EntityBase
    {
        [Required]
        public int DeviceId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int PreviousStatus { get; set; }

        [Required]
        public int CurrentStatus { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime CurentStatusDate { get; set; }
        public Device Device { get; set; }
    }
}
