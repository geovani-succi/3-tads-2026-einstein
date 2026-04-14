using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EinsteinGestaoAcademica.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MeuEndpointController : ControllerBase
    {
        [HttpGet("obter-dados")]
        public IActionResult ObterDados()
        {
            return Ok("ERRO 500"); //200
        }

        [HttpPost("incluir")]
        public IActionResult Incluir()
        {
            return Created(); //201
        }

        [HttpPut("atualizar")]
        public IActionResult Atualizar()
        {
            return NoContent(); //204
        }

        [HttpDelete("excluir")]
        public IActionResult Excluir()
        {
            return NoContent(); //204
        }
    }
}