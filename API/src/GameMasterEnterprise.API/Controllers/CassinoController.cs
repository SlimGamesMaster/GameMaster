using AutoMapper;
using GameMasterEnterprise.API.Controllers;
using GameMasterEnterprise.API.Extensions;
using GameMasterEnterprise.API.Models.Request;
using GameMasterEnterprise.API.ViewModels;
using GameMasterEnterprise.Data.Repository;
using GameMasterEnterprise.Domain.Intefaces;
using GameMasterEnterprise.Domain.Models;
using GameMasterEnterprise.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;

namespace Ipet.API.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("Cassino")]
    public class CassinoController : HomeController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ICassinoService _cassinoService;
        private readonly ICassinoRepository _cassinoRepository;
        private readonly IMasterService _masterService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;

        private readonly AppSettings _appSettings;
        private readonly ILogger _logger;
        public CassinoController(
            IMapper mapper, INotificador notificador,
            ICassinoService cassinoService,
            IMasterService masterService,
            ICassinoRepository cassinoRepository,
                              SignInManager<IdentityUser> signInManager,
                              UserManager<IdentityUser> userManager,
                              IOptions<AppSettings> appSettings,
                              IUser user, ILogger<AutenticacaoController> logger) : base(notificador, user)
        {
            _cassinoService = cassinoService;
            _cassinoRepository = cassinoRepository;
            _signInManager = signInManager;
            _masterService = masterService;
            _userManager = userManager;
            _logger = logger;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }
        //[AllowAnonymous]
        [HttpPost("cassino/cadastra-cassino")]
        public async Task<ActionResult> CriarCassino(CassinoViewModel cassino)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            cassino.User = Guid.Parse(userId);

            await _cassinoService.CriarCassino(_mapper.Map<Cassino>(cassino));

            return Ok();

        }
        //[AllowAnonymous]
        [HttpGet("cassino/obter-cassino-id")]
        public async Task<ActionResult<Cassino>> ObterCassinoId(Guid cassinoId)
        {
            var cassino = await _cassinoService.ObterCassino(cassinoId);

            if (cassino == null)
            {
                return NotFound("Cassino não encontrado.");
            }

            return Ok(cassino);
        }
        //[AllowAnonymous]
        [HttpPut("cassino/atualizar-cassino-id/{cassinoId}")]
        public async Task<ActionResult> AtualizarCassino(Guid cassinoId, CassinoViewModel cassinoNovo)
        {
            await _cassinoService.AtualizarCassino(cassinoId, _mapper.Map<Cassino>(cassinoNovo));


            return Ok();
        }
        //[AllowAnonymous]
        [HttpDelete("cassino/remover-cassino-id/{cassinoId}")]
        public async Task<ActionResult> RemoverCassino(Guid cassinoId)
        {
            await _cassinoService.RemoverCassino(cassinoId);

            return Ok();
        }
        //[AllowAnonymous]
        [HttpGet("cassino/obter-todos-cassinos")]
        public async Task<ActionResult<IEnumerable<CassinoViewModel>>> ObterTodosCassinos()
        {
            var cassinos = await _cassinoService.ObterTodosCassinos();
            return Ok(cassinos);
        }

        //FILTRADO POR USUARIO

        //[AllowAnonymous]
        [HttpGet("cassino/obter-historico-cassino-by-usuario")]
        public async Task<ActionResult> ObterHistoricoCassino(DateTime? dataLimiteInferior = null, DateTime? dataLimiteSuperior = null)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Guid userId = Guid.Parse(user);

            var historicoCassino = await _masterService.ObterHistoricoCassino(userId, dataLimiteInferior, dataLimiteSuperior);

            if (historicoCassino == null)
            {
                return NotFound("Sessao não encontrado.");
            }

            return Ok(historicoCassino);
        }
        //[AllowAnonymous]
        [HttpGet("cassino/obter-todos-cassinos-by-usuario")]
        public async Task<ActionResult<IEnumerable<CassinoViewModel>>> ObterCassinos()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Guid userId = Guid.Parse(user);

            var cassinos = await _cassinoRepository.ObterTodosPorUsuario(userId);
            return Ok(cassinos);
        }



    }
}

