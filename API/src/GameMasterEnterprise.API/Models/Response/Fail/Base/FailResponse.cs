namespace GameMasterEnterprise.API.Models.Response.Base
{
    public class FailResponse
    {
        public FailResponse()
        {
            Status = 0;
        }
        public int Status { get; set; }
        public string Msg { get; set; }
    }
}
