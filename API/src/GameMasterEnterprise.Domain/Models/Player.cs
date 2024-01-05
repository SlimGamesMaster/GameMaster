using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMasterEnterprise.Domain.Models
{
    public class Player : Entity
    {
        public Guid CassinoId {  get; set; }
        public Cassino Cassino { get; set; }
        public string Nome {  get; set; }
        public string Token { get; set; }

        public ICollection<Sessao>? Sessoes { get; set; }

    }
}
