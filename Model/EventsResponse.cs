using System;
using System.Collections.Generic;

namespace lectionapi.Model
{
    public class EventsResponse
    {
        public string Summary { get; set; }
        public List<ResponseEvent> Items { get; set; }

    }
    public class ResponseEvent
    {
        public string Id { get; set; }
        public string Summary { get; set; }
        public string HtmlLink { get; set; }

        public ResponseEvent(string id, string summary, string link)
        {
            this.Id = id;
            this.Summary = summary;
            this.HtmlLink = link;
        }
    }
}
