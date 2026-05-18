using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EinsteinGestaoAcademica.Dominio.Entidades;

namespace EinsteinGestaoAcademica.Aplicacao.Services.Usuarios.RealizarLogin
{
    public interface IRealizarLoginUseCase
    {
        Task<Usuario> RealizarLogin(string email, string senha);
    }
}