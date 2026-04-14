using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EinsteinGestaoAcademica.API.Dominio.Entidades
{
    public class Curso
    {
        public int id { get; set; }
        public string nome { get; set; }
        public int carga_horaria { get; set; }
        public decimal valor { get; set; }
        [NotMapped]
        public List<Disciplina> disciplinas { get; set; }
    }
}