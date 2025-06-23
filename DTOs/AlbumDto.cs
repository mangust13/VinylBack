namespace VinylBack.DTOs
{
    public class AlbumDto
    {
        public int AlbumId { get; set; }
        public string? AlbumName { get; set; }
        public int? ReleaseYear { get; set; }
        public string? AlbumURL { get; set; }

        public int? SingerId { get; set; }
        public int? ReleaseCountryId { get; set; }
        public int? LableId { get; set; }
        public int? GenreId { get; set; }
        public int? StyleId { get; set; }
    }
}
