using GameMasterEnterprise.Domain.Models;

namespace GameMasterEnterprise.Domain.Intefaces
{
    public interface IJogoRepository : IRepository<Jogo>
    {
        Task<string> ObterNomeJogo(Guid id);
        Task<Jogo> ObterPorCodigo(int codigo);
        Task<Jogo> ObterPorNome(string nome);
    }
}