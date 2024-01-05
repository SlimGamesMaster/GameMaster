using GameMasterEnterprise.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace GameMasterEnterprise.API.ViewModels
{
    public class PlayerSaldoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid PlayerId { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public float Saldo { get; set; }


    }

}