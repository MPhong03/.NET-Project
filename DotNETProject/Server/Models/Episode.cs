using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNETProject.Server.Models
{
    [Table("episodes")]
    public class Episode
    {
        [Key]
        public int Id { get; set; }
        public string EpisodeName { get; set; } = string.Empty;
        public string Time { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
        public long View { get; set; } = long.MinValue;
        public TVSeries Series { get; set; } = new TVSeries();
    }
}
