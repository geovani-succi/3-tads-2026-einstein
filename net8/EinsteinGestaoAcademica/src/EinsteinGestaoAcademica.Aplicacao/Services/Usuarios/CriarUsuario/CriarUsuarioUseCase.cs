using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EinsteinGestaoAcademica.Dominio.Entidades;
using EinsteinGestaoAcademica.Dominio.Repositorios;

namespace EinsteinGestaoAcademica.Aplicacao.Services.Usuarios.CriarUsuario
{
    public class CriarUsuarioUseCase : ICriarUsuarioUseCase
    {
        private readonly IUsuarioRepository usuarioRepository;

        public CriarUsuarioUseCase(IUsuarioRepository usuarioRepository)
        {
            this.usuarioRepository = usuarioRepository;
        }

        public async Task CriarUsuario(Usuario usuario)
        {
            await usuarioRepository.Criar(usuario);
        }
    }
}