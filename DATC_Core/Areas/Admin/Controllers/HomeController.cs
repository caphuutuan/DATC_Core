using DATC_Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace DATC_Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly DATCCoreMineDBContext db = new DATCCoreMineDBContext();

        public HomeController(DATCCoreMineDBContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            ViewBag.countRole = db.Roles.Count();
            ViewBag.countAccount = db.Accounts.Count();
            ViewBag.countProdCate = db.Categoryies.Count();
            ViewBag.countOrder = db.Orders.Count();
            ViewBag.countProd = db.Products.Count();
            return View();
        }
    }
}
