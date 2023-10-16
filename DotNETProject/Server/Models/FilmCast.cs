using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DotNETProject.Server.Models
{
    [Table("filmcasts")]
    public class FilmCast
    {
        [Key]
        public int Id { get; set; }
        public int FilmId { get; set; }
        [JsonIgnore]
        public Film Film { get; set; } = new Film();
        public int CastId { get; set; }
        [JsonIgnore]
        public Cast Cast { get; set; } = new Cast();
        public string Role { get; set; }
    }
}
