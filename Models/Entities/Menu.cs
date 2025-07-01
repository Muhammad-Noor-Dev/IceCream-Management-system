using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SemesterProject.Models.Entities
{
    public class Menu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-increment
        public int Mid { get; set; } // Menu ID

        [Required(ErrorMessage = "Menu item name is required")]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 9999.99, ErrorMessage = "Price must be a positive number")]
        public int Price { get; set; }

        [StringLength(255)]
        public byte[] Image { get; set; } // Image file name or path

        // Foreign Key to Category
        [Required]
        [ForeignKey("Category")]
        public string CategoryName { get; set; }

        // Navigation Property
        public Category Category { get; set; }

        // Status: Active or Inactive
        [Required]
        [StringLength(50)]
        public string Status { get; set; } // Can be "Active" or "Inactive"
    }
    }
