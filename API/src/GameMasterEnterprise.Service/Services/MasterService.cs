using GameMasterEnterprise.Domain.Intefaces;
using GameMasterEnterprise.Domain.Models;
using System.Drawing;
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

                return $"https://slimgamesmaster.com/{nomeJogo}/index.php?sessao={sessaoId}";
            }
            catch (Exception ex)
            {
                Notificar($"Erro ao criar a sessão: {ex.Message}");
                throw;
            }
        }

        //Prototipagem
        public async Task<float> ConsultaSaldoJogador(Guid sessaoId)
        {

            var idPlayer = await _sessaoRepository.ObterPlayerIdPorSessaoId(sessaoId);
            var token = await _playerRepository.ObterTokenJogador(idPlayer);

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

            //Produção

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

                var saldoNaoZero = userBalanceElement.GetSingle() != 0;
                float saldo = userBalanceElement.GetSingle();


                await _playerSaldoService.AtualizarPlayerSaldo(idSaldo, saldo);

                return saldo;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex.Message}");
                throw;
            }

            //Desenvolvimento
            //var saldo = 300;
            //await _playerSaldoService.AtualizarPlayerSaldo(idSaldo, saldo);

            //return saldo;



        }
        public async Task<float> ConsultaSaldoCassino(Guid sessaoId)
        {


            var idCassino = await _sessaoRepository.ObterCassinoIdPorSessaoId(sessaoId);
            float SaldoAtual = await _cassinoRepository.ObterSaldoCassino(idCassino);
            if (idCassino == Guid.Empty)
            {
                throw new InvalidOperationException("dados não encontrados.");
            }
            return SaldoAtual;

        }

        //Prototipagem
        public async Task<float> RealizaTransicao(Guid IdSessao, string operacao, float total)
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



            //Produção

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

                var saldoNaoZero = userBalanceElement.GetSingle() != 0;
                float saldo = userBalanceElement.GetSingle();


                var transacao = new HistoricoSessao
                {
                    SessaoId = IdSessao,
                    Operacao = operacao,
                    Valor = total,
                };

                float saldoAtual = 00;
                if (operacao == "credit") { saldoAtual = await _cassinoService.AtualizarSaldoCassino(cassinoId, total, true); }
                if (operacao == "debit") { saldoAtual = await _cassinoService.AtualizarSaldoCassino(cassinoId, total, false); }

                await _historicoSessaoService.CriarHistoricoSessao(transacao);


                return saldoAtual;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex.Message}");
                throw;
            }


            //Prototipagem
            //var transacao = new HistoricoSessao
            //{
            //        SessaoId = IdSessao,
            //        Operacao = operacao,
            //        Valor = total,
            //};

            //float saldoAtual = 00;
            //if (operacao == "credit") { saldoAtual = await _cassinoService.AtualizarSaldoCassino(cassinoId, total, true); }
            //if (operacao == "debit") { saldoAtual = await _cassinoService.AtualizarSaldoCassino(cassinoId, total, false); }

            //await _historicoSessaoService.CriarHistoricoSessao(transacao);
            //return saldoAtual;
        }


        public async Task<ResponseHistoricoJogador> ObterHistoricoJogadores(string jogoNome)
        {
            var historicos = await _historicoSessaoRepository.ObterUltimos100(jogoNome);

            var historicoJogadores = new List<ResponseHistoricoJogador>();

            float totalDebit = 0;
            float totalCredit = 0;

            int creditCount = 0;
            int debitCount = 0;

            foreach (var historico in historicos)
            {
                var playerId = await _sessaoRepository.ObterPlayerIdPorSessaoId(historico.SessaoId);

                var valorRecebido = historico.Valor;
                var op = historico.Operacao;

                if (op == "credit") { creditCount = creditCount + 1; totalCredit = totalCredit + valorRecebido; }
                if (op == "debit") { debitCount = debitCount + 1; totalDebit = totalDebit + valorRecebido; }
                //var nome = await _playerRepository.ObterNomeJogador(playerId);
                //var token = await _playerRepository.ObterTokenJogador(playerId);

            }

            var jogador = new ResponseHistoricoJogador
            {
                Credit = totalDebit,
                Debit = totalCredit,

                CreditCount = creditCount,
                DebitCount = debitCount,
            };
            return jogador;
        }
        public async Task<ResponseHistoricoCassino> ObterHistoricoCassino(Guid user, DateTime? dataLimiteInferior = null, DateTime? dataLimiteSuperior = null)
        {

            if (dataLimiteInferior.HasValue && dataLimiteSuperior.HasValue)
            {
                if (dataLimiteInferior > DateTime.Now || dataLimiteSuperior > DateTime.Now)
                {
                    throw new ArgumentException("As datas limite não podem ser no futuro.");
                }
                if ((dataLimiteSuperior - dataLimiteInferior).Value.TotalDays > 30)
                {
                    throw new ArgumentException("O intervalo entre as datas limite não pode ser maior que um mês.");
                }
            }


            var cassinoModel = await _cassinoRepository.ObterPorUsuario(user);
            var historicos = await _historicoSessaoRepository.ObterPorFiltroCassino(cassinoModel.Nome, dataLimiteInferior, dataLimiteSuperior);
            var historicoCassino = new List<HistoricoCassino>();

            foreach (var historico in historicos)
            {
                var playerId = await _sessaoRepository.ObterPlayerIdPorSessaoId(historico.SessaoId);
                var player = await _playerRepository.ObterPorId(playerId);


                var _historicoCassino = new HistoricoCassino
                {
                    Cassino = cassinoModel.Nome,
                    Player = player,
                    HistoricoSessao = historico,
                };

                historicoCassino.Add(_historicoCassino);
            }

            return new ResponseHistoricoCassino
            {
                HistoricoCassino = historicoCassino
            };
        }

    }
}
