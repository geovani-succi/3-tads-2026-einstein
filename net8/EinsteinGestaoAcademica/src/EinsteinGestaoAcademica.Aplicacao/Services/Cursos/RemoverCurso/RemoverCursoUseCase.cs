using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EinsteinGestaoAcademica.Dominio.Repositorios;

namespace EinsteinGestaoAcademica.Aplicacao.Services.Cursos.RemoverCurso
{
    public class RemoverCursoUseCase: IRemoverCursoUseCase
    {
        private readonly ICursoRepositorio cursoRepositorio;

        public RemoverCursoUseCase(ICursoRepositorio cursoRepositorio)
        {
            this.cursoRepositorio = cursoRepositorio;
        }

        public async Task RemoverCurso(int id)
        {
            var curso = await cursoRepositorio.ObterCurso(id);

            await cursoRepositorio.Deletar(curso);
        }
    }
}