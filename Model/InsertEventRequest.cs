using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lectionapi.Model
{
    public class InsertEventRequest
    {
        public string Summary { get; set; }
        public string Description { get; set; }

        public InsertEventRequest(string startTime, string endTime)
        {
            Start Start = new Start(startTime);
            End End = new End(endTime);
        }
    }
    public class Start
    {
        public string DateTime { get; set; }
        public Start(string StartTime) { DateTime = StartTime; }
    };
    public class End
    {
        public string DateTime { get; set; }
        public End(string EndTime) { DateTime = EndTime; }
    }
}
