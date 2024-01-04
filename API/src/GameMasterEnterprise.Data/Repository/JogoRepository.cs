using GameMasterEnterprise.Data.Context;
using GameMasterEnterprise.Domain.Intefaces;
using GameMasterEnterprise.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GameMasterEnterprise.Data.Repository
{
    public class JogoRepository : Repository<Jogo>, IJogoRepository
    {
        public JogoRepository(MeuDbContext context) : base(context) { }

    }
}