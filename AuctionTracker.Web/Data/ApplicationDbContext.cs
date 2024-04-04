using AuctionTracker.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace AuctionTracker.Web.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {   
        }

        // Add items here if doing migrations 'Code First'
        public DbSet<Game> Games { get; set; }
        public DbSet<Toy> Toys { get; set; }
    }
}