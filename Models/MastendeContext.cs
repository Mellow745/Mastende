using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Mastende.Models
{
    public class MastendeContext:IdentityDbContext
    {
        public MastendeContext(DbContextOptions<MastendeContext> options) : base(options)
        {
        }
        public DbSet<Owner> LandlordDb { get; set; }
       
    }
}
