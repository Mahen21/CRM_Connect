using Microsoft.AspNetCore.Mvc;
using CRMConnect.Data;
using CRMConnect.Models;
using Microsoft.EntityFrameworkCore;

[Route("api/salesactivities")]
[ApiController]
public class SalesActivityController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public SalesActivityController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/salesactivities
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SalesActivity>>> GetSalesActivities()
    {
        return await _context.SalesActivities.ToListAsync();
    }

    // GET: api/salesactivities/1
    [HttpGet("{id}")]
    public async Task<ActionResult<SalesActivity>> GetSalesActivity(int id)
    {
        var salesActivity = await _context.SalesActivities.FindAsync(id);

        if (salesActivity == null)
        {
            return NotFound();
        }

        return salesActivity;
    }

    // POST: api/salesactivities
    [HttpPost]
    public async Task<ActionResult<SalesActivity>> CreateSalesActivity(SalesActivity salesActivity)
    {
        _context.SalesActivities.Add(salesActivity);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetSalesActivity), new { id = salesActivity.SalesActivityId }, salesActivity);
    }

    // PUT: api/salesactivities/1
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSalesActivity(int id, SalesActivity salesActivity)
    {
        if (id != salesActivity.SalesActivityId)
        {
            return BadRequest();
        }

        _context.Entry(salesActivity).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.SalesActivities.Any(e => e.SalesActivityId == id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/salesactivities/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSalesActivity(int id)
    {
        var salesActivity = await _context.SalesActivities.FindAsync(id);
        if (salesActivity == null)
        {
            return NotFound();
        }

        _context.SalesActivities.Remove(salesActivity);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
