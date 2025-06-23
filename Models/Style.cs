using System.ComponentModel.DataAnnotations;

namespace VinylBack.Models
{
    public class Style
    {
        [Key]
        public int StyleId { get; set; }
        public string? StyleName { get; set; }
        public ICollection<Album> Albums { get; set; }
    }
}
