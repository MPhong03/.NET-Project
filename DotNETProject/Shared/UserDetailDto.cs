using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNETProject.Shared
{
    public class UserDetailDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime createdDate { get; set; }
        public string RoleName { get; set; }
        public ICollection<FilmDto> SavedFilms { get; set; } = new List<FilmDto>();
    }
}
