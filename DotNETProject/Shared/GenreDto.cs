using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNETProject.Shared
{
    public class GenreDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<FilmGenreDto> FilmGenres { get; set; } = new List<FilmGenreDto>();
    }
}
