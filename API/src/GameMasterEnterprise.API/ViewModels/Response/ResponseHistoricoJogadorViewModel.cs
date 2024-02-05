using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameMasterEnterprise.Domain.Models;

namespace GameMasterEnterprise.API.ViewModels.Response
{
    public class ResponseHistoricoJogadorViewModel
    {
        public HistoricoSessao HistoricoSessao { get; set; }
        public string NomePlayer { get; set; }

        public string TokenPlayer { get; set; }

    }
}
