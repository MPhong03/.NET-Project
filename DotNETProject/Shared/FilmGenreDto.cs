using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNETProject.Shared
{
    public class FilmGenreDto
    {
        public int Id { get; set; }
        public FilmDto Film { get; set; } = new FilmDto();
        public GenreDto Genre { get; set; } = new GenreDto();

    }
}
