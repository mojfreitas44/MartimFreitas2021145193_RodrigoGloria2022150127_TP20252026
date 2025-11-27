using System.ComponentModel.DataAnnotations;

namespace GestaoLoja.Entities // <-- Tem de estar neste namespace
{
    public class Categoria
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Nome { get; set; } = string.Empty;
        public string ImagemURL { get; set; } = string.Empty;
    }
}