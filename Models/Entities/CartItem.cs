using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SemesterProject.Models.Entities
{
    public class CartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartId { get; set; }

        [Required]
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        [Required]
        [ForeignKey("Menu")]
        public int MenuId { get; set; }

        public Menu Menu { get; set; }

        [Required]
        [Range(1, 100)]
        public int Quantity { get; set; } = 1;

        public DateTime AddedDate { get; set; } = DateTime.Now;
    }
}