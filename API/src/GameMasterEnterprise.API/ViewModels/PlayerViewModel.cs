using GameMasterEnterprise.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace GameMasterEnterprise.API.ViewModels
{
    public class PlayerViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Dificuldade { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid JogoId { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Jogo Jogo { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid PlayerId { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Player Player { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid CassinoId { get; set; }

    }

}