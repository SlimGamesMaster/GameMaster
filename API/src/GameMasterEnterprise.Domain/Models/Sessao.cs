using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMasterEnterprise.Domain.Models
{
    public class Sessao : Entity
    {
        public int Dificuldade {  get; set; }

        public Guid JogoId { get; set; }
        public Jogo? Jogo { get; set; }

        public Guid PlayerId { get; set; }
        public Player? Player { get; set; }

        public Guid CassinoId { get; set; }
        public Cassino? Cassino { get; set; }

        public int? Situacao { get; set; }

    }
}
