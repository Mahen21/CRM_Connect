using Microsoft.AspNetCore.Mvc;
using CRMConnect.Data;
using CRMConnect.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace CRMConnect.Controllers
{
    [Route("customers")]
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor to inject ApplicationDbContext into the controller
        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: customers/index
        [HttpGet("index")]
        public async Task<IActionResult> Index()
        {
            // Fetch all customers from the database asynchronously
            var customers = await _context.Customers.ToListAsync();
            return View(customers); // Pass customers to the view
        }

        // GET: customers/create
        [HttpGet("create")]
        public IActionResult Create()
        {
            return View(); // Return empty form for creating a new customer
        }

        // POST: customers/create
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Email,Phone,Address,Company")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                // Add new customer to the database
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync(); // Save changes to database
                return RedirectToAction(nameof(Index)); // Redirect to Index view after saving
            }

            return View(customer); // If model is invalid, return to the form with validation errors
        }

        // GET: customers/edit/{id}
        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            // Fetch the customer by ID
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound(); // If customer doesn't exist, return NotFound
            }

            return View(customer); // Return the edit view with the customer data
        }

        // POST: customers/edit/{id}
        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest(); // If customer ID doesn't match, return BadRequest
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync(); // Save updated customer to database
            }
            catch (DbUpdateConcurrencyException)
            {
                // Handle concurrency exception if customer no longer exists
                if (!_context.Customers.Any(e => e.Id == id))
                {
                    return NotFound(); // If customer not found, return NotFound
                }
                else
                {
                    throw; // Rethrow other exceptions
                }
            }

            return RedirectToAction(nameof(Index)); // Redirect to Index after editing
        }

        // GET: customers/delete/{id}
        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound(); // Return NotFound if customer doesn't exist
            }

            return View(customer); // Return the delete confirmation view with the customer data
        }

        // POST: customers/delete/{id}
        [HttpPost("delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound(); // Return NotFound if customer doesn't exist
            }

            _context.Customers.Remove(customer); // Remove customer from database
            await _context.SaveChangesAsync(); // Save changes to database

            return RedirectToAction(nameof(Index)); // Redirect to Index after deleting
        }
    }
}
