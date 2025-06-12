using System.ComponentModel.DataAnnotations;

namespace VinylBack.Models
{
    public class County
    {
        [Key]
        public int CountyId { get; set; }
        public string CountryName { get; set; }
        public ICollection<City> Cities { get; set; }

    }
}
