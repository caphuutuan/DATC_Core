using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DATC_Core.Models;
using System.Collections.Specialized;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace DATC_Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminRolesController : Controller
    {
        private readonly DATCCoreMineDBContext db = new DATCCoreMineDBContext();
        public INotyfService _notyfService { get; } 
        public AdminRolesController(DATCCoreMineDBContext context, INotyfService notyfService)
        {
            db = context;
            _notyfService = notyfService;
        }

        // GET: Admin/AdminRoles
        public async Task<IActionResult> Index()
        {
            ViewBag.countRole = db.Roles.Count();

            return db.Roles != null ? 
                          View(await db.Roles.ToListAsync()) :
                          Problem("Entity set 'DATCCoreMineContext.Roles'  is null.");
        }

        // GET: Admin/AdminRoles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || db.Roles == null)
            {
                return NotFound();
            }

            var role = await db.Roles
                .FirstOrDefaultAsync(m => m.RoleId == id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // GET: Admin/AdminRoles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoleId,RoleName,Description")] Role role)
        {
            if (ModelState.IsValid)
            {
                db.Add(role);
                await db.SaveChangesAsync();
                _notyfService.Success("Thêm Phân quyền mới thành công",3);
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        // GET: Admin/AdminRoles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || db.Roles == null)
            {
                return NotFound();
            }

            var role = await db.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        // POST: Admin/AdminRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoleId,RoleName,Description")] Role role)
        {
            if (id != role.RoleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(role);
                    await db.SaveChangesAsync();
                    _notyfService.Success("Cập nhật Phân quyền ID = " + id +" thành công", 3);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoleExists(role.RoleId))
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
            return View(role);
        }

        // GET: Admin/AdminRoles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || db.Roles == null)
            {
                return NotFound();
            }

            var role = await db.Roles
                .FirstOrDefaultAsync(m => m.RoleId == id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // POST: Admin/AdminRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (db.Roles == null)
            {
                return Problem("Entity set 'DATCCoreMineContext.Roles'  is null.");
            }
            var role = await db.Roles.FindAsync(id);
            if (role != null)
            {
                db.Roles.Remove(role);
            }
            
            await db.SaveChangesAsync();
            _notyfService.Success("Xoá Phân quyền ID = " + id +" thành công", 3);
            return RedirectToAction(nameof(Index));
        }

        private bool RoleExists(int id)
        {
          return (db.Roles?.Any(e => e.RoleId == id)).GetValueOrDefault();
        }

        public int countRole()
        {
            return ViewBag.countRole = db.Roles.Count();

        }
    }
}
