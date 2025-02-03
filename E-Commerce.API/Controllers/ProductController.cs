using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_Commerce.API.Models.Domain;
using ECommerceSystem;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.API.Controllers
{
    //https://localhost:xxxx/api/product
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

        // GET: api/product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            // Return all products from the database
            return await _context.Products.ToListAsync();
        }

        // GET: api/product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            // Find the product by ID
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound(); // Return 404 if the product is not found
            }

            return product; // Return the product
        }

        // POST: api/product
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            // Add the new product to the database
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Return the created product with a 201 status code
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        // PUT: api/product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest(); // Return 400 if the ID in the URL doesn't match the product ID
            }

            // Mark the product as modified and save changes
            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Products.Any(p => p.Id == id))
                {
                    return NotFound(); // Return 404 if the product doesn't exist
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); // Return 204 (No Content) for successful updates
        }

        // DELETE: api/product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            // Find the product by ID
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound(); // Return 404 if the product is not found
            }

            // Remove the product from the database
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent(); // Return 204 (No Content) for successful deletion
        }
    }
}