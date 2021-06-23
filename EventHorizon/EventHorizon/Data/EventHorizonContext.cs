
using Microsoft.EntityFrameworkCore;
using EventHorizon.Data.Models;

namespace EventHorizon.Data
{
    //the <User> in IdentityDbContext<User> tells the context what class to use for identity.
    public class EventHorizonContext : DbContext
    {
        public DbSet<Event> Event { get; set; }
        public DbSet<Attendee> User { get; set; }
        public DbSet<Feedback> Feedback { get; set; }

        public EventHorizonContext (DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        
    }
}
