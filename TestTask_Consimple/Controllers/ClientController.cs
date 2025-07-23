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
    public class ClientController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ClientController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientDto>>> GetClients()
        {
            return await _context.Clients
                .Select(c => new ClientDto
                {
                    IDClient = c.IDClient,
                    FullName = c.FullName,
                    DateOfBirth = c.DateOfBirth,
                    DateOfReg = c.DateOfReg
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClientDto>> GetClient(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
                return NotFound();
            return new ClientDto
            {
                IDClient = client.IDClient,
                FullName = client.FullName,
                DateOfBirth = client.DateOfBirth,
                DateOfReg = client.DateOfReg
            };
        }

        [HttpPost]
        public async Task<ActionResult<ClientDto>> CreateClient(ClientDto dto)
        {
            var client = new Client
            {
                FullName = dto.FullName,
                DateOfBirth = dto.DateOfBirth,
                DateOfReg = dto.DateOfReg
            };
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            dto.IDClient = client.IDClient;
            return CreatedAtAction(nameof(GetClient), new { id = client.IDClient }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(int id, ClientDto dto)
        {
            if (id != dto.IDClient)
                return BadRequest();
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
                return NotFound();
            client.FullName = dto.FullName;
            client.DateOfBirth = dto.DateOfBirth;
            client.DateOfReg = dto.DateOfReg;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
                return NotFound();
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}