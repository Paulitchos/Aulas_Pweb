using Microsoft.AspNetCore.Identity;

namespace PWEB_AulasP_2223.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string PrimeiroNome { get; set; }

        public string UltimoNome { get; set; }

        public DateTime DataNascimento { get; set; }

        public int NIF { get; set; }

        public ICollection<Agendamento> Agendamentos { get; set; }
    }
}
