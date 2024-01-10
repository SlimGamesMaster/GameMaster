using AutoMapper;
using GameMasterEnterprise.API.Controllers;
using GameMasterEnterprise.API.Extensions;
using GameMasterEnterprise.API.Models.Request;
using GameMasterEnterprise.API.Models.Response.Base;
using GameMasterEnterprise.API.Models.Response.Correct;
using GameMasterEnterprise.API.Models.Response.Correct.Base;
using GameMasterEnterprise.API.ViewModels;
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
    //[Authorize]
    [ApiVersion("1.0")]
    [Route("Sessao")]
    public class SessaoController : HomeController
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
        public SessaoController(
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

        [AllowAnonymous]
        [HttpGet("obter-todas-sessoes")]
        public async Task<ActionResult<ICollection<SessaoViewModel>>> ObterTodasSessoes()
        {
            var sessoes = await _sessaoService.ObterTodos();

            if (sessoes == null)
            {
                return NotFound("Nenhuma sessao encontrada.");
            }

            var sessoesDTO = _mapper.Map<ICollection<SessaoViewModel>>(sessoes);
            return Ok(sessoesDTO);
        }
    }
}

