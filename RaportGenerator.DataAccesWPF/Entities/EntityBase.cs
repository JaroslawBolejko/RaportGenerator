using System.ComponentModel.DataAnnotations;

namespace RaportGenerator.DataAccess.Entities
{
    public abstract class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}
