using GameMasterEnterprise.API.Models.Response.Base;

namespace GameMasterEnterprise.API.Models.Response.Correct
{
    public class FailLaunchGameResponse : FailResponse
    {
        public string LaunchUrl { get; set; }
    }
}
