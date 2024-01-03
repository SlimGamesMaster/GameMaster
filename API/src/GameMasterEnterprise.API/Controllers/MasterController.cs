using AutoMapper;
using GameMasterEnterprise.API.Controllers;
using GameMasterEnterprise.API.Extensions;
using GameMasterEnterprise.API.Models.Request;
using GameMasterEnterprise.Domain.Intefaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;

namespace Ipet.API.Controllers
{
    //[Authorize]
    [ApiVersion("1.0")]
    [Route("Master")]
    public class MasterController : HomeController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;

        private readonly AppSettings _appSettings;
        private readonly ILogger _logger;
        public MasterController(
            IMapper mapper,INotificador notificador,
                              SignInManager<IdentityUser> signInManager,
                              UserManager<IdentityUser> userManager,
                              IOptions<AppSettings> appSettings,
                              IUser user, ILogger<AutenticacaoController> logger) : base(notificador, user)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }

        [HttpPost("/")]
        public async Task<IActionResult> RecebeDados(MasterViewModel master)
        {



            
            return Ok();
        }

    }
}

