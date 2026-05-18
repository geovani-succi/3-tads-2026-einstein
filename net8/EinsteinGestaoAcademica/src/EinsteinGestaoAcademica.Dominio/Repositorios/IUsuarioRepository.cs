using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EinsteinGestaoAcademica.Dominio.Entidades;

namespace EinsteinGestaoAcademica.Dominio.Repositorios
{
    public interface IUsuarioRepository
    {
        Task Criar(Usuario usuario);

        Task<Usuario> ObterUsuarioPorEmailESenha(string email, string senha);
    }
}