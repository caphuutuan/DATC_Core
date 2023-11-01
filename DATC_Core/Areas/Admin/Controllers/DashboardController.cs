using Microsoft.AspNetCore.Mvc;

namespace DATC_Core.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
