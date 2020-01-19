using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdScoreShow.DataModel.Models
{
    public class Segment
    {
        public int SegmentId { get; set; }
        public string Category { get; set; }
        public List<Brand> Brands { get; set; }
        public List<Advertisement> Advertisements { get; set; }
    }
}
