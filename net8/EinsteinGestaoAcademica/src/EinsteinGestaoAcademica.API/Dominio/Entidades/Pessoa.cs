using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EinsteinGestaoAcademica.API.Dominio.Entidades
{
    public class Pessoa
    {
        public string nome { get; set; }
        public string cpf { get; set; }
        public string telefone { get; set; }
        public string cidade { get; set; }
        public string estado { get; set; }
    }
}