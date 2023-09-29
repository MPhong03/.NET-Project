﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNETProject.Server.Models
{
    [Table("films")]
    public class Film
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ReleaseYear { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double IMDBScore { get; set; } = double.MinValue;
        public long View { get; set; } = long.MinValue;
        public Film()
        {
            
        }
        
    }
}
