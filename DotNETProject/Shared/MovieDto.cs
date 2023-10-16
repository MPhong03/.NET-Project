using System.ComponentModel.DataAnnotations.Schema;

namespace DotNETProject.Shared
{
    public class MovieDto : FilmDto
    {
        public int Id { get; set; }
        public string Time { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
    }
}
