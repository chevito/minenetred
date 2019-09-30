using System;
using System.Collections.Generic;
using System.Text;

namespace Redmine.library.Models
{
    public class TimeEntryDto
    {
        public int Issue_id { get; set; }
        public string Spent_on { get; set; }
        public int Hours { get; set; }
        public int Activity_id { get; set; }
        public string Comments { get; set; }
    }
}
