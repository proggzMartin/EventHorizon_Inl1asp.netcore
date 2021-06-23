using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EventHorizon.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EventHorizon.Data
{
    //the <User> in IdentityDbContext<User> tells the context what class to use for identity.
    public class EventHorizonContext : IdentityDbContext<User>
    {
        public DbSet<Event> Event { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Feedback> Feedback { get; set; }

        public EventHorizonContext (DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        
    }
}
