using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DATC_Core.Models;
using PagedList.Core;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using DATC_Core.Helper;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace DATC_Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly DATCCoreMineDBContext db = new DATCCoreMineDBContext();
        public INotyfService _notyfService { get; }

        public ProductsController(DATCCoreMineDBContext context, INotyfService notyfService)
        {
            db = context;
            _notyfService = notyfService;
        }

        // GET: Admin/Products
        public IActionResult Index(int page = 1, int CateId = 0, int Active = 0)
        {
            var pageNumber = page;
            var pageSize = 20;

            List<Product> lsProducts = new List<Product>();
            if (CateId != 0)
            {
                lsProducts = db.Products
                .AsNoTracking()
                .Where(x => x.CateId == CateId)
                .Include(x => x.Cate)
                .OrderByDescending(x => x.ProductId).ToList();
            }
            else
            {
                lsProducts = db.Products
               .AsNoTracking()
               .Include(x => x.Cate)
               .OrderByDescending(x => x.ProductId).ToList();
            }
            PagedList<Product> models = new PagedList<Product>(lsProducts.AsQueryable(), pageNumber, pageSize);

            ViewBag.CurrentCateID = CateId;
            ViewBag.CurrentActive = Active;
            ViewBag.CurrentPage = pageNumber;

            //List<SelectListItem> lsStatus = new List<SelectListItem>();
            //lsStatus.Add(new SelectListItem() { Text = "Active", Value="1"});
            //lsStatus.Add(new SelectListItem() { Text = "Disable", Value="2"});

            //foreach(var item in lsStatus)
            //{
            //    if (item.Value == Active.ToString())
            //    {
            //        item.Selected = true;
            //        break;
            //    }
            //}

            //ViewData["lsStatus"] = lsStatus;
            ViewData["DanhMuc"] = new SelectList(db.Categoryies, "CateId", "CateName");
            return View(models);

        }
        // GET: Admin/Products/Filtter
        public IActionResult Filtter(int CateId = 0/*, int Active = 0*/)
        {
            var url = $"/Admin/Products?CateId={CateId}";
            if (CateId == 0)
            {
                url = $"/Admin/Products";
            }
            //else
            //{
            //    if (Active == 0)
            //    {
            //        url = $"/Admin/Products?CateId={CateId}";
            //    }
            //    if (CateId == 0)
            //    {
            //        url = $"/Admin/Products?Active={Active}";
            //    }
            //}
            return Json(new { status = "success", redirectUrl = url });
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || db.Products == null)
            {
                return NotFound();
            }

            var product = await db.Products
                .Include(p => p.Cate)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            ViewData["DanhMuc"] = new SelectList(db.Categoryies, "CateId", "CateName");

            List<Categoryie> categoryies = db.Categoryies.ToList();
            ViewBag.cateList = new SelectList(categoryies, "CateId", "CateName");

            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,ShortDesc,Description,CateId,Price,Discount,Thumb,Video,CreateDate,ModifiedDate,BestSeller,IsHome,Active,Title,Tags,Alias,MetaDesc,MetaKey,UnitsInStock")] Product product, IFormFile? fThumb, IFormFile? fVideo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    product.ProductName = Utilities.ToTitleCase(product.ProductName);
                    if (fThumb != null)
                    {
                        string extension = Path.GetExtension(fThumb.FileName);
                        string image = Utilities.SEOUrl(product.ProductName) + extension;
                        product.Thumb = await Utilities.UploadFile(fThumb, @"products", image.ToLower());
                    }
                    if (string.IsNullOrEmpty(product.Thumb))
                    {
                        product.Thumb = "avatar_profile_null.jpg";
                    }
                    if (fVideo != null)
                    {
                        string extension = Path.GetExtension(fVideo.FileName);
                        string image = Utilities.SEOUrl(product.ProductName) + extension;
                        product.Thumb = await Utilities.UploadFile(fVideo, @"products", image.ToLower());
                    }
                    if (string.IsNullOrEmpty(product.Thumb))
                    {
                        product.Thumb = "avatar_profile_null.jpg";
                    }

                    //String strSlug = XString.ToAscii(product.ProductName);
                    product.CreateDate = DateTime.Now;
                    product.UnitsInStock = 0;
                    product.Discount = 0;
                    product.Price = 0;
                    product.IsHome = false;
                    product.Alias = Utilities.SEOUrl(product.ProductName);
                    product.MetaDesc = product.ProductName;
                    product.Description = product.ProductName;
                    product.Title = product.ProductName;
                    product.MetaKey = product.ProductName;
                    product.Active = false;
                    db.Add(product);
                    await db.SaveChangesAsync();
                    _notyfService.Success("Tạo mới thành công");
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(Exception e)
            {
                var abc = e.Message;
            }

            ViewData["DanhMuc"] = new SelectList(db.Categoryies, "CateId", "CateName");

            List<Categoryie> categoryies = db.Categoryies.ToList();
            ViewBag.cateList = new SelectList(categoryies, "CateId", "CateName");

            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || db.Products == null)
            {
                return NotFound();
            }

            var product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            List<Categoryie> categoryies = db.Categoryies.ToList();
            ViewData["DanhMuc"] = new SelectList(db.Categoryies, "CateId", "CateName");
            ViewBag.cateList = new SelectList(categoryies, "CateId", "CateName");
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,ShortDesc,Description,CateId,Price,Discount,Thumb,Video,CreateDate,ModifiedDate,BestSeller,IsHome,Active,Title,Tags,Alias,MetaDesc,MetaKey,UnitsInStock")] Product product, IFormFile? fThumb, IFormFile? fVideo)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    product.ProductName = Utilities.ToTitleCase(product.ProductName);
                    if (fThumb != null)
                    {
                        string extension = Path.GetExtension(fThumb.FileName);
                        string image = Utilities.SEOUrl(product.ProductName) + extension;
                        product.Thumb = await Utilities.UploadFile(fThumb, @"products", image.ToLower());
                    }
                    if (string.IsNullOrEmpty(product.Thumb))
                    {
                        product.Thumb = "avatar_profile_null.jpg";
                    }

                    if (fVideo != null)
                    {
                        string extension = Path.GetExtension(fVideo.FileName);
                        string image = Utilities.SEOUrl(product.ProductName) + extension;
                        product.Thumb = await Utilities.UploadFile(fVideo, @"products", image.ToLower());
                    }
                    if (string.IsNullOrEmpty(product.Thumb))
                    {
                        product.Thumb = "avatar_profile_null.jpg";
                    }


                    product.Alias = Utilities.SEOUrl(product.ProductName);
                    product.ModifiedDate = DateTime.Now;
                    // Preserve the original CreateDate
                    //var originalProduct = await db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.ProductId == id);
                    //product.CreateDate = originalProduct.CreateDate;
                    db.Update(product);
                    await db.SaveChangesAsync();
                    _notyfService.Success("Cập nhật thành công ID = " + id);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            List<Categoryie> categoryies = db.Categoryies.ToList();
            ViewData["DanhMuc"] = new SelectList(db.Categoryies, "CateId", "CateName");
            ViewBag.cateList = new SelectList(categoryies, "CateId", "CateName");
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || db.Products == null)
            {
                return NotFound();
            }

            var product = await db.Products
                .Include(p => p.Cate)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (db.Products == null)
            {
                return Problem("Entity set 'DATCCoreMineDBContext.Products'  is null.");
            }
            var product = await db.Products.FindAsync(id);
            if (product != null)
            {
                db.Products.Remove(product);
            }

            await db.SaveChangesAsync();
                _notyfService.Success("Xoá thành công ID = "+ id);
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return (db.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> Review(int? id)
        {
            if (id == null || db.Products == null)
            {
                return NotFound();
            }

            var product = await db.Products
                .Include(p => p.Cate)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        //[HttpPost]
        //public JsonResult changeStatus(int id)
        //{
        //    Product product = db.Products.Find(id);
        //    product.Active = (product.Active == true) ? false : true;

        //    product.ModifiedDate = DateTime.Now;
        //    db.Entry(product).State = EntityState.Modified;
        //    db.SaveChanges();
        //    return Json(new { active = product.Active });
        //}
    }
}
