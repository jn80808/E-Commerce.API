using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_Commerce.API.Models.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ECommerceSystem;
using System.Text.Json.Serialization;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ECommerceDbContext _context;

        // Inject the DbContext into the controller
        public ProductController(ECommerceDbContext context)
        {
            _context = context;
        }

        // Create the JsonSerializerOptions once
        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve
        };

        // GET: api/product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            var json = JsonSerializer.Serialize(products, JsonOptions);
            return Content(json, "application/json");
        }

        // GET: api/product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound(); // Return 404 if the product is not found
            }

            var json = JsonSerializer.Serialize(product, JsonOptions);
            return Content(json, "application/json");
        }

        // POST: api/product
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            var existingProduct = await _context.Products
                .FirstOrDefaultAsync(p => p.Name == product.Name);

            if (existingProduct != null)
            {
                return BadRequest(new { message = "Product already exists." });
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var json = JsonSerializer.Serialize(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, json);
        }



        // PUT: api/product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest(new { message = "The provided ID does not match the product ID." });
            }

            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null)
            {
                return NotFound(new { message = "Product not found." });
            }

            // Update product fields
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.StockQuantity = product.StockQuantity;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict(new { message = "Conflict detected while updating the product" });
            }

            return Ok(existingProduct);
        }


        // DELETE: api/product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound(new { message = "Product not found." }); // Return 404 if the product is not found
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Product deleted successfully." }); // Return a success message
        }

    }
}
