using A_Ecommerce.Data;
using A_Ecommerce.Data;
using A_Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ECommerceMVC.Controllers
{
    public class HangHoasController : Controller
    {
        private readonly AShopContext _context;

        public HangHoasController(AShopContext context)
        {
            _context = context;
        }

        // GET: HangHoas
        public async Task<IActionResult> Index()
        {
            var hangHoas = _context.HangHoas
                                   .Include(h => h.MaLoaiNavigation)
                                   .Include(h => h.MaNccNavigation);
            return View(await hangHoas.ToListAsync());
        }

        // GET: HangHoas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var hangHoa = await _context.HangHoas
                                        .Include(h => h.MaLoaiNavigation)
                                        .Include(h => h.MaNccNavigation)
                                        .FirstOrDefaultAsync(m => m.MaHh == id);

            if (hangHoa == null) return NotFound();

            return View(hangHoa);
        }

        // GET: HangHoas/Create
        public IActionResult Create()
        {
            ViewData["MaLoai"] = new SelectList(_context.Loais, "MaLoai", "TenLoai");
            ViewData["MaNcc"] = new SelectList(_context.NhaCungCaps, "MaNcc", "TenCongTy");
            return View();
        }

        // POST: HangHoas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaHh,TenHh,MoTa,DonGia,MaLoai,MaNcc,Hinh,SoLuong")] HangHoa hangHoa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hangHoa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaLoai"] = new SelectList(_context.Loais, "MaLoai", "TenLoai", hangHoa.MaLoai);
            ViewData["MaNcc"] = new SelectList(_context.NhaCungCaps, "MaNcc", "TenCongTy", hangHoa.MaNcc);
            return View(hangHoa);
        }

        // GET: HangHoas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var hangHoa = await _context.HangHoas.FindAsync(id);
            if (hangHoa == null) return NotFound();

            ViewData["MaLoai"] = new SelectList(_context.Loais, "MaLoai", "TenLoai", hangHoa.MaLoai);
            ViewData["MaNcc"] = new SelectList(_context.NhaCungCaps, "MaNcc", "TenCongTy", hangHoa.MaNcc);
            return View(hangHoa);
        }

        // POST: HangHoas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaHh,TenHh,MoTa,DonGia,MaLoai,MaNcc,Hinh,SoLuong")] HangHoa hangHoa)
        {
            if (id != hangHoa.MaHh) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hangHoa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.HangHoas.Any(e => e.MaHh == id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["MaLoai"] = new SelectList(_context.Loais, "MaLoai", "TenLoai", hangHoa.MaLoai);
            ViewData["MaNcc"] = new SelectList(_context.NhaCungCaps, "MaNcc", "TenCongTy", hangHoa.MaNcc);
            return View(hangHoa);
        }

        // GET: HangHoas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var hangHoa = await _context.HangHoas
                                        .Include(h => h.MaLoaiNavigation)
                                        .Include(h => h.MaNccNavigation)
                                        .FirstOrDefaultAsync(m => m.MaHh == id);

            if (hangHoa == null) return NotFound();

            return View(hangHoa);
        }

        // POST: HangHoas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hangHoa = await _context.HangHoas.FindAsync(id);
            if (hangHoa != null)
            {
                _context.HangHoas.Remove(hangHoa);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
