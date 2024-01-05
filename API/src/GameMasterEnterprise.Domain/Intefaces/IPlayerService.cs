
using GameMasterEnterprise.Domain.Models;

namespace GameMasterEnterprise.Domain.Intefaces
{
    public interface IPlayerService
    {
        Task AtualizarPlayer(Guid PlayerId, Player PlayerNovo);
        Task CriarPlayer(Player Player);
        Task<Player> ObterPlayer(Guid PlayerId);
        Task<Player> ObterPlayerPorNome(string Nome);
        Task<Player> ObterPlayerPorToken(string Token);
        Task<IEnumerable<Player>> ObterTodosPlayers();
        Task RemoverPlayer(Guid PlayerId);
    }
}