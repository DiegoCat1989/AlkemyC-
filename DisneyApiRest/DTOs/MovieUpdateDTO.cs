using System.ComponentModel.DataAnnotations;

namespace DisneyApiRest.DTOs
{
    public class MovieUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public string Titulo { get; set; }
        public DateTime CreationDate { get; set; }

        [Range(0, 5, ErrorMessage = "Rango de 0 a 5")]
        public int Qualification { get; set; }
        public string History { get; set; }
        public virtual ICollection<int> Characters { get; set; }

    }
}
