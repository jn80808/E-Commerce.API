using System.ComponentModel.DataAnnotations;

namespace E_Commerce.API.Models.Domain
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string CustomerName { get; set; }

        public DateTime OrderDate { get; set; }

        // One-to-Many Relationship with OrderItem
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
