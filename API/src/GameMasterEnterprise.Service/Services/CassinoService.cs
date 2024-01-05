
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
            _cassinoRepository = cassinoRepository;
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

        public async Task<Cassino> ObterCassinoPorNome(string Nome)
        {
            var cassinoPorId = await _cassinoRepository.ObterPorNome(Nome);

            if (cassinoPorId == null)
            {
                Notificar("Cassino não encontrado.");
                return null;
            }
            return cassinoPorId;
        }
        public async Task<Cassino> ObterCassinoPorToken(string Token)
        {
            var cassinoPorId = await _cassinoRepository.ObterPorToken(Token);

            if (cassinoPorId == null)
            {
                Notificar("Cassino não encontrado.");
                return null;
            }
            return cassinoPorId;
        }
        public async Task CriarCassino(Cassino cassino)
        {

            var consultaCassino = await _cassinoRepository.ObterPorId(cassino.Id);
            var consultaCassino_2 = await _cassinoRepository.ObterPorNome(cassino.Nome);
            var consultaCassino_3 = await _cassinoRepository.ObterPorToken(cassino.Token);

            if (consultaCassino == null && consultaCassino_2 == null && consultaCassino_3 == null )
            {
                 await _cassinoRepository.Adicionar(cassino);
                return;
            }
            else
            {

                throw new InvalidOperationException("Cassino já cadastrado");
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
