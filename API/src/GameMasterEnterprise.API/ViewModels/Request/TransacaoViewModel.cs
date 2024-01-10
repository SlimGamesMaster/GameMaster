using System.ComponentModel.DataAnnotations;

namespace GameMasterEnterprise.API.Models.Request
{
    public class TransacaoViewModel
    {

        [Required(ErrorMessage = "O campo SessaoId é obrigatório.")]
        public Guid SessaoId { get; set; }

        [Required(ErrorMessage = "O campo Operacao é obrigatório.")]
        public string Operacao { get; set; }

        [Required(ErrorMessage = "O campo Total é obrigatório.")]
        public float Total { get; set; }
    }
}
