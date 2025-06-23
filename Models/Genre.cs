using System.ComponentModel.DataAnnotations;

namespace VinylBack.Models
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }
        public string? GenreName { get; set; }
        public ICollection<Album> Albums { get; set; }
    }
}
