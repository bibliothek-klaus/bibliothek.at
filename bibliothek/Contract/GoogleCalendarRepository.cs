using bibliothek.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace bibliothek.Contracts
{
    public class GoogleCalendarRepository : ICalendarRepository
    {
        public List<CalendarEvent> Get()
        {
            GoogleCredential credential = null;
            //using (var stream = new FileStream(new HostingEnvironment()..MapPath(@"~/App_Data/googlecalendarcredential.json"), FileMode.Open, FileAccess.Read))
            //{
            //    credential = GoogleCredential.FromStream(stream).CreateScoped(CalendarService.Scope.Calendar);
            //}

            // Create Google Calendar API service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "bibliothek.at Website",
            });

            var allowedCalendarId = ConfigurationManager.AppSettings["AllowedCalendarId"];
            var calendarEvents = new List<CalendarEvent>();

            var calendars = service.CalendarList.List().Execute().Items;
            foreach (CalendarListEntry calendar in calendars)
            {
                if (calendar.Id != allowedCalendarId)
                {
                    continue;
                }

                var events = service.Events.List(calendar.Id).Execute();
                var items = events.Items.Where(o => o.Start.DateTime >= DateTime.Now || o.End.DateTime <= DateTime.Now).Select(o =>
                    new CalendarEvent
                    {
                        Date = o.Start.DateTime.Value,
                        Title = o.Summary,
                        Description = o.Description?.Replace("\n", @"<br \>"),
                        Location = o.Location
                    }).ToList();

                calendarEvents.AddRange(items);
            }

            return calendarEvents.OrderBy(o => o.Date).ToList();
        }
    }
}