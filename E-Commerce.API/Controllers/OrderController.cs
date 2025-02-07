using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_Commerce.API.Models.Domain;
using ECommerceSystem;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ECommerceDbContext _context;

        // Inject the DbContext into the controller
        public OrderController(ECommerceDbContext context)
        {
            _context = context;
        }
        // Create the JsonSerializerOptions once
        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve
        };

        // GET: api/order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var orders = await _context.Orders.Include(o => o.OrderItems).ToListAsync();
            var json = JsonSerializer.Serialize(orders, JsonOptions);
            return Content(json, "application/json");
            
        }

        // GET: api/order/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound(new { message = "Order not found." });
            }
            var json = JsonSerializer.Serialize(order, JsonOptions);
            return Content(json, "application/json");


        }

        // POST: api/order
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(Order order)
        {
            // Optionally, you can validate if the order has items
            if (order.OrderItems == null || !order.OrderItems.Any())
            {
                return BadRequest(new { message = "Order must have at least one item." });
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }

        // PUT: api/order/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest(new { message = "The provided ID does not match the order ID." });
            }

            var existingOrder = await _context.Orders.FindAsync(id);
            if (existingOrder == null)
            {
                return NotFound(new { message = "Order not found." });
            }

            // Update order fields
            existingOrder.CustomerName = order.CustomerName;
            //existingOrder.Status = order.Status;
            existingOrder.OrderItems = order.OrderItems; // Assuming the entire list of order items needs to be replaced

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Orders.Any(o => o.Id == id))
                {
                    return NotFound(new { message = "Order not found." });
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { message = "Order updated successfully.", order = existingOrder });
        }

        // DELETE: api/order/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound(new { message = "Order not found." });
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Order deleted successfully." });
        }
    }
}
