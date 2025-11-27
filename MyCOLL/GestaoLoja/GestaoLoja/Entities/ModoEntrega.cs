using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoLoja.Entities // <-- Tem de estar neste namespace
{
    public class ModoEntrega
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Nome { get; set; } = string.Empty;
        [MaxLength(200)]
        public string Descricao { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Preco { get; set; }
    }
}