using GameMasterEnterprise.Domain.Models;

namespace GameMasterEnterprise.Domain.Intefaces
{
    public interface IJogoRepository : IRepository<Jogo>
    {
        Task<Jogo> ObterPorCodigo(int codigo);
        Task<Jogo> ObterPorNome(string nome);
    }
}