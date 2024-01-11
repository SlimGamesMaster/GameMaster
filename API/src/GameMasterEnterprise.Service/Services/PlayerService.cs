
using GameMasterEnterprise.Domain.Intefaces;
using GameMasterEnterprise.Domain.Models;
using GameMasterEnterprise.Service.Services;

namespace GameMasterEnterprise.Service.Services
{
    public class PlayerService : BaseService, IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        public PlayerService(IPlayerRepository playerRepository, INotificador notificador, HttpClient httpClient)
            : base(notificador, httpClient)
        {
            _playerRepository = playerRepository;
        }
        public async Task<Player> ObterPlayer(Guid PlayerId)
        {
            var PlayerPorId = await _playerRepository.ObterPorId(PlayerId);

            if (PlayerPorId == null)
            {
                Notificar("Player não encontrado.");
                return null;
            }
            return PlayerPorId;
        }
        public async Task<Player> ObterPlayerPorNome(String Nome)
        {
            var PlayerPorId = await _playerRepository.ObterPorNome(Nome);

            if (PlayerPorId == null)
            {
                Notificar("Player não encontrado.");
                return null;
            }
            return PlayerPorId;
        }
        public async Task<Player> ObterPlayerPorToken(string Token)
        {
            var PlayerPorId = await _playerRepository.ObterPorToken(Token);

            if (PlayerPorId == null)
            {
                Notificar("Player não encontrado.");
                return null;
            }
            return PlayerPorId;
        }
        public async Task CriarPlayer(Player Player)
        {
            if (Player != null)
            {
                var PlayerPorId = await _playerRepository.ObterPorId(Player.Id);

                if (PlayerPorId != null)
                {
                    Notificar("Player já existente.");
                    return;
                }
                else
                {
                    await _playerRepository.Adicionar(Player);
                }
            }
            else
            {
                throw new InvalidOperationException("Erro interno ao Salvar o Usuario");

            }
        }

        public async Task<Guid> ObterPlayerIdPorToken(string Token)
        {
            var id = await _playerRepository.ObterPorToken(Token);

            if (id == null)
            {
                Notificar("Player não encontrado.");
                return Guid.Empty;
            }

            return id.Id;
        }

        public async Task<Guid> ObterCassinoIdPorPlayerId(Guid idPlayer)
        {
            try
            {
                var jogador = await _playerRepository.ObterPorId(idPlayer);

                if (jogador == null)
                {
                    Notificar("Cassino não encontrado para o jogador.");
                    return Guid.Empty; // Retorna um Guid com todos os bytes definidos como zero.
                }

                return jogador.CassinoId;
            }
            catch (Exception ex)
            {
                // Log: Capturar exceções para diagnóstico posterior.
                // Log.Error($"Ocorreu um erro ao obter o cassino por ID do jogador: {ex}");
                return Guid.Empty; // Retorna um Guid com todos os bytes definidos como zero em caso de exceção.
            }
        }
        public async Task<IEnumerable<Player>> ObterTodosPlayers()
        {
            return await _playerRepository.ObterTodos();
        }
        public async Task AtualizarPlayer(Guid PlayerId, Player PlayerNovo)
        {
            var Player = await _playerRepository.ObterPorId(PlayerId);

            if (Player == null)
            {
                Notificar("Player não encontrado.");
                return;
            }

            if(PlayerNovo.Nome == Player.Nome) {
                Notificar("Dados Repetidos.");
                return;
            }

            Player.Token = PlayerNovo.Token;
            Player.Nome= PlayerNovo.Nome;
            await _playerRepository.Atualizar(Player);
        }
        public async Task RemoverPlayer(Guid PlayerId)
        {
            await _playerRepository.Remover(PlayerId);
        }
        public async Task Dispose()
        {
            _playerRepository?.Dispose();
        }
    }
}
