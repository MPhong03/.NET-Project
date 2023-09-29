using System.ComponentModel.DataAnnotations.Schema;

namespace DotNETProject.Server.Models
{
    [Table("tvseries")]
    public class TVSeries : Film
    {
        public List<Episode> episodes { get; set; } = new List<Episode>();
    }
}
