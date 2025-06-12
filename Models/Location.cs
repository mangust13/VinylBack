using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VinylBack.Models
{
    public class Location
    {
        [Key]
        public int LocationId { get; set; }
        public string LocationName {  get; set; }
        [ForeignKey("City")]
        public int CityId { get; set; }
        public City City { get; set; }
        public ICollection<Purchase> Purchases { get; set; }

    }
}
