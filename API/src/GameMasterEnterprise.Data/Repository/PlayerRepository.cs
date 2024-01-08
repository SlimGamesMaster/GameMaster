using GameMasterEnterprise.Data.Context;
using GameMasterEnterprise.Domain.Intefaces;
using GameMasterEnterprise.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GameMasterEnterprise.Data.Repository
{
    public class PlayerRepository : Repository<Player>, IPlayerRepository
    {
        public PlayerRepository(MeuDbContext context) : base(context) { }
        public async Task<Player> ObterPorNome(string nome)
        {
            return await DbSet.FirstOrDefaultAsync(c => c.Nome == nome);
        }

        public async Task<Player> ObterPorToken(string token)
        {
            return await DbSet.FirstOrDefaultAsync(c => c.Token == token);
        }
        public async Task<IEnumerable<Player>> ObterPlayersPorCassinoId(Guid cassinoId)
        {
            return await Db.Player.AsNoTracking()
                .Where(p => p.CassinoId == cassinoId)
                .ToListAsync();
        }

        public async Task<Guid> ObterCassinoIdPorPlayerId(Guid playerId)
        {
            var cassinoId = await Db.Player.AsNoTracking()
                .Where(p => p.Id == playerId)
                .Select(p => p.CassinoId)
                .FirstOrDefaultAsync();

            return cassinoId;
        }


    }
}