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
            return sessao.Id; // Retorna o ID após a adição
        }
    }
}
