using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EinsteinGestaoAcademica.API.Dominio.Entidades;
using EinsteinGestaoAcademica.API.Dominio.Repositorios;

namespace EinsteinGestaoAcademica.API.Aplicacao.AlterarCurso
{
    public class AlterarCursoUseCase : IAlterarCursoUseCase
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