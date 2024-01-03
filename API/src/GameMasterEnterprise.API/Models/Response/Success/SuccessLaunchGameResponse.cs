using GameMasterEnterprise.API.Models.Response.Correct.Base;

namespace GameMasterEnterprise.API.Models.Response.Correct
{
    public class SuccessLaunchGameResponse : SuccessResponse
    {
        public string LaunchUrl { get; set; }
    }
}
