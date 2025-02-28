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

        public CustomerTaskController(ApplicationDbContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }

        // GET: /CustomerTask
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _httpClient.GetStringAsync("http://localhost:5146/api/customertasks");
                var customerTasks = JsonConvert.DeserializeObject<List<CustomerTask>>(response);
                return View(customerTasks);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error fetching data: " + ex.Message;
                return View();
            }
        }

        // GET: CustomerTask/Create (Show Create Form)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: CustomerTask/Create (Handle Form Submission)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerTask customerTask)
        {
            if (ModelState.IsValid)
            {
                _context.CustomerTasks.Add(customerTask);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(customerTask);
        }

        // GET: CustomerTask/Edit/{id} (Show Edit Form)
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var customerTask = await _context.CustomerTasks.FindAsync(id);
            if (customerTask == null) return NotFound();
            return View(customerTask);
        }

        // POST: CustomerTask/Edit/{id} (Handle Form Submission)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CustomerTask customerTask)
        {
            if (id != customerTask.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                _context.Entry(customerTask).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(customerTask);
        }

        // DELETE: CustomerTask/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var customerTask = await _context.CustomerTasks.FindAsync(id);
            if (customerTask == null) return NotFound();

            _context.CustomerTasks.Remove(customerTask);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // API: GET api/customertasks
        [HttpGet("api/customertasks")]
        public async Task<ActionResult<IEnumerable<CustomerTask>>> GetCustomerTasks()
        {
            return await _context.CustomerTasks.ToListAsync();
        }

        // API: GET api/customertasks/{id}
        [HttpGet("api/customertasks/{id}")]
        public async Task<ActionResult<CustomerTask>> GetCustomerTask(int id)
        {
            var customerTask = await _context.CustomerTasks.FindAsync(id);
            if (customerTask == null) return NotFound();
            return customerTask;
        }

        // API: POST api/customertasks
        [HttpPost("api/customertasks")]
        public async Task<ActionResult<CustomerTask>> CreateCustomerTask(CustomerTask customerTask)
        {
            _context.CustomerTasks.Add(customerTask);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCustomerTask), new { id = customerTask.Id }, customerTask);
        }

        // API: PUT api/customertasks/{id}
        [HttpPut("api/customertasks/{id}")]
        public async Task<IActionResult> UpdateCustomerTask(int id, CustomerTask customerTask)
        {
            if (id != customerTask.Id) return BadRequest();

            _context.Entry(customerTask).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // API: DELETE api/customertasks/{id}
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
