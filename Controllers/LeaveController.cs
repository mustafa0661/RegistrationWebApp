using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RegistrationWebApp.Models;
using RegistrationWebApp.Services;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Linq;

namespace RegistrationWebApp.Controllers
{
    [Authorize]
    public class LeaveController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public LeaveController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(); // Handle null user scenario
            }
            var leaves = _context.Leaves.Where(l => l.UserId == user.Id).ToList();
            return View(leaves);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DateTime startDate, DateTime endDate)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(); // Handle null user scenario
            }
            if ((endDate - startDate).Days > 5)
            {
                ModelState.AddModelError(string.Empty, "Leave duration cannot exceed 5 days.");
                return View("Index", _context.Leaves.Where(l => l.UserId == user.Id).ToList());
            }

            var leave = new Leave
            {
                UserId = user.Id,
                StartDate = startDate,
                EndDate = endDate,
                IsApproved = null
            };

            _context.Leaves.Add(leave);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}