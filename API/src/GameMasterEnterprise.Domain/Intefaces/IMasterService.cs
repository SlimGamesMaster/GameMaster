
using GameMasterEnterprise.Domain.Models;

namespace GameMasterEnterprise.Domain.Intefaces
{
    public interface IMasterService
    {
        Task<string> CriarSessao(Master master, Guid cassinoId, Guid jogoId, Guid playerId);
        Task<Guid> ObterIdCassinoPorToken(string tokenCassino);
        Task<Guid> VerificarCodigoJogo(int codigoJogo);
        Task<Guid> VerificarUsuarioCassino(string tokenCassino, string tokenJogador, string nomeJogador);
    }
}