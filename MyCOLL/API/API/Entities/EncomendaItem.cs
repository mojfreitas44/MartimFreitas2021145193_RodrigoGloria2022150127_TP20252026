using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities // <-- Tem de estar neste namespace
{
    public class EncomendaItem
    {
        public int Id { get; set; }
        public int EncomendaId { get; set; }
        public Encomenda? Encomenda { get; set; } // <-- "Vê" Encomenda

        public int ProdutoId { get; set; }
        public Produto? Produto { get; set; } // <-- "Vê" Produto

        public int Quantidade { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal PrecoUnitario { get; set; }
    }
}