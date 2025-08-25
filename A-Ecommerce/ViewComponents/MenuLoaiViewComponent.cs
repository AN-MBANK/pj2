using A_Ecommerce.Data;

using A_Ecommerce.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceMVC.ViewComponents
{
    public class MenuLoaiViewComponent : ViewComponent
    {
        private readonly AShopContext db;

        public MenuLoaiViewComponent(AShopContext context) => db = context;

        public IViewComponentResult Invoke()
        {
            var data = db.Loais.Select(lo => new MenuLoaiVM
            {
                MaLoai = lo.MaLoai,
                TenLoai = lo.TenLoai,
                SoLuong = lo.HangHoas.Count
            }).OrderBy(p => p.TenLoai);

            return View(data); // Default.cshtml
                               //return View("Default", data);
        }
    }
}