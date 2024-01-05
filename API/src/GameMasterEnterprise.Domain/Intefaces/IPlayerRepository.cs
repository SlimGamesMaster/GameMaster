using GameMasterEnterprise.Domain.Models;

namespace GameMasterEnterprise.Domain.Intefaces
{
    public interface IPlayerRepository : IRepository<Player>
    {
        Task<Player> ObterPorNome(string nome);
        Task<Player> ObterPorToken(string nome);
    }
}