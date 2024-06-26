using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RegistrationWebApp.Models;
using RegistrationWebApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationWebApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Leaves()
        {
            var leaves = await _context.Leaves.Include(l => l.User).Where(l => l.IsApproved == null).ToListAsync();
            return View(leaves);
        }

        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            var leave = await _context.Leaves.FindAsync(id);
            if (leave != null)
            {
                leave.IsApproved = true;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Leaves");
        }

        [HttpPost]
        public async Task<IActionResult> Reject(int id)
        {
            var leave = await _context.Leaves.FindAsync(id);
            if (leave != null)
            {
                leave.IsApproved = false;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Leaves");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}