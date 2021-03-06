﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdScoreShow.DataModel.Models
{
    public class Brand
    {
        public Brand()
        {
            Advertisements = new HashSet<Advertisement>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string BrandName { get; set; }
        public int SegmentId { get; set; }
        public Segment Segment { get; set; }
        public IEnumerable<Advertisement> Advertisements { get; set; }
    }
}
