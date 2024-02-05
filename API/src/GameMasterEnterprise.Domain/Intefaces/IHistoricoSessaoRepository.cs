using GameMasterEnterprise.Domain.Models;

namespace GameMasterEnterprise.Domain.Intefaces
{
    public interface IHistoricoSessaoRepository : IRepository<HistoricoSessao>
    {
        Task<List<HistoricoSessao>> ObterSaldosPorSessaoId(Guid sessaoId);
        Task<List<HistoricoSessao>> ObterUltimos100();
    }
}