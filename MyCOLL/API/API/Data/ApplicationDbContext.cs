using API.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; // <-- Isto só funciona depois do Passo 1
using Microsoft.EntityFrameworkCore; // <-- Isto só funciona depois do Passo 1

namespace API.Data
{
    // Tem de herdar de IdentityDbContext<ApplicationUser>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // As tuas tabelas
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<ModoEntrega> ModosEntrega { get; set; }
        public DbSet<Encomenda> Encomendas { get; set; }
        public DbSet<EncomendaItem> EncomendaItems { get; set; }
        public DbSet<Favorito> Favoritos { get; set; }
        public DbSet<CarrinhoCompras> CarrinhoCompras { get; set; }

        // Bloco obrigatório para ligar Produtos a Utilizadores
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Produto>()
                .HasOne(p => p.ApplicationUser)
                .WithMany(u => u.ProdutosFornecidos)
                .HasForeignKey(p => p.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Produto>()
                .HasIndex(p => p.ApplicationUserId);
        }
    }
}