using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DotNETProject.Server.Models
{
    [Table("casts")]
    public class Cast
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string AvatarUrl { get; set; } = string.Empty;
        public DateTime? BirthDate { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string Nation { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [JsonIgnore]
        public ICollection<FilmCast> FilmCasts { get; set; } = new List<FilmCast>();
    }
}
