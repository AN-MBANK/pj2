using AutoMapper;
using A_Ecommerce.Data;
using A_Ecommerce.Helpers;
using A_Ecommerce.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace A_Ecommerce.Controllers
{
    public class KhachHangController(AShopContext context, IMapper mapper) : Controller
    {
        private readonly AShopContext _context = context;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public IActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        [HttpPost]
        public IActionResult DangKy(RegisterVM model, IFormFile Hinh)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var khachHang = _mapper.Map<KhachHang>(model);

                    khachHang.Randomkey = MyUtil.GenerateRandomKey();
                    khachHang.MatKhau = model.MatKhau.ToMd5Hash((string?)khachHang.Randomkey);
                    khachHang.HieuLuc = true; // xử lý khi dùng Mail để active
                    khachHang.VaiTro = 0;

                    if (Hinh != null)
                    {
                        khachHang.Hinh = MyUtil.UploadHinh(Hinh, "khachhang");
                    }

                    _context.Add(khachHang);
                    _context.SaveChanges();

                    return RedirectToAction("Index", "HangHoa");
                }
                catch (Exception ex)
                {
                    var mess = $"Lỗi: {ex.Message}";
                    // Có thể log lại mess hoặc dùng TempData để hiển thị ra View
                }
            }

            return View();
        }

    }
}
