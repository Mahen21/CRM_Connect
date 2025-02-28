using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRMConnect.Data;
using CRMConnect.Models;
using System.Linq;

public class CommunicationLogController : Controller
{
    private readonly ApplicationDbContext _context;

    public CommunicationLogController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: CommunicationLog/IndexCommunication
    public IActionResult IndexCommunication()
    {
        var communicationLogs = _context.CommunicationLogs
            .Include(c => c.Customer) // Ensure Customer data is loaded
            .Include(c => c.User) // Ensure User data is loaded
            .ToList();

        return View(communicationLogs);
    }

    // GET: CommunicationLog/Create
    public IActionResult Create()
    {
        // Initialize the communication log model with empty Customer
        var communicationLog = new CommunicationLog
        {
            Customer = new Customer() // Avoid null reference exception
        };

        // Load data for the dropdowns
        ViewBag.Customers = _context.Customers.ToList(); // Customers for dropdown
        ViewBag.Users = _context.Users.ToList(); // Users for dropdown

        // Return the view with the initialized CommunicationLog model
        return View(communicationLog);
    }

    // POST: CommunicationLog/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CommunicationLog communicationLog)
    {
        // Check if the model state is valid
        if (ModelState.IsValid)
        {
            // Add the new communication log to the database
            _context.Add(communicationLog);
            _context.SaveChanges();

            // Redirect to the IndexCommunication action
            return RedirectToAction("IndexCommunication");
        }

        // If the model state is not valid, reload data for the dropdowns
        ViewBag.Customers = _context.Customers.ToList();
        ViewBag.Users = _context.Users.ToList();

        // Return the view with the model
        return View(communicationLog);
    }

    // GET: CommunicationLog/Delete/5
    public IActionResult Delete(int id)
    {
        var communicationLog = _context.CommunicationLogs
            .Include(c => c.Customer)
            .Include(u => u.User)
            .FirstOrDefault(c => c.Id == id);

        if (communicationLog == null)
        {
            return NotFound();
        }

        return View(communicationLog); // Return the view with the model
    }

    // POST: CommunicationLog/DeleteConfirmed/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var communicationLog = _context.CommunicationLogs.Find(id);
        if (communicationLog != null)
        {
            _context.CommunicationLogs.Remove(communicationLog);
            _context.SaveChanges();
        }

        return RedirectToAction("IndexCommunication");
    }
}
