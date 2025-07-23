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
    public class PurchaseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public PurchaseController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchaseDto>>> GetPurchases()
        {
            return await _context.Purchases
                .Select(p => new PurchaseDto
                {
                    Number = p.Number,
                    IDClient = p.IDClient,
                    Date = p.Date,
                    Sum = p.Sum
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseDto>> GetPurchase(int id)
        {
            var purchase = await _context.Purchases.FindAsync(id);
            if (purchase == null)
                return NotFound();
            return new PurchaseDto
            {
                Number = purchase.Number,
                IDClient = purchase.IDClient,
                Date = purchase.Date,
                Sum = purchase.Sum
            };
        }

        [HttpPost]
        public async Task<ActionResult<PurchaseDto>> CreatePurchase(PurchaseDto dto)
        {
            var purchase = new Purchase
            {
                IDClient = dto.IDClient,
                Date = dto.Date,
                Sum = dto.Sum
            };
            _context.Purchases.Add(purchase);
            await _context.SaveChangesAsync();
            dto.Number = purchase.Number;
            return CreatedAtAction(nameof(GetPurchase), new { id = purchase.Number }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePurchase(int id, PurchaseDto dto)
        {
            if (id != dto.Number)
                return BadRequest();
            var purchase = await _context.Purchases.FindAsync(id);
            if (purchase == null)
                return NotFound();
            purchase.IDClient = dto.IDClient;
            purchase.Date = dto.Date;
            purchase.Sum = dto.Sum;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchase(int id)
        {
            var purchase = await _context.Purchases.FindAsync(id);
            if (purchase == null)
                return NotFound();
            _context.Purchases.Remove(purchase);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}