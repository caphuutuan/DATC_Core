using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DATC_Core.Models;

namespace DATC_Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountsController : Controller
    {
        private readonly DATCCoreMineDBContext db = new DATCCoreMineDBContext();

        public AccountsController(DATCCoreMineDBContext context)
        {
            db = context;
        }

        // GET: Admin/Accounts
        public async Task<IActionResult> Index()
        {
            var dATCCoreMineDBContext = db.Accounts.Include(a => a.Role);
            return View(await dATCCoreMineDBContext.ToListAsync());
        }

        // GET: Admin/Accounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || db.Accounts == null)
            {
                return NotFound();
            }

            var account = await db.Accounts
                .Include(a => a.Role)
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Admin/Accounts/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(db.Roles, "RoleId", "RoleId");
            ViewBag.listRole = new SelectList(db.Roles, "RoleId", "RoleId");
            ViewBag.ActiveOptions = new List<SelectListItem> {
                    new SelectListItem { Text = "Active", Value = "true" },
                    new SelectListItem { Text = "Inactive", Value = "false" }
                }
            ;
            return View();
        }

        // POST: Admin/Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccountId,Phone,Email,Salt,Password,FullName,Active,RoleId,LastLogin,CreateDate")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Add(account);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(db.Roles, "RoleId", "RoleId", account.RoleId);
            return View(account);
        }

        // GET: Admin/Accounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || db.Accounts == null)
            {
                return NotFound();
            }

            var account = await db.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(db.Roles, "RoleId", "RoleId", account.RoleId);
            return View(account);
        }

        // POST: Admin/Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccountId,Phone,Email,Salt,Password,FullName,Active,RoleId,LastLogin,CreateDate")] Account account)
        {
            if (id != account.AccountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(account);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.AccountId))
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
            ViewData["RoleId"] = new SelectList(db.Roles, "RoleId", "RoleId", account.RoleId);
            return View(account);
        }

        // GET: Admin/Accounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || db.Accounts == null)
            {
                return NotFound();
            }

            var account = await db.Accounts
                .Include(a => a.Role)
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Admin/Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (db.Accounts == null)
            {
                return Problem("Entity set 'DATCCoreMineDBContext.Accounts'  is null.");
            }
            var account = await db.Accounts.FindAsync(id);
            if (account != null)
            {
                db.Accounts.Remove(account);
            }

            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(int id)
        {
            return (db.Accounts?.Any(e => e.AccountId == id)).GetValueOrDefault();
        }
    }
}
