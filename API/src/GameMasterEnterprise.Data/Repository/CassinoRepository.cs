using GameMasterEnterprise.Data.Context;
using GameMasterEnterprise.Domain.Intefaces;
using GameMasterEnterprise.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GameMasterEnterprise.Data.Repository
{
    public class CassinoRepository : Repository<Cassino>, ICassinoRepository
    {
        public CassinoRepository(MeuDbContext context) : base(context) { }
        public virtual async Task<Cassino> ObterPorNome(string nome)
        {
            return await DbSet.FirstOrDefaultAsync(c => c.Nome == nome);
        }
    }
}