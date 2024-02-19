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
    [Route("Jogo")]
    public class JogoController : HomeController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMasterService _masterService;
        private readonly IJogoService _jogoService;
        private readonly IPlayerService _playerService;
        private readonly ISessaoService _sessaoService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;

        private readonly AppSettings _appSettings;
        private readonly ILogger _logger;
        public JogoController(
            IMapper mapper, INotificador notificador,
            IJogoService jogoService,
            IPlayerService playerService,
            ISessaoService sessaoService,


                              IMasterService masterService,
                              SignInManager<IdentityUser> signInManager,
                              UserManager<IdentityUser> userManager,
                              IOptions<AppSettings> appSettings,
                              IUser user, ILogger<AutenticacaoController> logger) : base(notificador, user)
        {
            _jogoService = jogoService;
            _jogoService = jogoService;
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
        [HttpPost("jogo/cadastrar-jogo")]
        public async Task<ActionResult> CriarJogo(JogoViewModel jogo)
        {


            await _jogoService.CriarJogo(_mapper.Map<Jogo>(jogo));

            return Ok();

        }
        //[AllowAnonymous]
        [HttpGet("jogo/obter-Jogoo")]
        public async Task<ActionResult<Jogo>> ObterJogo(Guid JogoId)
        {
            var Jogo = await _jogoService.ObterJogo(JogoId);

            if (Jogo == null)
            {
                return NotFound("Jogo não encontrado.");
            }

            return Ok(Jogo);
        }
        //[AllowAnonymous]
        [HttpPut("jogo/atualizar-jogo/{jogoId}")]
        public async Task<ActionResult> AtualizarJogo(Guid jogoId, JogoViewModel jogoNovo)
        {
            await _jogoService.AtualizarJogo(jogoId, _mapper.Map<Jogo>(jogoNovo));


            return Ok();
        }
        //[AllowAnonymous]
        [HttpDelete("jogo/remover-jogo/{jogoId}")]
        public async Task<ActionResult> Removerjogo(Guid jogoId)
        {
            await _jogoService.RemoverJogo(jogoId);

            return Ok();
        }
        //[AllowAnonymous]
        [HttpGet("jogo/obter-todos-jogos")]
        public async Task<ActionResult<IEnumerable<JogoViewModel>>> ObterTodosJogos()
        {
            var jogos = await _jogoService.ObterTodosJogos();
            return Ok(jogos);
        }

    }
}

