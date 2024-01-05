using DATC_Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            //var product = db.Products.Include(x => x.Cate).AsNoTracking();
            var item = db.Products.Include(x => x.Cate).AsNoTracking().FirstOrDefault(x => x.ProductId == id);
            if (item == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(item);

            }

            //var product = db.Products.Include(x => x.Cate).FirstOrDefault(x => x.ProductId == id);
            ////var countReview = db.Products.Where(x => x.ProductId == id).Count();
            ////ViewBag.CountReview = countReview;
            //return View(product);

        }

        //public decimal countReview()
        //{
        //    decimal review = (decimal)db.ProductReviews.Count();
        //    return review;
        //}

    }
}
