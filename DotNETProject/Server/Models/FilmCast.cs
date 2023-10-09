using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNETProject.Server.Models
{
    public class FilmCast
    {
        [Key]
        public int Id { get; set; }
        public int FilmId { get; set; }
        public Film Film { get; set; }
        public int CastId { get; set; }
        public Cast Cast { get; set; }
        public string Role { get; set; }
    }
}
