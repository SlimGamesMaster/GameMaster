
using GameMasterEnterprise.Domain.Intefaces;
using GameMasterEnterprise.Domain.Models;
using GameMasterEnterprise.Service.Services;

namespace GameMasterEnterprise.Service.Services
{
    public class PlayerSaldoService : BaseService, IPlayerSaldoService
    {
        private readonly IPlayerSaldoRepository _PlayerSaldoRepository;
        public PlayerSaldoService(IPlayerSaldoRepository PlayerSaldoRepository, INotificador notificador)
            : base(notificador)
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
        public async Task CriarPlayerSaldo(PlayerSaldo PlayerSaldo)
        {
            if (PlayerSaldo != null)
            {
                var PlayerSaldoPorId = await _PlayerSaldoRepository.ObterPorId(PlayerSaldo.Id);

                if (PlayerSaldoPorId != null)
                {
                    Notificar("PlayerSaldo já existente.");
                    return;
                }
                else
                {
                    await _PlayerSaldoRepository.Adicionar(PlayerSaldo);
                }
            }
            else
            {
                throw new InvalidOperationException("Erro interno ao Salvar o Usuario");

            }
        }
        public async Task<IEnumerable<PlayerSaldo>> ObterTodosPlayerSaldos()
        {
            return await _PlayerSaldoRepository.ObterTodos();
        }
        public async Task AtualizarPlayerSaldo(Guid PlayerSaldoId, PlayerSaldo playerSaldoNovo)
        {
            var playerSaldo = await _PlayerSaldoRepository.ObterPorId(PlayerSaldoId);

            if (playerSaldo == null)
            {
                Notificar("PlayerSaldo não encontrado.");
                return;
            }

            if(playerSaldoNovo.Saldo == playerSaldo.Saldo) {
                Notificar("Dados Repetidos.");
                return;
            }

            playerSaldo.Saldo = playerSaldoNovo.Saldo;
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
