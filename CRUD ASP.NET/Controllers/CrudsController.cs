using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUD_ASP.NET.Data;
using CRUD_ASP.NET.Models;

namespace CRUD_ASP.NET.Controllers
{
    public class CrudsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CrudsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cruds
        public async Task<IActionResult> Index()
        {
            return View(await _context.Crud.ToListAsync());
        }

        // GET: Cruds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crud = await _context.Crud
                .FirstOrDefaultAsync(m => m.id == id);
            if (crud == null)
            {
                return NotFound();
            }

            return View(crud);
        }

        // GET: Cruds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cruds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,description")] Crud crud)
        {
            if (ModelState.IsValid)
            {
                _context.Add(crud);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(crud);
        }

        // GET: Cruds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crud = await _context.Crud.FindAsync(id);
            if (crud == null)
            {
                return NotFound();
            }
            return View(crud);
        }

        // POST: Cruds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,description")] Crud crud)
        {
            if (id != crud.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(crud);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CrudExists(crud.id))
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
            return View(crud);
        }

        // GET: Cruds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crud = await _context.Crud
                .FirstOrDefaultAsync(m => m.id == id);
            if (crud == null)
            {
                return NotFound();
            }

            return View(crud);
        }

        // POST: Cruds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var crud = await _context.Crud.FindAsync(id);
            _context.Crud.Remove(crud);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CrudExists(int id)
        {
            return _context.Crud.Any(e => e.id == id);
        }
    }
}
