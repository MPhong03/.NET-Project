using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNETProject.Shared
{
    public class FilmDirectorDto
    {
        public int DirectorId { get; set; }
        public DirectorDto Director { get; set; }
        public int FilmId { get; set; }
        public FilmDto Film { get; set; } = new FilmDto();
    }
}
