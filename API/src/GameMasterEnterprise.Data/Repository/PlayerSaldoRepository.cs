using GameMasterEnterprise.Data.Context;
using GameMasterEnterprise.Domain.Intefaces;
using GameMasterEnterprise.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GameMasterEnterprise.Data.Repository
{
    public class PlayerSaldoRepository : Repository<PlayerSaldo>, IPlayerSaldoRepository
    {
        public PlayerSaldoRepository(MeuDbContext context) : base(context) { }

        public async Task<float?> ObterSaldoPorPlayerId(Guid playerId)
        {
            var saldo = await Db.PlayerSaldo.AsNoTracking()
                .Where(p => p.PlayerId == playerId)
                .Select(p => p.Saldo)
                .FirstOrDefaultAsync();

            return saldo;
        }
        public async Task<PlayerSaldo> ObterModelSaldoPorPlayerId(Guid playerId)
        {
            return await Db.PlayerSaldo.AsNoTracking()
                .FirstOrDefaultAsync(c => c.PlayerId == playerId);
        }

        public async Task<Guid> ObterSaldoIdPorPlayerId(Guid playerId)
        {
            var id = await Db.PlayerSaldo.AsNoTracking()
                    .Where(p => p.PlayerId == playerId)
                    .Select(p => p.Id)
                    .FirstOrDefaultAsync();

            return id;

        }

        public virtual async Task<Guid> GerarSaldo(PlayerSaldo saldo)
        {
            DbSet.Add(saldo);
            await Db.SaveChangesAsync();
            return saldo.Id;
        }
    }
}