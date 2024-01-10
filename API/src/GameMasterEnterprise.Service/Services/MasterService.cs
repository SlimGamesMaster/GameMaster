using GameMasterEnterprise.Domain.Intefaces;
using GameMasterEnterprise.Domain.Models;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GameMasterEnterprise.Service.Services
{
    public class MasterService : BaseService, IMasterService
    {
        private readonly IJogoService _jogoService;
        private readonly ICassinoService _cassinoService;
        private readonly IPlayerService _playerService;
        private readonly IPlayerSaldoService _playerSaldoService;
        private readonly ISessaoService _sessaoService;
        private readonly IHistoricoSessaoService _historicoSessaoService;
        private readonly IJogoRepository _jogoRepository;
        private readonly ICassinoRepository _cassinoRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IPlayerSaldoRepository _playerSaldoRepository;
        private readonly ISessaoRepository _sessaoRepository;
        private readonly IHistoricoSessaoRepository _historicoSessaoRepository;

        public MasterService(
            IHistoricoSessaoService historicoSessaoService,
            IHistoricoSessaoRepository historicoSessaoRepository,
            INotificador notificador,
            IPlayerSaldoRepository playerSaldoRepository,
            IPlayerSaldoService playerSaldoService,
            IJogoService jogoService,
            ICassinoService cassinoService,
            IPlayerService playerService,
            ISessaoService sessaoService,
            IJogoRepository jogoRepository,
            ICassinoRepository cassinoRepository,
            IPlayerRepository playerRepository,
            ISessaoRepository sessaoRepository, HttpClient httpClient)
            : base(notificador, httpClient)
        {
            _jogoService = jogoService;
            _cassinoService = cassinoService;
            _playerService = playerService;
            _sessaoService = sessaoService;
            _playerSaldoRepository = playerSaldoRepository;
            _playerSaldoService = playerSaldoService;
            _historicoSessaoService = historicoSessaoService;

            _jogoRepository = jogoRepository;
            _cassinoRepository = cassinoRepository;
            _playerRepository = playerRepository;
            _sessaoRepository = sessaoRepository;
            _historicoSessaoRepository = historicoSessaoRepository;
        }

        public async Task<Guid> ObterIdCassinoPorToken(string tokenCassino)
        {
            var cassinoId = await _cassinoService.ObterCassinoIdPorToken(tokenCassino);
            return (Guid)cassinoId;
        }
        public async Task<Guid> VerificarCodigoJogo(string codigoJogo)
        {
            var jogo = await _jogoService.ObterJogoPorCodigo(codigoJogo);
            return (Guid)jogo;
        }
        public async Task<Guid> VerificarUsuarioCassino(string tokenCassino, string tokenJogador, string nomeJogador)
        {
            try
            {
                var cassino = await _cassinoService.ObterCassinoPorToken(tokenCassino);

                if (cassino != null)
                {
                    var players = await _playerRepository.ObterPlayersPorCassinoId(cassino.Id);

                    var jogadorComToken = players.FirstOrDefault(p => p.Token == tokenJogador);

                    if (jogadorComToken != null)
                    {
                        return jogadorComToken.Id;
                    }

                    var novoPlayer = new Player
                    {
                        CassinoId = cassino.Id,
                        Nome = nomeJogador,
                        Token = tokenJogador
                    };
                    await _playerService.CriarPlayer(novoPlayer);
                    return novoPlayer.Id;
                }
                else
                {
                    return Guid.Empty;
                }
            }
            catch (Exception ex)
            {
                Notificar($"Erro ao verificar/criar jogador: {ex.Message}");
                return Guid.Empty; // Ou outra abordagem para indicar um erro
            }
        }
        public async Task<string> CriarSessao(Master master, Guid cassinoId, Guid jogoId, Guid playerId)
        {
            try
            {
                // Lógica para criar a sessão
                var novaSessao = new Sessao
                {
                    Dificuldade = master.Dificuldade,
                    CassinoId = cassinoId,
                    JogoId = jogoId,
                    PlayerId = playerId,
                    ativo = true
                };

                // Chame um método do seu serviço ou repositório para criar a sessão
                var sessaoId = await _sessaoService.CriarSessao(novaSessao);
                var urlCassino = await _cassinoRepository.ObterUrlCassino(cassinoId);
                var nomeJogo = await _jogoRepository.ObterNomeJogo(jogoId); 

                return $"slimgamesmaster.com/{nomeJogo}/index.php?sessao={sessaoId}";
            }
            catch (Exception ex)
            {
                Notificar($"Erro ao criar a sessão: {ex.Message}");
                throw;
            }
        }
        public async Task<bool> ConsultaSaldoJogador(string token)
        {
            var idPlayer = await _playerService.ObterPlayerIdPorToken(token);

            if (idPlayer == Guid.Empty || idPlayer == null)
            {
                throw new InvalidOperationException("Player não encontrado.");
            }


            var idSaldo = await _playerSaldoService.ObterIdPlayerSaldo(idPlayer);


            var idCassino = await _playerService.ObterCassinoIdPorPlayerId(idPlayer);
            var urlCassino = await _cassinoRepository.ObterUrlCassino(idCassino);
            var tokenCassino = await _cassinoRepository.ObterTokenCassino(idCassino);
            if (idCassino == Guid.Empty || urlCassino == null || tokenCassino ==null)
            {
                throw new InvalidOperationException("dados não encontrados.");
            }



            //string url = "https://bigluck.bet/apiteste";

            var httpService = new HttpClient();

            var requestBody = new
            {
                method = "user_balance",
                agent_token = tokenCassino,
                user_code = token
            };

            try
            {
                var respostaObjeto = await PostAsync<JsonElement>(urlCassino, requestBody);

                var userBalanceElement = respostaObjeto.GetProperty("user_balance");

                var saldoNaoZero = userBalanceElement.GetInt32() != 0;
                var saldo = userBalanceElement.GetInt32();


                await _playerSaldoService.AtualizarPlayerSaldo(idSaldo, saldo);

                return saldoNaoZero;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex.Message}");
                throw;
            }

        }
        public async Task<bool> RealizaTransicao(Guid IdSessao, string operacao, float total)
        {
            await _sessaoService.ObterSessaoAtiva(IdSessao);

            var cassinoId = await _sessaoRepository.ObterCassinoIdPorSessaoId(IdSessao);
            var urlCassino = await _cassinoRepository.ObterUrlCassino(cassinoId);

            var playerId = await _sessaoRepository.ObterPlayerIdPorSessaoId(IdSessao);
            var tokenPlayer = await _playerRepository.ObterTokenJogador(playerId);
            var idSaldo = await _playerSaldoService.ObterIdPlayerSaldo(playerId);

            var jogoId = await _sessaoRepository.ObterJogoIdPorSessaoId(IdSessao);
            
            var tokenCassino = await _cassinoRepository.ObterTokenCassino(cassinoId);
            var codigoJogo = await _jogoRepository.ObterCodigoJogo(jogoId);

            var httpService = new HttpClient();

            var requestBody = new
            {
                method = "transaction",
                agent_token = tokenCassino,
                user_code = tokenPlayer,
                type = operacao,
                amount = total,
                game_code = codigoJogo

            };
            try
            {
                var respostaObjeto = await PostAsync<JsonElement>(urlCassino, requestBody);

                var userBalanceElement = respostaObjeto.GetProperty("user_balance");

                var saldoNaoZero = userBalanceElement.GetInt32() != 0;
                var saldo = userBalanceElement.GetInt32();


                var transacao = new HistoricoSessao
                {
                    SessaoId = IdSessao,
                    Operacao = operacao,
                    Valor = total,
                };

                await _historicoSessaoService.CriarHistoricoSessao(transacao);


                return saldoNaoZero;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex.Message}");
                throw;
            }

        }




    }
}
