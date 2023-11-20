using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DATC_Core.Models;
using System.Drawing;
using DATC_Core.Helper;
using static System.Net.Mime.MediaTypeNames;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace DATC_Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PaymentsController : Controller
    {
        private readonly DATCCoreMineDBContext db = new DATCCoreMineDBContext();
        public INotyfService _notyfService { get; }

        public PaymentsController(DATCCoreMineDBContext context, INotyfService notyfService)
        {
            db = context;
            _notyfService = notyfService;
        }

        // GET: Admin/Payments
        public async Task<IActionResult> Index()
        {
            return db.Payments != null ?
                        View(await db.Payments.ToListAsync()) :
                        Problem("Entity set 'DATCCoreMineDBContext.Payments'  is null.");
        }

        // GET: Admin/Payments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || db.Payments == null)
            {
                return NotFound();
            }

            var payment = await db.Payments
                .FirstOrDefaultAsync(m => m.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Admin/Payments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentId,Name,Image")] Payment payment/*, IFormFile fImage*/)
        {
            if (ModelState.IsValid)
            {
                payment.Name = Utilities.ToTitleCase(payment.Name);
                //if (fImage != null)
                //{
                //    string extension = Path.GetExtension(fImage.FileName);
                //    string image = Utilities.SEOUrl(payment.Name) + extension;
                //    payment.Image = await Utilities.UploadFile(fImage, @"payments", image.ToLower());
                //}
                //if (string.IsNullOrEmpty(payment.Image))
                //{
                //    payment.Image= "placeholder-image.jpg";
                //}
                db.Add(payment);
                await db.SaveChangesAsync();
                _notyfService.Success("Thêm mới Phương thức thanh toán", 3);
                return RedirectToAction(nameof(Index));
            }
            return View(payment);
        }

        // GET: Admin/Payments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || db.Payments == null)
            {
                return NotFound();
            }

            var payment = await db.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            return View(payment);
        }

        // POST: Admin/Payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentId,Name,Image")] Payment payment/*, IFormFile fImage*/)
        {
            if (id != payment.PaymentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    payment.Name = Utilities.ToTitleCase(payment.Name);
                    //if (fImage != null)
                    //{
                    //    string extension = Path.GetExtension(fImage.FileName);
                    //    string image = Utilities.SEOUrl(payment.Name) + extension;
                    //    payment.Image = await Utilities.UploadFile(fImage, @"payments", image.ToLower());
                    //}
                    //if (string.IsNullOrEmpty(payment.Image))
                    //{
                    //    payment.Image = "placeholder-image.jpg";
                    //}
                    db.Update(payment);
                    await db.SaveChangesAsync();
                    _notyfService.Success("Cập nhật Phương thức thanh toán ID = " + id, 3);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentExists(payment.PaymentId))
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
            return View(payment);
        }

        // GET: Admin/Payments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || db.Payments == null)
            {
                return NotFound();
            }

            var payment = await db.Payments
                .FirstOrDefaultAsync(m => m.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: Admin/Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (db.Payments == null)
            {
                return Problem("Entity set 'DATCCoreMineDBContext.Payments'  is null.");
            }
            var payment = await db.Payments.FindAsync(id);
            if (payment != null)
            {
                db.Payments.Remove(payment);
            }

            await db.SaveChangesAsync();
            _notyfService.Success("Xoá Phương thức thanh toán ID = " + id, 3);

            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(int id)
        {
            return (db.Payments?.Any(e => e.PaymentId == id)).GetValueOrDefault();
        }

        public async Task<ActionResult> TrashAsync()
        {
            var list = await db.Payments.ToListAsync();
            return View(list);
        }
        public ActionResult DelTrash(int id)
        {
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                Notification.set_flash("Không tồn tại danh mục cần xóa vĩnh viễn!", "warning");
                return RedirectToAction("Index");
            }
            int count_child = db.Payments.Where(m => m.PaymentId == id).Count();
            if (count_child != 0)
            {
                Notification.set_flash("Không thể xóa, danh mục có chứa danh mục con!", "warning");
                return RedirectToAction("Index");
            }
            //payment.PaymentId = id;

            db.SaveChanges();
            Notification.set_flash("Ném thành công vào thùng rác!" + " ID = " + id, "success");
            return RedirectToAction("Index");
        }
        public ActionResult ReTrash(int? id)
        {
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                Notification.set_flash("Không tồn tại danh mục!", "warning");
                return RedirectToAction("Trash", "Category");
            }

            db.Entry(payment).State = EntityState.Modified;
            db.SaveChanges();
            Notification.set_flash("Khôi phục thành công!" + " ID = " + id, "success");
            return RedirectToAction("Trash", "Category");
        }
    }
}
