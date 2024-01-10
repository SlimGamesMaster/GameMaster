using GameMasterEnterprise.Domain.Models;

namespace GameMasterEnterprise.Domain.Intefaces
{
    public interface IJogoRepository : IRepository<Jogo>
    {
        Task<string> ObterCodigoJogo(Guid id);
        Task<string> ObterNomeJogo(Guid id);
        Task<Jogo> ObterPorCodigo(string codigo);
        Task<Jogo> ObterPorNome(string nome);
    }
}