using System.ComponentModel.DataAnnotations;

namespace ConcertDatabase.Entities
{
    public class Entity
    {
        [Key]
        public Guid ID { get; set; }
        
        [Required]
        public string Name { get; set; } = default!;
        public string? Description { get; set; }

    }
}
