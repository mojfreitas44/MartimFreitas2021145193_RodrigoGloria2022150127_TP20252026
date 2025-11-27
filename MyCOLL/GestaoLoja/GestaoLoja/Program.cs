using GestaoLoja.Components;
using GestaoLoja.Data;
using GestaoLoja.Components.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// 1. LIGAR À BASE DE DADOS
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// 2. CONFIGURAR IDENTITY (Isto regista o "Identity.Application" UMA VEZ)
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// 3. EMAIL FALSO (Para não dar erro)
builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

// 4. SERVIÇOS BLAZOR
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// 5. AUTENTICAÇÃO BLAZOR
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

// --- ATENÇÃO: NÃO HÁ MAIS NENHUM "AddAuthentication" AQUI! ---

var app = builder.Build();

// PIPELINE
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// ESTA LINHA É OBRIGATÓRIA PARA OS BOTÕES DE LOGIN FUNCIONAREM:
app.MapAdditionalIdentityEndpoints();

// SEED (Criação do Admin)
await Inicializacao.SeedDatabaseAsync(app);

app.Run();