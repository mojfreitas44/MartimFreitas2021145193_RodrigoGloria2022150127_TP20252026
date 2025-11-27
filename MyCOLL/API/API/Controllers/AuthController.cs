using API.Data;
using API.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            // 1. Verificar se o utilizador já existe
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
                return BadRequest("Utilizador já existe.");

            // 2. Criar o novo utilizador
            // Nota: IsAtivo começa a false (Pendente) por defeito na nossa entidade ApplicationUser
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                IsAtivo = false // Fica Pendente até o Admin aprovar na GestãoLoja
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            // 3. Atribuir o Perfil (Role) correto
            // O frontend envia "Cliente" ou "Fornecedor"
            if (model.TipoUsuario == "Cliente" || model.TipoUsuario == "Fornecedor")
            {
                await _userManager.AddToRoleAsync(user, model.TipoUsuario);
            }
            else
            {
                return BadRequest("Tipo de utilizador inválido. Escolha 'Cliente' ou 'Fornecedor'.");
            }

            return Ok(new { Message = "Registo efetuado com sucesso! Aguarde aprovação do administrador." });
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            // Verifica se existe e se a password está certa
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                // REGRA DE NEGÓCIO: Só pode entrar se estiver ATIVO
                if (!user.IsAtivo)
                {
                    return Unauthorized("A sua conta ainda está pendente de aprovação pelo Administrador.");
                }

                // Se passou tudo, gera o Token
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("UserId", user.Id) // Guardamos o ID para usar depois (ex: criar encomendas)
                };

                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Issuer"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    username = user.UserName,
                    role = userRoles.FirstOrDefault()
                });
            }

            return Unauthorized("Login falhou. Verifique email e password.");
        }
    }
}