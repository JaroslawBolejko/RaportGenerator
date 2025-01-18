using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaportGenerator.DataAccess.Entities
{
    public class Device : EntityBase
    {
        [Required]
        public string DeviceName { get; set; }
        [Required]
        public string SerialNumber { get; set; }

        //[Required]
        //public int ColorPrintCounter { get; set; }

        //[Required]
        //public int BlackWhitePrintCounter { get; set; }

        public List<ContractDevice> ContractDevices { get; set; }
        public List<Report> Reports { get; set; }
    }
}
