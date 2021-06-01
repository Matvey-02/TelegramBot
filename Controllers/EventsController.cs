using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lectionapi.Clients;
using lectionapi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace lectionapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventsController : ControllerBase
    {

        private readonly ILogger<EventsController> _logger;
        private readonly CalendarClient _calendarClient;

        public EventsController(ILogger<EventsController> logger, CalendarClient weatherClient)
        {
            _logger = logger;
            _calendarClient = weatherClient;
        }

        [HttpGet("get_events")]
        public async Task<EventsResponse> GetEventsByTimezone([FromQuery] EventTimeZone timeZone)
        {
            CalendarEvents Events = await _calendarClient.GetEvents(timeZone.StartTime, timeZone.EndTime);

            var response = new EventsResponse();
            response.Items = new List<ResponseEvent>();
            response.Summary = Events.Summary;

            foreach (var item in Events.Items)
            {
                response.Items.Add(new ResponseEvent(item.Id, item.Summary, item.HtmlLink));
            }
            return response;
        }

        [HttpPost("quick_add_event")]
        public async Task<QuickAddResponse> AddEvent([FromQuery] AddEventParameters parameters)
        {
            QuickAdd AddEvent = await _calendarClient.QuickAddEvent(parameters.Name);
            var response = new QuickAddResponse();
            response.Id = AddEvent.Id;
            response.Summary = AddEvent.Summary;
            response.HtmlLink = AddEvent.HtmlLink;
            return response;
        }

        [HttpDelete("delete_event")]
        public async Task DelEvent([FromQuery] DeleteEventParameters parameters)
        {
            await _calendarClient.DeleteEvent(parameters.eventId);
        }

        [HttpPost("insert_event")]
        public async Task<InsertEventResponse> InsertEvent([FromQuery] InsertEventParameters parameters)
        {

            InsertEvent AddEvent = await _calendarClient.AddEvent(parameters);
            var response = new InsertEventResponse();
            response.Id = AddEvent.Id;
            response.Status = AddEvent.Status;
            response.HtmlLink = AddEvent.HtmlLink;
            return response;
        }

        [HttpPut("update_event")]
        public async Task<UpdateEventResponse> UpdateEvent([FromQuery] UpdateEventParameters parameters)
        {
            UpdateEvent updateEvent = await _calendarClient.UpdateEvent(parameters);
            var response = new UpdateEventResponse();
            response.HtmlLink = updateEvent.HtmlLink;
            response.Status = updateEvent.Status;
            return response;
        }

    }
}
