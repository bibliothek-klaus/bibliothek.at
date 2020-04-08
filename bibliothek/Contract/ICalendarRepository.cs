using bibliothek.Models;
using System.Collections.Generic;

namespace bibliothek.Contracts
{
    public interface ICalendarRepository
    {
        List<CalendarEvent> Get();
    }
}