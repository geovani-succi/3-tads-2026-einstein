using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EinsteinGestaoAcademica.API.Dominio.Entidades;

namespace EinsteinGestaoAcademica.API.Aplicacao.AlterarCurso
{
    public interface IAlterarCursoUseCase
    {
        Task AlterarCurso(Curso curso);
    }
}