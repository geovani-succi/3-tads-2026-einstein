using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EinsteinGestaoAcademica.Dominio.Entidades;
using EinsteinGestaoAcademica.Dominio.Repositorios;

namespace EinsteinGestaoAcademica.Aplicacao.Services.Alunos.CriarAluno
{
    public class CriarAlunoUseCase : ICriarAlunoUseCase
    {
        private readonly IAlunoRepository alunoRepository;

        public CriarAlunoUseCase(IAlunoRepository alunoRepository)
        {
            this.alunoRepository = alunoRepository;
        }

        public async Task CriarAluno(Aluno aluno)
        {
            await alunoRepository.Criar(aluno);
        }
    }
}