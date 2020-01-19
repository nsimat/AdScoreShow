using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdScoreShow.DataModel.Models
{
    public class AdvertAired
    {
        public string Copy_Name { get; set; }
        public int Copy_Duration { get; set; }
        public Advertisement Advertisement { get; set; }
        public int MarketId { get; set; }
        public Market Market { get; set; }
        public int? Year { get; set; }
        public int? Score_1 { get; set; }
        public int? Score_2 { get; set; }
    }
}
