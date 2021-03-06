﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdScoreShow.DataModel.Models
{
    public class Segment
    {
        public Segment()
        {
            Brands = new HashSet<Brand>();
            Advertisements = new HashSet<Advertisement>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Category { get; set; }
        public IEnumerable<Brand> Brands { get; set; }
        public IEnumerable<Advertisement> Advertisements { get; set; }
    }
}
