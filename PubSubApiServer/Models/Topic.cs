using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PubSubApiServer.Models
{
    public class Topic
    {
        public int TopicId { get; set; }
        public string Title { get; set; }

        public List<Subscribe> Subscribers { get; set; }
    }

    public class TopicContext : DbContext
    {
        public TopicContext(DbContextOptions<TopicContext> options)
            : base(options)
        {


        }

        public DbSet<Topic> Topics { get; set; }
    }
}
