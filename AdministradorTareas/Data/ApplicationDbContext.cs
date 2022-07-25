using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AdministradorTareas.Models;

namespace AdministradorTareas.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<AdministradorTareas.Models.categorias> categorias { get; set; }
        public DbSet<AdministradorTareas.Models.tareas>? tareas { get; set; }
        public DbSet<AdministradorTareas.Models.estadoTarea>? estadoTarea { get; set; }
    }
}