using GameMasterEnterprise.Data.Context;
using GameMasterEnterprise.Domain.Intefaces;
using GameMasterEnterprise.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GameMasterEnterprise.Data.Repository
{
    public class PlayerSaldoRepository : Repository<PlayerSaldo>, IPlayerSaldoRepository
    {
        public PlayerSaldoRepository(MeuDbContext context) : base(context) { }


    }
}