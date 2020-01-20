using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdScoreShow.Models
{
    public class AdvertViewModel
    {
        public string Name { get; set; }
        public int Duration { get; set; }
        public string Brand { get; set; }
        public string Segment { get; set; }
        public int Year { get; set; }
        public int Score1 { get; set; }
        public int Score2 { get; set; }
    }
}