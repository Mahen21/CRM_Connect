using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMConnect.Models
{
    public class CommunicationLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; } // The sales rep who logged the communication

        [Required, StringLength(50)]
        public string? CommunicationType { get; set; } // "Email", "Call", "Meeting"

        [StringLength(500)]
        public string? Notes { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public virtual Customer? Customer { get; set; }
        public virtual User? User { get; set; }
    }
}
