
using GameMasterEnterprise.Domain.Intefaces;
using GameMasterEnterprise.Domain.Models;
using GameMasterEnterprise.Service.Services;

namespace GameMasterEnterprise.Service.Services
{
    public class SessaoService : BaseService, ISessaoService
    {
        private readonly ISessaoRepository _SessaoRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ICassinoRepository _cassinoRepository;
        private readonly IJogoRepository _jogoRepository;

        public SessaoService(ISessaoRepository SessaoRepository,
            IPlayerRepository PlayerRepository,
            ICassinoRepository cassinoRepository,
            IJogoRepository jogoRepository,
            INotificador notificador, HttpClient httpClient)
            : base(notificador, httpClient)
        {
            _SessaoRepository = SessaoRepository;
            _playerRepository = PlayerRepository;
            _jogoRepository = jogoRepository;
            _cassinoRepository = cassinoRepository;
        }


        public async Task<Sessao> ObterSessao(Guid SessaoId)
        {
            var Sessao = await _SessaoRepository.ObterPorId(SessaoId);
            Sessao.Cassino = await _cassinoRepository.ObterPorId(Sessao.CassinoId);
            Sessao.Jogo = await _jogoRepository.ObterPorId(Sessao.JogoId);
            Sessao.Player = await _playerRepository.ObterPorId(Sessao.PlayerId);


            if (Sessao == null)
            {
                Notificar("Sessao não encontrado.");
                return null;
            }
            return Sessao;
        }
        public async Task<Sessao> ObterSessaoAtiva(Guid SessaoId)
        {
            var Sessao = await _SessaoRepository.ObterPorId(SessaoId);
            Sessao.Cassino = await _cassinoRepository.ObterPorId(Sessao.CassinoId);
            Sessao.Jogo = await _jogoRepository.ObterPorId(Sessao.JogoId);
            Sessao.Player = await _playerRepository.ObterPorId(Sessao.PlayerId);


            if (Sessao == null)
            {
                throw new InvalidOperationException("Sessao Inexistente");
            }
            if(Sessao.ativo == false)
            {
                throw new InvalidOperationException("Sessao já Finalizada");
            }
            return Sessao;
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
        public async Task<IEnumerable<Sessao>> ObterTodos()
        {
            var sessoes = await _SessaoRepository.ObterTodos();

            foreach (var sessao in sessoes)
            {
                sessao.Cassino = await _cassinoRepository.ObterPorId(sessao.CassinoId);
                sessao.Jogo = await _jogoRepository.ObterPorId(sessao.JogoId);
                sessao.Player = await _playerRepository.ObterPorId(sessao.PlayerId);
            }

            return sessoes;
        }
        public async Task FinalizarSessao(Guid SessaoId)
        {
            var Sessao = await _SessaoRepository.ObterPorId(SessaoId);

            if (Sessao == null)
            {
                throw new InvalidOperationException("Sessao não encontrado.");
            }
             Sessao.ativo = false;
            Sessao.DataFinalizacao = DateTime.Now;
            await _SessaoRepository.Atualizar(Sessao);
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
