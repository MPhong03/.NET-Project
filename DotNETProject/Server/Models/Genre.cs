using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DotNETProject.Server.Models
{
    [Table("genres")]
    public class Genre
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        [JsonIgnore]
        public ICollection<FilmGenre> FilmGenres { get; set; } = new List<FilmGenre>();
    }
}
