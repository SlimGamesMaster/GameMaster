using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMasterEnterprise.Domain.Models
{
    public class ResponseHistoricoCassino
    {
        public IEnumerable<HistoricoCassino> HistoricoCassino { get; set; }
    }
    public class HistoricoCassino
    {
        public HistoricoSessao HistoricoSessao { get; set; }
        public string Cassino {  get; set; }
        public Player Player { get; set; }
    }
}
