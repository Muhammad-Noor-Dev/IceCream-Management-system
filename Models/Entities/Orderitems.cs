using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SemesterProject.Models.Entities
{
    public class Orderitems
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-increment for ItemId
        public int ItemId { get; set; } // Primary Key for the OrderItem

        // Foreign Key: OrderId (from Order table)
        [Required]
        [ForeignKey("Order")]
        public int OrderId { get; set; } // Foreign Key to the Order table

        // Foreign Key: Mid (from Menu table)
        [Required]
        [ForeignKey("Menu")]
        public int Mid { get; set; } // Foreign Key to the Menu table

        // Navigation property to access the related Order
        public Order Order { get; set; }

        // Navigation property to access the related Menu item
        public Menu Menu { get; set; }
    }
}
