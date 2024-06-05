using ConcertDatabase.Contexts;
using ConcertDatabase.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConcertDatabase.Repositories
{
    public class ConcertRepository(IDbContextFactory<MusicDbContext> myDbContextFactory, ILogger<ArtistRepository> logger) : Repository<Concert>(myDbContextFactory)
    {
        private readonly ILogger<ArtistRepository> _logger = logger;

        public async Task<Concert?> GetByIdWithArtist(Guid id)
        {
            _logger.LogInformation("Get Concert by ID with Artist");

            var result = await Entities
                .Include(c => c.Artist)
                .FirstOrDefaultAsync(c => c.ID == id);

            return result;
        }

        public IQueryable<Concert>? GetAllWithArtists()
        {
            var result = Entities
                .Include(c => c.Artist)
                .Select(c => c);

            return result;
        }
    }
}
