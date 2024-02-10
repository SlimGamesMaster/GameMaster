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
        public async Task<List<HistoricoSessao>> ObterUltimos100(string nomeJogo)
        {
            return await DbSet
                .Where(historico => historico.Sessao.Jogo.Nome == nomeJogo)
                .OrderByDescending(historico => historico.DataCadastro)
                .Take(100)
                .ToListAsync();
        }

        public async Task<List<HistoricoSessao>> ObterPorFiltroCassino(string nomeCassino, DateTime? dataLimiteInferior = null, DateTime? dataLimiteSuperior = null)
        {
            IQueryable<HistoricoSessao> query = DbSet
                .Where(historico => historico.Sessao.Cassino.Nome == nomeCassino)
                .OrderByDescending(historico => historico.DataCadastro)
                .Take(100);
            if (dataLimiteInferior.HasValue)
            {
                query = query.Where(historico => historico.DataCadastro >= dataLimiteInferior.Value);
            }
            if (dataLimiteSuperior.HasValue)
            {
                query = query.Where(historico => historico.DataCadastro <= dataLimiteSuperior.Value);
            }
            return await query.ToListAsync();
        }

    }
}
