using Microsoft.AspNetCore.Mvc;
using Register_Patient_Online.Models; // Thay đổi namespace theo project của bạn
using System.Collections.Generic;

namespace Register_Patient_Online.Controllers
{
    public class DangKyKhamBenhController : Controller
    {
        private static List<DangKyKhamBenh> danhSachDangKy = new List<DangKyKhamBenh>();

        // Hiển thị danh sách đăng ký
        public IActionResult Index()
        {
            return View(danhSachDangKy);
        }

        // Hiển thị form đăng ký
        public IActionResult Create()
        {
            return View();
        }

        // Xử lý khi người dùng submit form
        [HttpPost]
        public IActionResult Create(DangKyKhamBenh model)
        {
            if (ModelState.IsValid)
            {
                model.Id = danhSachDangKy.Count + 1; // Tạo ID tạm thời
                danhSachDangKy.Add(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}

