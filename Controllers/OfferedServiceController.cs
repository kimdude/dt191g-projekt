using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using bookingApplication;
using bookingApplication.Data;

namespace bookingApplication.Controllers
{
    public class OfferedServiceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OfferedServiceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OfferedService
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.OfferedServices.Include(o => o.Hairdresser).Include(o => o.Service);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: OfferedService/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offeredService = await _context.OfferedServices
                .Include(o => o.Hairdresser)
                .Include(o => o.Service)
                .FirstOrDefaultAsync(m => m.HairdresserId == id);
            if (offeredService == null)
            {
                return NotFound();
            }

            return View(offeredService);
        }

        // GET: OfferedService/Create
        public IActionResult Create()
        {
            ViewData["HairdresserId"] = new SelectList(_context.Hairdressers, "Id", "Fname");
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Title");
            return View();
        }

        // POST: OfferedService/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HairdresserId,ServiceId")] OfferedService offeredService)
        {
            if (ModelState.IsValid)
            {
                _context.Add(offeredService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HairdresserId"] = new SelectList(_context.Hairdressers, "Id", "Fname", offeredService.HairdresserId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Title", offeredService.ServiceId);
            return View(offeredService);
        }

        // GET: OfferedService/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offeredService = await _context.OfferedServices.FindAsync(id);
            if (offeredService == null)
            {
                return NotFound();
            }
            ViewData["HairdresserId"] = new SelectList(_context.Hairdressers, "Id", "Fname", offeredService.HairdresserId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Title", offeredService.ServiceId);
            return View(offeredService);
        }

        // POST: OfferedService/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HairdresserId,ServiceId")] OfferedService offeredService)
        {
            if (id != offeredService.HairdresserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(offeredService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfferedServiceExists(offeredService.HairdresserId))
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
            ViewData["HairdresserId"] = new SelectList(_context.Hairdressers, "Id", "Fname", offeredService.HairdresserId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Title", offeredService.ServiceId);
            return View(offeredService);
        }

        // GET: OfferedService/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offeredService = await _context.OfferedServices
                .Include(o => o.Hairdresser)
                .Include(o => o.Service)
                .FirstOrDefaultAsync(m => m.HairdresserId == id);
            if (offeredService == null)
            {
                return NotFound();
            }

            return View(offeredService);
        }

        // POST: OfferedService/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var offeredService = await _context.OfferedServices.FindAsync(id);
            if (offeredService != null)
            {
                _context.OfferedServices.Remove(offeredService);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OfferedServiceExists(int id)
        {
            return _context.OfferedServices.Any(e => e.HairdresserId == id);
        }
    }
}
