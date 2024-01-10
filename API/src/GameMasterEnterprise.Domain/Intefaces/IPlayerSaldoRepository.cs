using GameMasterEnterprise.Domain.Models;

namespace GameMasterEnterprise.Domain.Intefaces
{
    public interface IPlayerSaldoRepository : IRepository<PlayerSaldo>
    {
        Task<Guid> GerarSaldo(PlayerSaldo saldo);
        Task<PlayerSaldo> ObterModelSaldoPorPlayerId(Guid playerId);
        Task<Guid> ObterSaldoIdPorPlayerId(Guid playerId);
        Task<float?> ObterSaldoPorPlayerId(Guid playerId);
    }
}