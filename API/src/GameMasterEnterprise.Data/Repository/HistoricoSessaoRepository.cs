using GameMasterEnterprise.Data.Context;
using GameMasterEnterprise.Domain.Intefaces;
using GameMasterEnterprise.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace GameMasterEnterprise.Data.Repository
{
    public class HistoricoSessaoRepository : Repository<HistoricoSessao>, IHistoricoSessaoRepository
    {
        public HistoricoSessaoRepository(MeuDbContext context) : base(context) { }

        public async Task<List<HistoricoSessao>> ObterSaldosPorSessaoId(Guid sessaoId)
        {
            return await Db.HistoricoSessao.AsNoTracking()
                .Where(p => p.SessaoId == sessaoId)
                .ToListAsync();
        }
    }
}
