using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DATC_Core.Models;
using PagedList.Core;
using DATC_Core.Helper;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace DATC_Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomersController : Controller
    {
        private readonly DATCCoreMineDBContext db = new DATCCoreMineDBContext();
        public INotyfService _notyfService { get; }

        public CustomersController(DATCCoreMineDBContext context, INotyfService notyfService)
        {
            db = context;
            _notyfService = notyfService;
        }

        // GET: Admin/Customers
        public IActionResult Index(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 20;
            var lsCustomers = db.Customers.AsNoTracking().Include(x => x.Location).OrderByDescending(x => x.CreateDate);
            PagedList<Customer> models = new PagedList<Customer>(lsCustomers, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;

            return View(models);
        }

        // GET: Admin/Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || db.Customers == null)
            {
                return NotFound();
            }

            var customer = await db.Customers
                .Include(c => c.Location)
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Admin/Customers/Create
        public IActionResult Create()
        {
            ViewData["LocationId"] = new SelectList(db.Locations, "LocationId", "LocationId");
            return View();
        }

        // POST: Admin/Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,FullName,Dob,Avatar,Address,Email,Phone,LocationId,District,Ward,CreateDate,Password,Salt,LastLogin,Active")] Customer customer/*, IFormFile fAvatar*/)
        {
            if (ModelState.IsValid)
            {
                customer.Password = "1";
                customer.FullName = Utilities.ToTitleCase(customer.FullName);
                //if (fAvatar != null)
                //{
                //    string extension = Path.GetExtension(fAvatar.FileName);
                //    string image = Utilities.SEOUrl(customer.FullName) + extension;
                //    customer.Avatar = await Utilities.UploadFile(fAvatar, @"customers", image.ToLower());
                //}
                //if (string.IsNullOrEmpty(customer.Avatar))
                //{
                //    customer.Avatar = "avatar_profile_null.jpg";
                //}
                customer.CreateDate = DateTime.Now;
                db.Add(customer);
                await db.SaveChangesAsync();
                _notyfService.Success("Thêm mới User thành công");
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocationId"] = new SelectList(db.Locations, "LocationId", "LocationId", customer.LocationId);
            return View(customer);
        }

        // GET: Admin/Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || db.Customers == null)
            {
                return NotFound();
            }

            var customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            ViewData["LocationId"] = new SelectList(db.Locations, "LocationId", "LocationId", customer.LocationId);
            return View(customer);
        }

        // POST: Admin/Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,FullName,Dob,Avatar,Address,Email,Phone,LocationId,District,Ward,CreateDate,Password,Salt,LastLogin,Active")] Customer customer/*, IFormFile fAvatar*/)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    customer.FullName = Utilities.ToTitleCase(customer.FullName);
                    //if (fAvatar != null)
                    //{
                    //    string extension = Path.GetExtension(fAvatar.FileName);
                    //    string image = Utilities.SEOUrl(customer.FullName) + extension;
                    //    customer.Avatar = await Utilities.UploadFile(fAvatar, @"customers", image.ToLower());
                    //}
                    //if (string.IsNullOrEmpty(customer.Avatar))
                    //{
                    //    customer.Avatar = "avatar_profile_null.jpg";
                    //}
                    customer.ModifiedDate = DateTime.Now;
                    db.Update(customer);
                    _notyfService.Success("Cập nhật User ID = " + id + " thành công");
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerId))
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
            ViewData["LocationId"] = new SelectList(db.Locations, "LocationId", "LocationId", customer.LocationId);
            return View(customer);
        }

        // GET: Admin/Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || db.Customers == null)
            {
                return NotFound();
            }

            var customer = await db.Customers
                .Include(c => c.Location)
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Admin/Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (db.Customers == null)
            {
                return Problem("Entity set 'DATCCoreMineDBContext.Customers'  is null.");
            }
            var customer = await db.Customers.FindAsync(id);
            if (customer != null)
            {
                db.Customers.Remove(customer);
            }

            await db.SaveChangesAsync();
            _notyfService.Success("Xoá User ID = " + id + " thành công");
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return (db.Customers?.Any(e => e.CustomerId == id)).GetValueOrDefault();
        }
    }
}
