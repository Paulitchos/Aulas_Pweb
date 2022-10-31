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
        [Display (Name ="Idade m�nima",
            Prompt ="Introduza Idade m�nima",
            Description = "Idade m�nima para poder frequentar esta forma��o")]
        [Range(14, 100, ErrorMessage = "M�nimo: 14 anos, m�ximo: 100 anos")]
        public int IdadeMinima { get; set; }

        public decimal Price { get; set; }

    }
}