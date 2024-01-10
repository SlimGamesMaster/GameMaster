using GameMasterEnterprise.Domain.Models;

namespace GameMasterEnterprise.Domain.Intefaces
{
    public interface ISessaoRepository : IRepository<Sessao>
    {
        Task<Guid> GerarSessao(Sessao sessao);
        Task<Guid> ObterCassinoIdPorSessaoId(Guid sessaoId);
        Task<Guid> ObterJogoIdPorSessaoId(Guid sessaoId);
        Task<Guid> ObterPlayerIdPorSessaoId(Guid sessaoId);
    }
}