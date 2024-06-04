using ConcertDatabase.Entities;
using Microsoft.AspNetCore.Identity;

namespace ConcertDatabase.Contexts
{
    public class ApplicationUser : IdentityUser
    {
        public string? Image { get; set; }
        public ICollection<UserProfile>? UserProfiles { get; set; }
    }
}
