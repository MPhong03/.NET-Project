using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNETProject.Server.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        [Required]
        public string UserName { get; set; } = string.Empty;
        [MaxLength(100)]
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }
        public string Otp { get; set; } = string.Empty;
        public DateTime ResetPasswordExpiry { get; set; }
        public bool IsPermit { get; set; } = false;
        public DateTime createdDate { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; } = new Role();
        public ICollection<Film> Films { get; set; }
    }
}
