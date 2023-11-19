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
    public class OrdersController : Controller
    {
        private readonly DATCCoreMineDBContext db = new DATCCoreMineDBContext();
        public INotyfService _notyfService { get; }

        public OrdersController(DATCCoreMineDBContext context, INotyfService notyfService)
        {
            db = context;
            _notyfService = notyfService;
        }

        // GET: Admin/Orders
        public async Task<IActionResult> Index()
        {
            ViewBag.countOrder = db.Orders.Where(m => m.TransactStatusId == 1).Count();
            var dATCCoreMineDBContext = db.Orders.Include(o => o.Customer).Include(o => o.Payment).Include(o => o.TransactStatus);
            return View(await dATCCoreMineDBContext.ToListAsync());
        }

        // GET: Admin/Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || db.Orders == null)
            {
                return NotFound();
            }

            var order = await db.Orders
                .Include(o => o.Customer)
                .Include(o => o.Payment)
                .Include(o => o.TransactStatus)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Admin/Orders/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(db.Customers, "CustomerId", "CustomerId");
            ViewData["PaymentId"] = new SelectList(db.Payments, "PaymentId", "PaymentId");
            ViewData["TransactStatusId"] = new SelectList(db.TransactStatusses, "TransactStatusId", "TransactStatusId");
            return View();
        }

        // POST: Admin/Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,CustomerId,OrderDate,ShipDate,TransactStatusId,Deleted,Paid,PaymentId,Note")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Add(order);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(db.Customers, "CustomerId", "CustomerId", order.CustomerId);
            ViewData["PaymentId"] = new SelectList(db.Payments, "PaymentId", "PaymentId", order.PaymentId);
            ViewData["TransactStatusId"] = new SelectList(db.TransactStatusses, "TransactStatusId", "TransactStatusId", order.TransactStatusId);
            return View(order);
        }

        // GET: Admin/Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || db.Orders == null)
            {
                return NotFound();
            }

            var order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(db.Customers, "CustomerId", "CustomerId", order.CustomerId);
            ViewData["PaymentId"] = new SelectList(db.Payments, "PaymentId", "PaymentId", order.PaymentId);
            ViewData["TransactStatusId"] = new SelectList(db.TransactStatusses, "TransactStatusId", "TransactStatusId", order.TransactStatusId);
            return View(order);
        }

        // POST: Admin/Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,CustomerId,OrderDate,ShipDate,TransactStatusId,Deleted,Paid,PaymentId,Note")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(order);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
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
            ViewData["CustomerId"] = new SelectList(db.Customers, "CustomerId", "CustomerId", order.CustomerId);
            ViewData["PaymentId"] = new SelectList(db.Payments, "PaymentId", "PaymentId", order.PaymentId);
            ViewData["TransactStatusId"] = new SelectList(db.TransactStatusses, "TransactStatusId", "TransactStatusId", order.TransactStatusId);
            return View(order);
        }

        // GET: Admin/Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || db.Orders == null)
            {
                return NotFound();
            }

            var order = await db.Orders
                .Include(o => o.Customer)
                .Include(o => o.Payment)
                .Include(o => o.TransactStatus)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Admin/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (db.Orders == null)
            {
                return Problem("Entity set 'DATCCoreMineDBContext.Orders'  is null.");
            }
            var order = await db.Orders.FindAsync(id);
            if (order != null)
            {
                db.Orders.Remove(order);
            }

            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return (db.Orders?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }

        public decimal Revenue()
        {
            return 0;
        }
    }
}
