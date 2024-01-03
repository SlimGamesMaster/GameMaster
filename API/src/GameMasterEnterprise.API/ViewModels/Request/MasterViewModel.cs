using System.ComponentModel.DataAnnotations;

namespace GameMasterEnterprise.API.Models.Request
{
    public class MasterViewModel
    {
        [Required(ErrorMessage = "O campo Method é obrigatório.")]
        public string Method { get; set; }

        [Required(ErrorMessage = "O campo AgentCode é obrigatório.")]
        public string AgentCode { get; set; }

        [Required(ErrorMessage = "O campo AgentToken é obrigatório.")]
        public string AgentToken { get; set; }

        [Required(ErrorMessage = "O campo UserCode é obrigatório.")]
        public string UserCode { get; set; }

        [Required(ErrorMessage = "O campo ProviderCode é obrigatório.")]
        public string ProviderCode { get; set; }

        [Required(ErrorMessage = "O campo GameCode é obrigatório.")]
        public string GameCode { get; set; }

        [Required(ErrorMessage = "O campo Lang é obrigatório.")]
        public string Lang { get; set; }
    }
}
