using System.ComponentModel.DataAnnotations;

namespace GameMasterEnterprise.API.Models.Request
{
    public class MasterViewModel
    {

        [Required(ErrorMessage = "O campo Method é obrigatório.")]
        public string NomeJogador { get; set; }

        [Required(ErrorMessage = "O campo Method é obrigatório.")]
        public string TokenJogador { get; set; }

        [Required(ErrorMessage = "O campo Method é obrigatório.")]
        public string TokenCassino { get; set; }

        [Required(ErrorMessage = "O campo GameCode é obrigatório.")]
        public int CodigoJogo { get; set; }

        [Required(ErrorMessage = "O campo Lang é obrigatório.")]
        public int Dificuldade { get; set; }
    }
}
