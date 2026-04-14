using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EinsteinGestaoAcademica.API.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace EinsteinGestaoAcademica.API.Data
{
    public class ApplicationDbContext : DbContext
    {
         public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

        public DbSet<Curso> Cursos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<Curso>(e =>
            {
                e.ToTable("curso", "public");
                e.Ignore(x => x.disciplinas);
                e.HasKey(x => x.id);
            });

        }

    }
}