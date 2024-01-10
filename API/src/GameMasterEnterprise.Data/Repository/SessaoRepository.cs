using GameMasterEnterprise.Data.Context;
using GameMasterEnterprise.Domain.Intefaces;
using GameMasterEnterprise.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace GameMasterEnterprise.Data.Repository
{
    public class SessaoRepository : Repository<Sessao>, ISessaoRepository
    {
        public SessaoRepository(MeuDbContext context) : base(context) { }

        public virtual async Task<Guid> GerarSessao(Sessao sessao)
        {
            DbSet.Add(sessao);
            await Db.SaveChangesAsync();
            return sessao.Id;
        }

        public async Task<Guid> ObterPlayerIdPorSessaoId(Guid sessaoId)
        {
            var playerId = await Db.Sessao.AsNoTracking()
                .Where(p => p.Id == sessaoId)
                .Select(p => p.PlayerId)
                .FirstOrDefaultAsync();

            return playerId;
        }
        public async Task<Guid> ObterCassinoIdPorSessaoId(Guid sessaoId)
        {
            var cassinoId = await Db.Sessao.AsNoTracking()
                .Where(p => p.Id == sessaoId)
                .Select(p => p.CassinoId)
                .FirstOrDefaultAsync();

            return cassinoId;
        }
        public async Task<Guid> ObterJogoIdPorSessaoId(Guid sessaoId)
        {
            var jogoId = await Db.Sessao.AsNoTracking()
                .Where(p => p.Id == sessaoId)
                .Select(p => p.JogoId)
                .FirstOrDefaultAsync();

            return jogoId;
        }
    }
}
