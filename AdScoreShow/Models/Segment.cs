using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdScoreShow.Models
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

        [StringLength(25)]
        public string Category { get; set; }
        public ICollection<Brand> Brands { get; set; }
        public ICollection<Advertisement> Advertisements { get; set; }
    }
}