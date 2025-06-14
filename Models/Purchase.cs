using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VinylBack.Models
{
    public class Purchase
    {
        [Key]
        public int PurchaseId { get; set; }

        [ForeignKey("AppUser")]
        public int UserId { get; set; }
        public AppUser User { get; set; }
        public DateTime PurchaseDate { get; set; }
        public double TotalAmount { get; set; }

        [ForeignKey("Status")]
        public int StatusId { get; set; }
        public PurchaseStatus Status { get; set; }

        [ForeignKey("Location")]
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public ICollection<PurchasedTrack> PurchasedTracks { get; set; }

    }
}
