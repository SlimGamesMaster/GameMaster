using GameMasterEnterprise.Domain.Models;

namespace GameMasterEnterprise.Domain.Intefaces
{
    public interface ICassinoRepository : IRepository<Cassino>
    {
        Task<Cassino> ObterPorNome(string nome);
    }
}