using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EinsteinGestaoAcademica.API.Requests
{
    public class RealizarLoginRequest
    {
        public string email { get; set; }
        public string senha { get; set; }
    }
}