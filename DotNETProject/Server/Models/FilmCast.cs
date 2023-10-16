﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNETProject.Server.Models
{
    [Table("filmcasts")]
    public class FilmCast
    {
        [Key]
        public int Id { get; set; }
        public int FilmId { get; set; }
        public Film Film { get; set; } = new Film();
        public int CastId { get; set; }
        public Cast Cast { get; set; } = new Cast();
        public string Role { get; set; }
    }
}