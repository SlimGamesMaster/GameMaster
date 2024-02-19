using AutoMapper;
using GameMasterEnterprise.API.Controllers;
using GameMasterEnterprise.API.Extensions;
using GameMasterEnterprise.API.Models.Request;
using GameMasterEnterprise.API.ViewModels;
using GameMasterEnterprise.Domain.Intefaces;
using GameMasterEnterprise.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;

namespace Ipet.API.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("Player")]
    public class PlayerController : HomeController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IPlayerService _playerService;
        private readonly IPlayerSaldoService _playerSaldoService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;

        private readonly AppSettings _appSettings;
        private readonly ILogger _logger;
        public PlayerController(
            IMapper mapper, INotificador notificador,
            IPlayerSaldoService playerSaldoService,
            IPlayerService playerService,
                              SignInManager<IdentityUser> signInManager,
                              UserManager<IdentityUser> userManager,
                              IOptions<AppSettings> appSettings,
                              IUser user, ILogger<AutenticacaoController> logger) : base(notificador, user)
        {
            _playerService = playerService;
            _playerSaldoService = playerSaldoService;
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }

        //[AllowAnonymous]
        [HttpGet("player/obter-player")]
        public async Task<ActionResult<PlayerViewModel>> ObterPlayer(Guid playerId)
        {
            var player = await _playerService.ObterPlayer(playerId);

            if (player == null)
            {
                return NotFound("player não encontrado.");
            }

            return Ok(player);
        }
        //[AllowAnonymous]
        [HttpGet("player/obter-saldo-player")]
        public async Task<ActionResult<PlayerSaldoViewModel>> ObterSaldoPlayer(Guid playerId)
        {
            var player = await _playerSaldoService.ObterPlayerSaldo(playerId);

            if (player == null)
            {
                return NotFound("player não encontrado.");
            }

            return Ok(player);
        }
        //[AllowAnonymous]
        [HttpPut("player/atualizar-player/{playerId}")]
        public async Task<ActionResult> Atualizarplayer(Guid playerId, PlayerViewModel playerNovo)
        {
            await _playerService.AtualizarPlayer(playerId, _mapper.Map<Player>(playerNovo));


            return Ok();
        }
        //[AllowAnonymous]
        [HttpDelete("player/remover-player/{playerId}")]
        public async Task<ActionResult> Removerplayer(Guid playerId)
        {
            await _playerService.RemoverPlayer(playerId);

            return Ok();
        }
        //[AllowAnonymous]
        [HttpGet("player/obter-todos-players")]
        public async Task<ActionResult<IEnumerable<PlayerViewModel>>> ObterTodosplayers()
        {
            var players = await _playerService.ObterTodosPlayers();
            return Ok(players);
        }

    }
}

