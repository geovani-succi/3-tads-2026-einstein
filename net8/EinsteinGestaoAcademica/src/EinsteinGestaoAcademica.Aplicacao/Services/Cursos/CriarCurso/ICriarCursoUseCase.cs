using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EinsteinGestaoAcademica.Dominio.Entidades;

namespace EinsteinGestaoAcademica.Aplicacao.Services.Cursos.CriarCurso
{
    public interface ICriarCursoUseCase
    {
        Task CriarCurso(Curso curso);
    }
}