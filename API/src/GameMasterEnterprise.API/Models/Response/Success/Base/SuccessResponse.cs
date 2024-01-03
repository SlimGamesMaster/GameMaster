namespace GameMasterEnterprise.API.Models.Response.Correct.Base
{
    public class SuccessResponse
    {
        public SuccessResponse()
        {
            Status = 1;
            Msg = "SUCCESS";
        }
        public int Status { get; set; }
        public string Msg { get; set; }
    }
}
