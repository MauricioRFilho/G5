using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using G5.Data;
using WebMvcMysql.Models;

namespace G5.Controllers
{
    public class BrandsController : Controller
    {
        private readonly Contexto _context;

        public BrandsController(Contexto context)
        {
            _context = context;
        }

        // GET: Brands
        public async Task<IActionResult> Index()
        {
            if (_context.Brands != null)
            {
                ViewBag.User = TempData["User"].ToString();
                return View(await _context.Brands.ToListAsync());

            }
            else
            {
                return Problem("Entity set 'Contexto.Brands'  is null.");

            }
        }

        // GET: Brands/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Brands == null)
            {
                return NotFound();
            }

            var brand = await _context.Brands
                .FirstOrDefaultAsync(m => m.Id == id);
            if (brand == null)
            {
                return NotFound();
            }
            TempData["User"] = ViewBag.User;
            return View(brand);
        }

        // GET: Brands/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Brands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BrandName,National,Status")] Brand brand)
        {
            if (ModelState.IsValid)
            {
                var brands = _context.Brands.Where(model => model.BrandName == brand.BrandName).FirstOrDefault();
                if (brands == null)
                {
                    _context.Add(brand);
                    await _context.SaveChangesAsync();
                    TempData["User"] = ViewBag.User;
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    var newBrand = new Brand
                    {
                        BrandName = brand.BrandName + "-2",
                        Status = brand.Status,
                        National = brand.National
                    };
                    _context.Add(newBrand);
                    await _context.SaveChangesAsync();
                    TempData["User"] = ViewBag.User;
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(brand);
        }

        // GET: Brands/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Brands == null)
            {
                return NotFound();
            }

            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            TempData["User"] = ViewBag.User;
            return View(brand);
        }

        // POST: Brands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BrandName,National,Status")] Brand brand)
        {
            if (id != brand.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(brand);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrandExists(brand.Id))
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
            return View(brand);
        }

        // GET: Brands/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Brands == null)
            {
                return NotFound();
            }

            var brand = await _context.Brands
                .FirstOrDefaultAsync(m => m.Id == id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // POST: Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Brands == null)
            {
                return Problem("Entity set 'Contexto.Brands'  is null.");
            }
            var brand = await _context.Brands.FindAsync(id);
            if (brand != null)
            {
                _context.Brands.Remove(brand);
            }
            
            await _context.SaveChangesAsync();
            TempData["User"] = ViewBag.User;
            return RedirectToAction(nameof(Index));
        }

        private bool BrandExists(int id)
        {
          return (_context.Brands?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        [HttpPost]
        public async Task<IActionResult> LogBrands(Brand brand, Users user)
        {
            var logger = new Logs
            {
                UserName = user.Name,
                BrandName = brand.BrandName,
                Date = DateTime.UtcNow,
                Status = brand.Status,
                National = brand.National
            };
            _context.Log.Add(logger);
            await _context.SaveChangesAsync();
            return ViewBag.Log("Add");
        }
    }
}
