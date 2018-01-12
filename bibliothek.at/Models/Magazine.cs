namespace bibliothek.at.Models
{
    public class Magazine
    {
        public string Name { get; set; }
        public string Website { get; set; }
        public string PublicationFrequency { get; set; }

        public Magazine(string name)
        {
            this.Name = name;
        }

        public Magazine(string name, string website)
        {
            this.Name = name;
            this.Website = website;
        }

        public Magazine(string name, string website, string publicationFrequency)
        {
            this.Name = name;
            this.Website = website;
            this.PublicationFrequency = publicationFrequency;
        }
    }
}