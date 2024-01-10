
using GameMasterEnterprise.Domain.Models;

namespace GameMasterEnterprise.Domain.Intefaces
{
    public interface IPlayerSaldoService
    {
        Task AtualizarPlayerSaldo(Guid PlayerSaldoId, float saldoNovo);
        Task CriarPlayerSaldo(PlayerSaldo PlayerSaldo);
        Task Dispose();
        Task<PlayerSaldo> ObterPlayerSaldo(Guid PlayerSaldoId);
        Task<Guid> ObterPorPlayerPlayerSaldo(Guid playerId);
        Task RemoverPlayerSaldo(Guid PlayerSaldoId);
    }
}