﻿using AutoMapper;
using GameMasterEnterprise.API.Controllers;
using GameMasterEnterprise.API.Extensions;
using GameMasterEnterprise.API.Models.Request;
using GameMasterEnterprise.API.Models.Response.Base;
using GameMasterEnterprise.API.Models.Response.Correct;
using GameMasterEnterprise.API.Models.Response.Correct.Base;
using GameMasterEnterprise.Domain.Intefaces;
using GameMasterEnterprise.Domain.Models;
using GameMasterEnterprise.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;

namespace Ipet.API.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("Master")]
    public class MasterController : HomeController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMasterService _masterService;
        private readonly IJogoRepository _jogoRepository;
        private readonly IJogoService _jogoService;
        private readonly ICassinoService _cassinoService;
        private readonly IPlayerService _playerService;
        private readonly ISessaoService _sessaoService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;

        private readonly AppSettings _appSettings;
        private readonly ILogger _logger;
        public MasterController(
            IMapper mapper, INotificador notificador,
            IJogoService jogoService,
            ICassinoService cassinoService,
            IPlayerService playerService,
            ISessaoService sessaoService,


                              IMasterService masterService,
                              IJogoRepository jogoRepository,
                              SignInManager<IdentityUser> signInManager,
                              UserManager<IdentityUser> userManager,
                              IOptions<AppSettings> appSettings,
                              IUser user, ILogger<AutenticacaoController> logger) : base(notificador, user)
        {
            _jogoRepository = jogoRepository;
            _jogoService = jogoService;
            _cassinoService = cassinoService;
            _playerService = playerService;
            _sessaoService = sessaoService;
            _masterService = masterService;
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }
        //[AllowAnonymous]
        [HttpPost("master/abrir-jogo")]
        public async Task<IActionResult> RecebeDados(MasterViewModel master)
        {

                // Consulta Token Cassino   
                var cassinoId = await _masterService.ObterIdCassinoPorToken(master.TokenCassino);
                if (cassinoId == null)
                {
                    var failResponse = new FailResponse
                    {
                        Msg = "Token de Cassino Desconhecido"
                    };
                    return BadRequest(failResponse);
                }

                // Consulta Codigo Jogo
                var jogoId = await _masterService.VerificarCodigoJogo(master.CodigoJogo);
                if (jogoId == null)
                {
                    var failResponse = new FailResponse
                    {
                        Msg = "Jogo Desconhecido"
                    };
                    return BadRequest(failResponse);
                }

                // Consulta Usuario
                var playerId = await _masterService.VerificarUsuarioCassino(master.TokenCassino, master.TokenJogador, master.NomeJogador);
                if (playerId == null)
                {
                    // Ação a ser realizada se a verificação do usuário falhar
                    var failResponse = new FailResponse
                    {
                        Msg = "Falha na verificação do usuário do cassino"
                    };
                    return BadRequest(failResponse);
                }

                // Criação da Sessão
                var urlDaSessao = await _masterService.CriarSessao(_mapper.Map<Master>(master), cassinoId, jogoId, playerId);


                    var url = new SuccessLaunchGameResponse
                    {
                        LaunchUrl = urlDaSessao,
                    };
                    return Ok(url);
 

                
        }
        [HttpGet("master/consulta-saldo")]
        public async Task<ActionResult> ConsultaSaldo (Guid sessaoId)
        {         
            
            var userBalance = await _masterService.ConsultaSaldoJogador(sessaoId);
            var cassinoBalance = await _masterService.ConsultaSaldoCassino(sessaoId);
            var url = new SuccessBalanceResponse
            {
                UserBalance = userBalance,
                CassinoBalance = cassinoBalance
            };
            return Ok(url);


        }
        //[AllowAnonymous]
        [HttpPost("master/transacao")]
        public async Task<float> RealizaTransacao(TransacaoViewModel transacaoViewModel)
        {
            if (transacaoViewModel.Operacao == "debit" || transacaoViewModel.Operacao == "credit") 
            {
                return await _masterService.RealizaTransicao(transacaoViewModel.SessaoId, transacaoViewModel.Operacao, transacaoViewModel.Total);
            }
            else
            {
                throw new ArgumentException("A operação deve ser 'debit' ou 'credit'.");
            }

        }

        //[AllowAnonymous]
        [HttpGet("master/obter-sessao")]
        public async Task<ActionResult> ObterSessao(Guid sessaoId)
        {
            var cassino = await _sessaoService.ObterSessaoAtiva(sessaoId);

            if (cassino == null)
            {
                return NotFound("Sessao não encontrado.");
            }

            return Ok(cassino);
        }

        //[AllowAnonymous]
        [HttpGet("master/finalizar-sessao")]
        public async Task<ActionResult> FinalizarSessao(Guid sessaoId)
        {


            await _sessaoService.FinalizarSessao(sessaoId);

            return Ok("Sessao Finalizada com sucesso");
        }

        //[AllowAnonymous]
        [HttpGet("master/obter-ultimas-rodadas")]
        public async Task<ActionResult> ObterUltimasRodadasJogo(string nomeJogo)
        {
            var historicoJogadores = await _masterService.ObterHistoricoJogadores(nomeJogo);

            if (historicoJogadores == null)
            {
                return NotFound("Sessao não encontrado.");
            }

            return Ok(historicoJogadores);
        }


    }
}

