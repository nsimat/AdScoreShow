using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdScoreShow.Models
{
    public class AdvertAired
    {
        [Key, Column(Order = 0)]
        public int AdvertisementID { get; set; }
        public Advertisement Advertisement { get; set; }      

        [Key, Column(Order = 1)]
        public int MarketID { get; set; }
        public Market Market { get; set; }

        public int? Year { get; set; }
        public int? Score_1 { get; set; }
        public int? Score_2 { get; set; }
    }
}