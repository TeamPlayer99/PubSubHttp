using System;
using Microsoft.EntityFrameworkCore;

namespace PubSubApiServer.Models
{
    public class Publish
    {
        public long Id { get; set; }
        public string Message { get; set; }
    }

    public class PublishContext : DbContext
    {
        public PublishContext(DbContextOptions<PublishContext> options)
            : base(options)
        {


        }

        public DbSet<Publish> Publications { get; set; }
    }
}
