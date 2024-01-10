
using GameMasterEnterprise.Domain.Intefaces;
using GameMasterEnterprise.Domain.Models;
using GameMasterEnterprise.Service.Services;

namespace GameMasterEnterprise.Service.Services
{
    public class HistoricoSessaoService : BaseService, IHistoricoSessaoService
    {
        private readonly ISessaoRepository _SessaoRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ICassinoRepository _cassinoRepository;
        private readonly IHistoricoSessaoRepository _historicosessaoRepository;

        public HistoricoSessaoService(ISessaoRepository SessaoRepository,
            IPlayerRepository PlayerRepository,
            ICassinoRepository cassinoRepository,
            IHistoricoSessaoRepository historicosessaoRepository,
            INotificador notificador, HttpClient httpClient)
            : base(notificador, httpClient)
        {
            _SessaoRepository = SessaoRepository;
            _playerRepository = PlayerRepository;
            _historicosessaoRepository = historicosessaoRepository;
            _cassinoRepository = cassinoRepository;
        }

        public async Task<HistoricoSessao> ObterHistoricoSessao(Guid historicosessaoId)
        {
            var historicosessaoPorId = await _historicosessaoRepository.ObterPorId(historicosessaoId);

            if (historicosessaoPorId == null)
            {
                Notificar("historicosessao não encontrado.");
                return null;
            }
            return historicosessaoPorId;
        }
        public async Task CriarHistoricoSessao(HistoricoSessao historicosessao)
        {
            if (await _SessaoRepository.ObterPorId(historicosessao.SessaoId) == null)
            {
                throw new InvalidOperationException("Sessao Inexistente");
            }
            if (historicosessao != null)
            {

                Notificar("Favor passar os parametros corretamente.");
                return;

            }
            else
            {
                await _historicosessaoRepository.Adicionar(historicosessao);
                Notificar("historicosessao criado com sucesso.");
                return;
            }
        }
        public async Task<IEnumerable<HistoricoSessao>> ObterTodosHistoricosessaos()
        {
            return await _historicosessaoRepository.ObterTodos();
        }
        public async Task RemoverHistoricoSessao(Guid historicosessaoId)
        {
            await _historicosessaoRepository.Remover(historicosessaoId);
        }
        public async Task Dispose()
        {
            _historicosessaoRepository?.Dispose();
        }

    }
}
