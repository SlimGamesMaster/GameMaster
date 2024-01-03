using AutoMapper;
using GameMasterEnterprise.API.Controllers;
using GameMasterEnterprise.API.Extensions;
using GameMasterEnterprise.Domain.Intefaces;
using GameMasterEnterprise.Domain.Models;
using GameMasterEnterprise.Service.Services;
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
        private readonly UserManager<IdentityUser> _userManager;

        private readonly ICassinoRepository _cassinoRepository;
        private readonly CassinoService _cassinoService;


        private readonly IMapper _mapper;

        private readonly AppSettings _appSettings;
        private readonly ILogger _logger;
        public CassinoController(
            IMapper mapper,INotificador notificador,
            ICassinoRepository cassinoRepository, CassinoService cassinoService,
                              SignInManager<IdentityUser> signInManager,
                              UserManager<IdentityUser> userManager,
                              IOptions<AppSettings> appSettings,
                              IUser user, ILogger<AutenticacaoController> logger) : base(notificador, user)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _cassinoRepository = cassinoRepository;
            _cassinoService = cassinoService;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }

        [HttpPost("cadastra-cassino")]

        public async Task<ActionResult> CriarCassino(Cassino cassino)
        {


            await _cassinoService.CriarCassino(cassino);

            return Ok();

        }
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
        [HttpPut("atualizar-cassino/{cassinoId}")]
        public async Task<ActionResult> AtualizarCassino(Guid cassinoId, Cassino cassinoNovo)
        {
            await _cassinoService.AtualizarCassino(cassinoId, cassinoNovo);

            return Ok();
        }
        [HttpDelete("remover-cassino/{cassinoId}")]
        public async Task<ActionResult> RemoverCassino(Guid cassinoId)
        {
            await _cassinoService.RemoverCassino(cassinoId);

            return Ok();
        }
        [HttpGet("obter-todos-cassinos")]
        public async Task<ActionResult<IEnumerable<Cassino>>> ObterTodosCassinos()
        {
            var cassinos = await _cassinoService.ObterTodosCassinos();
            return Ok(cassinos);
        }

    }
}

