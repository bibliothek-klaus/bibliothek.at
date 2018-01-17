using System.Collections.Generic;

namespace bibliothek.at.Models
{
    public class MediaItem
    {
        public int Id { get; set; }
        public string MedienArt { get; set; }
        public string Sachtitel { get; set; }
        public string Titelzusatz { get; set; }
        public string Verfasser { get; set; }
        public string Systematik { get; set; }
        public bool Status { get; set; }
        public string ISBN { get; set; }
        public string Rezension { get; set; }
        public string Verlag { get; set; }
        public int Entlehnungen { get; set; }

        public string ImageUrl { get; set; }

        public List<SimilarBooks> SimilarBooks { get; set; }
    }
}
