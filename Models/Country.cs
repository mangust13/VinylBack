using System.ComponentModel.DataAnnotations;

namespace VinylBack.Models
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public ICollection<City> Cities { get; set; }

    }
}
