using bibliothek.at.Models;
using System.Collections.Generic;

namespace bibliothek.at.Contracts
{
    public interface ICalendarRepository
    {
        List<CalendarEvent> Get();
    }
}