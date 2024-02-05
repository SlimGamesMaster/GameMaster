using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMasterEnterprise.Domain.Models
{
    public class ResponseHistoricoJogador
    {
        public HistoricoSessao HistoricoSessaoAtivo { get; set; }
        public string NomePlayer { get; set; }

        public string TokenPlayer { get; set; }

    }
}
