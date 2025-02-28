using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRMConnect.Data;
using CRMConnect.Models;
using System.Linq;
using System.Threading.Tasks;

public class CustomerController : Controller
{
    private readonly ApplicationDbContext _context;

    public CustomerController(ApplicationDbContext context)
    {
        _context = context;
    }

    // ✅ List all customers
    public async Task<IActionResult> Index()
    {
        var customers = await _context.Customers.ToListAsync();
        return View(customers);
    }

    // ✅ Load the Create form
    public IActionResult Create()
    {
        return View();
    }

    // ✅ Handle Create form submission
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Customer customer)
    {
        if (ModelState.IsValid)
        {
            _context.Add(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(customer);
    }

    // ✅ Confirm Delete Page (You don't need a separate page, handling via JS)
    public async Task<IActionResult> Delete(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null)
        {
            return NotFound();
        }

        return PartialView("_DeleteConfirmation", customer); // Return PartialView for inline confirmation
    }

    // ✅ Handle Delete action
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer != null)
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }
}
