public class SingerDto
{
    public int SingerId { get; set; }
    public string SingerFullName { get; set; }
    public string? SingerURL { get; set; }

    public List<AlbumDto> Albums { get; set; } = new();
}
