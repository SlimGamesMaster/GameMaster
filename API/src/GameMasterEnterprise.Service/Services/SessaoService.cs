
using GameMasterEnterprise.Domain.Intefaces;
using GameMasterEnterprise.Domain.Models;
using GameMasterEnterprise.Service.Services;

namespace GameMasterEnterprise.Service.Services
{
    public class SessaoService : BaseService, ISessaoService
    {
        private readonly ISessaoRepository _SessaoRepository;

        public SessaoService(ISessaoRepository SessaoRepository, INotificador notificador)
            : base(notificador)
        {
            _SessaoRepository = SessaoRepository;
        }

        public async Task<Sessao> ObterSessao(Guid SessaoId)
        {
            var SessaoPorId = await _SessaoRepository.ObterPorId(SessaoId);

            if (SessaoPorId == null)
            {
                Notificar("Sessao não encontrado.");
                return null;
            }
            return SessaoPorId;
        }
        public async Task<Guid> CriarSessao(Sessao sessao)
        {
            if (sessao != null)
            {
                var sessaoPorId = await _SessaoRepository.ObterPorId(sessao.Id);

                if (sessaoPorId != null)
                {
                    Notificar("Sessao já existente.");
                    return sessaoPorId.Id; 
                }
                else
                {
                    var sessaoId = await _SessaoRepository.GerarSessao(sessao);
                    return sessaoId;
                }
            }
            else
            {
                throw new InvalidOperationException("Sessão já cadastrada");
            }
        }

        public async Task<IEnumerable<Sessao>> ObterTodosSessaos()
        {
            return await _SessaoRepository.ObterTodos();
        }
        public async Task AtualizarSessao(Guid SessaoId, Sessao SessaoNovo)
        {
            var Sessao = await _SessaoRepository.ObterPorId(SessaoId);

            if (Sessao == null)
            {
                Notificar("Sessao não encontrado.");
                return;
            }

            Sessao = SessaoNovo;
            await _SessaoRepository.Atualizar(Sessao);
        }


        public async Task RemoverSessao(Guid SessaoId)
        {
            await _SessaoRepository.Remover(SessaoId);
        }

        public async Task Dispose()
        {
            _SessaoRepository?.Dispose();
        }
    }
}
