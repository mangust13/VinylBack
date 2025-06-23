using System.ComponentModel.DataAnnotations;

namespace VinylBack.Models
{
    public class Lable
    {
        [Key]
        public int LableId { get; set; }
        public string? LableName { get; set; }
        public ICollection<Album> Albums { get; set; }
    }
}
