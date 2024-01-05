using GameMasterEnterprise.Domain.Models;

namespace GameMasterEnterprise.Domain.Intefaces
{
    public interface ICassinoRepository : IRepository<Cassino>
    {
        Task<Cassino> ObterPorNome(string nome);
        Task<Cassino> ObterPorToken(string token);
        Task<string> ObterUrlCassino(Guid id);
    }
}