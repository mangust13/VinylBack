using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VinylBack.Models
{
    public class Purchase
    {
        [Key]
        public int PurchaseId { get; set; }

        [ForeignKey("Client")]
        public int ClientId {get; set; }
        public Client Client { get; set; }
        public DateTime PurchaseDate { get; set; }
        public double TotalAmount { get; set; }

        [ForeignKey("Status")]
        public int StatusId { get; set; }
        public PurchaseStatus Status { get; set; }

        [ForeignKey("Location")]
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public ICollection<PurchasedTracks> PurchasedTracks { get; set; }

    }
}
