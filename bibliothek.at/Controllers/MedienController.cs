using bibliothek.at.Contracts;
using bibliothek.at.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace bibliothek.at.Controllers
{
    public class MedienController : Controller
    {
        private IMediaRepository _mediaRepository;
        private IEnhanceMedia _enhanceMedia;

        public MedienController(IMediaRepository mediaRepository, IEnhanceMedia enhanceMedia)
        {
            this._mediaRepository = mediaRepository;
            this._enhanceMedia = enhanceMedia;
        }

        [OutputCache(Duration = 3600)]
        public JsonResult Status()
        {
            var item = this._mediaRepository.GetMediaStatus();
            return Json(item, JsonRequestBehavior.AllowGet);
        }

        //[OutputCache(Duration = 3600)]
        public ActionResult Neuanschaffungen()
        {
            var items = this._mediaRepository.GetNewMediaItems().OrderBy(o => o.MedienArt);

            foreach (var item in items)
            {
                #region Fallback Cover Image

                if (string.IsNullOrEmpty(item.ImageUrl))
                {
                    var cleanIsbn = item.ISBN.Replace("-", string.Empty);
                    item.ImageUrl = $"http://cover.ekz.de/{cleanIsbn}.jpg";

                    //item.ImageUrl = $"http://covers.openlibrary.org/b/isbn/{item.ISBN}-L.jpg";
                }

                #endregion
            }

            return View(items.ToList());
        }
        public ActionResult Suche()
        {
            return View(new SearchRequest());
        }

        public ActionResult Zeitschriftenabos()
        {
            var items = new List<Magazine>();
            items.Add(new Magazine("Anna", "https://www.oz-verlag.de/anna/"));
            items.Add(new Magazine("Architektur und Wohnen", "http://www.awmagazin.de/"));
            items.Add(new Magazine("Bergsteiger", "http://bergsteiger.de/"));
            items.Add(new Magazine("Bike"));
            items.Add(new Magazine("Bild der Wissenschaft", "http://www.wissenschaft.de/"));
            items.Add(new Magazine("Bio"));
            items.Add(new Magazine("Blooms"));
            items.Add(new Magazine("Brandeins"));
            items.Add(new Magazine("Bravo", "http://www.bravo.de/", "jede zweite Woche"));
            items.Add(new Magazine("Bravo Sport"));
            items.Add(new Magazine("Brigitte"));
            items.Add(new Magazine("Brigitte woman"));
            items.Add(new Magazine("Burda"));
            items.Add(new Magazine("Chip"));
            items.Add(new Magazine("ct Magazin"));
            items.Add(new Magazine("Das Kochrezept"));
            items.Add(new Magazine("Einfach hausgemacht"));
            items.Add(new Magazine("GEO"));
            items.Add(new Magazine("GEOlino", "https://www.geo.de/geolino"));
            items.Add(new Magazine("GEOmini"));
            items.Add(new Magazine("GEO Saison"));
            items.Add(new Magazine("GEO spezial"));
            items.Add(new Magazine("Gesund leben"));
            items.Add(new Magazine("Gusto"));
            items.Add(new Magazine("Häuser"));
            items.Add(new Magazine("I love English"));
            items.Add(new Magazine("Kochen und genießen"));
            items.Add(new Magazine("Konsument"));
            items.Add(new Magazine("Köstlich vegetarisch"));
            items.Add(new Magazine("Kraut & Rüben"));
            items.Add(new Magazine("Landapotheke"));
            items.Add(new Magazine("Landlust", "http://www.landlust.de/"));
            items.Add(new Magazine("Living at Home"));
            items.Add(new Magazine("Mein schöner Garten"));
            items.Add(new Magazine("Micky Maus Comics", "https://www.micky-maus.de/magazin/"));
            items.Add(new Magazine("Mollie makes"));
            items.Add(new Magazine("Motor-Freizeit-Trends"));
            items.Add(new Magazine("Natur und Heilen"));
            items.Add(new Magazine("ÖKO -Test"));
            items.Add(new Magazine("Outdoor"));
            items.Add(new Magazine("Popcorn"));
            items.Add(new Magazine("Photographie"));
            items.Add(new Magazine("PM Magazin"));
            items.Add(new Magazine("Psychologie heute"));
            items.Add(new Magazine("Schöner Wohnen"));
            items.Add(new Magazine("Servus"));
            items.Add(new Magazine("Spotlight – einfach Englisch"));
            items.Add(new Magazine("Terra Mater"));
            items.Add(new Magazine("Trekkingbike"));
            items.Add(new Magazine("Trend"));
            items.Add(new Magazine("Welt der Frau"));
            items.Add(new Magazine("Welt der Wunder"));
            items.Add(new Magazine("Wohnen & Dekorieren"));
            items.Add(new Magazine("Wohnen und Garten"));
            items.Add(new Magazine("Wohnidee"));
            return View(items);
        }

        [HttpPost]
        public ActionResult QuickSearch(string search)
        {
            var searchRequest = new SearchRequest();
            searchRequest.SearchValue = search;
            return this.Suche(searchRequest);
        }

        [HttpPost]
        public ActionResult Suche(SearchRequest request)
        {
            return View("Suche", request);
        }

        public JsonResult GetData(string search, string systematik = null, string medienart = null, string sachtitel = null, bool? status = null, string verfasser = null, string medianArt = null, int id = 0)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var items = this._mediaRepository.GetMediaItems(search);

                if (!string.IsNullOrEmpty(systematik))
                {
                    items = items.Where(o => o.Systematik.IndexOf(systematik, StringComparison.OrdinalIgnoreCase) != -1).ToList();
                }

                if (!string.IsNullOrEmpty(medienart))
                {
                    items = items.Where(o => o.MedienArt.IndexOf(medienart, StringComparison.OrdinalIgnoreCase) != -1).ToList();
                }

                if (!string.IsNullOrEmpty(sachtitel))
                {
                    items = items.Where(o => o.Sachtitel.IndexOf(sachtitel, StringComparison.OrdinalIgnoreCase) != -1).ToList();
                }

                if (status != null)
                {
                    items = items.Where(o => o.Status.Equals(status.Value)).ToList();
                }

                if (!string.IsNullOrEmpty(verfasser))
                {
                    items = items.Where(o => o.Verfasser.IndexOf(verfasser, StringComparison.OrdinalIgnoreCase) != -1).ToList();
                }

                if (!string.IsNullOrEmpty(medianArt))
                {
                    items = items.Where(o => o.MedienArt.IndexOf(medianArt, StringComparison.OrdinalIgnoreCase) != -1).ToList();
                }

                if (!id.Equals(0))
                {
                    items = items.Where(o => o.Id.Equals(id)).ToList();
                }

                return Json(items, JsonRequestBehavior.AllowGet);
            }

            return Json(null);
        }

        //[OutputCache(Duration = 3600, VaryByParam = "id")]
        public ActionResult Detail(int id)
        {
            var item = this._mediaRepository.GetMediaItem(id);

            #region Bild hinzufügen und ähnliche Bücher suchen

            if (!string.IsNullOrEmpty(item.ISBN))
            {
                var result = this._enhanceMedia.GetDetails(item.ISBN);
                item.ImageUrl = result.Item1;

                var similarBooks = result.Item2;
                var availableMedias = this._mediaRepository.CheckIsbnsAvailable(similarBooks?.Select(o => o.Isbn).ToList());
                if (availableMedias != null)
                {
                    foreach (var availableMedia in availableMedias)
                    {
                        var similarBook = similarBooks.Where(o => o.Isbn == availableMedia.ISBN).FirstOrDefault();
                        similarBook.Mediennummer = availableMedia.Id;
                    }
                }

                item.SimilarBooks = similarBooks;
            }

            #endregion

            #region Fallback Cover Image

            if (string.IsNullOrEmpty(item.ImageUrl))
            {
                var cleanIsbn = item.ISBN.Replace("-", string.Empty);
                item.ImageUrl = $"http://cover.ekz.de/{cleanIsbn}.jpg";

                //item.ImageUrl = $"http://covers.openlibrary.org/b/isbn/{item.ISBN}-L.jpg";
            }

            #endregion

            if (item == null)
            {
                return View("Suche");
            }

            return View(item);
        }
    }
}
