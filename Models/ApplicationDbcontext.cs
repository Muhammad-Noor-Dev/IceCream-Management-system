using Microsoft.EntityFrameworkCore;
using SemesterProject.Models.Entities;

namespace SemesterProject.Models
{
    public class ApplicationDbcontext: DbContext
    {
        public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options) : base(options) { }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Orderitems> Orderitems { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }



    }
}
