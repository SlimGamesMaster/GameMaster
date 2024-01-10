using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMasterEnterprise.Domain.Models
{
    public class HistoricoSessao : Entity
    {
        public Guid SessaoId { get; set; }
        public string Operacao { get; set; }
        public float Valor { get; set; }
        public Sessao Sessao { get; set; }

    }
}
