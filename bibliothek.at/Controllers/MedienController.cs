using bibliothek.at.Contracts;
using bibliothek.at.Models;
using SimpleMvcSitemap;
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

        private Dictionary<string, string> GetMedienArtMapping()
        {
            return this._mediaRepository.GetMediaTypes();
        }

        [OutputCache(Duration = 3600, VaryByParam = "mediaType")]
        public ActionResult Sitemap(string mediaType)
        {
            if (string.IsNullOrEmpty(mediaType))
            {
                var indexNodes= new List<SitemapIndexNode>();
                var items = this._mediaRepository.GetMediaTypes();
                foreach (var item in items)
                {
                    indexNodes.Add(new SitemapIndexNode(Url.Action("Sitemap", "Medien", new { MediaType = item.Key })));
                }

                return new SitemapProvider().CreateSitemapIndex(new SitemapIndexModel(indexNodes));
            }

            var nodes = new List<SitemapNode>();
            var mediaItems = this._mediaRepository.GetMediaItemsByMediaType(mediaType);
            foreach (var item in mediaItems)
            {
                nodes.Add(new SitemapNode(Url.Action("Detail", "Medien", new { item.Id })) { ChangeFrequency = ChangeFrequency.Monthly });
            }

            return new SitemapProvider().CreateSitemap(new SitemapModel(nodes));
        }

        [OutputCache(Duration = 3600)]
        public JsonResult Status()
        {
            var item = this._mediaRepository.GetMediaStatus();
            return Json(item, JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration = 3600)]
        public ActionResult Neuanschaffungen()
        {
            ViewBag.Mapping = this.GetMedienArtMapping();

            var items = this._mediaRepository.GetNewMediaItems().OrderBy(o => o.MedienArt);

            #region Fallback Cover Image

            foreach (var item in items)
            {
                if (string.IsNullOrEmpty(item.ImageUrl) && !string.IsNullOrEmpty(item.ISBN))
                {
                    var cleanIsbn = item.ISBN.Replace("-", string.Empty);
                    item.ImageUrl = $"http://cover.ekz.de/{cleanIsbn}.jpg";
                }
            }

            #endregion

            return View(items.ToList());
        }

        [OutputCache(Duration = 3600)]
        public ActionResult Trends()
        {
            ViewBag.Mapping = this.GetMedienArtMapping().Where(o => o.Key != "3" && o.Key != "4").ToDictionary(o => o.Key, o => o.Value);

            var items = this._mediaRepository.GetPopularMediaItems();

            #region Fallback Cover Image

            foreach (var item in items)
            {
                if (string.IsNullOrEmpty(item.ImageUrl) && !string.IsNullOrEmpty(item.ISBN))
                {
                    var cleanIsbn = item.ISBN.Replace("-", string.Empty);
                    item.ImageUrl = $"http://cover.ekz.de/{cleanIsbn}.jpg";
                    //item.ImageUrl = $"http://covers.openlibrary.org/b/isbn/{item.ISBN}-L.jpg";
                }
            }

            #endregion

            return View(items);
        }

        public ActionResult Zeitschriften()
        {
            var items = new List<Magazine>();
            items.Add(new Magazine("Anna", "https://www.oz-verlag.de/anna/", "Monatlich") { Description = "Handarbeitsideen zum Selbermachen" });
            items.Add(new Magazine("Architektur und Wohnen", "http://www.awmagazin.de/", "alle zwei Monate") { Description = "Trends im Bereich Wohnen, Architektur, Interior" });
            items.Add(new Magazine("Bergsteiger", "http://bergsteiger.de/", "Monatlich") { Description = "Tourenbeschreibungen, Ausrüstungsteste" });
            items.Add(new Magazine("Bike", "http://www.bike-magazin.de/", "Monatlich") { Description = "Mountainbike: Tourenvorschläge, Materialteste" });
            items.Add(new Magazine("Bike & Travel", "https://www.biketravel-magazin.com/", "alle zwei Monate") { Description = "Mountainbike: Tourenvorschläge" });
            items.Add(new Magazine("Bild der Wissenschaft", "http://www.wissenschaft.de/", "Monatlich") { Description = "Wissenschaft und Forschung, anspruchsvolle Berichte" });
            items.Add(new Magazine("Bio", "https://www.biomagazin.de/", "alle zwei Monate") { Description = "Magazin für Gesundheit, Körper, Geist und Seele" });
            items.Add(new Magazine("Blooms", "https://www.blooms.de/", "alle zwei Monate") { Description = "Kreative Dekoideen mit Blumen und anderen Materialien" });
            items.Add(new Magazine("Brandeins", "https://www.brandeins.de/", "alle zwei Monate") { Description = "Wirtschaftsmagazin" });
            items.Add(new Magazine("Bravo", "http://www.bravo.de/", "jede zweite Woche") { Description = "Für Jugendliche von 10 bis 19 Jahren" });
            //items.Add(new Magazine("Bravo Sport", "", "jede zweite Woche") { Description = "Jugendmagazin für Sport" });
            items.Add(new Magazine("Brigitte", "https://www.brigitte.de/", "jede zweite Woche") { Description = "Mode, Beauty, Kultur, Partnerschaft" });
            items.Add(new Magazine("Brigitte woman", "https://www.brigitte.de/woman/", "Monatlich") { Description = "Mode, Kosmetik, Psychologie für die Frau ab 40" });
            items.Add(new Magazine("Burda", "https://www.burdastyle.de/shop/ausgaben?listImage=mood&page=1", "Monatlich") { Description = "Modezeitschrift mit Schnittmustern" });
            items.Add(new Magazine("Chip", "https://www.chip.de/", "monatlich") { Description = "Testmagazin, Technikratgeber für die digitale Welt" });
            items.Add(new Magazine("ct Magazin", "https://www.heise.de/ct/", "alle zwei Wochen") { Description = "EDV für Profis" });
            items.Add(new Magazine("Donald Duck", "https://www.egmont-shop.de/comics/disney/donald-duck-sonderheft/", "monatlich") { Description = "Comic" });
            items.Add(new Magazine("Einfach hausgemacht", "https://www.essen-und-trinken.de/einfach-hausgemacht", "jeden zweiten Monat") { Description = "Tipps für Lebensmittel, Küchengeräte, Haus und Kleidung" });
            items.Add(new Magazine("GEO", "https://www.geo.de/magazine/geo-magazin", "Monatlich") { Description = "Reiseberichte rund um den Globus" });
            items.Add(new Magazine("GEOlino", "https://www.geo.de/geolino", "Monatlich") { Description = "Kinderzeitschrift von 8 bis 14 Jahren. Wissen, Spiele" });
            items.Add(new Magazine("GEOmini", "https://www.geo.de/magazine/geolino-mini", "Monatlich") { Description = "Kinderzeitschrift von 5 bis 8 Jahren. Tiere, Basteln" });
            items.Add(new Magazine("GEO Saison", "https://www.geo.de/magazine/17032-thma-geo-saison", "Monatlich") { Description = "Reisemagazin" });
            items.Add(new Magazine("GEO spezial", "https://www.geo.de/magazine/geo-special", "alle zwei Monate") { Description = "Reisedestinationen weltweit" });
            items.Add(new Magazine("GEO wissen", "https://www.geo.de/magazine/geo-wissen", "Halbjährlich") { Description = "Ausführliche Sonderhefte zu einem Thema" });
            items.Add(new Magazine("Gesund leben", "https://www.gesundundleben.at/de/", "alle zwei Monate") { Description = "Das Beste für Körper, Geist und Seele" });
            items.Add(new Magazine("Gusto", "https://www.gusto.at/", "Monatlich") { Description = "Kochmagazin: Rezepte, Gourmet-News" });
            items.Add(new Magazine("Happinez", "https://www.happinez.de/", "jeden zweiten Monat") { Description = "Entdecken ihrer Innerlichkeit" });
            items.Add(new Magazine("Häuser", "https://www.schoener-wohnen.de/architektur/haeuser-awards/30042-rtkl-die-haeuser-app", "alle zwei Monate") { Description = "Internationale Architektur, Design und Wohnen" });
            items.Add(new Magazine("I love English", "https://www.iloveenglish.com/nos-magazines/i-love-english", "Monatlich") { Description = "Englischsprachig für Schüler mit Vokabeln und CD" });
            items.Add(new Magazine("Kochen und genießen", "https://www.lecker.de/kochen-geniessen-liebe-die-auf-den-tisch-kommt-59874.html", "Monatlich") { Description = "Leckere Rezepte für jeden Geschmack" });
            items.Add(new Magazine("Konsument", "https://www.konsument.at/geschenkabo", "Monatlich") { Description = "Österreichisches Testmagazin, Konsumentenschutz" });
            items.Add(new Magazine("Köstlich vegetarisch", "https://www.koestlich-vegetarisch.de/", "alle zwei Monate") { Description = "Vegetarische Kochmagazin" });
            items.Add(new Magazine("Kraut & Rüben", "https://www.krautundrueben.de/", "Monatlich") { Description = "Biologisches Gärtnern und naturgemäßes Leben" });
            items.Add(new Magazine("Landapotheke", "http://www.landidee.info/landapotheke", "einmal im Quartal") { Description = "Naturheilkunde, Kräuter, Alternativmedizin" });
            items.Add(new Magazine("Landlust", "http://www.landlust.de/", "alle zwei Monate") { Description = "Garten, Küche, ländliches Wohnen" });
            items.Add(new Magazine("Living at Home", "https://www.livingathome.de/", "Monatlich") { Description = "Inneneinrichtung, Dekoration, Küche und Garten" });
            items.Add(new Magazine("Ma vie", "https://mavie-mag.de/", "alle zwei Monate") { Description = "... sich einfach eine Pause gönnen" });
            items.Add(new Magazine("Mein schöner Garten", "https://www.mein-schoener-garten.de/", "Monatlich") { Description = "Ratgeber zur Gartengestaltung und Gartenarbeit" });
            items.Add(new Magazine("Mein schönes Land", "https://www.mein-schoenes-land.de/", "alle zwei Monate") { Description = "Inspirationen für das Landleben" });
            items.Add(new Magazine("Micky Maus Comics", "https://www.micky-maus.de/magazin/", "alle zwei Monate") { Description = "Comics aus der Disney-Produktion" });
            items.Add(new Magazine("My Bike", "https://www.mybike-magazin.de/", "Monatlich") { Description = "Alles rund ums Bike" });
            //items.Add(new Magazine("Mollie makes", "", "alle zwei Monate") { Description = "Selbermachen mit Nadel, Wolle, Stoff, ..." });
            //items.Add(new Magazine("Motor-Freizeit-Trends", "", "alle zwei Monate") { Description = "Herausgeber aus Klaus; Beschreibung div. Automodelle" });
            items.Add(new Magazine("National Geographic", "https://www.nationalgeographic.com/", "Monatlich") { Description = "Reportagen über fremde Länder, Kulturen, Natur" });
            items.Add(new Magazine("Natur und Heilen", "https://www.naturundheilen.de/", "Monatlich") { Description = "Naturgemäßes ganzheitliches Heilen und Leben" });
            items.Add(new Magazine("ÖKO -Test", "https://www.oekotest.de/", "Monatlich") { Description = "Teste zu bedenklichen Stoffen in Lebensmitteln und diversen Gebrauchsgegenständen" });
            items.Add(new Magazine("Outdoor", "https://www.outdoor-magazin.com/", "Monatlich") { Description = "Reiseziele, Ausrüstung, Tipps, Abenteuer" });
            //items.Add(new Magazine("Popcorn", "", "Monatlich") { Description = "Mädchen von 12 bis 17, Stars, Beauty" });
            items.Add(new Magazine("Photographie", "https://photographie.de/", "Monatlich") { Description = "Neueste Fotoprodukte, Trends und Teste" });
            items.Add(new Magazine("PM Magazin", "https://www.pm-wissen.com/", "Monatlich") { Description = "Wissenschaft und Forschung, leicht verständliche Berichte" });
            items.Add(new Magazine("Psychologie heute", "https://www.psychologie-heute.de/", "Monatlich") { Description = "Psychologie und Lebenshilfe" });
            items.Add(new Magazine("Schöner Wohnen", "https://www.schoener-wohnen.de/", "Monatlich") { Description = "Einrichten, Disign, Architektur, Lebensart" });
            items.Add(new Magazine("Servus", "https://www.servus.com/", "Monatlich") { Description = "Tradition, Natur, Essen, Brauchtum" });
            items.Add(new Magazine("Spotlight – einfach Englisch", "https://www.spotlight-online.de/", "Monatlich") { Description = "Englisches Sprachmagazin mit Vokabelhilfen" });
            items.Add(new Magazine("Terra Mater", "https://www.terramater.at/", "alle zwei Monate") { Description = "Naturwissenschaften, Tiere, Pflanzen, Fotografie" });
            items.Add(new Magazine("My Bike", "https://www.mybike-magazin.de/", "alle zwei Monate") { Description = "Tourenbeschreibungen, Materialteste" });
            items.Add(new Magazine("Trend", "https://www.trend.at/", "Wöchentlich") { Description = "Aktuelle Nachrichten, Wirtschaft, Hintergründe" });
            items.Add(new Magazine("Vital", "https://www.vital.de/", "Monatlich") { Description = "Gesunde Ernährung, Wohlbefinden" });
            //items.Add(new Magazine("Welt der Frau", "", "Monatlich") { Description = "Mode, Beauty, Kochen, Reise, Gesundheit" });
            items.Add(new Magazine("Welt der Wunder", "https://www.weltderwunder.de/", "Monatlich") { Description = "Entdecken, Staunen, Wissen" });
            items.Add(new Magazine("Wohnen & Dekorieren", "https://www.schoener-wohnen.de/einrichten/dekorieren", "Monatlich") { Description = "Einrichtungs- und DIY-Ideen" });
            items.Add(new Magazine("Wohnen und Garten", "http://www.wohnen-und-garten.de/start.html", "Monatlich") { Description = "Tipps für Wohnen Drinnen und Draußen, Rezepte" });
            items.Add(new Magazine("Wohnidee", "https://wohnidee.wunderweib.de/", "Monatlich") { Description = "Einrichten, Dekorieren, Wohnen" });
            return View(items);
        }

        public ActionResult Suche()
        {
            return View(new SearchRequest());
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

        [OutputCache(Duration = 3600, VaryByParam = "id")]
        public ActionResult Detail(int id)
        {
            var item = this._mediaRepository.GetMediaItem(id);

            #region Bild hinzufügen und ähnliche Bücher suchen

            if (!string.IsNullOrEmpty(item.ISBN))
            {
                var result = this._enhanceMedia.GetDetails(item.ISBN);
                item.ImageUrl = result.Item1;

                var similarBooks = result.Item2?.Where(o => !string.IsNullOrEmpty(o.Isbn) && !string.IsNullOrEmpty(o.Verfasser)).ToList();
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
                var cleanIsbn = item.ISBN?.Replace("-", string.Empty);
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