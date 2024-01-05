
using GameMasterEnterprise.Domain.Models;

namespace GameMasterEnterprise.Domain.Intefaces
{
    public interface ICassinoService
    {
        Task AtualizarCassino(Guid cassinoId, Cassino cassinoNovo);
        Task CriarCassino(Cassino cassino);
        Task<Cassino> ObterCassino(Guid cassinoId);
        Task<Guid?> ObterCassinoIdPorToken(string Token);
        Task<Cassino> ObterCassinoPorNome(string Nome);
        Task<Cassino> ObterCassinoPorToken(string Token);
        Task<IEnumerable<Cassino>> ObterTodosCassinos();
        Task RemoverCassino(Guid cassinoId);
    }
}