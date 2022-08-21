using System.ComponentModel.DataAnnotations;

namespace DisneyApiRest.DTOs
{
    public class RegisterUserRequestDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Mail{get; set; }
        [Required]
        public string Password { get; set; }
    }
}
