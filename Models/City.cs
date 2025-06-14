using System.ComponentModel.DataAnnotations.Schema;

namespace VinylBack.Models
{
    public class City
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        [ForeignKey("County")]
        public int CountyId { get; set; }
        public Country County { get; set; }
        public ICollection<Location> Locations { get; set; }

    }
}
