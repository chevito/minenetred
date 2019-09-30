using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minenetred.web.Models
{
    public class TimeEntryFormDto
    {
        public int IssueId { get; set; }
        public string SpentOn { get; set; }
        public int Hours { get; set; }
        public int ActivityId { get; set; }
        public string Comments { get; set; }

    }
}
