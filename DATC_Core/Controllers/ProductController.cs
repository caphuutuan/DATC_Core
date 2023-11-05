using DATC_Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DATC_Core.Controllers
{
	public class ProductController : Controller
	{
		private readonly DATCCoreMineDBContext db = new DATCCoreMineDBContext();

		public ProductController(DATCCoreMineDBContext context)
		{
			db = context;
		}

		public IActionResult Index()
		{
			return View();
		}
		public IActionResult Detail(int id)
		{
			var product = db.Products.Include(x => x.CateId).FirstOrDefault(x => x.ProductId == id);
			if (product ==null)
			{
				return RedirectToAction("Index");
			}
			else
			{
				return View(product);

			}
		}
	}
}
