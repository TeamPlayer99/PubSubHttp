using System;
using Microsoft.EntityFrameworkCore;

namespace PubSubApiServer.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Message { get; set; }
    }

    public class EventContext : DbContext
    {
        public EventContext(DbContextOptions<EventContext> options)
            : base(options)
        {


        }

        public DbSet<Event> Events { get; set; }
    }
}
