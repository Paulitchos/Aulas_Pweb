namespace Aula_P1.Models
{
    public class Curso
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public bool Disponivel { get; set; }

        public string Categoria { get; set; }

        public string Descricao { get; set; }

        public string DescricaoResumida { get; set; }

        public string Rquisitos { get; set; }

        public int IdadeMinima { get; set; }

        public decimal Price { get; set; }

    }
}