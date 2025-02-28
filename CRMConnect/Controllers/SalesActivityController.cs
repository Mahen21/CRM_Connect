using Microsoft.AspNetCore.Mvc;
using CRMConnect.Data;
using CRMConnect.Models;
using System.Threading.Tasks;
using System.Linq;

namespace CRMConnect.Controllers
{
    [Route("SalesActivities")]
    public class SalesActivityController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesActivityController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SalesActivities/Index
        [Route("Index")]
        public IActionResult Index()
        {
            var activities = _context.SalesActivities.ToList();
            return View(activities);
        }

        // GET: SalesActivities/Create
        [Route("Create")]
        public IActionResult Create()
        {
            return View(new SalesActivity());
        }

        // POST: SalesActivities/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create(SalesActivity salesActivity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salesActivity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(salesActivity);
        }

        // POST: SalesActivities/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salesActivity = await _context.SalesActivities.FindAsync(id);
            if (salesActivity == null)
            {
                return NotFound();
            }

            _context.SalesActivities.Remove(salesActivity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
