using System.ComponentModel.DataAnnotations.Schema;

namespace VinylBack.Models
{
    public class City
    {
        public int CityId { get; set; }
        public string CityName { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }
        public Country Country { get; set; }

        public ICollection<Location> Locations { get; set; }
    }

}
