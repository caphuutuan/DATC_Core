using Microsoft.AspNetCore.Mvc;

namespace DATC_Core.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogIn() 
        {
            return View();
        }
    }
}
