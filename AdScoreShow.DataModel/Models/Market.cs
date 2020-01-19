using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdScoreShow.DataModel.Models
{
    public class Market
    {
        public int MarketId { get; set; }
        public string Country { get; set; }
        public List<AdvertAired> AdvertAireds { get; set; }
    }
}
