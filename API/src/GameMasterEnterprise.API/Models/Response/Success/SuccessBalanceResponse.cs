using GameMasterEnterprise.API.Models.Response.Correct.Base;

namespace GameMasterEnterprise.API.Models.Response.Correct
{
    public class SuccessBalanceResponse : SuccessResponse
    {
        public float UserBalance { get; set; }
        public float CassinoBalance { get; set; }
    }
}
