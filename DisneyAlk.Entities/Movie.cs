using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisneyAlk.Entities
{
    public class Movie:Entity
    {
        public string Image { get; set; }
        public string Titulo { get; set; }
        public DateTime CreationDate { get; set; }
        public int Qualification { get; set; }
        public string History { get; set; }

        public virtual ICollection<Character> Characters { get; set; }
        public Movie()
        {
            this.Characters = new HashSet<Character>();
        }


    }
}
