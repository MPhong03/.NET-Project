using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DotNETProject.Server.Models
{
    [Table("tvseries")]
    public class TVSeries : Film
    {
        [JsonIgnore]
        public ICollection<Episode> episodes { get; set; } = new List<Episode>();
    }
}
