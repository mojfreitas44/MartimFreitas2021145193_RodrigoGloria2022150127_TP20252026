using API.Data; // <-- Precisa disto para ver "ApplicationUser"
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities // <-- Tem de estar neste namespace
{
    public class Encomenda
    {
        public int Id { get; set; }

        [Required]
        public string ApplicationUserId { get; set; } = string.Empty;
        public ApplicationUser? ApplicationUser { get; set; } // <-- O nosso "Cliente"

        public DateTime DataEncomenda { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal PrecoTotal { get; set; }

        public string Estado { get; set; } = "Pendente";

        public int ModoEntregaId { get; set; }
        public ModoEntrega? ModoEntrega { get; set; } // <-- "Vê" o ModoEntrega

        public ICollection<EncomendaItem> EncomendaItems { get; set; } = new List<EncomendaItem>(); // <-- "Vê" o EncomendaItem
    }
}