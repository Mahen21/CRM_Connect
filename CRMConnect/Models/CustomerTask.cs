using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMConnect.Models
{
    public class CustomerTask
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        [Required, StringLength(255)]
        public string? Description { get; set; }

        [Required, StringLength(100)]
        public string? AssignedTo { get; set; } // User or Sales Rep

        public DateTime DueDate { get; set; }

        [Required, StringLength(50)]
        public string? Status { get; set; } // "Pending", "Completed"

        // Navigation Property
        public Customer? Customer { get; set; }
    }
}
