using ConcertDatabase.Contexts;
using ConcertDatabase.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConcertDatabase.Repositories
{
    public class ArtistRepository(IDbContextFactory<MusicDbContext> myDbContextFactory, ILogger<ArtistRepository> logger) : Repository<Artist>(myDbContextFactory)
    {
        private readonly ILogger<ArtistRepository> _logger = logger;

        public async Task<Artist?> GetByIdWithConcerts(Guid id)
        {
            _logger.LogInformation("Get Artist by ID with Concerts");

            var result = await Entities
                .Include(a => a.Concerts)
                .FirstOrDefaultAsync(a => a.ID == id);

            return result;
        }

        public void AddConcert(Artist artist, Concert concert)
        {
            _logger.LogInformation("Add concert to artist");

            if (artist == null ||
                concert == null)
            {
                _logger.LogWarning("- Artist or concert is null");
                return;
            }

            concert.ArtistID = artist.ID;
            _context.Concerts.Add(concert);
            _context.SaveChanges();
        }

        public bool UpdateConcert(Concert concert)
        {
            _context.Concerts.Update(concert);
            return true;
        }

        public bool DeleteConcert(Concert concert)
        {
            _context.Concerts.Remove(concert);
            return true;
        }
    }
}
