using System;
using System.Collections.Generic;
using System.Text;

namespace Redmine.library.Models
{
    public abstract class ResultContent 
    {

        public List<Project> Projects { get; set; }
        public int TotalCount { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
    }
}
