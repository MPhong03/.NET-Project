using System.ComponentModel.DataAnnotations.Schema;

namespace DotNETProject.Shared
{
    public class MovieDto : FilmDto
    {
        public string Time { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
    }
}
