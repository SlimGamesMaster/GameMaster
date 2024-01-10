
using GameMasterEnterprise.Domain.Intefaces;
using GameMasterEnterprise.Domain.Models;
using GameMasterEnterprise.Service.Services;

namespace GameMasterEnterprise.Service.Services
{
    public class PlayerSaldoService : BaseService, IPlayerSaldoService
    {
        private readonly IPlayerSaldoRepository _PlayerSaldoRepository;
        public PlayerSaldoService(IPlayerSaldoRepository PlayerSaldoRepository, INotificador notificador, HttpClient httpClient)
            : base(notificador, httpClient)
        {
            _PlayerSaldoRepository = PlayerSaldoRepository;
        }
        public async Task<PlayerSaldo> ObterPlayerSaldo(Guid PlayerSaldoId)
        {
            var PlayerSaldoPorId = await _PlayerSaldoRepository.ObterPorId(PlayerSaldoId);

            if (PlayerSaldoPorId == null)
            {
                Notificar("PlayerSaldo não encontrado.");
                return null;
            }
            return PlayerSaldoPorId;
        }
        public async Task<Guid> ObterPorPlayerPlayerSaldo(Guid playerId)
        {
            var PlayerSaldo = await _PlayerSaldoRepository.ObterModelSaldoPorPlayerId(playerId);

            if (PlayerSaldo == null)
            {
                var saldo = new PlayerSaldo
                {
                    PlayerId = playerId,
                    Saldo = 1
                };

                var saldoId = await _PlayerSaldoRepository.GerarSaldo(saldo);
                return saldoId;
            }
            return Guid.Empty;
        }
        public async Task CriarPlayerSaldo(PlayerSaldo PlayerSaldo)
        {
            if (PlayerSaldo != null)
            {
                var PlayerSaldoPorId = await _PlayerSaldoRepository.ObterPorId(PlayerSaldo.Id);

             
            }
            else
            {
                throw new InvalidOperationException("Erro interno ao Salvar o Usuario");

            }
        }
        public async Task<IEnumerable<PlayerSaldo>> ObterTodosPlayerSaldos() => await _PlayerSaldoRepository.ObterTodos();
        public async Task AtualizarPlayerSaldo(Guid PlayerSaldoId, float saldoNovo)
        {
            var playerSaldo = await _PlayerSaldoRepository.ObterPorId(PlayerSaldoId);

            if (playerSaldo == null)
            {
                Notificar("PlayerSaldo não encontrado.");
                return;
            }

            playerSaldo.Saldo = saldoNovo;
            await _PlayerSaldoRepository.Atualizar(playerSaldo);
        }
        public async Task RemoverPlayerSaldo(Guid PlayerSaldoId)
        {
            await _PlayerSaldoRepository.Remover(PlayerSaldoId);
        }
        public async Task Dispose()
        {
            _PlayerSaldoRepository?.Dispose();
        }
    }
}
