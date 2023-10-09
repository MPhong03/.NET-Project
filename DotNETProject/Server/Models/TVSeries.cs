using System.ComponentModel.DataAnnotations.Schema;

namespace DotNETProject.Server.Models
{
    [Table("tvseries")]
    public class TVSeries : Film
    {
        public ICollection<Episode> episodes { get; set; }
    }
}
