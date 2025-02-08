using E_Commerce.API.Models.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

[Table("OrderItem")]
public class OrderItem
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Order")]
    public int OrderId { get; set; }

    [JsonIgnore]
    public Order? Order { get; set; } // Make nullable to avoid validation errors

    [ForeignKey("Product")]
    public int ProductId { get; set; }

    [JsonIgnore]
    public Product? Product { get; set; } // Make nullable to avoid validation errors

    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
    public int Quantity { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal UnitPrice { get; set; }
}
