using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        // "Cliente" ou "Fornecedor"
        [Required]
        public string TipoUsuario { get; set; } = string.Empty;
    }
}