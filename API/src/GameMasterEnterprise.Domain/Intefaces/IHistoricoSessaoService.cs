
using GameMasterEnterprise.Domain.Models;

namespace GameMasterEnterprise.Domain.Intefaces
{
    public interface IHistoricoSessaoService
    {
        Task CriarHistoricoSessao(HistoricoSessao historicosessao);
        Task<HistoricoSessao> ObterHistoricoSessao(Guid historicosessaoId);
        Task<IEnumerable<HistoricoSessao>> ObterTodosHistoricosessaos();
        Task RemoverHistoricoSessao(Guid historicosessaoId);
    }
}