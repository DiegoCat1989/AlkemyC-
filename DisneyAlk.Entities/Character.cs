namespace DisneyAlk.Entities
{
    public class Character:Entity
    {
        public string Image { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public double Weight { get; set; }
        public string History { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
        public Character()
        {
            this.Movies = new HashSet<Movie>();
        }

    }
}