using GameMasterEnterprise.Domain.Models;

namespace GameMasterEnterprise.Domain.Intefaces
{
    public interface IHistoricoSessaoRepository : IRepository<HistoricoSessao>
    {
        Task<List<HistoricoSessao>> ObterPorFiltroCassino(string nomeCassino, DateTime? dataLimiteInferior = null, DateTime? dataLimiteSuperior = null);
        Task<List<HistoricoSessao>> ObterSaldosPorSessaoId(Guid sessaoId);
        Task<List<HistoricoSessao>> ObterUltimos100(string nomeJogo);
    }
}