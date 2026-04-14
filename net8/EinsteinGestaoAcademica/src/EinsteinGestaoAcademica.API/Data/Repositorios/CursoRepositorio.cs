using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EinsteinGestaoAcademica.API.Dominio.Entidades;
using EinsteinGestaoAcademica.API.Dominio.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace EinsteinGestaoAcademica.API.Data.Repositorios
{
    public class CursoRepositorio : ICursoRepositorio
    {
        private readonly ApplicationDbContext _context;

        public CursoRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Alterar(Curso curso)
        {
            _context.Cursos.Update(curso);
            await _context.SaveChangesAsync();
        }

        public async Task Criar(Curso curso)
        {
            await _context.Cursos.AddAsync(curso);
            await _context.SaveChangesAsync();
        }
         public async Task Deletar(Curso curso)
        {
            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();
        }

        public async Task<Curso> ObterCurso(int id)
        {
            var curso = _context.Cursos
                .FromSql($"SELECT * FROM curso where id = {id}");

            return await curso.FirstOrDefaultAsync();
        }

        public async Task<List<Curso>> ObterCursos()
        {
            var cursos = _context.Database
            .SqlQuery<Curso>($"Select * From curso");
            //Join iremos fazer depois

            return await cursos.ToListAsync();
        }
    }
}