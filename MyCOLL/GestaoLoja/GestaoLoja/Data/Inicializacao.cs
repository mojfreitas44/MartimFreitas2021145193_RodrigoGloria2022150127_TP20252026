using GestaoLoja.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GestaoLoja.Data
{
    public static class Inicializacao
    {
        // Define os nomes dos perfis (Roles)
        public const string PERFIL_ADMIN = "Admin";
        public const string PERFIL_FUNCIONARIO = "Funcionario";
        public const string PERFIL_CLIENTE = "Cliente";
        public const string PERFIL_FORNECEDOR = "Fornecedor";

        public static async Task SeedDatabaseAsync(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                    // Assegura que a BD está criada
                    await context.Database.MigrateAsync();

                    // --- 1. CRIAR PERFIS (ROLES) ---
                    // Se não existirem perfis, cria-os
                    if (!await roleManager.Roles.AnyAsync())
                    {
                        await roleManager.CreateAsync(new IdentityRole(PERFIL_ADMIN));
                        await roleManager.CreateAsync(new IdentityRole(PERFIL_FUNCIONARIO));
                        await roleManager.CreateAsync(new IdentityRole(PERFIL_CLIENTE));
                        await roleManager.CreateAsync(new IdentityRole(PERFIL_FORNECEDOR));
                    }

                    // --- 2. CRIAR UTILIZADOR ADMIN ---
                    // Se não houver utilizadores, cria um Admin
                    if (!await userManager.Users.AnyAsync())
                    {
                        var adminUser = new ApplicationUser
                        {
                            UserName = "admin@mycoll.pt",
                            Email = "admin@mycoll.pt",
                            EmailConfirmed = true
                        };
                        await userManager.CreateAsync(adminUser, "Admin123!");
                        await userManager.AddToRoleAsync(adminUser, PERFIL_ADMIN);
                    }

                    // --- 3. CRIAR UTILIZADOR FORNECEDOR (PARA TESTES) ---
                    // Tenta encontrar o fornecedor; se não existir, cria-o
                    var fornecedorUser = await userManager.FindByNameAsync("fornecedor@mycoll.pt");
                    if (fornecedorUser == null)
                    {
                        fornecedorUser = new ApplicationUser
                        {
                            UserName = "fornecedor@mycoll.pt",
                            Email = "fornecedor@mycoll.pt",
                            EmailConfirmed = true
                        };
                        await userManager.CreateAsync(fornecedorUser, "Forn123!");
                        await userManager.AddToRoleAsync(fornecedorUser, PERFIL_FORNECEDOR);
                    }
                }
                catch (Exception ex)
                {
                    // Logar o erro (opcional, mas boa prática)
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Ocorreu um erro ao popular a base de dados.");
                }
            }
        }
    }
}