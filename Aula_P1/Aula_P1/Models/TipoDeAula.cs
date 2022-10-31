using System.ComponentModel.DataAnnotations;

namespace Aula_P1.Models
{
    public class TipoDeAula
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public decimal ValorHora { get; set; }

      
    }
}