using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Aula_P1.Models
{
    public class PesquisaCursoViewModel
    {
        public List<Curso> ListaDeCursos { get; set; }
        public int NumResultados { get; set; }

        [Display(Name = "Pesquisa de Cursos", Prompt = "Curso a pesquisar")]
        public string TextoAPesquisar { get; set; }

    }
}