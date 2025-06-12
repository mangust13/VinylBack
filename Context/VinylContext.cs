using Microsoft.EntityFrameworkCore;
using VinylBack.Models;

namespace VinylBack.Context
{
    public class VinylContext : DbContext
    {
        public VinylContext(DbContextOptions<VinylContext> options) : base(options) { }

        //Authorization
        public DbSet<User> users { get; set; }
        public DbSet<Singer> Singer { get; set; }
        public DbSet<Album> Album { get; set; }

    }
}
