using System.ComponentModel.DataAnnotations;

namespace DisneyApiRest.DTOs
{


    public class CharacterUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Name { get; set; }
        public int Age { get; set; }
        public double Weight { get; set; }
        public string History { get; set; }

        public virtual ICollection<int> Movies { get; set; }

        public CharacterUpdateDTO(int id, string image, String name, int age, double weight, string history){
            Id = id;
            Image = image;
            Name = name;
            Age = age;
            Weight=weight;
            History=history;
            }

    }


}


