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
    public class BookedServiceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookedServiceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BookedService
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BookedServices.Include(b => b.Booking).Include(b => b.Service);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BookedService/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookedService = await _context.BookedServices
                .Include(b => b.Booking)
                .Include(b => b.Service)
                .FirstOrDefaultAsync(m => m.BookingId == id);
            if (bookedService == null)
            {
                return NotFound();
            }

            return View(bookedService);
        }

        // GET: BookedService/Create
        public IActionResult Create()
        {
            ViewData["BookingId"] = new SelectList(_context.Booking, "Id", "Id");
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Title");
            return View();
        }

        // POST: BookedService/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingId,ServiceId")] BookedService bookedService)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookedService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookingId"] = new SelectList(_context.Booking, "Id", "Id", bookedService.BookingId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Title", bookedService.ServiceId);
            return View(bookedService);
        }

        // GET: BookedService/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookedService = await _context.BookedServices.FindAsync(id);
            if (bookedService == null)
            {
                return NotFound();
            }
            ViewData["BookingId"] = new SelectList(_context.Booking, "Id", "Id", bookedService.BookingId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Title", bookedService.ServiceId);
            return View(bookedService);
        }

        // POST: BookedService/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingId,ServiceId")] BookedService bookedService)
        {
            if (id != bookedService.BookingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookedService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookedServiceExists(bookedService.BookingId))
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
            ViewData["BookingId"] = new SelectList(_context.Booking, "Id", "Id", bookedService.BookingId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Title", bookedService.ServiceId);
            return View(bookedService);
        }

        // GET: BookedService/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookedService = await _context.BookedServices
                .Include(b => b.Booking)
                .Include(b => b.Service)
                .FirstOrDefaultAsync(m => m.BookingId == id);
            if (bookedService == null)
            {
                return NotFound();
            }

            return View(bookedService);
        }

        // POST: BookedService/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookedService = await _context.BookedServices.FindAsync(id);
            if (bookedService != null)
            {
                _context.BookedServices.Remove(bookedService);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookedServiceExists(int id)
        {
            return _context.BookedServices.Any(e => e.BookingId == id);
        }
    }
}
