using API.Data; // <-- Precisa disto para ver "ApplicationUser"
using System.ComponentModel.DataAnnotations;

namespace API.Entities // <-- Tem de estar neste namespace
{
    public class CarrinhoCompras
    {
        public int Id { get; set; }

        [Required]
        public string ApplicationUserId { get; set; } = string.Empty;
        public ApplicationUser? ApplicationUser { get; set; } // <-- O nosso "Cliente"

        public int ProdutoId { get; set; }
        public Produto? Produto { get; set; } // <-- Agora "vê" o Produto

        public int Quantidade { get; set; }
    }
}