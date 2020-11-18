using System;
using Microsoft.EntityFrameworkCore;

namespace PubSubApiServer.Models
{
    public class Subscribe
    {
        public int SubscribeId { get; set; }
        public string Url { get; set; }

        public Topic Topic { get; set; }
        public int TopicId { get; set; }
    }

    public class SubscribeContext : DbContext
    {
        public SubscribeContext(DbContextOptions<SubscribeContext> options)
            : base(options)
        {


        }

        public DbSet<Subscribe> Subscriptions { get; set; }
    }
}
