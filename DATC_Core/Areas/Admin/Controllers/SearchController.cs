using AspNetCoreHero.ToastNotification.Abstractions;
using DATC_Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DATC_Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SearchController : Controller
    {
        private readonly DATCCoreMineDBContext db = new DATCCoreMineDBContext();

        public SearchController(DATCCoreMineDBContext context)
        {
            db = context;
        }

        [HttpPost]
        public IActionResult FindProduct(string keyword)
        {
            List<Product> ls = new List<Product>();
            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                return PartialView("ListProductSearchPartialView", null);
            }
            ls = db.Products
                .AsNoTracking()
                .Include(a => a.Cate)
                .Where(x => x.ProductName.Contains(keyword))
                .OrderByDescending(x => x.ProductName)
                .Take(10)
                .ToList();
            if (ls == null)
            {
                return PartialView("ListProductSearchPartialView", null);

            }
            else
            {
                return PartialView("ListProductSearchPartialView", ls);

            }
        }
    }
}
