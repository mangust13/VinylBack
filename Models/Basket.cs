using System.ComponentModel.DataAnnotations.Schema;

namespace VinylBack.Models
{
    public class Basket
    {
        public int BasketId { get; set; }

        [ForeignKey("AppUser")]
        public int UserId { get; set; }
        public AppUser User { get; set; }
        public double TotalCost { get; set; }

        public ICollection<TrackInBasket> TracksInBasket { get; set; }

    }
}
