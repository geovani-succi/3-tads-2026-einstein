using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EinsteinGestaoAcademica.API.Dominio.Entidades;

namespace EinsteinGestaoAcademica.API.Dominio.Repositorios
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