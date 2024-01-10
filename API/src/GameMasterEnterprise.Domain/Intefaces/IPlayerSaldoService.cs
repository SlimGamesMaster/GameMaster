
using GameMasterEnterprise.Domain.Models;

namespace GameMasterEnterprise.Domain.Intefaces
{
    public interface IPlayerSaldoService
    {
        Task AtualizarPlayerSaldo(Guid PlayerSaldoId, float saldoNovo);
        Task CriarPlayerSaldo(PlayerSaldo PlayerSaldo);
        Task Dispose();
        Task<Guid> ObterIdPlayerSaldo(Guid playerId);
        Task<PlayerSaldo> ObterPlayerSaldo(Guid PlayerId);
        Task RemoverPlayerSaldo(Guid PlayerSaldoId);
    }
}