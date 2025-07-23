using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestTask_Consimple.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TestTask_Consimple.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActionsController:ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ActionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{date}")]
        public async Task<ActionResult<IEnumerable<ActionDto>>> GetBirthdayFellasList(DateTime date)
        {
            var result =   await _context.Clients
                .Where(f => f.DateOfBirth.Day == date.Day && f.DateOfBirth.Month == date.Month)
                .Select(f => new ActionDto
                {
                    IDClient = f.IDClient,
                    FullName = f.FullName,
                })
                .ToListAsync();
            return Ok(result);
        }

        [HttpGet("GetBuyersList/{n}")]
        public async Task<ActionResult<IEnumerable<ActionDto>>> GetBuyersList(int n)
        {
            var today = DateTime.Today;
            var from = today.AddDays(-n);

            var result = await _context.Purchases
                .Where(p => p.Date >= from && p.Date <= today)
                .Select(p => new ActionDto
                {
                    IDClient = p.IDClient,
                    FullName = p.Client.FullName,
                })
                .ToListAsync();
            return Ok(result);
        }

        [HttpGet("CategoriesByCustomer/{idCustomer}")]
        public async Task<ActionResult<IEnumerable<CategoryUnitsDto>>> GetCategoriesByCustomer(int idCustomer)
        {
            var result = await _context.Purchases
                .Where(p => p.IDClient == idCustomer)
                .SelectMany(p => p.PurchaseItems)
                .GroupBy(pi => pi.Product.Category)
                .Select(g => new CategoryUnitsDto
                {
                    Category = g.Key,
                    UnitsPurchased = g.Sum(pi => pi.Quantity)
                })
                .ToListAsync();

            return Ok(result);
        }
    }
}