using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SemesterProject.Models.Entities
{
    public class Contact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContactId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [StringLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Number is required")]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        [StringLength(15)]
        public string Number { get; set; }

        [Required(ErrorMessage = "Topic is required")]
        [StringLength(100)]
        public string Topic { get; set; }

        [Required(ErrorMessage = "Message is required")]
        [StringLength(1000)]
        public string Message { get; set; }
    }
}
