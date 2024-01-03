using AutoMapper;
using GameMasterEnterprise.API.ViewModels;
using GameMasterEnterprise.Domain.Models;

namespace Ipet.API.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {

            CreateMap<Cassino, CassinoViewModel>().ReverseMap();


        }
    }
}
