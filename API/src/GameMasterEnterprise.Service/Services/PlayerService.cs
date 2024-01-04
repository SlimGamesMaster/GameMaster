
using GameMasterEnterprise.Domain.Intefaces;
using GameMasterEnterprise.Domain.Models;
using GameMasterEnterprise.Service.Services;

namespace GameMasterEnterprise.Service.Services
{
    public class PlayerService : BaseService, IPlayerService
    {
        private readonly IPlayerRepository _PlayerRepository;

        public PlayerService(IPlayerRepository PlayerRepository, INotificador notificador)
            : base(notificador)
        {
            PlayerRepository = _PlayerRepository;
        }

        public async Task<Player> ObterPlayer(Guid PlayerId)
        {
            var PlayerPorId = await _PlayerRepository.ObterPorId(PlayerId);

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
                var PlayerPorId = await _PlayerRepository.ObterPorId(Player.Id);

                if (PlayerPorId != null)
                {
                    Notificar("Player já existente.");
                    return;
                }
            }
            else
            {
                await _PlayerRepository.Adicionar(Player);
                Notificar("Player criado com sucesso.");
                return;
            }
        }
        public async Task<IEnumerable<Player>> ObterTodosPlayers()
        {
            return await _PlayerRepository.ObterTodos();
        }
        public async Task AtualizarPlayer(Guid PlayerId, Player PlayerNovo)
        {
            var Player = await _PlayerRepository.ObterPorId(PlayerId);

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
            await _PlayerRepository.Atualizar(Player);
        }


        public async Task RemoverPlayer(Guid PlayerId)
        {
            await _PlayerRepository.Remover(PlayerId);
        }

        public async Task Dispose()
        {
            _PlayerRepository?.Dispose();
        }
    }
}
