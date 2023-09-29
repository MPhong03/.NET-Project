using System.ComponentModel.DataAnnotations.Schema;

namespace DotNETProject.Server.Models
{
    [Table("movies")]
    public class Movie : Film
    {
        public int Time { get; set; } = 0;
        public string Title { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
    }
}
