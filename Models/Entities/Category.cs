using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SemesterProject.Models.Entities
{
    public class Category
    {
        [Key]
        [Required(ErrorMessage = "Category name is required")]
        [StringLength(100, ErrorMessage = "Category name cannot exceed 100 characters")]
        public string CategoryName { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-increment
        public int CategoryId { get; set; }

        
    }
}
