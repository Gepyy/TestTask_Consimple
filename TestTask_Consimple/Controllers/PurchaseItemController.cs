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
    public class PurchaseItemController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public PurchaseItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchaseItemDto>>> GetPurchaseItems()
        {
            return await _context.PurchaseItems
                .Select(pi => new PurchaseItemDto
                {
                    ID = pi.ID,
                    PurchaseNumber = pi.PurchaseNumber,
                    IDProduct = pi.IDProduct,
                    Quantity = pi.Quantity,
                    PricePerUnit = pi.PricePerUnit
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseItemDto>> GetPurchaseItem(int id)
        {
            var pi = await _context.PurchaseItems.FindAsync(id);
            if (pi == null)
                return NotFound();
            return new PurchaseItemDto
            {
                ID = pi.ID,
                PurchaseNumber = pi.PurchaseNumber,
                IDProduct = pi.IDProduct,
                Quantity = pi.Quantity,
                PricePerUnit = pi.PricePerUnit
            };
        }

        [HttpPost]
        public async Task<ActionResult<PurchaseItemDto>> CreatePurchaseItem(PurchaseItemDto dto)
        {
            var pi = new PurchaseItem
            {
                PurchaseNumber = dto.PurchaseNumber,
                IDProduct = dto.IDProduct,
                Quantity = dto.Quantity,
                PricePerUnit = dto.PricePerUnit
            };
            _context.PurchaseItems.Add(pi);
            await _context.SaveChangesAsync();
            dto.ID = pi.ID;
            return CreatedAtAction(nameof(GetPurchaseItem), new { id = pi.ID }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePurchaseItem(int id, PurchaseItemDto dto)
        {
            if (id != dto.ID)
                return BadRequest();
            var pi = await _context.PurchaseItems.FindAsync(id);
            if (pi == null)
                return NotFound();
            pi.PurchaseNumber = dto.PurchaseNumber;
            pi.IDProduct = dto.IDProduct;
            pi.Quantity = dto.Quantity;
            pi.PricePerUnit = dto.PricePerUnit;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchaseItem(int id)
        {
            var pi = await _context.PurchaseItems.FindAsync(id);
            if (pi == null)
                return NotFound();
            _context.PurchaseItems.Remove(pi);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}