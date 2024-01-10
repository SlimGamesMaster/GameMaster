using GameMasterEnterprise.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace GameMasterEnterprise.Domain.Models
{
    public class Master : Entity
    {
        public string Metodo { get; set; }
        public string NomeJogador { get; set; }
        public string TokenJogador { get; set; }
        public string TokenCassino { get; set; }
        public string ProviderCode { get; set; }
        public string CodigoJogo { get; set; }

        public int Dificuldade { get; set; }
    }
}
