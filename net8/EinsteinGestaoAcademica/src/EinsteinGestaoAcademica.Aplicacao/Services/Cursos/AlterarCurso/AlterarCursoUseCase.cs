using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EinsteinGestaoAcademica.Dominio.Entidades;
using EinsteinGestaoAcademica.Dominio.Repositorios;

namespace EinsteinGestaoAcademica.Aplicacao.Services.Cursos.AlterarCurso
{
    public class AlterarCursoUseCase: IAlterarCursoUseCase
    {
        private readonly ICursoRepositorio cursoRepositorio;

        public AlterarCursoUseCase(ICursoRepositorio cursoRepositorio)
        {
            this.cursoRepositorio = cursoRepositorio;
        }

        public async Task AlterarCurso(Curso curso)
        {
            await cursoRepositorio.Alterar(curso);
        }
    }
}