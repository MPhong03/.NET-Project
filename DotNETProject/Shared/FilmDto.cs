using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNETProject.Shared
{
    public class FilmDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ReleaseYear { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double IMDBScore { get; set; } = 0;
        public long View { get; set; } = 0;
        public string PosterUrl { get; set; } = string.Empty;
        public string BackgroundUrl { get; set; } = string.Empty;
        public string LogoUrl { get; set; } = string.Empty;
        public ICollection<FilmCastDto> FilmCasts { get; set; } = new List<FilmCastDto>();
        public ICollection<FilmDirectorDto> FilmDirectors { get; set; } = new List<FilmDirectorDto>();
        public ICollection<FilmGenreDto> FilmGenres { get; set; } = new List<FilmGenreDto>();
        public string TrailerUrl { get; set; } = string.Empty;
        public bool isActiveBanner { get; set; } = false;
    }
}
