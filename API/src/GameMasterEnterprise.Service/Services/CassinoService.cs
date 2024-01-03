
using GameMasterEnterprise.Domain.Intefaces;
using GameMasterEnterprise.Domain.Models;
using GameMasterEnterprise.Service.Services;

namespace GameMasterEnterprise.Service.Services
{
    public class CassinoService : BaseService, ICassinoService
    {
        private readonly ICassinoRepository _cassinoRepository;

        public CassinoService(ICassinoRepository cassinoRepository, INotificador notificador)
            : base(notificador)
        {
            cassinoRepository = _cassinoRepository;
        }

        public async Task<Cassino> ObterCassino(Guid cassinoId)
        {
            var cassinoPorId = await _cassinoRepository.ObterPorId(cassinoId);

            if (cassinoPorId == null)
            {
                Notificar("Cassino não encontrado.");
                return null;
            }
            return cassinoPorId;
        }
        public async Task CriarCassino(Cassino cassino)
        {
            if (cassino != null)
            {
                var cassinoPorId = await _cassinoRepository.ObterPorId(cassino.Id);
                var cassinoPorNome = await _cassinoRepository.ObterPorNome(cassino.Nome);

                if (cassinoPorId != null || cassinoPorNome != null)
                {
                    Notificar("Cassino já existente.");
                    return;
                }
            }
            else
            {
                await _cassinoRepository.Adicionar(cassino);
                Notificar("Cassino criado com sucesso.");
                return;
            }
        }
        public async Task<IEnumerable<Cassino>> ObterTodosCassinos()
        {
            return await _cassinoRepository.ObterTodos();
        }
        public async Task AtualizarCassino(Guid cassinoId, Cassino cassinoNovo)
        {
            var cassino = await _cassinoRepository.ObterPorId(cassinoId);

            if (cassino == null)
            {
                Notificar("Cassino não encontrado.");
                return;
            }

            if(cassinoNovo.Nome == cassino.Nome) {
                Notificar("Dados Repetidos.");
                return;
            }

            cassino.Url = cassinoNovo.Url;
            cassino.Nome= cassinoNovo.Nome;
            await _cassinoRepository.Atualizar(cassino);
        }


        public async Task RemoverCassino(Guid cassinoId)
        {
            await _cassinoRepository.Remover(cassinoId);
        }

        public async Task Dispose()
        {
            _cassinoRepository?.Dispose();
        }
    }
}
