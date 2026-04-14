using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EinsteinGestaoAcademica.API.Dominio.Entidades
{
    public class Professor : Pessoa
    {
        public List<Curso> cursos { get; set; }
    }
}