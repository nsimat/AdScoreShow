using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdScoreShow.DataModel.Models
{
    public class Advertisement
    {
        public int AdvertId { get; set; }
        public string Copy_Name { get; set; }
        public string Copy_Duration { get; set; }
        public int SegmentId { get; set; }
        public Segment Segment { get; set; }
        public int BrandId { get; set; }
        public List<AdvertAired> AdvertAireds { get; set; }
    }
}
