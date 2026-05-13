using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EinsteinGestaoAcademica.Dominio.Entidades;

namespace EinsteinGestaoAcademica.Aplicacao.Services.Cursos.AlterarCurso
{
    public interface IAlterarCursoUseCase
    {
         Task AlterarCurso(Curso curso);
    }
}