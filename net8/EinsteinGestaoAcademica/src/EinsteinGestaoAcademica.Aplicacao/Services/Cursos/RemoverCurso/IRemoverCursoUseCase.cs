using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EinsteinGestaoAcademica.Aplicacao.Services.Cursos.RemoverCurso
{
    public interface IRemoverCursoUseCase
    {
         Task RemoverCurso(int id);
    }
}