using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_Commerce.API.Models.Domain;
using ECommerceSystem;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.ComponentModel.DataAnnotations.Schema;

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
            var orders = await _context.Orders.Include(o => o.OrderItem).ToListAsync();
            var json = JsonSerializer.Serialize(orders, JsonOptions);
            return Content(json, "application/json");
            
        }

        // GET: api/order/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItem)
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
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Ensure OrderItems reference the new Order
            foreach (var item in order.OrderItem)
            {
                item.OrderId = order.Id;  // Assign OrderId before saving
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }


        // PUT: api/order/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] Order order)
        {
            if (order == null)
            {
                return BadRequest(new { message = "Invalid request. Order data is required." });
            }

            if (id != order.Id)
            {
                return BadRequest(new { message = "The provided ID does not match the order ID." });
            }

            var existingOrder = await _context.Orders
                .Include(o => o.OrderItem)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (existingOrder == null)
            {
                return NotFound(new { message = "Order not found." });
            }

            _context.Entry(existingOrder).Reload(); // Reload to avoid concurrency issues

            existingOrder.CustomerName = order.CustomerName;
            existingOrder.OrderDate = order.OrderDate;

            // Remove old items
            _context.OrderItems.RemoveRange(existingOrder.OrderItem);

            // Add new items
            foreach (var item in order.OrderItem)
            {
                item.OrderId = existingOrder.Id; // Ensure correct FK mapping
                item.Id = 0; // Prevent conflicts
                _context.OrderItems.Add(item);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict(new { message = "A conflict occurred while updating the order." });
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
