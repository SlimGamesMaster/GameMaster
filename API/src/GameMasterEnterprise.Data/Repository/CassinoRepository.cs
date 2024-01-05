using GameMasterEnterprise.Data.Context;
using GameMasterEnterprise.Domain.Intefaces;
using GameMasterEnterprise.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GameMasterEnterprise.Data.Repository
{
    public class CassinoRepository : Repository<Cassino>, ICassinoRepository
    {
        public CassinoRepository(MeuDbContext context) : base(context) { }
        public async Task<Cassino> ObterPorNome(string nome)
        {
            return await Db.Cassino.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Nome == nome);
        }
        public async Task<string> ObterUrlCassino(Guid id)
        {
            var cassino = await Db.Cassino.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            return cassino?.Url;
        }
        public async Task<Cassino> ObterPorToken(string token)  
        {
            return await Db.Cassino.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Token == token);
        }
    }
}