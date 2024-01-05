using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMasterEnterprise.Domain.Models
{
    public class PlayerSaldo : Entity
    {
        public Guid PlayerId {  get; set; }
        public float Saldo {  get; set; }         
        public Player Player { get; set; }

    }
}
