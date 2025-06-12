using System.ComponentModel.DataAnnotations;

namespace VinylBack.Models
{
    public class Singer
    {
        [Key]
        public int SingerId { get; set; }
        public string? SingerFullName { get; set; }
        public string? SingerURL { get; set; }
        public ICollection<Album> Albums { get; set; }
    }
}
