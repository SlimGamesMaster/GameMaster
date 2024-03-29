﻿
using GameMasterEnterprise.Domain.Models;

namespace GameMasterEnterprise.Domain.Intefaces
{
    public interface ISessaoService
    {
        Task AtualizarSessao(Guid SessaoId, Sessao SessaoNovo);
        Task<Guid> CriarSessao(Sessao sessao);
        Task Dispose();
        Task FinalizarSessao(Guid SessaoId);
        Task<Sessao> ObterSessao(Guid SessaoId);
        Task<Sessao> ObterSessaoAtiva(Guid SessaoId);
        Task<IEnumerable<Sessao>> ObterTodos();
        Task RemoverSessao(Guid SessaoId);
    }
}