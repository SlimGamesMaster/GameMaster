
using GameMasterEnterprise.Domain.Models;

namespace GameMasterEnterprise.Domain.Intefaces
{
    public interface IMasterService
    {
        Task<float> ConsultaSaldoJogador(Guid sessaoId);
        Task<string> CriarSessao(Master master, Guid cassinoId, Guid jogoId, Guid playerId);
        Task<Guid> ObterIdCassinoPorToken(string tokenCassino);
        Task<bool> RealizaTransicao(Guid IdSessao, string operacao, float total);
        Task<Guid> VerificarCodigoJogo(string codigoJogo);
        Task<Guid> VerificarUsuarioCassino(string tokenCassino, string tokenJogador, string nomeJogador);
    }
}