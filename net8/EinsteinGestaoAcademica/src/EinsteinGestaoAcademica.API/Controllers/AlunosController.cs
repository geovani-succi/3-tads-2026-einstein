using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EinsteinGestaoAcademica.API.Requests;
using EinsteinGestaoAcademica.Aplicacao.Services.Alunos.CriarAluno;
using EinsteinGestaoAcademica.Dominio.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EinsteinGestaoAcademica.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class AlunosController : ControllerBase
    {
        private readonly ICriarAlunoUseCase criarAlunoUseCase;

        public AlunosController(ICriarAlunoUseCase criarAlunoUseCase)
        {
            this.criarAlunoUseCase = criarAlunoUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> CriarAluno([FromBody] CriarAlunoRequest criarAlunoRequest)
        {
            try
            {
                await criarAlunoUseCase.CriarAluno(new Aluno()
                {
                    nome = criarAlunoRequest.nome,
                    cpf = criarAlunoRequest.cpf,
                    estado = criarAlunoRequest.estado,
                    cidade = criarAlunoRequest.cidade,
                    telefone = criarAlunoRequest.telefone,
                    id_curso = criarAlunoRequest.id_curso
                });

                return Created();
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }
    }
}