using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VinylBack.Models
{
    public class PurchasedTrack
    {
        [Key]
        public int PurchasedTrackId { get; set; }

        [ForeignKey("Purchase")]
        public int PurchaseId { get; set; }
        public Purchase Purchase { get; set; }

        [ForeignKey("Track")]
        public int TrackId { get; set; }
        public Track Track { get; set; }
    }
}
