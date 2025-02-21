using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMConnect.Models
{
    public class Customer
    {
        [Key]
        [Column("CustomerId")]  // Map to the existing column
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string? Name { get; set; }

        [Required, EmailAddress]
        public string? Email { get; set; }

        [Required, StringLength(15)]
        public string? Phone { get; set; }

        [StringLength(100)]
        public string? Company { get; set; }

        [StringLength(255)]
        public string? Address { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public ICollection<SalesActivity>? SalesActivities { get; set; }
        public ICollection<CustomerTask>? Tasks { get; set; }
        public ICollection<CommunicationLog>? CommunicationLogs { get; set; }
    }
}
