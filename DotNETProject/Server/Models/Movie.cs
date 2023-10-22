using System.ComponentModel.DataAnnotations.Schema;

namespace DotNETProject.Server.Models
{
    [Table("movies")]
    public class Movie : Film
    {
        public string Time { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
    }
}
