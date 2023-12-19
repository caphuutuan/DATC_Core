using DATC_Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using System.Drawing.Printing;

namespace DATC_Core.Controllers
{
	public class ProductController : Controller
	{
		private readonly DATCCoreMineDBContext db = new DATCCoreMineDBContext();

		public ProductController(DATCCoreMineDBContext context)
		{
			db = context;
		}

		public IActionResult Index(int? id)
		{
            var items = db.Products.Include(x=>x.Cate).ToList();
            if (id != null)
            {
                items = items.Where(x => x.ProductId == id).ToList();
            }
            return View(items);
		}
		public IActionResult Detail(int id)
		{
            var product = db.Products.Include(x => x.Cate).FirstOrDefault(x => x.ProductId == id);
            if (product == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);

            }

            //var product = db.Products.Include(x => x.Cate).FirstOrDefault(x => x.ProductId == id);
            ////var countReview = db.Products.Where(x => x.ProductId == id).Count();
            ////ViewBag.CountReview = countReview;
            //return View(product);

        }
	}
}
