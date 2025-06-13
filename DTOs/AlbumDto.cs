using VinylBack.DTOs;

namespace VinylBack.DTOs
{
    public class AlbumDto
    {
        public int AlbumId { get; set; }
        public int? ReleaseYear { get; set; }
        public string? ReleaseCountry { get; set; }
        public string? Lable { get; set; }
        public string? Genre { get; set; }
        public string? Style { get; set; }
        public int? SingerId { get; set; }
        public string? AlbumURL { get; set; }
    }
}
