using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdScoreShow.DataModel.Models
{
    public class Brand
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public int SegmentId { get; set; }
        public Segment Segment { get; set; }
        public List<Advertisement> Advertisements { get; set; }
    }
}
