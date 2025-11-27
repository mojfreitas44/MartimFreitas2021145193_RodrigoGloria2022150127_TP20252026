namespace API.DTO
{
    public class ProdutoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal Preco { get; set; } // Será o PrecoFinal
        public string ImagemURL { get; set; } = string.Empty;
        public string EstadoConservacao { get; set; } = string.Empty;
        public string CategoriaNome { get; set; } = string.Empty;
        public string FornecedorNome { get; set; } = string.Empty;
        public bool IsDestaque { get; set; }
    }
}