using System.ComponentModel.DataAnnotations;

namespace RaportGenerator.DataAccess.Entities
{
    public class Device : EntityBase
    {
        [Required]
        public int CounterId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int  FabricNumber { get; set; }
        public Counter Counter { get; set; }


    }
}
