using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace band_catalogue.Models
{
    public class Band
    {
        [Key]
        public int BandId { get; set; }

        [Required]
        [Display(Name = "Band Name")]
        public string BandName { get; set; }

        public string Genre { get; set; }
        public string Country { get; set; }
        public int FormedYear { get; set; }

        public List<Album> Albums { get; set; } = new List<Album>();
    }
}
