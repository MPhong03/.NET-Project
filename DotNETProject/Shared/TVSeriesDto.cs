using System.ComponentModel.DataAnnotations.Schema;

namespace DotNETProject.Shared
{
    public class TVSeriesDto : FilmDto
    {
        public ICollection<EpisodeDto> episodes { get; set; } = new List<EpisodeDto>();
    }
}
