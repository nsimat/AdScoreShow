using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdScoreShow.DataModel.Models
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
        public string Copy_Name { get; set; }
        public string Copy_Duration { get; set; }

        public int SegmentId { get; set; }
        public Segment Segment { get; set; }
        public int BrandId { get; set; }
        public IEnumerable<AdvertAired> AdvertAireds { get; set; }
    }
}
