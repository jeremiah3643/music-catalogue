using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace band_catalogue.Models
{
    public class Album
    {
        [Key]
        public int AlbumId { get; set; }

        [Required]
        [Display(Name = "Album Title")]
        public string Title { get; set; }

        public int ReleaseYear { get; set; }

        // Foreign Key linking the Album to a Band
        [ForeignKey("Band")]
        public int BandId { get; set; }
        // Make nullable
        public Band? Band { get; set; }

        // One-to-Many: An Album can have multiple Songs
        public List<Song> Songs { get; set; } = new List<Song>();
    }
}
