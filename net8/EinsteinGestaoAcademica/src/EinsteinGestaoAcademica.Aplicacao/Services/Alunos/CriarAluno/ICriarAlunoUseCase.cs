using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EinsteinGestaoAcademica.Dominio.Entidades;

namespace EinsteinGestaoAcademica.Aplicacao.Services.Alunos.CriarAluno
{
    public interface ICriarAlunoUseCase
    {
        Task CriarAluno(Aluno aluno);
    }
}