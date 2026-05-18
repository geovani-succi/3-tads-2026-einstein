using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EinsteinGestaoAcademica.API.Requests
{
    public class CriarUsuarioRequest
    {
        public int? id_professor { get; set; }
        public int? id_aluno { get; set; }
        public string email { get; set; }
        public string senha { get; set; }
    }
}