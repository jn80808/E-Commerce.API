using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_Commerce.API.Models.Domain;
using ECommerceSystem;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly ECommerceDbContext _context;

        public ProductCategoryController(ECommerceDbContext context)
        {
            _context = context;
        }

        // GET: api/productcategory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategory>>> GetProductCategories()
        {
            return await _context.ProductCategories.ToListAsync();
        }

        // GET: api/productcategory/5
        [HttpGet("{productId}/{categoryId}")]
        public async Task<ActionResult<ProductCategory>> GetProductCategory(int productId, int categoryId)
        {
            var productCategory = await _context.ProductCategories
                .FirstOrDefaultAsync(pc => pc.ProductId == productId && pc.CategoryId == categoryId);

            if (productCategory == null)
            {
                return NotFound();
            }

            return productCategory;
        }

        // POST: api/productcategory
        [HttpPost]
        public async Task<ActionResult<ProductCategory>> CreateProductCategory(ProductCategory productCategory)
        {
            _context.ProductCategories.Add(productCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductCategory), new { productId = productCategory.ProductId, categoryId = productCategory.CategoryId }, productCategory);
        }

        // DELETE: api/productcategory/5
        [HttpDelete("{productId}/{categoryId}")]
        public async Task<IActionResult> DeleteProductCategory(int productId, int categoryId)
        {
            var productCategory = await _context.ProductCategories
                .FirstOrDefaultAsync(pc => pc.ProductId == productId && pc.CategoryId == categoryId);

            if (productCategory == null)
            {
                return NotFound();
            }

            _context.ProductCategories.Remove(productCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}