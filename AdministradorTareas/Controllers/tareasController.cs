using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdministradorTareas.Data;
using AdministradorTareas.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace AdministradorTareas.Controllers
{
    [Authorize]
    public class tareasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public tareasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: tareas
        public async Task<IActionResult> Index(int f)
        {
            f = f == 0 ? 1 : f;
            //var allTareas = await _context.tareas.ToListAsync();

            if (f >= 1 && f <= 3)
            {
                var tareas = (from O in _context.tareas
                              join C in _context.categorias on O.categoria equals C.ID into CC
                              from CCC in CC.DefaultIfEmpty()
                              join A in _context.Users on O.asignacion equals A.Id into AA
                              from AAA in AA.DefaultIfEmpty()
                              join R in _context.Users on O.asignacion equals R.Id into RR
                              from RRR in RR.DefaultIfEmpty()
                              join E in _context.estadoTarea on O.estado equals E.ID into EE
                              from EEE in EE.DefaultIfEmpty()
                              orderby O.ID descending
                              where (O.estado == f)
                              select new
                              {
                                  ID = O.ID,
                                  idas = O.asignacion,
                                  categoria = CCC.nombre,
                                  tarea = O.tarea,
                                  asignacion = AAA.UserName,
                                  creador = RRR.UserName,
                                  fechaLimite = O.fechaLimite,
                                  estado = EEE.estado
                              }).ToList();
                ViewData["tareas"] = tareas;
            }
            else
            {
                var tareas = (from O in _context.tareas
                              join C in _context.categorias on O.categoria equals C.ID into CC
                              from CCC in CC.DefaultIfEmpty()
                              join A in _context.Users on O.asignacion equals A.Id into AA
                              from AAA in AA.DefaultIfEmpty()
                              join R in _context.Users on O.asignacion equals R.Id into RR
                              from RRR in RR.DefaultIfEmpty()
                              join E in _context.estadoTarea on O.estado equals E.ID into EE
                              from EEE in EE.DefaultIfEmpty()
                              orderby O.ID descending
                              select new
                              {
                                  ID = O.ID,
                                  idas = O.asignacion,
                                  categoria = CCC.nombre,
                                  tarea = O.tarea,
                                  asignacion = AAA.UserName,
                                  creador = RRR.UserName,
                                  fechaLimite = O.fechaLimite,
                                  estado = EEE.estado
                              }).ToList();
                ViewData["tareas"] = tareas;
            }
            ViewData["filtro"] = f;
            ViewData["id"] = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View();
        }

        // GET: tareas/Create
        public async Task<IActionResult> Create()
        {
            var Categorias = await _context.categorias.ToListAsync();
            ViewData["categorias"] = Categorias;
            var usuarios = await _context.Users.ToListAsync();
            ViewData["usuarios"] = usuarios;
            return View();
        }

        // POST: tareas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,categoria,tarea,asignacion,creador,fechaCreacion,fechaLimite,estado")] tareas tareas)
        {
            tareas.creador = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context.Add(tareas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: tareas/Edit/5
        public async Task<IActionResult> Terminar(int? id)
        {
            var tarea = await _context.tareas.Where(p => p.ID == id && p.asignacion == User.FindFirstValue(ClaimTypes.NameIdentifier)).ToListAsync();
            if (tarea != null)
            {
                tarea[0].estado = 2;
                _context.Update(tarea[0]);
                await _context.SaveChangesAsync();
                            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Cancelar(int? id)
        {
            var tarea = await _context.tareas.Where(p => p.ID == id && p.asignacion == User.FindFirstValue(ClaimTypes.NameIdentifier)).ToListAsync();
            if (tarea != null)
            {
                tarea[0].estado = 3;
                _context.Update(tarea[0]);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool tareasExists(int id)
        {
            return (_context.tareas?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
