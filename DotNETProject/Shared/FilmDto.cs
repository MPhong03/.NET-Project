using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNETProject.Shared
{
    public class FilmDto
    {
        public string Name { get; set; } = string.Empty;
        public string ReleaseYear { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double IMDBScore { get; set; } = double.MinValue;
        public long View { get; set; } = long.MinValue;
        public string PosterUrl { get; set; } = string.Empty;
        public string BackgroundUrl { get; set; } = string.Empty;
        public string LogoUrl { get; set; } = string.Empty;
        public ICollection<FilmCastDto> FilmCasts { get; set; } = new List<FilmCastDto>();
        public ICollection<FilmDirectorDto> FilmDirectors { get; set; } = new List<FilmDirectorDto>();
        public string TrailerUrl { get; set; } = string.Empty;
        
    }
}
