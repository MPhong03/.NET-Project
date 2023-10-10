using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNETProject.Server.Models
{
    [Table("filmdirectors")]
    public class FilmDirector
    {
        [Key]
        public int Id { get; set; }
        public int DirectorId { get; set; }
        public Director Director { get; set; }
        public int FilmId { get; set; }
        public Film Film { get; set; } = new Film();
    }
}
