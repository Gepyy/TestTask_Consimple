using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestTask_Consimple.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask_Consimple.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            return await _context.Products
                .Select(p => new ProductDto
                {
                    IDProduct = p.IDProduct,
                    Name = p.Name,
                    Category = p.Category,
                    Article = p.Article,
                    Price = p.Price
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();
            return new ProductDto
            {
                IDProduct = product.IDProduct,
                Name = product.Name,
                Category = product.Category,
                Article = product.Article,
                Price = product.Price
            };
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct(ProductDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Category = dto.Category,
                Article = dto.Article,
                Price = dto.Price
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            dto.IDProduct = product.IDProduct;
            return CreatedAtAction(nameof(GetProduct), new { id = product.IDProduct }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductDto dto)
        {
            if (id != dto.IDProduct)
                return BadRequest();
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();
            product.Name = dto.Name;
            product.Category = dto.Category;
            product.Article = dto.Article;
            product.Price = dto.Price;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}