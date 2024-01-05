﻿
using GameMasterEnterprise.Domain.Models;

namespace GameMasterEnterprise.Domain.Intefaces
{
    public interface ISessaoService
    {
        Task AtualizarSessao(Guid SessaoId, Sessao SessaoNovo);
        Task CriarSessao(Sessao Sessao);
        Task Dispose();
        Task<Sessao> ObterSessao(Guid SessaoId);
        Task<IEnumerable<Sessao>> ObterTodosSessaos();
        Task RemoverSessao(Guid SessaoId);
    }
}