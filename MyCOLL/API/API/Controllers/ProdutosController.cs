using API.Data;
using API.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProdutosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/produtos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDto>>> GetProdutos()
        {
            // REGRA DE NEGÓCIO: O Frontend só vê produtos ATIVOS
            var produtos = await _context.Produtos
                .Include(p => p.Categoria)
                .Include(p => p.ApplicationUser)
                .Where(p => p.Ativo == true) // <--- Filtro Importante
                .ToListAsync();

            var produtosDto = produtos.Select(p => new ProdutoDto
            {
                Id = p.Id,
                Nome = p.Nome,
                Descricao = p.Descricao,
                Preco = p.PrecoFinal, // O cliente vê o Preço Final
                ImagemURL = p.ImagemURL,
                EstadoConservacao = p.EstadoConservacao,
                CategoriaNome = p.Categoria?.Nome ?? "Sem Categoria",
                FornecedorNome = p.ApplicationUser?.UserName ?? "Desconhecido",
                IsDestaque = p.IsDestaque
            }).ToList();

            return Ok(produtosDto);
        }

        // GET: api/produtos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoDto>> GetProduto(int id)
        {
            var p = await _context.Produtos
                .Include(p => p.Categoria)
                .Include(p => p.ApplicationUser)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (p == null || p.Ativo == false)
            {
                return NotFound("Produto não encontrado ou indisponível.");
            }

            var produtoDto = new ProdutoDto
            {
                Id = p.Id,
                Nome = p.Nome,
                Descricao = p.Descricao,
                Preco = p.PrecoFinal,
                ImagemURL = p.ImagemURL,
                EstadoConservacao = p.EstadoConservacao,
                CategoriaNome = p.Categoria?.Nome ?? "Sem Categoria",
                FornecedorNome = p.ApplicationUser?.UserName ?? "Desconhecido",
                IsDestaque = p.IsDestaque
            };

            return Ok(produtoDto);
        }

        // GET: api/produtos/categoria/1
        [HttpGet("categoria/{categoriaId}")]
        public async Task<ActionResult<IEnumerable<ProdutoDto>>> GetProdutosPorCategoria(int categoriaId)
        {
            var produtos = await _context.Produtos
               .Include(p => p.Categoria)
               .Include(p => p.ApplicationUser)
               .Where(p => p.Ativo == true && p.CategoriaId == categoriaId)
               .ToListAsync();

            var produtosDto = produtos.Select(p => new ProdutoDto
            {
                Id = p.Id,
                Nome = p.Nome,
                Descricao = p.Descricao,
                Preco = p.PrecoFinal,
                ImagemURL = p.ImagemURL,
                EstadoConservacao = p.EstadoConservacao,
                CategoriaNome = p.Categoria?.Nome ?? "Sem Categoria",
                FornecedorNome = p.ApplicationUser?.UserName ?? "Desconhecido",
                IsDestaque = p.IsDestaque
            }).ToList();

            return Ok(produtosDto);
        }
    }
}