using GameMasterEnterprise.Domain.Models;

namespace GameMasterEnterprise.Domain.Intefaces
{
    public interface IPlayerRepository : IRepository<Player>
    {
        Task<Guid> ObterCassinoIdPorPlayerId(Guid playerId);
        Task<string> ObterNomeJogador(Guid id);
        Task<IEnumerable<Player>> ObterPlayersPorCassinoId(Guid cassinoId);
        Task<Player> ObterPorNome(string nome);
        Task<Player> ObterPorToken(string nome);
        Task<string> ObterTokenJogador(Guid id);
    }
}