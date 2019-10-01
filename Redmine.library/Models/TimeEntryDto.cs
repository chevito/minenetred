using System;
using System.Collections.Generic;
using System.Text;

namespace Redmine.library.Models
{
    public class TimeEntryDto
    {
        public int issue_id { get; set; }
        public string spent_on { get; set; }
        public float hours { get; set; }
        public int activity_id { get; set; }
        public string comments { get; set; }
    }
}
