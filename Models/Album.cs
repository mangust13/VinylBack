using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VinylBack.Models
{
    public class Album
    {
        [Key]
        public int AlbumId {  get; set; }
        public int? ReleaseYer { get; set; }
        public string? ReleaseCounty { get; set; }
        public string? Lable { get; set; }
        public string? Genre { get; set; }
        public string? Style { get; set; }

        [ForeignKey("Singer")]
        public int? SingerId { get; set; }
        public Singer? Singer { get; set; }
        public string? AlbumURL { get; set; }
        public ICollection<Track> Tracks { get; set; }
    }
}
