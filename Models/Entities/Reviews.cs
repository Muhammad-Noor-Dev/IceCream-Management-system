using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SemesterProject.Models.Entities
{
    public class Reviews
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-increment for ReviewId
        public int ReviewId { get; set; } // Primary Key for the Review

        // User's Email (who is submitting the review)
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        // Stars for the review (Rating from 1 to 5)
        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5 stars")]
        public int Stars { get; set; }

        // The name of the product being reviewed
        [Required]
        [StringLength(200, ErrorMessage = "Product name cannot exceed 200 characters")]
        public string ProductName { get; set; }

        // Title of the review
        [Required]
        [StringLength(200, ErrorMessage = "Review title cannot exceed 200 characters")]
        public string ReviewTitle { get; set; }

        // The actual review statement/content
        [Required]
        [StringLength(1000, ErrorMessage = "Review statement cannot exceed 1000 characters")]
        public string ReviewStatement { get; set; }

        // Images associated with the review (can store file paths or URLs to images)
        public string ImageUrls { get; set; } // Store image paths/URLs as a string

        // Optionally, you can add the date when the review was submitted
        public DateTime CreatedAt { get; set; } = DateTime.Now; // Default to current date/time
    }
}
