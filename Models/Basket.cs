using System.ComponentModel.DataAnnotations.Schema;

namespace VinylBack.Models
{
    public class Basket
    {
        public int BasketId { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public double TotalCost { get; set; }

        public ICollection<TrackInBasket> TracksInBasket { get; set; }

    }
}
