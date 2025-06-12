using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VinylBack.Models
{
    public class Track
    {
        [Key]
        public int TrackId { get; set; }
        public string? TrackName { get; set; }
        public string? TrackDuration { get; set; }
        public double? Price { get; set; }
        [ForeignKey("Album")]
        public int? AlbumId { get; set; }
        public Album? Album { get; set; }
        public string? TrackURL { get; set; }
        public ICollection<PurchasedTracks> PurchasedTracks { get; set; }
        public ICollection<TrackInBasket> TracksInBasket { get; set; }

    }
}
