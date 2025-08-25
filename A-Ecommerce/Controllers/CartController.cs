using A_Ecommerce.Data;
using A_Ecommerce.ViewModels;
using Microsoft.AspNetCore.Mvc;
using A_Ecommerce.Helpers;
using System.Linq;
using System.Collections.Generic;

namespace A_Ecommerce.Controllers
{
    public class CartController : Controller
    {
        private readonly AShopContext db;

        public CartController(AShopContext context)
        {
            db = context;
        }

        // Lấy giỏ hàng từ Session
        public List<CartItem> Cart => HttpContext.Session.Get<List<CartItem>>(MySetting.CART_KEY) ?? new List<CartItem>();

        // Trang giỏ hàng
        public IActionResult Index()
        {
            return View(Cart);
        }

        // Thêm sản phẩm vào giỏ
        public IActionResult AddToCart(int id, int quantity = 1)
        {
            var gioHang = Cart;
            var item = gioHang.SingleOrDefault(p => p.MaHh == id);
            if (item == null)
            {
                var hangHoa = db.HangHoas.SingleOrDefault(p => p.MaHh == id);
                if (hangHoa == null)
                {
                    TempData["Message"] = $"Không tìm thấy hàng hóa có mã {id}";
                    return Redirect("/404");
                }
                item = new CartItem
                {
                    MaHh = hangHoa.MaHh,
                    TenHH = hangHoa.TenHh,
                    DonGia = hangHoa.DonGia ?? 0,
                    Hinh = hangHoa.Hinh ?? string.Empty,
                    SoLuong = quantity
                };
                gioHang.Add(item);
            }
            else
            {
                item.SoLuong += quantity;
            }

            HttpContext.Session.Set(MySetting.CART_KEY, gioHang);

            return RedirectToAction("Index");
        }

        // Xóa sản phẩm khỏi giỏ
        public IActionResult RemoveCart(int id)
        {
            var gioHang = Cart;
            var item = gioHang.SingleOrDefault(p => p.MaHh == id);
            if (item != null)
            {
                gioHang.Remove(item);
                HttpContext.Session.Set(MySetting.CART_KEY, gioHang);
            }
            return RedirectToAction("Index");
        }
    }
}
