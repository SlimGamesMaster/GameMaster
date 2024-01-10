
using GameMasterEnterprise.Domain.Models;

namespace GameMasterEnterprise.Domain.Intefaces
{
    public interface IJogoService
    {
        Task AtualizarJogo(Guid JogoId, Jogo JogoNovo);
        Task CriarJogo(Jogo Jogo);
        Task Dispose();
        Task<Jogo> ObterJogo(Guid JogoId);
        Task<Guid?> ObterJogoPorCodigo(string JogoId);
        Task<IEnumerable<Jogo>> ObterTodosJogos();
        Task RemoverJogo(Guid JogoId);
    }
}