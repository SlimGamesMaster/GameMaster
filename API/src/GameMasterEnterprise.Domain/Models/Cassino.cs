using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMasterEnterprise.Domain.Models
{
    public class Cassino : Entity
    {
        public string Nome {  get; set; }

        public string Token { get; set; }
        public string Url { get; set; }

        public float Banco {  get; set; }
        public ICollection<Player>? Players { get; set; }
        public ICollection<Sessao>? Sessoes { get; set; }
    }
}
