﻿
using GameMasterEnterprise.Domain.Models;

namespace GameMasterEnterprise.Domain.Intefaces
{
    public interface IMasterService
    {
        Task<float> ConsultaSaldoCassino(Guid sessaoId);
        Task<float> ConsultaSaldoJogador(Guid sessaoId);
        Task<string> CriarSessao(Master master, Guid cassinoId, Guid jogoId, Guid playerId);
        Task<ResponseHistoricoCassino> ObterHistoricoCassino(Guid user, DateTime? dataLimiteInferior = null, DateTime? dataLimiteSuperior = null);
        Task<ResponseHistoricoJogador> ObterHistoricoJogadores(string jogo);
        Task<Guid> ObterIdCassinoPorToken(string tokenCassino);
        Task<float> RealizaTransicao(Guid IdSessao, string operacao, float total);
        Task<Guid> VerificarCodigoJogo(string codigoJogo);
        Task<Guid> VerificarUsuarioCassino(string tokenCassino, string tokenJogador, string nomeJogador);
    }
}