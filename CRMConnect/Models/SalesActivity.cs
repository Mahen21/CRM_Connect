using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMConnect.Models
{
    public class SalesActivity
    {
        [Key]
        [Column("SalesActivityId")]  // Map to the existing column
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SalesActivityId { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        [Required, StringLength(100)]
        public string? Product { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required, StringLength(50)]
        public string? Status { get; set; } // "Pending", "Completed", "Failed"

        public DateTime Date { get; set; } = DateTime.UtcNow;

        // Navigation Property
        public Customer? Customer { get; set; }
    }
}
