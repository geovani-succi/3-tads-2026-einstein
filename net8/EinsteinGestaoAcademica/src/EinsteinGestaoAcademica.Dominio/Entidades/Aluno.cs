using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EinsteinGestaoAcademica.Dominio.Entidades
{
    public class Aluno : Pessoa
    {
        public int id_curso { get; set; }
        [NotMapped]
        public Curso curso { get; set; }
    }
}