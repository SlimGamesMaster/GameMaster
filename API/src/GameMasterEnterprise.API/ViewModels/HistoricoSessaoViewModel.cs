using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMasterEnterprise.Domain.Models
{
    public class HistoricoSessaoViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid SessaoId { get; set; }
        public string Operacao { get; set; }
        public float Valor { get; set; }
        public Sessao Sessao { get; set; }

    }
}
