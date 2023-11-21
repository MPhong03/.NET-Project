using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNETProject.Shared
{
    public class DirectorDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string AvatarUrl { get; set; } = string.Empty;
        public DateTime? BirthDate { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string Nation { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ICollection<FilmDirectorDto> FilmDirectors { get; set; } = new List<FilmDirectorDto>();
        public override bool Equals(object o)
        {
            var other = o as DirectorDto;
            return other?.Name == Name;
        }

        public override int GetHashCode() => Name?.GetHashCode() ?? 0;
        public override string ToString() => Name;
    }
}
