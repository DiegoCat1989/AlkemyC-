using DisneyAlk.Entities;
using System.ComponentModel.DataAnnotations;

namespace DisneyApiRest.DTOs
{


    public class CharacterCreateDTO
    {
        [Required]
        public string Image { get; set; }

        [Required]
        public string Name { get; set; }
        public int Age { get; set; }
        public double Weight { get; set; }
        public string History { get; set; }

        public virtual ICollection<int> Movies { get; set; }
        public CharacterCreateDTO(string image, String name, int age, double weight, string history){

            Image = image;
            Name = name;
            Age = age;
            Weight=weight;
            History=history;
            
            }

    }


}


