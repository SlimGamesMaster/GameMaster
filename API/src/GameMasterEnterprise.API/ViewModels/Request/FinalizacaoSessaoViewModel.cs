using System.ComponentModel.DataAnnotations;

namespace GameMasterEnterprise.API.Models.Request
{
    public class FinalizacaoSessaoViewModel
    {

        [Required(ErrorMessage = "O campo SessaoId é obrigatório.")]
        public Guid SessaoId { get; set; }

        [Required(ErrorMessage = "O campo Status é obrigatório.")]
        public bool Status { get; set; }

        [Required(ErrorMessage = "O campo Valor é obrigatório.")]
        public float Valor { get; set; }
    }
}
