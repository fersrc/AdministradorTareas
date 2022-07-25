using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdministradorTareas.Data;
using AdministradorTareas.Models;
using Microsoft.AspNetCore.Authorization;

namespace AdministradorTareas.Controllers
{
    [Authorize]
    public class categoriasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public categoriasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: categorias
        public async Task<IActionResult> Index()
        {
            return _context.categorias != null ?
                        View(await _context.categorias.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.categorias'  is null.");
        }


        // GET: categorias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: categorias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,nombre")] categorias categorias)
        {
            if (ModelState.IsValid)
            {
                if (!categoriasExistsName(categorias.nombre))
                {
                    _context.Add(categorias);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(categorias);
        }

        // GET: categorias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.categorias == null)
            {
                return NotFound();
            }

            var categorias = await _context.categorias.FindAsync(id);
            if (categorias == null)
            {
                return NotFound();
            }
            return View(categorias);
        }

        // POST: categorias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,nombre")] categorias categorias)
        {
            if (id != categorias.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categorias);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!categoriasExists(categorias.ID))
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
            return View(categorias);
        }

        private bool categoriasExists(int id)
        {
            return (_context.categorias?.Any(e => e.ID == id)).GetValueOrDefault();
        }

        private bool categoriasExistsName(string nombre)
        {
            return (_context.categorias?.Any(e => e.nombre == nombre)).GetValueOrDefault();
        }
    }
}