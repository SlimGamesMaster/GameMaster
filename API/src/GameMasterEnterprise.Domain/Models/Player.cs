using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMasterEnterprise.Domain.Models
{
    public class Player : Entity
    {
        public string Nome {  get; set; }
        public int Token { get; set; }
        public ICollection<Sessao> Sessoes { get; set; }
    }
}
