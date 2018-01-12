using bibliothek.at.Contracts;
using bibliothek.at.Models;
using Nager.Date;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace bibliothek.at.Controllers
{
    public class HomeController : Controller
    {
        ICalendarRepository _calendarRepository;

        public HomeController(ICalendarRepository calendarRepository)
        {
            this._calendarRepository = calendarRepository;
        }

        public ActionResult Index()
        {
            var items = DateSystem.GetPublicHoliday(CountryCode.AT, DateTime.Today, DateTime.Today.AddMonths(6)).ToList();

            //Add custom holidays
            items.Add(new Nager.Date.Model.PublicHoliday(new DateTime(2018, 4, 1), "Ostersonntag", "Ostersonntag", CountryCode.AT));
            items.Add(new Nager.Date.Model.PublicHoliday(new DateTime(2018, 5, 20), "Pfingstsonntag", "Pfingstsonntag", CountryCode.AT));
            items.Add(new Nager.Date.Model.PublicHoliday(new DateTime(2019, 4, 21), "Ostersonntag", "Ostersonntag", CountryCode.AT));
            items.Add(new Nager.Date.Model.PublicHoliday(new DateTime(2019, 6, 9), "Pfingstsonntag", "Pfingstsonntag", CountryCode.AT));

            ViewBag.PublicHolidays = items.Where(o => o.Date >= DateTime.Now).Take(5);

            return View();
        }
               
        public ActionResult Benutzererklaerung()
        {
            return View();
        }

        public ActionResult Links()
        {
            return View();
        }

        public ActionResult Team()
        {
            var items = new List<Team>();
            items.Add(new Team("Robert Summer", "Bücherei Leiter", "Summer.jpg"));
            items.Add(new Team("Roswitha Ehrne", "Stv. Leiterin", "Ehrne.jpg"));
            items.Add(new Team("Melanie Bernecker", "Ehrenamtlicher Mitarbeiter", "Bernecker.jpg"));
            items.Add(new Team("Johanna Bernhart", "Ehrenamtlicher Mitarbeiter", "Bernhart.jpg"));
            items.Add(new Team("Eva Cukrowicz", "Ehrenamtlicher Mitarbeiter", "CukrowiczE.jpg"));
            items.Add(new Team("Cukrowicz Heidi", "Ehrenamtlicher Mitarbeiter", "CukrowiczH.jpg"));
            items.Add(new Team("Maria Christa Faisst", "Ehrenamtlicher Mitarbeiter", "Faisst.jpg"));
            items.Add(new Team("Annemarie Frick", "Ehrenamtlicher Mitarbeiter", "Frick.jpg"));
            items.Add(new Team("Gabriele Gell", "Ehrenamtlicher Mitarbeiter", "Gell.jpg"));
            items.Add(new Team("Evelyne Gugger", "Ehrenamtlicher Mitarbeiter", "Guger.jpg"));
            items.Add(new Team("Oli Halbeisen", "Ehrenamtlicher Mitarbeiter", "Halbeisen.jpg"));
            items.Add(new Team("Maria Heinzle", "Ehrenamtlicher Mitarbeiter", "Heinzle.jpg"));
            items.Add(new Team("Rosi Hödl", "Ehrenamtlicher Mitarbeiter", "Hoedl.jpg"));
            items.Add(new Team("Andrea Kopf", "Ehrenamtlicher Mitarbeiter", "Kopf.jpg"));
            items.Add(new Team("Karin Längle", "Ehrenamtlicher Mitarbeiter", "Laengle.jpg"));
            items.Add(new Team("Matthias Lampert", "Ehrenamtlicher Mitarbeiter", "Lampert.jpg"));
            items.Add(new Team("Quido Morscher", "Ehrenamtlicher Mitarbeiter", "MorscherQ.jpg"));
            items.Add(new Team("Daniel Morscher", "Ehrenamtlicher Mitarbeiter", "Morscher.jpg"));
            items.Add(new Team("Verena Müller", "Ehrenamtlicher Mitarbeiter", "MuellerV.jpg"));
            items.Add(new Team("Antonia Primisser", "Ehrenamtlicher Mitarbeiter", "Primisser.jpg"));
            items.Add(new Team("Ulrike Geiger", "Ehrenamtlicher Mitarbeiter", "GeigerUlrike.jpg"));
            items.Add(new Team("Brigitte Summer", "Ehrenamtlicher Mitarbeiter", "SummerB.jpg"));
            items.Add(new Team("Gabriele Thöni", "Ehrenamtlicher Mitarbeiter", "Thoeni.jpg"));
            items.Add(new Team("Irma Thurnher", "Ehrenamtlicher Mitarbeiter", "ThurnherI.jpg"));
            items.Add(new Team("Erich Thurnher", "Ehrenamtlicher Mitarbeiter", "Thurnher.jpg"));
            items.Add(new Team("Anja Mittelberger", "Ehrenamtlicher Mitarbeiter", "MittelbergerAnja.jpg"));
            items.Add(new Team("Patrik Primisser", "Ehrenamtlicher Mitarbeiter", "Primisser1.jpg"));

            return View(items);
        }

        public ActionResult Veranstaltungen()
        {
            var items = this._calendarRepository.Get();
            return View(items);
        }

        public ActionResult Subventionsgeber()
        {
            return View();
        }
    }
}
