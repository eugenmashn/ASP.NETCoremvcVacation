using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Workers.Models_View
{
    public class CalendarEventy
    {
        public Guid Id { get; set; }
        public string title { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public bool allDay { get; set; }
        public string backgroundColor { get; set; }
    }
}
