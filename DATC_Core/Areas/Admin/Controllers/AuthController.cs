using Microsoft.AspNetCore.Mvc;

namespace DATC_Core.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
