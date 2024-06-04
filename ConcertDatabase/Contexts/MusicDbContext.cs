using ConcertDatabase.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConcertDatabase.Contexts
{
    public class MusicDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : DbContext(options)
    {
        public DbSet<Concert> Concerts { get; set; }
        public DbSet<Artist> Artists { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = DateTime.UtcNow;
                        entry.Entity.CreatedBy = httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "unknown user";
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedOn = DateTime.UtcNow;
                        entry.Entity.LastModifiedBy = httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "unknown user";
                        break;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Default Artists

            List<Artist> artists = [];

            Artist beatles = new()
            {
                Name = "The Beatles",
                Description = "The Beatles were an English rock band formed in Liverpool in 1960. With a line-up comprising John Lennon, Paul McCartney, George Harrison and Ringo Starr, they are regarded as the most influential band of all time.",
                ID = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            };

            artists.Add(beatles);

            Artist rollingStones = new()
            {
                Name = "The Rolling Stones",
                Description = "The Rolling Stones are an English rock band formed in London in 1962. The first settled line-up consisted of Brian Jones (guitar, harmonica), Ian Stewart (piano), Mick Jagger (lead vocals, harmonica), Keith Richards (guitar), Bill Wyman (bass) and Charlie Watts (drums).",
                ID = Guid.Parse("00000000-0000-0000-0000-000000000002"),
            };

            artists.Add(rollingStones);

            Artist metallica = new()
            {
                Name = "Metallica",
                Description = "Metallica is an American heavy metal band. The band was formed in 1981 in Los Angeles by vocalist/guitarist James Hetfield and drummer Lars Ulrich, and has been based in San Francisco for most of its career.",
                ID = Guid.Parse("00000000-0000-0000-0000-000000000003")
            };
            artists.Add(metallica);

            Artist acdc = new()
            {
                Name = "AC/DC",
                Description = "AC/DC are an Australian rock band formed in Sydney in 1973 by Scottish-born brothers Malcolm and Angus Young. Although their music has been variously described as hard rock, blues rock, and heavy metal, the band themselves call it simply 'rock and roll'.",
                ID = Guid.Parse("00000000-0000-0000-0000-000000000004")
            };
            artists.Add(acdc);

            Artist u2 = new()
            {
                Name = "U2",
                Description = "U2 are an Irish rock band from Dublin, formed in 1976. The group consists of Bono (lead vocals and rhythm guitar), the Edge (lead guitar, keyboards, and backing vocals), Adam Clayton (bass guitar), and Larry Mullen Jr. (drums and percussion).",
                ID = Guid.Parse("00000000-0000-0000-0000-000000000005")
            };
            artists.Add(u2);

            #endregion

            modelBuilder.Entity<Artist>().HasData(artists);

            #region Default Concerts

            List<Concert> concerts = [];
            Concert metallicaConcert1 = new()
            {
                Name = "M72 Tour",
                Description = "Metallica's M72 Tour",
                ID = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                //Artist = metallica,
                ArtistID = metallica.ID,
                Date = new DateTime(2024, 5, 24),
                City = "Munich",
                Venue = "Olympiastadion",
            };
            concerts.Add(metallicaConcert1);

            Concert metallicaConcert2 = new()
            {
                Name = "M72 Tour",
                Description = "Metallica's M72 Tour",
                ID = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                //Artist = metallica,
                ArtistID = metallica.ID,
                Date = new DateTime(2024, 5, 26),
                City = "Munich",
                Venue = "Olympiastadion",
            };
            concerts.Add(metallicaConcert2);

            #endregion

            modelBuilder.Entity<Concert>().HasData(concerts);

            base.OnModelCreating(modelBuilder);
        }
    }
}
