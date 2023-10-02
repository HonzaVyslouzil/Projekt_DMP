using System;
using Microsoft.EntityFrameworkCore;

namespace Projekt_DMP
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<RssFeed> RssFeeds { get; set; }
        public DbSet<Article> Articles { get; set; }
    }
}

