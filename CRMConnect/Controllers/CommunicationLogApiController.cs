using Microsoft.AspNetCore.Mvc;
using CRMConnect.Data;
using CRMConnect.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMConnect.Controllers
{
    [Route("api/communicationlogs")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/communicationlogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommunicationLog>>> GetCommunicationLogs()
        {
            return await _context.CommunicationLogs.Include(c => c.Customer).Include(c => c.User).ToListAsync();
        }

        // GET: api/communicationlogs/1
        [HttpGet("{id}")]
        public async Task<ActionResult<CommunicationLog>> GetCommunicationLog(int id)
        {
            var communicationLog = await _context.CommunicationLogs.FindAsync(id);
            if (communicationLog == null)
                return NotFound();

            return communicationLog;
        }

        // POST: api/communicationlogs
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId, UserId, CommunicationType, Notes, Date")] CommunicationLog communicationLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(communicationLog);
                await _context.SaveChangesAsync();
                return new JsonResult(communicationLog); // Return the created log as JSON
            }
            return new JsonResult(new { success = false }); // Return failure as JSON
        }

        // PUT: api/communicationlogs/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [Bind("Id, CustomerId, UserId, CommunicationType, Notes, Date")] CommunicationLog communicationLog)
        {
            if (id != communicationLog.Id)
                return BadRequest();

            _context.Entry(communicationLog).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new JsonResult(new { success = true }); // Return success as JSON
        }

        // DELETE: api/communicationlogs/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var communicationLog = await _context.CommunicationLogs.FindAsync(id);
            if (communicationLog == null)
                return NotFound();

            _context.CommunicationLogs.Remove(communicationLog);
            await _context.SaveChangesAsync();
            return new JsonResult(new { success = true }); // Return success as JSON
        }
    }
}
