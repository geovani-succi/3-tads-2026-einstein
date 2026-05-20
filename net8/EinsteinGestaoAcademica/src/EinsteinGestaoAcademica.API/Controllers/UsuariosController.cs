using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EinsteinGestaoAcademica.API.Requests;
using EinsteinGestaoAcademica.Aplicacao.Services.Usuarios.CriarUsuario;
using EinsteinGestaoAcademica.Aplicacao.Services.Usuarios.RealizarLogin;
using EinsteinGestaoAcademica.Dominio;
using EinsteinGestaoAcademica.Dominio.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EinsteinGestaoAcademica.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly ICriarUsuarioUseCase criarUsuarioUseCase;
        private readonly IRealizarLoginUseCase realizarLoginUseCase;

        private readonly ITokenService tokenService;

        public UsuariosController(ICriarUsuarioUseCase criarUsuarioUseCase, IRealizarLoginUseCase realizarLoginUseCase, ITokenService tokenService)
        {
            this.criarUsuarioUseCase = criarUsuarioUseCase;
            this.realizarLoginUseCase = realizarLoginUseCase;
            this.tokenService = tokenService;
        }

        [HttpPost]
        public async Task<IActionResult> CriarUsuario([FromBody] CriarUsuarioRequest request)
        {
            try
            {
                await criarUsuarioUseCase.CriarUsuario(new Usuario()
                {
                    id_aluno = request.id_aluno,
                    id_professor = request.id_professor,
                    email = request.email,
                    senha = request.senha
                });

                return Created();
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }


        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> RealizarLogin([FromBody] RealizarLoginRequest request)
        {
            try
            {
                var usuario = await realizarLoginUseCase.RealizarLogin(request.email, request.senha);
                if (usuario == null)
                {
                    return Forbid();
                }
                var token = tokenService.GerarToken(usuario);
                return Ok(new { Token = token });
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }
    }
}