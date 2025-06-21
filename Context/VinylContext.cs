using Microsoft.EntityFrameworkCore;
using VinylBack.Models;

namespace VinylBack.Context
{
    public class VinylContext : DbContext
    {
        public VinylContext(DbContextOptions<VinylContext> options) : base(options) { }

        //Authorization
        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Singer> Singer { get; set; }
        public DbSet<Album> Album { get; set; }
        public DbSet<Track> Track { get; set; }
        public DbSet<PurchasedTrack> PurchasedTrack { get; set; }
        public DbSet<Purchase> Purchase { get; set; }
        public DbSet<TrackInBasket> TrackInBasket { get; set; }
        public DbSet<Basket> Basket { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<PurchaseStatus> PurchaseStatus { get; set; }
    }
}
