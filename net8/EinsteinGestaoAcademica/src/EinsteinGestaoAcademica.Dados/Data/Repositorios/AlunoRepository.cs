using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EinsteinGestaoAcademica.Dominio.Entidades;
using EinsteinGestaoAcademica.Dominio.Repositorios;

namespace EinsteinGestaoAcademica.Dados.Data.Repositorios
{
    public class AlunoRepository : IAlunoRepository
    {
         private readonly ApplicationDbContext _context;

        public AlunoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Criar(Aluno aluno)
        {
            await _context.Alunos.AddAsync(aluno);

            await _context.SaveChangesAsync();
        }
    }
}