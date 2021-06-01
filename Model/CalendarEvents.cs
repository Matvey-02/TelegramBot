using System;
using System.Collections.Generic;

namespace lectionapi.Model
{
    public class CalendarEvents
    {
        public string Summary { get; set; }
        public List<Items> Items { get; set; }
    }

    public class Items
    {
        public string Id { get; set; }
        public string Summary { get; set; }
        public string HtmlLink { get; set; }

        public Items(string id, string summary, string link)
        {
            this.Id = id;
            this.Summary = summary;
            this.HtmlLink = link;
        }
    }
}
