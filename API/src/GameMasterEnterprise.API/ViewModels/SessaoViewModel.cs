using GameMasterEnterprise.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace GameMasterEnterprise.API.ViewModels
{
    public class SessaoViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public int Dificuldade { get; set; }
        public Guid JogoId { get; set; }
        public Jogo Jogo { get; set; }
        public Guid PlayerId { get; set; }
        public Player Player { get; set; }
        public Guid CassinoId { get; set; }
        public Cassino Cassino { get; set; }
        public DateTime DataFinalizacao { get; set; }
        public bool ativo { get; set; }
    }

}