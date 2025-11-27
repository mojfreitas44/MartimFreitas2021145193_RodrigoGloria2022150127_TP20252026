using API.Data; // <--- ESTE 'USING' É OBRIGATÓRIO
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class Produto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do artigo é obrigatório.")]
        [MaxLength(100)]
        public string Nome { get; set; } = string.Empty;

        [MaxLength(2000)]
        public string Descricao { get; set; } = string.Empty;

        [MaxLength(200)]
        public string ImagemURL { get; set; } = "/images/noproductstrans.png";

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        [Display(Name = "Preço Base")]
        public decimal PrecoBase { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        [Display(Name = "Preço Final")]
        public decimal PrecoFinal { get; set; }

        public bool Ativo { get; set; } = false;

        public bool IsDestaque { get; set; } = false;

        [MaxLength(50)]
        [Display(Name = "Estado de Conservação")]
        public string EstadoConservacao { get; set; } = "Não especificado";

        public int? Ano { get; set; }

        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }

        [Required]
        public string ApplicationUserId { get; set; } = string.Empty;

        // Esta linha agora funciona porque o 'using' em cima
        // diz ao C# que 'ApplicationUser' é a classe que definimos no Ficheiro 1
        public ApplicationUser? ApplicationUser { get; set; }

        public ICollection<EncomendaItem> EncomendaItems { get; set; } = new List<EncomendaItem>();
        public ICollection<Favorito> Favoritos { get; set; } = new List<Favorito>();
    }
}