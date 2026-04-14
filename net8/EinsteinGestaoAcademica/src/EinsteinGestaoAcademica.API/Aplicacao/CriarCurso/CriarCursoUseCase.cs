using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EinsteinGestaoAcademica.API.Dominio.Entidades;
using EinsteinGestaoAcademica.API.Dominio.Repositorios;

namespace EinsteinGestaoAcademica.API.Aplicacao.CriarCurso
{
    public class CriarCursoUseCase : ICriarCursoUseCase
    {
        private readonly ICursoRepositorio cursoRepositorio;

        public CriarCursoUseCase(ICursoRepositorio cursoRepositorio)
        {
            this.cursoRepositorio = cursoRepositorio;
        }

        public async Task CriarCurso(Curso curso)
        {
            await cursoRepositorio.Criar(curso);
        }
    }
}