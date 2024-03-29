using System.ComponentModel.DataAnnotations;

namespace Aula_P1.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        public ICollection <Curso> Cursos { get; set; }
        [Display(Name ="Categoria")]
        public string Nome { get; set; }

        public bool Disponivel { get; set; }

        public string Descricao { get; set; }

      
    }
}