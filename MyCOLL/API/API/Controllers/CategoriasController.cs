using API.Data;
using API.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoriasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/categorias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDto>>> GetCategorias()
        {
            // Vai buscar apenas as categorias que têm produtos ativos (opcional, mas boa prática)
            // Ou todas, se preferires. Aqui vou buscar todas.
            var categorias = await _context.Categorias.ToListAsync();

            var categoriasDto = categorias.Select(c => new CategoriaDto
            {
                Id = c.Id,
                Nome = c.Nome
            }).ToList();

            return Ok(categoriasDto);
        }
    }
}