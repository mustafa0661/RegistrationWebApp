using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace RegistrationWebApp.Controllers
{
    public class ClientController : Controller
    {
        [Authorize(Roles = "client")]
        public IActionResult Index()
        {
            return View();
        }
    }
}