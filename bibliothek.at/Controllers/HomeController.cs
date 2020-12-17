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
        IEmailMarketing _emailMarketing;

        public HomeController(ICalendarRepository calendarRepository, IEmailMarketing emailMarketing)
        {
            this._calendarRepository = calendarRepository;
            this._emailMarketing = emailMarketing;
        }

        public ActionResult Index()
        {
            var items = DateSystem.GetPublicHoliday(CountryCode.AT, DateTime.Today, DateTime.Today.AddMonths(8)).ToList();

            //Add custom holidays
            items.Add(new Nager.Date.Model.PublicHoliday(new DateTime(2020, 12, 24), "HL Abend", "HL Abend", CountryCode.AT));
            items.Add(new Nager.Date.Model.PublicHoliday(new DateTime(2020, 12, 31), "Silvester", "Silvester", CountryCode.AT));
            items.Add(new Nager.Date.Model.PublicHoliday(new DateTime(2021, 4, 4), "Ostersonntag", "Ostersonntag", CountryCode.AT));
            items.Add(new Nager.Date.Model.PublicHoliday(new DateTime(2021, 5, 23), "Pfingstsonntag", "Pfingstsonntag", CountryCode.AT));
            items.Add(new Nager.Date.Model.PublicHoliday(new DateTime(2021, 12, 24), "HL Abend", "HL Abend", CountryCode.AT));
            items.Add(new Nager.Date.Model.PublicHoliday(new DateTime(2021, 12, 31), "Silvester", "Silvester", CountryCode.AT));
            items.Add(new Nager.Date.Model.PublicHoliday(new DateTime(2022, 4, 17), "Ostersonntag", "Ostersonntag", CountryCode.AT));
            items.Add(new Nager.Date.Model.PublicHoliday(new DateTime(2022, 6, 5), "Pfingstsonntag", "Pfingstsonntag", CountryCode.AT));

            ViewBag.PublicHolidays = items.OrderBy(o => o.Date).Where(o => o.Date >= DateTime.Now).Take(5);

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
            //items.Add(new Team("Johanna Bernhart", "Ehrenamtlicher Mitarbeiter", "Bernhart.jpg"));
            //items.Add(new Team("Eva Cukrowicz", "Ehrenamtlicher Mitarbeiter", "CukrowiczE.jpg"));
            items.Add(new Team("Heidi Cukrowicz", "Ehrenamtlicher Mitarbeiter", "CukrowiczH.jpg"));
            items.Add(new Team("Maria Christa Faisst", "Ehrenamtlicher Mitarbeiter", "Faisst.jpg"));
            //items.Add(new Team("Annemarie Frick", "Ehrenamtlicher Mitarbeiter", "Frick.jpg"));
            items.Add(new Team("Gabriele Gell", "Ehrenamtlicher Mitarbeiter", "Gell.jpg"));
            items.Add(new Team("Evelyn Guger", "Ehrenamtlicher Mitarbeiter", "Guger.jpg"));
            //items.Add(new Team("Oli Halbeisen", "Ehrenamtlicher Mitarbeiter", "Halbeisen.jpg"));
            items.Add(new Team("Maria Heinzle", "Ehrenamtlicher Mitarbeiter", "Heinzle.jpg"));
            items.Add(new Team("Rosi Gächter", "Ehrenamtlicher Mitarbeiter", "Hoedl.jpg"));
            items.Add(new Team("Andrea Kopf", "Ehrenamtlicher Mitarbeiter", "Kopf.jpg"));
            items.Add(new Team("Karin Längle", "Ehrenamtlicher Mitarbeiter", "Laengle.jpg"));
            items.Add(new Team("Matthias Lampert", "Ehrenamtlicher Mitarbeiter", "Lampert.jpg"));
            items.Add(new Team("Quido Morscher", "Ehrenamtlicher Mitarbeiter", "MorscherQ.jpg"));
            items.Add(new Team("Daniel Morscher", "Ehrenamtlicher Mitarbeiter", "Morscher.jpg"));
            items.Add(new Team("Verena Müller", "Ehrenamtlicher Mitarbeiter", "MuellerV.jpg"));
            //items.Add(new Team("Antonia Primisser", "Ehrenamtlicher Mitarbeiter", "Primisser.jpg"));
            items.Add(new Team("Ulrike Geiger", "Ehrenamtlicher Mitarbeiter", "GeigerUlrike.jpg"));
            items.Add(new Team("Brigitte Summer", "Ehrenamtlicher Mitarbeiter", "SummerB.jpg"));
            items.Add(new Team("Gabriele Thöni", "Ehrenamtlicher Mitarbeiter", "Thoeni.jpg"));
            //items.Add(new Team("Irma Thurnher", "Ehrenamtlicher Mitarbeiter", "ThurnherI.jpg"));
            //items.Add(new Team("Erich Thurnher", "Ehrenamtlicher Mitarbeiter", "Thurnher.jpg"));
            //items.Add(new Team("Anja Mittelberger", "Ehrenamtlicher Mitarbeiter", "MittelbergerAnja.jpg"));
            //items.Add(new Team("Patrik Primisser", "Ehrenamtlicher Mitarbeiter", "Primisser1.jpg"));
            items.Add(new Team("Barbara Fleisch", "Ehrenamtlicher Mitarbeiter", "Fleisch.jpg"));
            //items.Add(new Team("Helga Halbeisen-Maure", "Ehrenamtlicher Mitarbeiter", "HalbeisenMaurer.jpg"));
            items.Add(new Team("Doris Jenny", "Ehrenamtlicher Mitarbeiter", "Jenny.jpg"));
            items.Add(new Team("Martina Morscher", "Ehrenamtlicher Mitarbeiter", "MorscherM.jpg"));
            items.Add(new Team("Constantin Piber", "Ehrenamtlicher Mitarbeiter", "Piber.jpg"));
            items.Add(new Team("Monika Wachter", "Ehrenamtlicher Mitarbeiter", "Wachter.jpg"));
            
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

        public ActionResult Newsletter()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Newsletter(string emailAddress)
        {
            this._emailMarketing.RegisterRecipient(emailAddress);
            return RedirectToAction("NewsletterConfirm");
        }

        public ActionResult NewsletterConfirm()
        {
            return View();
        }
    }
}