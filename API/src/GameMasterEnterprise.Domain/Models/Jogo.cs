using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMasterEnterprise.Domain.Models
{
    public class Jogo : Entity
    {
        public string Nome {  get; set; }
        public int Codigo { get; set; }
        public ICollection<Sessao> Sessoes { get; set; }
    }
}
