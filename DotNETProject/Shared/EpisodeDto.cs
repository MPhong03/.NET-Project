using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNETProject.Shared
{
    public class EpisodeDto
    {
        public int Id { get; set; }
        public string EpisodeName { get; set; } = string.Empty;
        public string Time { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
        public long View { get; set; } = 0;
        public TVSeriesDto Series { get; set; } = new TVSeriesDto();
    }
}
