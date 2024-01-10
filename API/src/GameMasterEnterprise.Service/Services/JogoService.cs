
using GameMasterEnterprise.Domain.Intefaces;
using GameMasterEnterprise.Domain.Models;
using GameMasterEnterprise.Service.Services;

namespace GameMasterEnterprise.Service.Services
{
    public class JogoService : BaseService, IJogoService
    {
        private readonly IJogoRepository _JogoRepository;

        public JogoService(IJogoRepository JogoRepository, INotificador notificador, HttpClient httpClient)
            : base(notificador, httpClient)
        {
            _JogoRepository = JogoRepository;
        }

        public async Task<Jogo> ObterJogo(Guid JogoId)
        {
            var JogoPorId = await _JogoRepository.ObterPorId(JogoId);

            if (JogoPorId == null)
            {
                Notificar("Jogo não encontrado.");
                return null;
            }
            return JogoPorId;
        }
        public async Task<Guid?> ObterJogoPorCodigo(int JogoId)
        {
            var JogoPorId = await _JogoRepository.ObterPorCodigo(JogoId);

            if (JogoPorId == null)
            {
                Notificar("Cassino não encontrado.");
                return null;
            }

            return JogoPorId.Id;
        }
        public async Task CriarJogo(Jogo Jogo)
        {
            if (Jogo != null)
            {
                var JogoPorId = await _JogoRepository.ObterPorId(Jogo.Id);
                var JogoPorId_1 = await _JogoRepository.ObterPorNome(Jogo.Nome);
                var JogoPorId_2 = await _JogoRepository.ObterPorCodigo(Jogo.Codigo);

                if (JogoPorId != null || JogoPorId_1 != null || JogoPorId_2 != null)
                {
                    throw new InvalidOperationException("Jogo já cadastrado");
                }
                else
                {
                    await _JogoRepository.Adicionar(Jogo);
                    return;
                }
            }
            else
            {
                await _JogoRepository.Adicionar(Jogo);
                Notificar("Jogo criado com sucesso.");
                return;
            }
        }
        public async Task<IEnumerable<Jogo>> ObterTodosJogos()
        {
            return await _JogoRepository.ObterTodos();
        }
        public async Task AtualizarJogo(Guid JogoId, Jogo JogoNovo)
        {
            var Jogo = await _JogoRepository.ObterPorId(JogoId);

            if (Jogo == null)
            {
                Notificar("Jogo não encontrado.");
                return;
            }

            if(JogoNovo.Nome == Jogo.Nome) {
                Notificar("Dados Repetidos.");
                return;
            }

            Jogo = JogoNovo;
            await _JogoRepository.Atualizar(Jogo);
        }


        public async Task RemoverJogo(Guid JogoId)
        {
            await _JogoRepository.Remover(JogoId);
        }

        public async Task Dispose()
        {
            _JogoRepository?.Dispose();
        }
    }
}
