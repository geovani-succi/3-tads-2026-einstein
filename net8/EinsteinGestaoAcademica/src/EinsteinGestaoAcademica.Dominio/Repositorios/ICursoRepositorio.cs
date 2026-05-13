using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EinsteinGestaoAcademica.Dominio.Entidades;

namespace EinsteinGestaoAcademica.Dominio.Repositorios
{
    public interface ICursoRepositorio
    {
        Task Criar(Curso curso);
        Task Alterar(Curso curso);
        Task Deletar(Curso curso);
        Task<Curso> ObterCurso(int id);
        Task<List<Curso>> ObterCursos();
    }
}