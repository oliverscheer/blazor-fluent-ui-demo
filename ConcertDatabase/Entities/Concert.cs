using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ConcertDatabase.Entities
{
    public class Concert : Entity
    {
        public string? Venue { get; set; }
        public DateTime? Date { get; set; }
        public string? City { get; set; }

        [JsonIgnore]
        [ForeignKey("ArtistID")]
        public virtual Artist Artist { get; set; } = default!;
        public Guid ArtistID { get; set; }

    }
}
