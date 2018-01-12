using bibliothek.at.Models;
using System;
using System.Collections.Generic;

namespace bibliothek.at.Contracts
{
    public class MockCalendarRepository : ICalendarRepository
    {
        public List<CalendarEvent> Get()
        {
            var items = new List<CalendarEvent>();
            items.Add(new CalendarEvent { Date = new DateTime(2018, 1, 1, 8, 0, 0), Title = "Test Jannuar", Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor.", Location = "Klaus" });
            items.Add(new CalendarEvent { Date = new DateTime(2018, 2, 10, 17, 0, 0), Title = "Test Februar", Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor.", Location = "Klaus" });

            return items;
        }
    }
}