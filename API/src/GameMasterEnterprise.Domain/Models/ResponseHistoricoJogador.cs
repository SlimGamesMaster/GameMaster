using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMasterEnterprise.Domain.Models
{
    public class ResponseHistoricoJogador
    {
        public float Credit { get; set; }

        public float Debit { get; set; }

        public int CreditCount { get; set; }

        public int DebitCount { get; set; }

    }
}
