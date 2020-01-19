using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdScoreShow.Models
{
    public class Market
    {
        public Market()
        {
            AdvertAireds = new HashSet<AdvertAired>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Country { get; set; }
        public ICollection<AdvertAired> AdvertAireds { get; set; }
    }
}