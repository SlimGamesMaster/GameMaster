using GameMasterEnterprise.Data.Context;
using GameMasterEnterprise.Domain.Intefaces;
using GameMasterEnterprise.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GameMasterEnterprise.Data.Repository
{
    public class SessaoRepository : Repository<Sessao>, ISessaoRepository
    {
        public SessaoRepository(MeuDbContext context) : base(context) { }

    }
}