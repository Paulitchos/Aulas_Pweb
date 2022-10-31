using System.ComponentModel.DataAnnotations;

namespace Aula_P1.Models
{
    public class Curso
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public bool Disponivel { get; set; }


        public int? CategoriaId { get; set; }
        public Categoria categoria{ get; set; }

        public string Descricao { get; set; }

        public string DescricaoResumida { get; set; }

        public string Rquisitos { get; set; }
        [Display (Name ="Idade mínima",
            Prompt ="Introduza Idade mínima",
            Description = "Idade mínima para poder frequentar esta formação")]
        [Range(14, 100, ErrorMessage = "Mínimo: 14 anos, máximo: 100 anos")]
        public int IdadeMinima { get; set; }

        public decimal Price { get; set; }

    }
}