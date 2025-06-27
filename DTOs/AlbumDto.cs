using VinylBack.DTOs;

public class AlbumDto
{
    public int AlbumId { get; set; }
    public string? AlbumName { get; set; }
    public int? ReleaseYear { get; set; }
    public string? AlbumURL { get; set; }

    public SingerDto? Singer { get; set; }
    public CountryDto? ReleaseCountry { get; set; }
    public LableDto? Lable { get; set; }
    public GenreDto? Genre { get; set; }
    public StyleDto? Style { get; set; }

    public List<TrackDto> Tracks { get; set; } = new();
}
