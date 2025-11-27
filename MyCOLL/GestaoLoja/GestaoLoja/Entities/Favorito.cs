using GestaoLoja.Data; // <-- Precisa disto para ver "ApplicationUser"
using System.ComponentModel.DataAnnotations;

namespace GestaoLoja.Entities // <-- Tem de estar neste namespace
{
    public class Favorito
    {
        public int Id { get; set; }

        public Produto? Produto { get; set; } // <-- Agora "vê" o Produto
        public int ProdutoId { get; set; }

        public ApplicationUser? ApplicationUser { get; set; } // <-- O nosso "Cliente"

        [Required]
        public string ApplicationUserId { get; set; } = string.Empty;
    }
}