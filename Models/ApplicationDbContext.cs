using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GestionDeTareas.Models
{
    public class TareasDBDbContext : DbContext
    {
        
        public TareasDBDbContext(DbContextOptions<TareasDBDbContext> options) : base(options)
        {

        }
        public DbSet<Tarea> Tareas { get; set; } = null!;

    }
}
