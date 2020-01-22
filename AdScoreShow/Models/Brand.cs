using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdScoreShow.Models
{
    public class Brand
    {
        public Brand()
        {
            Advertisements = new HashSet<Advertisement>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        [StringLength(25)]        
        public string Name { get; set; }
        public int SegmentID { get; set; }
        public Segment Segment { get; set; }
        public ICollection<Advertisement> Advertisements { get; set; }
    }
}