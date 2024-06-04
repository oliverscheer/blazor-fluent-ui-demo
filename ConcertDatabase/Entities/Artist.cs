namespace ConcertDatabase.Entities
{
    public class Artist : Entity
    {
        public virtual ICollection<Concert>? Concerts { get; set; }
    }
}
