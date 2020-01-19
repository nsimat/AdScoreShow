using AdScoreShow.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdScoreShow.DataModel.Repository
{
    public class AdScoreShowDbContext : DbContext
    {
        public AdScoreShowDbContext() : base("name=AdScoreDB")
        {

        }

        public DbSet<Market> Markets { get; set; }
        public DbSet<AdvertAired> AdvertAireds { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<Segment> Segments { get; set; }
        public DbSet<Brand> Brands { get; set; }

    }
}
