using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EventHorizon.Data.Entities;

namespace EventHorizon.Data
{
    public class DataContext : DbContext
    {
        public DataContext (DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Event { get; set; }
        public DbSet<Attendee> Attendee { get; set; }
        public DbSet<Organizer> Organizer { get; set; }

        public DbSet<UserFeedback> UserFeedback { get; set; }
    }
}
