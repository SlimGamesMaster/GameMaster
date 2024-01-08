
using GameMasterEnterprise.Domain.Models;

namespace GameMasterEnterprise.Domain.Intefaces
{
    public interface IPlayerService
    {
        Task AtualizarPlayer(Guid PlayerId, Player PlayerNovo);
        Task CriarPlayer(Player Player);
        Task<Guid> ObterCassinoIdPorPlayerId(Guid idPlayer);
        Task<Player> ObterPlayer(Guid PlayerId);
        Task<Guid> ObterPlayerIdPorToken(string Token);
        Task<Player> ObterPlayerPorNome(string Nome);
        Task<Player> ObterPlayerPorToken(string Token);
        Task<IEnumerable<Player>> ObterTodosPlayers();
        Task RemoverPlayer(Guid PlayerId);
    }
}