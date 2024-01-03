
using GameMasterEnterprise.Domain.Models;

namespace GameMasterEnterprise.Domain.Intefaces
{
    public interface ICassinoService
    {
        Task AtualizarCassino(Guid cassinoId, Cassino cassinoNovo);
        Task CriarCassino(Cassino cassino);
        Task<Cassino> ObterCassino(Guid cassinoId);
        Task<IEnumerable<Cassino>> ObterTodosCassinos();
        Task RemoverCassino(Guid cassinoId);
    }
}