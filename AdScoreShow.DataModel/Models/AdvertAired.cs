using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdScoreShow.DataModel.Models
{
    public class AdvertAired
    {
        public int AdvertisementId { get; set; }
        public Advertisement Advertisement { get; set; }

        //The couple (Copy_Name, Copy_Duration) is unique in the database
        public string Copy_Name { get; set; }
        public int Copy_Duration { get; set; }
        
        public int MarketId { get; set; }
        public Market Market { get; set; }
        public int? Year { get; set; }
        public int? Score_1 { get; set; }
        public int? Score_2 { get; set; }
    }
}
