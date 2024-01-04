using GameMasterEnterprise.Data.Context;
using GameMasterEnterprise.Domain.Intefaces;
using GameMasterEnterprise.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GameMasterEnterprise.Data.Repository
{
    public class PlayerRepository : Repository<Player>, IPlayerRepository
    {
        public PlayerRepository(MeuDbContext context) : base(context) { }

    }
}