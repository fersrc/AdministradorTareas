using AdministradorTareas.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using AdministradorTareas.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace AdministradorTareas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;          

        //}

        public async Task<IActionResult> Index()
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
                          where (O.estado == 1 )
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
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}