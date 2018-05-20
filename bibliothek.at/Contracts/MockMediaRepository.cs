using bibliothek.at.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace bibliothek.at.Contracts
{
    public class MockMediaRepository : IMediaRepository
    {
        private List<MediaItem> _mediaItems;

        public MockMediaRepository()
        {
            this._mediaItems = new List<MediaItem> {
                new MediaItem { Id =  1746, ISBN = "978-3551559005", MedienArt = "S", Sachtitel = "Harry Potter und das verwunschene Kind. Teil eins und zwei", Verfasser = "J.K. Rowling", Systematik = "NA7" },
                new MediaItem { Id = 14492, ISBN = "978-3551556950", MedienArt = "J", Sachtitel = "Die Märchen von Beedle dem Barden", Verfasser = "J.K. Rowling", Systematik = "PU" },
                new MediaItem { Id = 14541, ISBN = "978-3431039993", MedienArt = "J", Sachtitel = "Origin ", Verfasser = "Dan Brown", Systematik = "PU" },
                new MediaItem { Id = 14591, ISBN = "978-3431039993", MedienArt = "J", Sachtitel = "Origin ", Verfasser = "Dan Brown", Systematik = "PU", Status = true  },
                new MediaItem { Id = 14542, ISBN = "978-3551354044", MedienArt = "J", Sachtitel = "Harry Potter und der Feuerkelch Bd. 4", Verfasser = "Verfasser", Systematik = "PU" },
                new MediaItem { Id = 15465, ISBN = "978-3426199213", MedienArt = "J", Sachtitel = "Flugangst 7A: Psychothriller", Verfasser = "Sebastian Fitzek", Systematik = "PU", Status = true },
                new MediaItem { Id = 15466, ISBN = "978-3831034215", MedienArt = "J", Sachtitel = "Jamies 5-Zutaten-Küche: Quick & Easy", Verfasser = "Jamie Oliver", Systematik = "PU" },
                new MediaItem { Id = 15467, ISBN = "978-3833816772", MedienArt = "J", Sachtitel = "20 Minuten sind genug!: Über 150 Rezepte aus der frischen Küche (GU Themenkochbuch)", Verfasser = "Verfasser", Systematik = "PU", Status = true },
                new MediaItem { Id = 15487, ISBN = "978-3947188055", MedienArt = "J", Sachtitel = "Märchenbuch: Mein Gebrüder Grimm Märchen Wimmelbuch für Kinder ab 3 Jahren", Verfasser = "Rowling, Joanne K.", Systematik = "PU" },
                new MediaItem { Id = 18945, ISBN = "978-3942491532", MedienArt = "S", Sachtitel = "Zoo Wimmelbuch: Meine wimmeligen Kinderbücher ab 2 Jahre", Verfasser = "Verfasser", Systematik = "PU" },
                new MediaItem { Id = 20637, ISBN = "978-3414820440", MedienArt = "K", Sachtitel = "Der kleine Hase wird großer Bruder", Verfasser = "Verfasser", Systematik = "PU", Status = true },
                new MediaItem { Id = 21632, ISBN = "978-3630875231", MedienArt = "K", Sachtitel = "Leere Herzen: Roman", Verfasser = "Juli Zeh", Systematik = "PU", Status = true },
                new MediaItem { Id = 60046, ISBN = "978-3518427583", MedienArt = "K", Sachtitel = "Die Hauptstadt: Roman", Verfasser = "Robert Menasse", Systematik = "PU", Status = true },
                new MediaItem { Id = 60047, ISBN = "978-3401600390", MedienArt = "1", Sachtitel = "Mein Lotta-Leben (8). Kein Drama ohne Lama", Verfasser = "Alice Pantermüller", Systematik = "KO", Status = true },
                new MediaItem { Id = 60166, ISBN = "978-3401601366", MedienArt = "K", Sachtitel = "Mein Lotta-Leben (11). Volle Kanne Koala", Verfasser = "Alice Pantermüller", Systematik = "PU", Status = true },
                new MediaItem { Id = 62018, ISBN = "978-0708898406", MedienArt = "K", Sachtitel = "The Underground Railroad: Winner of the Pulitzer Prize for Fiction 2017", Verfasser = "Colson Whitehead", Systematik = "PU", Status = true },
                new MediaItem { Id = 60421, ISBN = "978-0349414683", MedienArt = "1", Sachtitel = "The Choice: The top-ten Amazon bestseller", Verfasser = "Samantha King", Systematik = "PU", Status = true },
                new MediaItem { Id = 61012, ISBN = "978-3958702912", MedienArt = "1", Sachtitel = "Ivanhoe", Verfasser = "Walter Scott", Systematik = "PU", Status = true },
                new MediaItem { Id = 61340, ISBN = "978-0008121938", MedienArt = "K", Sachtitel = "The Man Who Created the Middle East: A Story of Empire, Conflict and the Sykes-Picot Agreement", Verfasser = "Christopher Simon Sykes", Systematik = "PU", Status = true },
                new MediaItem { Id = 61686, ISBN = "978-0008157128", MedienArt = "K", Sachtitel = "The Bookshop on Rosemary Lane: The Funny, Feel-Good Read of the Summer", Verfasser = "Ellen Berry", Systematik = "PU", Status = true },
                new MediaItem { Id = 65043, ISBN = "978-3426276990", MedienArt = "2", Sachtitel = "Haut nah: Alles über unser größtes Organ", Verfasser = "Yael Adler", Systematik = "PU", Status = true },
                new MediaItem { Id = 65044, ISBN = "978-3442745692", MedienArt = "2", Sachtitel = "Nullzeit: Roman", Verfasser = "Juli Zeh", Systematik = "PU", Status = true },
                new MediaItem { Id = 65045, ISBN = "978-3442715732", MedienArt = "2", Sachtitel = "Unterleuten: Roman", Verfasser = "Juli Zeh", Systematik = "PU", Status = true },
                new MediaItem { Id = 65046, ISBN = "978-3518427583", MedienArt = "2", Sachtitel = "Die Hauptstadt: Roman", Verfasser = "Robert Menasse", Systematik = "PU", Status = true }
            };
        }

        public Dictionary<string, string> GetMediaTypes()
        {
            var medienArt = new Dictionary<string, string>();
            medienArt.Add("D", "Dichtung");
            medienArt.Add("K", "Kinderbücher");
            medienArt.Add("J", "Jugendbücher");
            medienArt.Add("S", "Sachbücher");
            medienArt.Add("W", "Kinderhörbücher");
            //medienArt.Add("1", "Filme");
            //medienArt.Add("2", "Filme");
            medienArt.Add("3", "Hörbücher");
            medienArt.Add("4", "Jugendhörbücher");
            return medienArt;
        }

        public List<AvailableMediaItem> CheckIsbnsAvailable(List<string> isbns)
        {
            if (isbns == null)
            {
                return null;
            }

            var items = this._mediaItems.Where(o => isbns.Contains(o.ISBN)).ToList();
            return items.Select(o => new AvailableMediaItem { Id = o.Id, ISBN = o.ISBN }).ToList();
        }

        public MediaItem GetMediaItem(int id)
        {
            var item = this._mediaItems.Where(o => o.Id == id).FirstOrDefault();
            if (item == null)
            {
                return null;
            }
            item.Rezension = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus. Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus. Phasellus viverra nulla ut metus varius laoreet. Quisque rutrum. Aenean imperdiet. Etiam ultricies nisi vel augue. Curabitur ullamcorper ultricies nisi. Nam eget dui. Etiam rhoncus. Maecenas tempus, tellus eget condimentum rhoncus, sem quam semper libero, sit amet adipiscing sem neque sed ipsum. Nam quam nunc, blandit vel, luctus pulvinar, hendrerit id, lorem. Maecenas nec odio et ante tincidunt tempus. Donec vitae sapien ut libero venenatis faucibus. Nullam quis ante. Etiam sit amet orci eget eros faucibus tincidunt. Duis leo. Sed fringilla mauris sit amet nibh. Donec sodales sagittis magna. Sed consequat, leo eget bibendum sodales, augue velit cursus nunc";
            return item;
        }

        public List<MediaItem> GetMediaItems(string search)
        {
            return this._mediaItems.Where(o => o.Sachtitel.IndexOf(search, StringComparison.OrdinalIgnoreCase) != -1 || o.Verfasser.IndexOf(search, StringComparison.OrdinalIgnoreCase) != -1).ToList();
        }

        public List<MediaItem> GetMediaItemsByMediaType(string mediaType)
        {
            return this._mediaItems.Where(o => o.MedienArt.Equals(mediaType, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public MediaStatus GetMediaStatus()
        {
            return new MediaStatus { BookCount = 5, MovieCount = 10, AudioBookCount = 15 };
        }

        public List<MediaItem> GetNewMediaItems()
        {
            return this._mediaItems;
        }

        public List<MediaItem> GetPopularMediaItems()
        {
            return this._mediaItems;
        }
    }
}