using bibliothek.Contracts;
using bibliothek.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nager.Date;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace bibliothek.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        readonly ICalendarRepository _calendarRepository;

        public HomeController(ILogger<HomeController> logger, ICalendarRepository calendarRepository)
        {
            this._logger = logger;
            this._calendarRepository = calendarRepository;
        }

        public IActionResult Index()
        {
            var items = DateSystem.GetPublicHoliday(DateTime.Today, DateTime.Today.AddMonths(6), CountryCode.AT).ToList();

            //Add custom holidays
            items.Add(new Nager.Date.Model.PublicHoliday(new DateTime(2018, 4, 1), "Ostersonntag", "Ostersonntag", CountryCode.AT));
            items.Add(new Nager.Date.Model.PublicHoliday(new DateTime(2018, 5, 20), "Pfingstsonntag", "Pfingstsonntag", CountryCode.AT));
            items.Add(new Nager.Date.Model.PublicHoliday(new DateTime(2018, 12, 24), "HL Abend", "HL Abend", CountryCode.AT));
            items.Add(new Nager.Date.Model.PublicHoliday(new DateTime(2018, 12, 31), "Silvester", "Silvester", CountryCode.AT));
            items.Add(new Nager.Date.Model.PublicHoliday(new DateTime(2019, 4, 21), "Ostersonntag", "Ostersonntag", CountryCode.AT));
            items.Add(new Nager.Date.Model.PublicHoliday(new DateTime(2019, 6, 9), "Pfingstsonntag", "Pfingstsonntag", CountryCode.AT));

            ViewBag.PublicHolidays = items.OrderBy(o => o.Date).Where(o => o.Date >= DateTime.Now).Take(5);

            return View();
        }

        public IActionResult Benutzererklaerung()
        {
            return View();
        }

        public IActionResult Links()
        {
            return View();
        }

        public IActionResult Team()
        {
            var items = new List<Team>
            {
                new Team("Robert Summer", "Bücherei Leiter", "Summer.jpg"),
                new Team("Roswitha Ehrne", "Stv. Leiterin", "Ehrne.jpg"),
                new Team("Melanie Bernecker", "Ehrenamtlicher Mitarbeiter", "Bernecker.jpg"),
                //items.Add(new Team("Johanna Bernhart", "Ehrenamtlicher Mitarbeiter", "Bernhart.jpg"));
                //items.Add(new Team("Eva Cukrowicz", "Ehrenamtlicher Mitarbeiter", "CukrowiczE.jpg"));
                new Team("Heidi Cukrowicz", "Ehrenamtlicher Mitarbeiter", "CukrowiczH.jpg"),
                new Team("Maria Christa Faisst", "Ehrenamtlicher Mitarbeiter", "Faisst.jpg"),
                new Team("Annemarie Frick", "Ehrenamtlicher Mitarbeiter", "Frick.jpg"),
                new Team("Gabriele Gell", "Ehrenamtlicher Mitarbeiter", "Gell.jpg"),
                new Team("Evelyn Guger", "Ehrenamtlicher Mitarbeiter", "Guger.jpg"),
                //items.Add(new Team("Oli Halbeisen", "Ehrenamtlicher Mitarbeiter", "Halbeisen.jpg"));
                new Team("Maria Heinzle", "Ehrenamtlicher Mitarbeiter", "Heinzle.jpg"),
                new Team("Rosi Gächter", "Ehrenamtlicher Mitarbeiter", "Hoedl.jpg"),
                new Team("Andrea Kopf", "Ehrenamtlicher Mitarbeiter", "Kopf.jpg"),
                new Team("Karin Längle", "Ehrenamtlicher Mitarbeiter", "Laengle.jpg"),
                new Team("Matthias Lampert", "Ehrenamtlicher Mitarbeiter", "Lampert.jpg"),
                new Team("Quido Morscher", "Ehrenamtlicher Mitarbeiter", "MorscherQ.jpg"),
                new Team("Daniel Morscher", "Ehrenamtlicher Mitarbeiter", "Morscher.jpg"),
                new Team("Verena Müller", "Ehrenamtlicher Mitarbeiter", "MuellerV.jpg"),
                //items.Add(new Team("Antonia Primisser", "Ehrenamtlicher Mitarbeiter", "Primisser.jpg"));
                new Team("Ulrike Geiger", "Ehrenamtlicher Mitarbeiter", "GeigerUlrike.jpg"),
                new Team("Brigitte Summer", "Ehrenamtlicher Mitarbeiter", "SummerB.jpg"),
                new Team("Gabriele Thöni", "Ehrenamtlicher Mitarbeiter", "Thoeni.jpg"),
                //items.Add(new Team("Irma Thurnher", "Ehrenamtlicher Mitarbeiter", "ThurnherI.jpg"));
                //items.Add(new Team("Erich Thurnher", "Ehrenamtlicher Mitarbeiter", "Thurnher.jpg"));
                //items.Add(new Team("Anja Mittelberger", "Ehrenamtlicher Mitarbeiter", "MittelbergerAnja.jpg"));
                //items.Add(new Team("Patrik Primisser", "Ehrenamtlicher Mitarbeiter", "Primisser1.jpg"));
                new Team("Barbara Fleisch", "Ehrenamtlicher Mitarbeiter", "Fleisch.jpg"),
                new Team("Helga Halbeisen-Maure", "Ehrenamtlicher Mitarbeiter", "HalbeisenMaurer.jpg"),
                new Team("Doris Jenny", "Ehrenamtlicher Mitarbeiter", "Jenny.jpg"),
                new Team("Martina Morscher", "Ehrenamtlicher Mitarbeiter", "MorscherM.jpg"),
                new Team("Constantin Piber", "Ehrenamtlicher Mitarbeiter", "Piber.jpg"),
                new Team("Monika Wachter", "Ehrenamtlicher Mitarbeiter", "Wachter.jpg")
            };

            return View(items);
        }

        public IActionResult Veranstaltungen()
        {
            var items = this._calendarRepository.Get();
            return View(items);
        }

        public IActionResult Subventionsgeber()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
