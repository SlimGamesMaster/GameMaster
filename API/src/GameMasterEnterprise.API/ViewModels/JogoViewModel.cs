﻿using System.ComponentModel.DataAnnotations;

namespace GameMasterEnterprise.API.ViewModels
{
    public class JogoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Codigo { get; set; }

    }

}