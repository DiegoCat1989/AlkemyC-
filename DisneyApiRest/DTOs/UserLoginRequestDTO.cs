using System.ComponentModel.DataAnnotations;

namespace DisneyApiRest.DTOs
{
    public class UserLoginRequestDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }    
    }
}
