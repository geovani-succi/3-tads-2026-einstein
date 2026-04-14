using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EinsteinGestaoAcademica.API.Requests
{
    public class CriarCursoRequest
    {
        public string nome { get; set; }
        public int carga_horaria { get; set; }
        public decimal valor { get; set; }
    }
}