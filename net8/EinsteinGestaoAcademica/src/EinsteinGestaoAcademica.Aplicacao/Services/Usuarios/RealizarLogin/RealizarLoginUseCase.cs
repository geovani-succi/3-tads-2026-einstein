using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EinsteinGestaoAcademica.Dominio.Entidades;
using EinsteinGestaoAcademica.Dominio.Repositorios;

namespace EinsteinGestaoAcademica.Aplicacao.Services.Usuarios.RealizarLogin
{
    public class RealizarLoginUseCase : IRealizarLoginUseCase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public RealizarLoginUseCase(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Usuario> RealizarLogin(string email, string senha)
        {
            return await _usuarioRepository.ObterUsuarioPorEmailESenha(email, senha);
        }
    }
}