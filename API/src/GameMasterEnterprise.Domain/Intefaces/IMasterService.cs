
using GameMasterEnterprise.Domain.Models;

namespace GameMasterEnterprise.Domain.Intefaces
{
    public interface IPlayerService
    {
        Task AtualizarPlayer(Guid PlayerId, Player PlayerNovo);
        Task CriarPlayer(Player Player);
        Task Dispose();
        Task<Player> ObterPlayer(Guid PlayerId);
        Task<IEnumerable<Player>> ObterTodosPlayers();
        Task RemoverPlayer(Guid PlayerId);
    }
}