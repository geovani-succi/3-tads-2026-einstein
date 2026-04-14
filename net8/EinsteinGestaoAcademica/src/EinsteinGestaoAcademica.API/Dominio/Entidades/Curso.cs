using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EinsteinGestaoAcademica.API.Dominio.Entidades
{
    public class Curso
    {
        public string nome { get; set; }
        public int carga_horaria { get; set; }
        public decimal valor { get; set; }
        public List<Disciplina> disciplinas { get; set; }
    }
}