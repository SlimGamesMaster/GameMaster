using GameMasterEnterprise.Domain.Models;

namespace GameMasterEnterprise.Domain.Intefaces
{
    public interface ISessaoRepository : IRepository<Sessao>
    {
        Task<Guid> GerarSessao(Sessao sessao);
    }
}