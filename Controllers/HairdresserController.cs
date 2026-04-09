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
    public class HairdresserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HairdresserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Hairdresser
        public async Task<IActionResult> Index()
        {
            return View(await _context.Hairdressers.ToListAsync());
        }

        // GET: Hairdresser/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hairdresser = await _context.Hairdressers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hairdresser == null)
            {
                return NotFound();
            }

            return View(hairdresser);
        }

        // GET: Hairdresser/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hairdresser/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fname,Lname,UserId")] Hairdresser hairdresser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hairdresser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hairdresser);
        }

        // GET: Hairdresser/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hairdresser = await _context.Hairdressers.FindAsync(id);
            if (hairdresser == null)
            {
                return NotFound();
            }
            return View(hairdresser);
        }

        // POST: Hairdresser/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fname,Lname,UserId")] Hairdresser hairdresser)
        {
            if (id != hairdresser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hairdresser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HairdresserExists(hairdresser.Id))
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
            return View(hairdresser);
        }

        // GET: Hairdresser/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hairdresser = await _context.Hairdressers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hairdresser == null)
            {
                return NotFound();
            }

            return View(hairdresser);
        }

        // POST: Hairdresser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hairdresser = await _context.Hairdressers.FindAsync(id);
            if (hairdresser != null)
            {
                _context.Hairdressers.Remove(hairdresser);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HairdresserExists(int id)
        {
            return _context.Hairdressers.Any(e => e.Id == id);
        }
    }
}
