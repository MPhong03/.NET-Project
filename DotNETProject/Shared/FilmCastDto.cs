using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNETProject.Shared
{
    public class FilmCastDto
    {
        public FilmDto Film { get; set; } = new FilmDto();
        public CastDto Cast { get; set; } = new CastDto();
        public string Role { get; set; }
    }
}
