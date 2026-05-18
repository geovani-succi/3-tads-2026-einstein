using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EinsteinGestaoAcademica.Dominio.Entidades;

namespace EinsteinGestaoAcademica.Dominio.Repositorios
{
    public interface IAlunoRepository
    {
        Task Criar(Aluno aluno);
    }
}