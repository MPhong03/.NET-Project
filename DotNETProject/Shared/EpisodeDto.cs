﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNETProject.Shared
{
    public class EpisodeDto
    {
        public string EpisodeName { get; set; } = string.Empty;
        public string Time { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
        public long View { get; set; } = long.MinValue;
        public TVSeriesDto Series { get; set; } = new TVSeriesDto();
    }
}
