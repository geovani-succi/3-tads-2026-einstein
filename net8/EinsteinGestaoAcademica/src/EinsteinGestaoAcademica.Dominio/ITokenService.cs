using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EinsteinGestaoAcademica.Dominio.Entidades;

namespace EinsteinGestaoAcademica.Dominio
{
    public interface ITokenService
    {
        string GerarToken(Usuario usuario);
    }
}