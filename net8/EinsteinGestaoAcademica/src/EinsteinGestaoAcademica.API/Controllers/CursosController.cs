using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EinsteinGestaoAcademica.API.Aplicacao.AlterarCurso;
using EinsteinGestaoAcademica.API.Aplicacao.CriarCurso;
using EinsteinGestaoAcademica.API.Aplicacao.RemoverCurso;
using EinsteinGestaoAcademica.API.Dominio.Entidades;
using EinsteinGestaoAcademica.API.Dominio.Repositorios;
using EinsteinGestaoAcademica.API.Requests;
using Microsoft.AspNetCore.Mvc;

namespace EinsteinGestaoAcademica.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CursosController : ControllerBase
    {
        

        private readonly ICriarCursoUseCase criarCursoUseCase;
        private readonly IAlterarCursoUseCase alterarCursoUseCase;
        private readonly IRemoverCursoUseCase removerCursoUseCase;

        private readonly ICursoRepositorio cursoRepositorio;

        public CursosController(ICriarCursoUseCase criarCursoUseCase, IAlterarCursoUseCase alterarCursoUseCase, IRemoverCursoUseCase removerCursoUseCase, ICursoRepositorio cursoRepositorio)
        {
            this.criarCursoUseCase = criarCursoUseCase;
            this.alterarCursoUseCase = alterarCursoUseCase;
            this.removerCursoUseCase = removerCursoUseCase;
            this.cursoRepositorio = cursoRepositorio;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CriarCursoRequest request)
        {
            try
            {
                var curso = new Curso();
                curso.nome = request.nome;
                curso.carga_horaria = request.carga_horaria;
                curso.valor = request.valor;

                await criarCursoUseCase.CriarCurso(curso);

                return Created();
            }
            catch (System.Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Post([FromRoute] int id, [FromBody] AlterarCursoRequest request)
        {
            try
            {
                var curso = new Curso();
                curso.id = id;
                curso.nome = request.nome;
                curso.carga_horaria = request.carga_horaria;
                curso.valor = request.valor;

                await alterarCursoUseCase.AlterarCurso(curso);

                return NoContent();
            }
            catch (System.Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            try
            {
                await removerCursoUseCase.RemoverCurso(id);

                return NoContent();
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }
        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            try
            {
                var cursos = await cursoRepositorio.ObterCursos();

                return Ok(cursos);
            }
            catch (System.Exception ex)
            {
                return NoContent();
            }
        }
    }
}