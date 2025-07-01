using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SemesterProject.Models.Entities
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-increment for OrderId
        public int OrderId { get; set; } // Order ID

        // Foreign Key: Customer ID (using "Id" as the foreign key name)
        [Required]
        [ForeignKey("Customer")]
        public int Id { get; set; } // Foreign Key to the Customer table

        // Navigation property to access the associated Customer
        public Customer Customer { get; set; }

        // Order date set automatically
        [Required]
        public DateTime OrderDate { get; set; } = DateTime.Now; // Default to current date/time

        // Order Amount
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public int Amount { get; set; }

        // Order Status (e.g., Pending, Shipped, Delivered, etc.)
        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        // Shipping Address
        [Required]
        [StringLength(500)]
        public string Address { get; set; }
    }
}
