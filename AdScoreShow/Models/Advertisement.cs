using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdScoreShow.Models
{
    public class Advertisement
    {
        public Advertisement()
        {
            AdvertAireds = new HashSet<AdvertAired>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //The couple (Copy_Name, Copy_Duration) is unique in the database
        [Index("SameAdvert", 1, IsUnique = true)]
        public string Copy_Name { get; set; }
        [Index("SameAdvert", 2, IsUnique = true)]
        public string Copy_Duration { get; set; }

        public int SegmentID { get; set; }
        public Segment Segment { get; set; }
        public int BrandID { get; set; }
        public ICollection<AdvertAired> AdvertAireds { get; set; }
    }
}