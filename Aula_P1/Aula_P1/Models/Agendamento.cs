using System.ComponentModel.DataAnnotations;

namespace Aula_P1.Models
{
    public class Agendamento
    {
        public int Id { get; set; }

        public string Cliente { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }

        public int DuracaoHoras { get; set; }

        public int DuracaoMinutos { get; set; }

        public decimal Preco { get; set; }

        public DateTime DataHoraDoPedido { get; set; }

        public int TipoDeAulaID { get; set; }
        public TipoDeAula tipoDeAula { get; set; }
      
    }
}