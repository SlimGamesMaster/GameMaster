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
    //[Authorize]
    [ApiVersion("1.0")]
    [Route("Cassino")]
    public class CassinoController : HomeController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMasterService _masterService;
        private readonly IJogoService _jogoService;
        private readonly ICassinoService _cassinoService;
        private readonly IPlayerService _playerService;
        private readonly ISessaoService _sessaoService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;

        private readonly AppSettings _appSettings;
        private readonly ILogger _logger;
        public CassinoController(
            IMapper mapper, INotificador notificador,
            IJogoService jogoService,
            ICassinoService cassinoService,
            IPlayerService playerService,
            ISessaoService sessaoService,


                              IMasterService masterService,
                              SignInManager<IdentityUser> signInManager,
                              UserManager<IdentityUser> userManager,
                              IOptions<AppSettings> appSettings,
                              IUser user, ILogger<AutenticacaoController> logger) : base(notificador, user)
        {
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
        [AllowAnonymous]
        [HttpPost("cadastra-cassino")]
        public async Task<ActionResult> CriarCassino(CassinoViewModel cassino)
        {


            await _cassinoService.CriarCassino(_mapper.Map<Cassino>(cassino));

            return Ok();

        }
        [AllowAnonymous]
        [HttpGet("obter-cassino")]
        public async Task<ActionResult<Cassino>> ObterCassino(Guid cassinoId)
        {
            var cassino = await _cassinoService.ObterCassino(cassinoId);

            if (cassino == null)
            {
                return NotFound("Cassino não encontrado.");
            }

            return Ok(cassino);
        }
        [AllowAnonymous]
        [HttpPut("atualizar-cassino/{cassinoId}")]
        public async Task<ActionResult> AtualizarCassino(Guid cassinoId, CassinoViewModel cassinoNovo)
        {
            await _cassinoService.AtualizarCassino(cassinoId, _mapper.Map<Cassino>(cassinoNovo));


            return Ok();
        }
        [AllowAnonymous]
        [HttpDelete("remover-cassino/{cassinoId}")]
        public async Task<ActionResult> RemoverCassino(Guid cassinoId)
        {
            await _cassinoService.RemoverCassino(cassinoId);

            return Ok();
        }
        [AllowAnonymous]
        [HttpGet("obter-todos-cassinos")]
        public async Task<ActionResult<IEnumerable<CassinoViewModel>>> ObterTodosCassinos()
        {
            var cassinos = await _cassinoService.ObterTodosCassinos();
            return Ok(cassinos);
        }

    }
}

