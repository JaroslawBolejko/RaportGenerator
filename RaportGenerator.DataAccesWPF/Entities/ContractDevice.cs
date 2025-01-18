using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaportGenerator.DataAccess.Entities
{
    public class ContractDevice : EntityBase
    {
        public int ContractId { get; set; }
        public Contract Contract { get; set; }

        public int DeviceId { get; set; }
        public Device Device { get; set; }

        public bool IsValid { get; set; }

        public DateTime AssociatedDate { get; set; }
    }
}
