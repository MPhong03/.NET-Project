using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DotNETProject.Server.Models
{
    [Table("filmgenres")]
    public class FilmGenre
    {
        [Key]
        public int Id { get; set; }
        public int FilmId { get; set; }
        [JsonIgnore]
        public Film Film { get; set; } = new Film();
        public int GenreId { get; set; }
        [JsonIgnore]
        public Genre Genre { get; set; } = new Genre();
    }
}
