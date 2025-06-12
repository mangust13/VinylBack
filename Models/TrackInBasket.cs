using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VinylBack.Models
{
    public class TrackInBasket
    {
        [Key]
        public int TrackInBasketId { get; set; }

        [ForeignKey("Basket")]
        public int BasketId { get; set; }
        public Basket Basket { get; set; }

        [ForeignKey("Track")]
        public int TrackId { get; set; }
        public Track Track {get; set; }
    }
}
