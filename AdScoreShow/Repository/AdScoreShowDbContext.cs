using AdScoreShow.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AdScoreShow.Repository
{
    public class AdScoreShowDbContext : DbContext
    {
        public AdScoreShowDbContext() : base("AdScoreShowDB")
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<AdScoreShowDbContext>())
        }

        public DbSet<Market> Markets { get; set; }
        public DbSet<AdvertAired> AdvertAireds { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<Segment> Segments { get; set; }
        public DbSet<Brand> Brands { get; set; }

        public static AdScoreShowDbContext Create()
        {
            return new AdScoreShowDbContext();
        }
    }
}