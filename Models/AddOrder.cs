using SemesterProject.Models.Entities;

namespace SemesterProject.Models
{
    public class AddOrder
    {
        public List<CartItem> CartItems { get; set; }
        public string Address { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
