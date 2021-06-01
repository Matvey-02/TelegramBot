using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lectionapi.Model
{
    public class InsertEventParameters
    {
        public string EndTime { get; set; }
        public string StartTime { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
    }
}
