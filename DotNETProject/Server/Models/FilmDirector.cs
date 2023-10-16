using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DotNETProject.Server.Models
{
    [Table("filmdirectors")]
    public class FilmDirector
    {
        [Key]
        public int Id { get; set; }
        public int DirectorId { get; set; }
        [JsonIgnore]
        public Director Director { get; set; }
        public int FilmId { get; set; }
        [JsonIgnore]
        public Film Film { get; set; } = new Film();
    }
}
