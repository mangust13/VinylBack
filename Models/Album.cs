using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VinylBack.Models
{
    public class Album
    {
        [Key]
        public int AlbumId {  get; set; }
        public string? AlbumName { get; set; }
        public int? ReleaseYear { get; set; }
        public string? AlbumURL { get; set; }

        [ForeignKey("Singer")]
        public int? SingerId { get; set; }
        public Singer? Singer { get; set; }

        [ForeignKey("ReleaseCountry")]
        public int? ReleaseCountryId { get; set; }
        public Country? ReleaseCountry { get; set; }

        [ForeignKey("Lable")]
        public int? LableId { get; set; }
        public Lable? Lable { get; set; }

        [ForeignKey("Style")]
        public int? StyleId { get; set; }
        public Style? Style { get; set; }

        [ForeignKey("Genre")]
        public int? GenreId { get; set; }
        public Genre? Genre { get; set; }
        public ICollection<Track> Tracks { get; set; }
    }
}
