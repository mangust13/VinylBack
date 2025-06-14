namespace VinylBack.DTOs
{
    public class TrackDto
    {
        public int TrackId { get; set; }
        public string? TrackName { get; set; }
        public string? TrackDuration { get; set; }
        public double? Price { get; set; }
        public int? AlbumId { get; set; }
        public string? TrackURL { get; set; }
    }
}
