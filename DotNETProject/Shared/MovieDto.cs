using System.ComponentModel.DataAnnotations.Schema;

namespace DotNETProject.Shared
{
    public class MovieDto : FilmDto
    {
        public int Time { get; set; } = 0;
        public string Title { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
    }
}
