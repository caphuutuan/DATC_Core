using Microsoft.AspNetCore.Mvc;

namespace DATC_Core.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.TongDoanhThu = ThongKeDoanhThu();
            ViewBag.SoDonHang = ThongKeDonHang();
            ViewBag.SoThanhVien = ThongKeThanhVien();
            ViewBag.SoLuongSanPham = ThongKeSanPham();
            return View();
        }

        private dynamic ThongKeSanPham()
        {
            throw new NotImplementedException();
        }

        private dynamic ThongKeThanhVien()
        {
            throw new NotImplementedException();
        }

        private dynamic ThongKeDonHang()
        {
            throw new NotImplementedException();
        }

        private dynamic ThongKeDoanhThu()
        {
            throw new NotImplementedException();
        }
    }
}
