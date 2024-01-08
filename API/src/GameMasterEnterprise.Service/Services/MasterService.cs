﻿using GameMasterEnterprise.Domain.Intefaces;
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
        private readonly ISessaoService _sessaoService;

        private readonly IJogoRepository _jogoRepository;
        private readonly ICassinoRepository _cassinoRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ISessaoRepository _sessaoRepository;

        public MasterService(
            INotificador notificador,
            IJogoService jogoService,
            ICassinoService cassinoService,
            IPlayerService playerService,
            ISessaoService sessaoService,
            IJogoRepository jogoRepository,
            ICassinoRepository cassinoRepository,
            IPlayerRepository playerRepository,
            ISessaoRepository sessaoRepository)
            : base(notificador)
        {
            _jogoService = jogoService;
            _cassinoService = cassinoService;
            _playerService = playerService;
            _sessaoService = sessaoService;

            _jogoRepository = jogoRepository;
            _cassinoRepository = cassinoRepository;
            _playerRepository = playerRepository;
            _sessaoRepository = sessaoRepository;
        }

        public async Task<Guid> ObterIdCassinoPorToken(string tokenCassino)
        {
            var cassinoId = await _cassinoService.ObterCassinoIdPorToken(tokenCassino);
            return (Guid)cassinoId;
        }
        public async Task<Guid> VerificarCodigoJogo(int codigoJogo)
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
                    PlayerId = playerId
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
            if (idPlayer == null) { return false; }

            var idCassino = await _playerService.ObterCassinoIdPorPlayerId(idPlayer);
            var urlCassino = await _cassinoRepository.ObterUrlCassino(idCassino);

            string url = "https://bigluck.bet/apiteste";

            string corpoRequisicao = @"{
        ""method"": ""user_balance"",
        ""agent_token"": ""59963150456c5037bb32f60af1952ff6"",
        ""user_code"": ""teste""
    }";
            using (HttpClient httpClient = new HttpClient())
            {
                HttpContent conteudo = new StringContent(corpoRequisicao, Encoding.UTF8, "application/json");

                try
                {
                    HttpResponseMessage resposta = await httpClient.PostAsync(url, conteudo);

                    if (resposta.IsSuccessStatusCode)
                    {

                        string respostaJson = await resposta.Content.ReadAsStringAsync();
                        Console.WriteLine("Resposta do servidor:");
                        Console.WriteLine(respostaJson);

                        var respostaObjeto = JsonSerializer.Deserialize<JsonElement>(respostaJson);

                        return respostaObjeto.GetProperty("user_balance").GetInt32() != 0;

                    }
                    else
                    {
                        Console.WriteLine($"Erro na requisição. Código de status: {resposta.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ocorreu um erro durante a requisição: {ex.Message}");
                }
            }

            return true;
        }






    }
}
