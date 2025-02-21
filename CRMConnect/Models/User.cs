using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMConnect.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }


        [Required, StringLength(50)]
        public string? Username { get; set; }

        [Required, EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? PasswordHash { get; set; } // Hashed password for security

        [Required, StringLength(20)]
        public string? Role { get; set; } // "Admin", "SalesRep"

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
