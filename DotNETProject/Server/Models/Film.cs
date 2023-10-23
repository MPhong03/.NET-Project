using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNETProject.Server.Models
{
    [Table("films")]
    public class Film
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ReleaseYear { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double IMDBScore { get; set; } = double.MinValue;
        public long View { get; set; } = long.MinValue;
        public string PosterUrl { get; set; } = string.Empty;
        public string BackgroundUrl { get; set; } = string.Empty;
        public string LogoUrl { get; set; } = string.Empty;
        public ICollection<FilmCast> FilmCasts { get; set; } = new List<FilmCast>();
        public ICollection<FilmDirector> FilmDirectors { get; set; } = new List<FilmDirector>();
        public ICollection<FilmGenre> FilmGenres { get; set; } = new List<FilmGenre>();
        public string TrailerUrl { get; set; } = string.Empty;
        
    }
}
