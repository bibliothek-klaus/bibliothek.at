namespace bibliothek.Models
{
    public class Team
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public Team(string name, string description, string image)
        {
            this.Name = name;
            this.Description = description;
            this.Image = image;
        }
    }
}