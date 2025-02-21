using CRMConnect.Data;
using CRMConnect.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CRMConnect.Controllers
{
    public class CustomerTaskController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly HttpClient _httpClient;

        // Inject the HttpClient and ApplicationDbContext through Dependency Injection
        public CustomerTaskController(ApplicationDbContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }

        // GET: /customertasks (View Action)
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                // Call the API to get customer tasks data
                var response = await _httpClient.GetStringAsync("http://localhost:5146/customertasks");

                // Deserialize the response into a list of CustomerTask objects
                var customerTasks = JsonConvert.DeserializeObject<List<CustomerTask>>(response);

                // Pass the data to the view
                return View(customerTasks);
            }
            catch (Exception ex)
            {
                // If something goes wrong with the API call, log the error
                ViewBag.ErrorMessage = "Error fetching data: " + ex.Message;
                return View();
            }
        }

        // API Action: GET api/customertasks (API Action)
        [HttpGet("api/customertasks")]
        public async Task<ActionResult<IEnumerable<CustomerTask>>> GetCustomerTasks()
        {
            // Get customer tasks from the database
            return await _context.CustomerTasks.ToListAsync();
        }

        // API Action: GET api/customertasks/{id} (API Action)
        [HttpGet("api/customertasks/{id}")]
        public async Task<ActionResult<CustomerTask>> GetCustomerTask(int id)
        {
            // Find a specific customer task by id
            var customerTask = await _context.CustomerTasks.FindAsync(id);
            if (customerTask == null) return NotFound();
            return customerTask;
        }

        // API Action: POST api/customertasks (API Action)
        [HttpPost("api/customertasks")]
        public async Task<ActionResult<CustomerTask>> CreateCustomerTask(CustomerTask customerTask)
        {
            _context.CustomerTasks.Add(customerTask);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCustomerTask), new { id = customerTask.Id }, customerTask);
        }

        // API Action: PUT api/customertasks/{id} (API Action)
        [HttpPut("api/customertasks/{id}")]
        public async Task<IActionResult> UpdateCustomerTask(int id, CustomerTask customerTask)
        {
            if (id != customerTask.Id) return BadRequest();

            _context.Entry(customerTask).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // API Action: DELETE api/customertasks/{id} (API Action)
        [HttpDelete("api/customertasks/{id}")]
        public async Task<IActionResult> DeleteCustomerTask(int id)
        {
            var customerTask = await _context.CustomerTasks.FindAsync(id);
            if (customerTask == null) return NotFound();

            _context.CustomerTasks.Remove(customerTask);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
