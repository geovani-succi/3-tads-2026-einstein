using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EinsteinGestaoAcademica.Dominio.Enums;

namespace EinsteinGestaoAcademica.Dominio.Entidades
{
    public class Disciplina
    {
        public string nome { get; set; }
        public Periodo periodo { get; set; }
        public string dia_semana { get; set; }
    }
}