using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplicationCoreMvc.Data;
using WebApplicationCoreMvc.Models;

namespace WebApplicationCoreMvc.Controllers
{
    [Authorize]
    public class SuperHeroesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SuperHeroesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SuperHeroes
        public async Task<IActionResult> Index()
        {
              return _context.SuperHeroes != null ? 
                          View(await _context.SuperHeroes.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.SuperHeroes'  is null.");
        }

        // GET: SuperHeroes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SuperHeroes == null)
            {
                return NotFound();
            }

            var superHero = await _context.SuperHeroes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (superHero == null)
            {
                return NotFound();
            }

            return View(superHero);
        }

        // GET: SuperHeroes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SuperHeroes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,FirstName,LastName,Place")] SuperHero superHero)
        {
            if (ModelState.IsValid)
            {
                _context.Add(superHero);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(superHero);
        }

        // GET: SuperHeroes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SuperHeroes == null)
            {
                return NotFound();
            }

            var superHero = await _context.SuperHeroes.FindAsync(id);
            if (superHero == null)
            {
                return NotFound();
            }
            return View(superHero);
        }

        // POST: SuperHeroes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,FirstName,LastName,Place")] SuperHero superHero)
        {
            if (id != superHero.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(superHero);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SuperHeroExists(superHero.Id))
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
            return View(superHero);
        }

        // GET: SuperHeroes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SuperHeroes == null)
            {
                return NotFound();
            }

            var superHero = await _context.SuperHeroes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (superHero == null)
            {
                return NotFound();
            }

            return View(superHero);
        }

        // POST: SuperHeroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SuperHeroes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.SuperHeroes'  is null.");
            }
            var superHero = await _context.SuperHeroes.FindAsync(id);
            if (superHero != null)
            {
                _context.SuperHeroes.Remove(superHero);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SuperHeroExists(int id)
        {
          return (_context.SuperHeroes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
