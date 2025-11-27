using API.Entities;
using Microsoft.AspNetCore.Identity;

namespace API.Data
{
    // DEFINIÇÃO: ApplicationUser É uma CLASSE (um tipo de referência)
    public class ApplicationUser : IdentityUser
    {
        // Relação: Um Fornecedor (User) pode ter muitos Produtos
        public ICollection<Produto> ProdutosFornecidos { get; set; } = new List<Produto>();

        public bool IsAtivo { get; set; } = false;
    }
}