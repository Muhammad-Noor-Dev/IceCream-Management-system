using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SemesterProject.Models.Entities
{
    public class AddMenu
    {
        
            [Required(ErrorMessage = "Menu item name is required")]
            [StringLength(100)]
            public string Name { get; set; }

            [StringLength(500)]
            public string Description { get; set; }

            [Required(ErrorMessage = "Price is required")]
            [Range(0.01, 9999.99, ErrorMessage = "Price must be a positive number")]
            public int Price { get; set; }

            [Required(ErrorMessage = "Image is required")]
            public IFormFile ImageFile { get; set; }

            [Required(ErrorMessage = "Category is required")]
            [ForeignKey("Category")]
            public string CategoryName { get; set; }

            [Required(ErrorMessage = "Status is required")]
            [StringLength(50)]
            public string Status { get; set; } = "Active";
        }
    }

