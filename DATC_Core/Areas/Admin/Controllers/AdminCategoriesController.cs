using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DATC_Core.Models;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace DATC_Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminCategoriesController : Controller
    {
        private readonly DATCCoreMineDBContext db = new DATCCoreMineDBContext();

        public AdminCategoriesController(DATCCoreMineDBContext context)
        {
            db = context;
        }

        // GET: Admin/AdminCategories
        public async Task<IActionResult> Index()
        {
              return db.Categoryies != null ? 
                          View(await db.Categoryies.ToListAsync()) :
                          Problem("Entity set 'DATCCoreMineDBContext.Categoryies'  is null.");
        }

        // GET: Admin/AdminCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || db.Categoryies == null)
            {
                return NotFound();
            }

            var categoryie = await db.Categoryies
                .FirstOrDefaultAsync(m => m.CateId == id);
            if (categoryie == null)
            {
                return NotFound();
            }

            return View(categoryie);
        }

        // GET: Admin/AdminCategories/Create
        public IActionResult Create()
        {
            ViewBag.listCat = new SelectList(db.Categoryies.Where(m => m.Published == true), "CateId", "CateName", 0);
            ViewBag.listOrder = new SelectList(db.Categoryies.Where(m => m.Published == true), "Ordering", "CateName", 0);
            return View();
        }

        // POST: Admin/AdminCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CateId,CateName,Description,ParentId,Levels,Ordering,Published,Thumb,Title,Alias,MetaDesc,MetaKey,Cover,SchemaMarkup")] Categoryie categoryie)
        {
            ViewBag.listCat = new SelectList(db.Categoryies.Where(m => m.Published == true), "CateId", "CateName", 0);
            ViewBag.listOrder = new SelectList(db.Categoryies.Where(m => m.Published == true), "Ordering", "CateName", 0);
            if (ModelState.IsValid)
            {
                if (categoryie.ParentId==0)
                {
                    categoryie.ParentId = 0;
                }

                String Slug = XString.ToAscii(categoryie.CateName);        

                db.Add(categoryie);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoryie);
        }

        // GET: Admin/AdminCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.listCat = new SelectList(db.Categoryies.Where(m => m.Published == true), "CateId", "CateName", 0);
            ViewBag.listOrder = new SelectList(db.Categoryies.Where(m => m.Published == true), "Ordering", "CateName", 0);
            if (id == null || db.Categoryies == null)
            {
                return NotFound();
            }

            var categoryie = await db.Categoryies.FindAsync(id);
            if (categoryie == null)
            {
                return NotFound();
            }
            return View(categoryie);
        }

        // POST: Admin/AdminCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CateId,CateName,Description,ParentId,Levels,Ordering,Published,Thumb,Title,Alias,MetaDesc,MetaKey,Cover,SchemaMarkup")] Categoryie categoryie)
        {
            ViewBag.listCat = new SelectList(db.Categoryies.Where(m => m.Published == true), "CateId", "CateName", 0);
            ViewBag.listOrder = new SelectList(db.Categoryies.Where(m => m.Published == true), "Ordering", "CateName", 0);
            if (id != categoryie.CateId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(categoryie);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryieExists(categoryie.CateId))
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
            return View(categoryie);
        }

        // GET: Admin/AdminCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || db.Categoryies == null)
            {
                return NotFound();
            }

            var categoryie = await db.Categoryies
                .FirstOrDefaultAsync(m => m.CateId == id);
            if (categoryie == null)
            {
                return NotFound();
            }

            return View(categoryie);
        }

        // POST: Admin/AdminCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (db.Categoryies == null)
            {
                return Problem("Entity set 'DATCCoreMineDBContext.Categoryies'  is null.");
            }
            var categoryie = await db.Categoryies.FindAsync(id);
            if (categoryie != null)
            {
                db.Categoryies.Remove(categoryie);
            }
            
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryieExists(int id)
        {
          return (db.Categoryies?.Any(e => e.CateId == id)).GetValueOrDefault();
        }

        // Trash
        public ActionResult Trash()
        {
            var list = db.Categoryies.Where(m => m.Published == true).ToList();
            return View(list);
        }
        //public ActionResult DelTrash(int id)
        //{
        //    Categoryie categoryie = db.Categoryies.Find(id);
        //    if (categoryie == null)
        //    {
        //        Notification.set_flash("Không tồn tại danh mục cần xóa vĩnh viễn!", "warning");
        //        return RedirectToAction("Index");
        //    }
        //    int count_child = db.Categoryies.Where(m => m.ParentId == id).Count();
        //    if (count_child != 0)
        //    {
        //        Notification.set_flash("Không thể xóa, danh mục có chứa danh mục con!", "warning");
        //        return RedirectToAction("Index");
        //    }
        //    categoryie.Published = true;

        //    db.SaveChanges();
        //    Notification.set_flash("Ném thành công vào thùng rác!" + " ID = " + id, "success");
        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        //[CustomAuthorizeAttribute(RoleID = "ADMIN")]
        public JsonResult changeStatus(int id)
        {
            Categoryie categoryie = db.Categoryies.Find(id);
            categoryie.Published = (categoryie.Published == true) ? true : false;

            //categoryie.Updated_at = DateTime.Now;
            //categoryie.Updated_by = int.Parse(Session["Admin_ID"].ToString());
            db.Entry(categoryie).State = EntityState.Modified;
            db.SaveChanges();
            return Json(new
            {
                published = categoryie.Published
            });
        }
    }
}
