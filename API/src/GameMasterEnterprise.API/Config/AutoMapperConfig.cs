using AutoMapper;
using GameMasterEnterprise.API.Models.Request;
using GameMasterEnterprise.API.ViewModels;
using GameMasterEnterprise.Domain.Models;

namespace Ipet.API.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {

            CreateMap<Cassino, CassinoViewModel>().ReverseMap();
            CreateMap<Master, MasterViewModel>().ReverseMap();
            CreateMap<Player, PlayerViewModel>().ReverseMap();
            CreateMap<Sessao, SessaoViewModel>().ReverseMap();
            CreateMap<Jogo, JogoViewModel>().ReverseMap();


        }
    }
}
