using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace band_catalogue.Models
{
    public class Song
    {
        [Key]
        public int SongId { get; set; }

        [Required]
        [Display(Name = "Song Title")]
        public string Title { get; set; }

        public TimeSpan Duration { get; set; }

        // Foreign Key linking this song to an Album
        [ForeignKey("Album")]
        public int AlbumId { get; set; }
        public Album? Album { get; set; }
    }
}
