using GameMasterEnterprise.Domain.Models;

namespace GameMasterEnterprise.Domain.Intefaces
{
    public interface ICassinoRepository : IRepository<Cassino>
    {
        Task<Cassino> ObterPorNome(string nome);
        Task<Cassino> ObterPorToken(string token);
        Task<float> ObterSaldoCassino(Guid id);
        Task<string> ObterTokenCassino(Guid id);
        Task<string> ObterUrlCassino(Guid id);
    }
}